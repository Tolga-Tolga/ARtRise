using System;
using System.Collections.Generic;
using UnityEngine;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;

public class CardArtworkExtractor : MonoBehaviour
{
    [Header("Warp output size (card normalized)")]
    public int warpWidth = 800;
    public int warpHeight = 1200;

    [Header("Preprocessing")]
    public int gaussianBlur = 5;              // must be odd
    public double canny1 = 60;
    public double canny2 = 180;

    [Header("Contour filters")]
    public double minQuadAreaRatio = 0.08;    // relative to image area
    public double maxQuadAreaRatio = 0.98;
    public double approxEpsilonFactor = 0.02; // contour perimeter factor
    public double maxAspectError = 0.35;      // allow portrait-ish card; we don't force exact aspect
    public double minAngleCos = 0.3;          // smaller -> stricter 90deg, larger -> looser

    [Header("Marker sampling (in warped image coords)")]
    [Range(0.0f, 0.2f)] public float markerInsetX = 0.06f; // normalized inset from left/right
    [Range(0.0f, 0.2f)] public float markerInsetY = 0.06f; // normalized inset from top/bottom
    [Range(3, 25)] public int markerPatchSize = 9;         // odd size patch to average

    [Header("Artwork crop (in warped image, normalized rect)")]
    // Default: top artwork area (adjust to your final card layout)
    [Range(0, 1)] public float artX = 0.10f;
    [Range(0, 1)] public float artY = 0.10f;
    [Range(0, 1)] public float artW = 0.80f;
    [Range(0, 1)] public float artH = 0.45f;

    [Header("HSV color thresholds (default reasonable)")]
    // HSV ranges are in OpenCV: H 0..179, S 0..255, V 0..255
    public HSVRange green = new HSVRange(45, 80, 80, 255, 80, 255);
    public HSVRange blue  = new HSVRange(95, 130, 80, 255, 60, 255);
    public HSVRange yellow= new HSVRange(18, 35, 80, 255, 80, 255);

    // Red wraps around 0 in HSV, so we use two ranges.
    public HSVRange red1  = new HSVRange(0, 10, 80, 255, 80, 255);
    public HSVRange red2  = new HSVRange(170, 179, 80, 255, 80, 255);

    [Serializable]
    public struct HSVRange
    {
        public int hMin, hMax;
        public int sMin, sMax;
        public int vMin, vMax;

        public HSVRange(int hMin, int hMax, int sMin, int sMax, int vMin, int vMax)
        {
            this.hMin = hMin; this.hMax = hMax;
            this.sMin = sMin; this.sMax = sMax;
            this.vMin = vMin; this.vMax = vMax;
        }

        public bool Contains(Scalar hsv)
        {
            double h = hsv.val[0], s = hsv.val[1], v = hsv.val[2];
            return h >= hMin && h <= hMax && s >= sMin && s <= sMax && v >= vMin && v <= vMax;
        }
    }

    /// <summary>
    /// Main entry: finds the card, validates colored corners, returns warped card + extracted artwork.
    /// </summary>
    public bool TryExtractArtwork(Texture2D inputTex, out Texture2D artworkTex, out Texture2D warpedCardTex, out Point[] cardCornersInInput)
    {
        artworkTex = null;
        warpedCardTex = null;
        cardCornersInInput = null;

        if (inputTex == null) return false;

        // 1) Texture2D -> Mat (RGBA)
        Mat rgba = new Mat(inputTex.height, inputTex.width, CvType.CV_8UC4);
        Utils.texture2DToMat(inputTex, rgba);

        // 2) Preprocess: grayscale + blur + canny
        Mat gray = new Mat();
        Imgproc.cvtColor(rgba, gray, Imgproc.COLOR_RGBA2GRAY);

        if (gaussianBlur % 2 == 0) gaussianBlur += 1;
        Imgproc.GaussianBlur(gray, gray, new Size(gaussianBlur, gaussianBlur), 0);

        Mat edges = new Mat();
        Imgproc.Canny(gray, edges, canny1, canny2);

        // Optional: close gaps
        Mat kernel = Imgproc.getStructuringElement(Imgproc.MORPH_RECT, new Size(3, 3));
        Imgproc.morphologyEx(edges, edges, Imgproc.MORPH_CLOSE, kernel);

        // 3) Find contours
        List<MatOfPoint> contours = new List<MatOfPoint>();
        Mat hierarchy = new Mat();
        Imgproc.findContours(edges, contours, hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_SIMPLE);

        if (contours.Count == 0) return false;

        // 4) Find best quad candidate (largest valid quad)
        double imgArea = rgba.cols() * rgba.rows();
        MatOfPoint bestQuad = null;
        double bestArea = 0;

        foreach (var c in contours)
        {
            double area = Imgproc.contourArea(c);
            if (area < imgArea * minQuadAreaRatio || area > imgArea * maxQuadAreaRatio) continue;

            MatOfPoint2f c2f = new MatOfPoint2f(c.toArray());
            double peri = Imgproc.arcLength(c2f, true);
            MatOfPoint2f approx = new MatOfPoint2f();
            Imgproc.approxPolyDP(c2f, approx, approxEpsilonFactor * peri, true);

            Point[] pts = approx.toArray();
            if (pts.Length != 4) continue;

            MatOfPoint approxInt = new MatOfPoint();
            approxInt.fromArray(pts);

            if (!Imgproc.isContourConvex(approxInt)) continue;

            // Angle sanity check (roughly right angles)
            if (!LooksRectangular(pts, minAngleCos)) continue;

            // Aspect sanity check (portrait card-ish)
            if (!LooksCardAspect(pts, maxAspectError)) continue;

            if (area > bestArea)
            {
                bestArea = area;
                bestQuad = approxInt;
            }
        }

        if (bestQuad == null) return false;

        // 5) Order corners in input image: TL, TR, BR, BL
        Point[] quadPts = bestQuad.toArray();
        Point[] ordered = OrderCornersTLTRBRBL(quadPts);
        cardCornersInInput = ordered;

        // 6) Warp to canonical size (warped card image)
        Mat warped = WarpQuad(rgba, ordered, warpWidth, warpHeight);

        // 7) Validate color markers on the warped image
        if (!ValidateMarkers(warped))
            return false;

        // 8) Extract artwork ROI from warped image
        OpenCVForUnity.CoreModule.Rect artRectPx = NormalizedRectToPx(artX, artY, artW, artH, warpWidth, warpHeight);
        Mat artwork = new Mat(warped, artRectPx).clone();

        // 9) Mat -> Texture2D outputs
        warpedCardTex = MatToTexture(warped);
        artworkTex = MatToTexture(artwork);

        // cleanup
        rgba.release(); gray.release(); edges.release(); hierarchy.release(); kernel.release();
        warped.release(); artwork.release();

        return true;
    }

    // ---------- Helpers ----------

    private static Texture2D MatToTexture(Mat matRgbaOrRgb)
    {
        Mat rgba = matRgbaOrRgb;
        if (matRgbaOrRgb.channels() == 3)
        {
            rgba = new Mat();
            Imgproc.cvtColor(matRgbaOrRgb, rgba, Imgproc.COLOR_RGB2RGBA);
        }
        Texture2D tex = new Texture2D(rgba.cols(), rgba.rows(), TextureFormat.RGBA32, false);
        Utils.matToTexture2D(rgba, tex);
        if (rgba != matRgbaOrRgb) rgba.release();
        return tex;
    }

    private static OpenCVForUnity.CoreModule.Rect NormalizedRectToPx(float x, float y, float w, float h, int W, int H)
    {
        int px = Mathf.Clamp(Mathf.RoundToInt(x * W), 0, W - 1);
        int py = Mathf.Clamp(Mathf.RoundToInt(y * H), 0, H - 1);
        int pw = Mathf.Clamp(Mathf.RoundToInt(w * W), 1, W - px);
        int ph = Mathf.Clamp(Mathf.RoundToInt(h * H), 1, H - py);
        return new OpenCVForUnity.CoreModule.Rect(px, py, pw, ph);
    }

    private bool ValidateMarkers(Mat warpedRgba)
    {
        // Work in HSV for robust color checks
        Mat rgb = new Mat();
        Imgproc.cvtColor(warpedRgba, rgb, Imgproc.COLOR_RGBA2RGB);

        Mat hsv = new Mat();
        Imgproc.cvtColor(rgb, hsv, Imgproc.COLOR_RGB2HSV);

        // Marker sample points in warped image
        Point tl = new Point(markerInsetX * warpWidth, markerInsetY * warpHeight);
        Point tr = new Point((1.0 - markerInsetX) * warpWidth, markerInsetY * warpHeight);
        Point br = new Point((1.0 - markerInsetX) * warpWidth, (1.0 - markerInsetY) * warpHeight);
        Point bl = new Point(markerInsetX * warpWidth, (1.0 - markerInsetY) * warpHeight);

        Scalar tlHSV = MeanHSVPatch(hsv, tl, markerPatchSize);
        Scalar trHSV = MeanHSVPatch(hsv, tr, markerPatchSize);
        Scalar brHSV = MeanHSVPatch(hsv, br, markerPatchSize);
        Scalar blHSV = MeanHSVPatch(hsv, bl, markerPatchSize);

        bool okTL = green.Contains(tlHSV);
        bool okTR = (red1.Contains(trHSV) || red2.Contains(trHSV));
        bool okBR = yellow.Contains(brHSV);
        bool okBL = blue.Contains(blHSV);

        rgb.release();
        hsv.release();

        return okTL && okTR && okBR && okBL;
    }

    private static Scalar MeanHSVPatch(Mat hsv, Point center, int patchSize)
    {
        int half = patchSize / 2;
        int cx = (int)Math.Round(center.x);
        int cy = (int)Math.Round(center.y);

        int x0 = Mathf.Clamp(cx - half, 0, hsv.cols() - 1);
        int y0 = Mathf.Clamp(cy - half, 0, hsv.rows() - 1);
        int x1 = Mathf.Clamp(cx + half, 0, hsv.cols() - 1);
        int y1 = Mathf.Clamp(cy + half, 0, hsv.rows() - 1);

        int w = Math.Max(1, x1 - x0 + 1);
        int h = Math.Max(1, y1 - y0 + 1);

        Mat roi = new Mat(hsv, new OpenCVForUnity.CoreModule.Rect(x0, y0, w, h));
        Scalar mean = Core.mean(roi);
        roi.release();
        return mean;
    }

    private static Mat WarpQuad(Mat rgba, Point[] orderedTLTRBRBL, int outW, int outH)
    {
        // src points
        MatOfPoint2f src = new MatOfPoint2f(
            orderedTLTRBRBL[0],
            orderedTLTRBRBL[1],
            orderedTLTRBRBL[2],
            orderedTLTRBRBL[3]
        );

        // dst points
        MatOfPoint2f dst = new MatOfPoint2f(
            new Point(0, 0),
            new Point(outW - 1, 0),
            new Point(outW - 1, outH - 1),
            new Point(0, outH - 1)
        );

        Mat M = Imgproc.getPerspectiveTransform(src, dst);
        Mat warped = new Mat(outH, outW, rgba.type());
        Imgproc.warpPerspective(rgba, warped, M, new Size(outW, outH), Imgproc.INTER_LINEAR);

        M.release();
        src.release();
        dst.release();

        return warped;
    }

    private static Point[] OrderCornersTLTRBRBL(Point[] pts)
    {
        // TL: min (x+y), BR: max (x+y)
        // TR: max (x-y), BL: min (x-y)
        Point tl = pts[0], tr = pts[0], br = pts[0], bl = pts[0];
        double minSum = double.PositiveInfinity, maxSum = double.NegativeInfinity;
        double minDiff = double.PositiveInfinity, maxDiff = double.NegativeInfinity;

        foreach (var p in pts)
        {
            double sum = p.x + p.y;
            double diff = p.x - p.y;

            if (sum < minSum) { minSum = sum; tl = p; }
            if (sum > maxSum) { maxSum = sum; br = p; }
            if (diff < minDiff) { minDiff = diff; bl = p; }
            if (diff > maxDiff) { maxDiff = diff; tr = p; }
        }

        return new[] { tl, tr, br, bl };
    }

    private static bool LooksCardAspect(Point[] quad, double maxError)
    {
        // Very loose: we just ensure it is not wildly skewed.
        // Compute side lengths after ordering.
        Point[] o = OrderCornersTLTRBRBL(quad);
        double w1 = Dist(o[0], o[1]);
        double w2 = Dist(o[3], o[2]);
        double h1 = Dist(o[0], o[3]);
        double h2 = Dist(o[1], o[2]);
        double w = (w1 + w2) * 0.5;
        double h = (h1 + h2) * 0.5;

        if (w <= 1 || h <= 1) return false;

        // Expect portrait-ish, but allow either orientation.
        double aspect = Math.Max(w, h) / Math.Min(w, h);
        // Typical cards are ~1.4-1.6 aspect. We allow wide range.
        double target = 1.5;
        double err = Math.Abs(aspect - target) / target;
        return err <= maxError || aspect >= 1.15; // don't reject reasonable rectangles
    }

    private static bool LooksRectangular(Point[] quad, double maxAbsCos)
    {
        // Check angle cosines near 0 (90 degrees)
        Point[] o = OrderCornersTLTRBRBL(quad);

        // vectors around each corner
        for (int i = 0; i < 4; i++)
        {
            Point p = o[i];
            Point pPrev = o[(i + 3) % 4];
            Point pNext = o[(i + 1) % 4];

            double cos = AngleCos(pPrev, p, pNext);
            if (Math.Abs(cos) > maxAbsCos) return false;
        }
        return true;
    }

    private static double AngleCos(Point a, Point b, Point c)
    {
        // cosine of angle ABC
        double abx = a.x - b.x;
        double aby = a.y - b.y;
        double cbx = c.x - b.x;
        double cby = c.y - b.y;

        double dot = abx * cbx + aby * cby;
        double ab = Math.Sqrt(abx * abx + aby * aby);
        double cb = Math.Sqrt(cbx * cbx + cby * cby);
        if (ab < 1e-6 || cb < 1e-6) return 1.0;

        return dot / (ab * cb);
    }

    private static double Dist(Point a, Point b)
    {
        double dx = a.x - b.x;
        double dy = a.y - b.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
