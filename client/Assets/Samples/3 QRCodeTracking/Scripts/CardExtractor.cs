using System;
using System.Linq;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;


public class CardExtractor : MonoBehaviour
{
    [Header("Shaders")]
    public ComputeShader warpShader;

    [Header("Coordinate Fixes")]
    [Tooltip("ZXing liefert y von oben nach unten. Unity-Texturen sind y von unten nach oben.")]
    public bool flipInputY = true;

    [Tooltip("Dein Template ist wie Photoshop gedacht (y=0 oben). Unity-Output-Texture ist (y=0 unten).")]
    public bool flipTemplateY = true;

    [Header("Homography")]
    [Tooltip("ComputeShader macht Backward-Mapping (Output->Input). Dafür brauchen wir Hinv.")]
    public bool useInverse = true;

    [Header("Debug")]
    public bool debugLogs = true;

    [Tooltip("Wenn true: speichert eine Debug-Textur (aligned full template) als PNG in persistentDataPath.")]
    public bool debugSaveAlignedPng = false;

    [Tooltip("Optional: zeig statt Warp ein Debug-Farbmuster (wenn du Shader damit ergänzt).")]
    public bool debugShaderGradient = false;

    // --- Public API ---------------------------------------------------------

    // public Texture2D ExtractArtwork(Texture2D camTex, QrCodeResult qr, CardTemplate template)
    // {
    //     // old ----------------------------------------------------
    //     // var aligned = WarpToTemplate(camTex, qr, template);

    //     // var r = template.artworkRect;
    //     // var art = new Texture2D(r.width, r.height, TextureFormat.RGBA32, false);
    //     // var pixels = aligned.GetPixels(r.x, r.y, r.width, r.height);
    //     // art.SetPixels(pixels);
    //     // art.Apply();
    //     // return art;
    //     // old ----------------------------------------------------

    //     // new
    //     Vector2 TL = ToPixel(qr.corners[0], qr.captureResolution);
    //     Vector2 TR = ToPixel(qr.corners[1], qr.captureResolution);
    //     Vector2 BL = ToPixel(qr.corners[2], qr.captureResolution);
    //     Vector2 BR = ToPixel(qr.corners[3], qr.captureResolution);

    //     // sort corners
    //     Vector2[] srcRaw = { TL, TR, BR, BL };

    //     Vector2[] src = RotateCorners(srcRaw);
    //     Vector2 TLq = src[0];
    //     Vector2 TRq = src[1];
    //     Vector2 BRq = src[2];
    //     Vector2 BLq = src[3];
    //     src[2] = src[1]+src[3]-src[0];
    //     Vector2 right = (TRq - TLq).normalized;
    //     Vector2 down  = (BLq - TLq).normalized;

    //     float w = (TRq - TLq).magnitude;
    //     float h = (BLq - TLq).magnitude;

    //     // QR-Datenbereich liegt realistisch bei ~25–29 Modulen
    //     float modulePx = (w + h) * 0.5f / 27f;
    //     float quietPx  = 4f * modulePx;
    //     float innerwidth = (src[1]-src[0]).magnitude;
    //     // float modulePx = innerwidth/21f;
    //     // float quietPx = 4f * modulePx;

    //     Vector2 center =
    //         0.25f * (src[0] + src[1] + src[2] + src[3]);

    //     src[0] = TLq - right*quietPx - down*quietPx;
    //     src[1] = TRq + right*quietPx - down*quietPx;
    //     src[3] = BLq - right*quietPx + down*quietPx;
    //     src[2] = BRq + right*quietPx + down*quietPx;
    //     // Vector2[] src = NormalizeCorners(srcRaw);
    //     // Vector2[] src = {TR, BR, TL, BL};


    //     Debug.Log("[PIXEL BEFORE FLIP]");
    //     Debug.Log($"[PIXEL BEFORE TL={srcRaw[0]} TR={srcRaw[1]} BR={srcRaw[3]} BL={srcRaw[2]}");

    //     Debug.Log("[PIXEL AFTER FLIP]");
    //     Debug.Log($"[PIXEL AFTER TL={src[0]} TR={src[1]} BR={src[2]} BL={src[3]}");


    //     // extract texture
    //     Mat mat = new Mat(camTex.height, camTex.width, CvType.CV_8UC4);
    //     Utils.texture2DToMat(camTex, mat);
    //     Mat artworkMat = ExtractArtworkMat(
    //         mat, 
    //         src[0], 
    //         src[1], 
    //         src[2], 
    //         src[3], 
    //         0.10f, 
    //         512);
    //     Texture2D artworkTex = new Texture2D(
    //         artworkMat.cols(),
    //         artworkMat.rows(),
    //         TextureFormat.RGBA32,
    //         false
    //     );
    //     Utils.matToTexture2D(artworkMat, artworkTex);

    //     DrawLine(camTex, src[0], src[1], new Color(1f,0f,0f));
    //     DrawLine(camTex, src[1], src[2], new Color(1f,0f,0f));
    //     DrawLine(camTex, src[2], src[3], new Color(1f,0f,0f));
    //     DrawLine(camTex, src[3], src[0], new Color(1f,0f,0f));
    //     return camTex;
    //     // new
    // }
    public Texture2D ExtractArtwork(Texture2D camTex, QrCodeResult qr, CardTemplate template)
    {
        // old ----------------------------------------------------
        // var aligned = WarpToTemplate(camTex, qr, template);

        // var r = template.artworkRect;
        // var art = new Texture2D(r.width, r.height, TextureFormat.RGBA32, false);
        // var pixels = aligned.GetPixels(r.x, r.y, r.width, r.height);
        // art.SetPixels(pixels);
        // art.Apply();
        // return art;
        // old ----------------------------------------------------

        // new
        Vector2 TL = ToPixel(qr.corners[0], qr.captureResolution);
        Vector2 TR = ToPixel(qr.corners[1], qr.captureResolution);
        Vector2 BL = ToPixel(qr.corners[2], qr.captureResolution);
        Vector2 BR = ToPixel(qr.corners[3], qr.captureResolution);

        // sort corners
        Vector2[] srcRaw = { TL, TR, BR, BL };

        Vector2[] src = RotateCorners(srcRaw);
        src[1] = src[2] + src[0] - src[3];
        Vector2 TLq = src[0];
        Vector2 TRq = src[1];
        Vector2 BLq = src[3];
        Vector2 BRq = src[2];

        // --- 3) BR rekonstruieren (QR-Asymmetrie fix) --- TL->TR->BR->BL
        // --- 4) Quiet-Zone-Größe schätzen ---
        // Vector2 right = (TRq - TLq).normalized;
        // Vector2 down  = (BLq - TLq).normalized;

        Vector2 right = ((src[1] - src[0]) + (src[2] - src[3])) * 0.5f;
        Vector2 down  = ((src[3] - src[0]) + (src[2] - src[1])) * 0.5f;

        right.Normalize();
        down.Normalize();

        float w = (src[1] - src[0]).magnitude;
        float h = (src[3] - src[0]).magnitude;

        float moduleX = w / 27f;
        float moduleY = h / 27f;

        float quietX = 4f * moduleX;
        float quietY = 4f * moduleY;

        src[0] = TLq - right * quietX - down * quietY;
        src[1] = TRq + right * quietX - down * quietY;
        src[3] = BLq - right * quietX + down * quietY;
        src[2] = BRq + right * quietX + down * quietY;

        // src[2] += (nBottom + nRight) * quietPx;
        // src[3] += (nBottom + nLeft)  * quietPx;
        // Vector2[] src = NormalizeCorners(srcRaw);
        // Vector2[] src = {TR, BR, TL, BL};


        Debug.Log("[PIXEL BEFORE FLIP]");
        Debug.Log($"[PIXEL BEFORE TL={srcRaw[0]} TR={srcRaw[1]} BR={srcRaw[3]} BL={srcRaw[2]}");

        Debug.Log("[PIXEL AFTER FLIP]");
        Debug.Log($"[PIXEL AFTER TL={src[0]} TR={src[1]} BR={src[2]} BL={src[3]}");

        TLq = src[0];
        TRq = src[1];
        BLq = src[3];
        BRq = src[2];

        Vector2 dy = (BLq-TLq)*1.1f;
        Vector2 dx = BRq-BLq;
        Vector2 BBL = BLq+dy;
        Vector2 RBBL = BBL+dx;

        // --- 3) BR rekonstruieren (QR-Asymmetrie fix) --- TL->TR->BR->BL
        Vector2 ATR = BRq;
        Vector2 ATL = BLq;
        Vector2 ABR = RBBL;
        Vector2 ABL = BBL;
        
        // DrawLine(camTex, src[0], src[1], new Color(1f,0f,0f));
        // DrawLine(camTex, src[1], src[2], new Color(1f,0f,0f));
        // DrawLine(camTex, src[2], src[3], new Color(1f,0f,0f));
        // DrawLine(camTex, src[3], src[0], new Color(1f,0f,0f));

        // DrawLine(camTex, ATL, ATR, new Color(1f,0f,0f));
        // DrawLine(camTex, ATR, ABR, new Color(0f,1f,0f));
        // DrawLine(camTex, ABR, ABL, new Color(0f,0f,1f));
        // DrawLine(camTex, ABL, ATL, new Color(1f,1f,0f));


        // extract texture
        Mat mat = new Mat(camTex.height, camTex.width, CvType.CV_8UC4);
        Utils.texture2DToMat(camTex, mat);
        Mat artworkMat = ExtractArtworkMat(
            mat, 
            ATL, 
            ATR, 
            ABR, 
            ABL, 
            512);
        Texture2D artworkTex = new Texture2D(
            artworkMat.cols(),
            artworkMat.rows(),
            TextureFormat.RGBA32,
            false
        );
        Utils.matToTexture2D(artworkMat, artworkTex);


        return artworkTex;
        // new
    }


    // --- Core: Warp ---------------------------------------------------------

    // ---- neu
    
    bool IsCCW(Vector2[] q)
    {
        float sum = 0f;
        for (int i = 0; i < 4; i++)
        {
            Vector2 a = q[i];
            Vector2 b = q[(i + 1) % 4];
            sum += (b.x - a.x) * (b.y + a.y);
        }
        return sum < 0f; // CCW
    }


    static Vector2 EdgeNormal(Vector2 a, Vector2 b, bool ccw)
    {
        Vector2 e = b - a;
        Vector2 n = ccw
            ? new Vector2(e.y, -e.x)   // CCW → außen
            : new Vector2(-e.y, e.x);  // CW → außen
        return n.normalized;
    }


    public static void DrawLine(Texture2D tex, Vector2 p0, Vector2 p1, Color col)
    {
        int x0 = Mathf.RoundToInt(p0.x);
        int y0 = Mathf.RoundToInt(p0.y);
        int x1 = Mathf.RoundToInt(p1.x);
        int y1 = Mathf.RoundToInt(p1.y);

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (x0 >= 0 && x0 < tex.width && y0 >= 0 && y0 < tex.height)
                tex.SetPixel(x0, y0, col);

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 <  dx) { err += dx; y0 += sy; }
        }
    }

    
    /**
    * @input: takes a 4 point vector2
    * @return: returns 4 corners TL->TR->BR->BL
    */
    Vector2[] RotateCorners(Vector2[] points)
    {
        if (points == null || points.Length != 4)
            throw new ArgumentException("RotateCorners expects exactly 4 points");
        // first sort to top and bottom corners
        Vector2[] corners = SortVector2Y((Vector2[])points.Clone());

        // afterwars decide the corners
        Vector2 TL = corners[0].x < corners[1].x ? corners[0] : corners[1];
        Vector2 TR = corners[0].x > corners[1].x ? corners[0] : corners[1];
        Vector2 BL = corners[2].x < corners[3].x ? corners[2] : corners[3];
        Vector2 BR = corners[2].x > corners[3].x ? corners[2] : corners[3];
        return new Vector2[]{BL, BR, TR, TL};
    }

    Vector2[] SortVector2Y(Vector2[] points)
    {
        bool sorted;
        do
        {
            sorted = true;

            for (int i = 1; i < points.Length; i++)
            {
                if (points[i - 1].y < points[i].y)
                {
                    var temp = points[i];
                    points[i] = points[i - 1];
                    points[i - 1] = temp;
                    sorted = false;
                }
            }
        }
        while (!sorted);
        return points;
    }

    bool isFlippedY(Vector2 TL, Vector2 BL)
    {
        return TL.y > BL.y; // TL sollte über BL liegen
    }
    
    public static Mat ExtractArtworkMat( Mat inputRgba, Vector2 TL, Vector2 TR, Vector2 BR, Vector2 BL, int outSize = 512) 
    {   // 1) QR-Größe in Pixel 
        // Vector2 dy = (BL-TL)*1.1f;
        // Vector2 dx = BR-BL;
        // Vector2 BBL = BL+dy;
        // Vector2 RBBL = BBL+dx;
        // float w = (TR - TL).magnitude;
        // float h = (BL - TL).magnitude; 
        // float qrSizePx = 0.5f * (w + h); 
        // float aPx = aRatio * qrSizePx; 
        // // 2) Basisrichtung 
        // Vector2 down = (BL - TL).normalized; 
        // Vector2 up = -down; 
        // Vector2 offset = up * aPx + up; 
        // // 3) Artwork-Corners (erste Version) 
        // Vector2 ATL = TL + offset; 
        // Vector2 ATR = TR + offset; 
        // Vector2 ABL = BL + offset; 
        // Vector2 ABR = BR + offset; 
        // // 4) Richtung validieren 
        // Vector2 qrCenter = 0.25f * (TL + TR + BL + BR); 
        // Vector2 artCenter = 0.25f * (ATL + ATR + ABL + ABR); 
        // if (Vector2.Dot(artCenter - qrCenter, up) < 0) 
        // { 
        //     offset = -offset; ATL = TL + offset; 
        //     ATR = TR + offset; 
        //     ABL = BL + offset; 
        //     ABR = BR + offset; 
        // } 
        // ATR = BR;
        // ATL = BL;
        // ABR = RBBL;
        // ABL = BBL;
        // 5) Homographie 
        int h = inputRgba.rows();
        
        Point[] srcPts = 
        { 
            new Point(TL.x, h - 1 - TL.y), 
            new Point(TR.x, h - 1 - TR.y), 
            new Point(BR.x, h - 1 - BR.y), 
            new Point(BL.x, h - 1 - BL.y) 
        }; 

        Point[] dstPts = { new Point(0, 0), new Point(outSize, 0), new Point(outSize, outSize), new Point(0, outSize) }; 
        Mat H = Imgproc.getPerspectiveTransform( new MatOfPoint2f(srcPts), new MatOfPoint2f(dstPts) ); 
        Mat outMat = new Mat(outSize, outSize, inputRgba.type()); 
        Imgproc.warpPerspective( inputRgba, outMat, H, new Size(outSize, outSize), Imgproc.INTER_LINEAR, Core.BORDER_REPLICATE ); return outMat; 
    }


    static Mat TranslationScale(Vector2 c, float s)
    {
        Mat T = Mat.eye(3, 3, CvType.CV_64F);
        T.put(0, 0, s);
        T.put(1, 1, s);
        T.put(0, 2, -s * c.x);
        T.put(1, 2, -s * c.y);
        return T;
    }



    // ---- neu

    Texture2D CutRect(Texture2D source, int xMin, int yMin, int width, int height)
    {
        Texture2D result = new Texture2D(width, height, source.format, false);

        Color[] pixels = source.GetPixels(xMin, yMin, width, height);
        result.SetPixels(pixels);
        result.Apply();

        return result;
    }


    private Vector2 FlipY(Vector2 p, int height)
    {
        return new Vector2(p.x, (height - 1) - p.y);
    }

    private Texture2D WarpToTemplate(RenderTexture camTex, QrCodeResult qr, CardTemplate template)
    {
        // --- 1) SRC: QR-Ecken im Kamerabild (PIXEL, Unity-Y)
        Vector2 TL = ToPixel(qr.corners[0], qr.captureResolution);
        Vector2 TR = ToPixel(qr.corners[1], qr.captureResolution);
        Vector2 BL = ToPixel(qr.corners[2], qr.captureResolution);
        Vector2 BR = ToPixel(qr.corners[3], qr.captureResolution);

        Vector2[] srcRaw = { TL, TR, BL, BR };
        Vector2[] src = NormalizeCorners(srcRaw);

        // --- DEBUG: vor dem Flip (empfohlen)
        Debug.Log("[PIXEL BEFORE FLIP]");
        Debug.Log($"TL={src[0]} TR={src[1]} BR={src[2]} BL={src[3]}");

        // --- 3) EINMAL flippen (ZXing → Unity-Texturraum)
        // for (int i = 0; i < 4; i++)
        // {
        //     src[i].y = qr.captureResolution.y - src[i].y;
        // }

        // --- DEBUG: nach dem Flip (sehr wichtig!)
        Debug.Log("[PIXEL AFTER FLIP]");
        Debug.Log($"TL={src[0]} TR={src[1]} BR={src[2]} BL={src[3]}");
        

        // Debug.Log("[PIXEL] ===================================================");
        // Debug.Log("[PIXEL] TL = (x:" + TL.x + ", y:" + TL.y + "), TR = (x:" + TR.x + ", y:" + TR.y + "), BL = (x:" + BL.x + ", y:" + BL.y + "), BR = (x:" + BR.x + ", y:" + BR.y + ")");
        // Debug.Log("[PIXEL] ===================================================");


        // --- 2) DST: Template-QR (Unity-Texture-Koordinaten!)
        float H = template.height;

        // Vector2[] dst =
        // {
        //     new Vector2(0, H - 1100),        // TL
        //     new Vector2(1000, H - 1100),     // TR
        //     new Vector2(1000, H - 2100),     // BR
        //     new Vector2(0, H - 2100)         // BL
        // };

        // neu --------------
        Vector2[] cardQR =
        {
            new Vector2(0,0),        // TL
            new Vector2(1000,0),     // TR
            new Vector2(1000,1000),  // BR
            new Vector2(0,1000)      // BL
        };


        Vector2[] imgQR = src;
        Matrix4x4 H_cardToImg = Homography.Compute(cardQR, imgQR);

        Vector2[] cardArt =
        {
            new Vector2(0,   2500), // TL
            new Vector2(1000, 2500), // TR
            new Vector2(1000, 1500),  // BR
            new Vector2(0,   1500)   // BL
        };

        Vector2[] imgArt =
        {
            ApplyH(H_cardToImg, cardArt[0]),
            ApplyH(H_cardToImg, cardArt[1]),
            ApplyH(H_cardToImg, cardArt[2]),
            ApplyH(H_cardToImg, cardArt[3])
        };

        Vector2[] outQuad =
        {
            new Vector2(0,0),
            new Vector2(1000,0),
            new Vector2(1000,1000),
            new Vector2(0,1000)
        };

        Matrix4x4 H_outToImg = Homography.Compute(outQuad, imgArt);
        // alt ------------------
        // Vector2[] dst =
        // {
        //     new Vector2(0, 2100),        // TL
        //     new Vector2(1000, 2100),     // TR
        //     new Vector2(1000, 1100),     // BR
        //     new Vector2(0, 1100)         // BL
        // };

        // // --- 3) WICHTIG: Homographie direkt DST → SRC
        // Matrix4x4 HdstToSrc = Homography.Compute(dst, src);
        // Vector2 test = ApplyHomography(HdstToSrc, dst[0]);
        // Debug.Log($"[H TEST] [WARP] dst[0] -> {test} (should be ~ src[0])");
        // if (debugLogs)
        // {
        //     DebugDump(camTex, qr, template, src, dst, HdstToSrc, HdstToSrc);
        // }
        // alt ------------------------

        // --- 4) GPU Warp
        // RenderTexture rt = new RenderTexture(template.width, template.height, 0, RenderTextureFormat.ARGB32);
        RenderTexture rt = new RenderTexture(500, 500, 0, RenderTextureFormat.ARGB32);
        rt.enableRandomWrite = true;
        rt.Create();

        int kernel = warpShader.FindKernel("Warp");
        warpShader.SetTexture(kernel, "_Input", camTex);
        warpShader.SetTexture(kernel, "_Output", rt);
        // warpShader.SetMatrix("_H", HdstToSrc);
        warpShader.SetMatrix("_H", H_outToImg);

        warpShader.Dispatch(kernel,
            Mathf.CeilToInt(template.width / 8f),
            Mathf.CeilToInt(template.height / 8f),
            1
        );

        // --- 5) RT → Texture2D
        Texture2D result = new Texture2D(template.width, template.height, TextureFormat.RGBA32, false);
        RenderTexture.active = rt;
        result.ReadPixels(new UnityEngine.Rect(0, 0, template.width, template.height), 0, 0);
        result.Apply();
        RenderTexture.active = null;

        return result;
    }

    private Vector2 ToPixel(Vector3 uv, Vector2Int res)
    {
        return new Vector2(uv.x * res.x, uv.y * res.y);
    }
    static Vector2 ApplyH(Matrix4x4 H, Vector2 p)
    {
        float x = p.x, y = p.y;
        float X = H.m00*x + H.m01*y + H.m02;
        float Y = H.m10*x + H.m11*y + H.m12;
        float W = H.m20*x + H.m21*y + H.m22;
        if (Mathf.Abs(W) < 1e-8f) W = 1e-8f;
        return new Vector2(X/W, Y/W);
    }
    private Vector2[] NormalizeCorners(Vector2[] pts)
    {
        // Mittelpunkt
        Vector2 c = Vector2.zero;
        foreach (var p in pts) c += p;
        c /= pts.Length;

        // Sortiere im Uhrzeigersinn
        var ordered = pts
            .OrderBy(p => Mathf.Atan2(p.y - c.y, p.x - c.x))
            .ToArray();

        // Finde Top-Left (kleinstes y, dann kleinstes x)
        int tl = 0;
        for (int i = 1; i < 4; i++)
        {
            if (ordered[i].y < ordered[tl].y ||
            (Mathf.Approximately(ordered[i].y, ordered[tl].y) && ordered[i].x < ordered[tl].x))
            {
                tl = i;
            }
        }

        // Rotieren, sodass TL bei Index 0 ist
        Vector2[] result = new Vector2[4];
        for (int i = 0; i < 4; i++)
            result[i] = ordered[(tl + i) % 4];

        return result;
    }



    // --- Debug --------------------------------------------------------------

    private void DebugDump(Texture camTex, QrCodeResult qr, CardTemplate template,
        Vector2[] src, Vector2[] dst, Matrix4x4 H, Matrix4x4 MUsed)
    {
        int camW = camTex.width;
        int camH = camTex.height;

        Debug.Log($"[WARP] camTex.size={camW}x{camH}  captureRes={qr.captureResolution.x}x{qr.captureResolution.y}");
        Debug.Log($"[WARP] template.size={template.width}x{template.height}  flipInputY={flipInputY} flipTemplateY={flipTemplateY} useInverse={useInverse}");

        Debug.Log($"[WARP] src (input px, ordered TL,TR,BR,BL): " +
                  $"{Fmt(src[0])} {Fmt(src[1])} {Fmt(src[2])} {Fmt(src[3])}");

        Debug.Log($"[WARP] dst (output px, ordered TL,TR,BR,BL): " +
                  $"{Fmt(dst[0])} {Fmt(dst[1])} {Fmt(dst[2])} {Fmt(dst[3])}");

        // Mapping sanity checks:
        // Wenn useInverse=true, dann ist MUsed = H^-1 und muss dst -> src abbilden.
        // Wir testen: MUsed(dst[i]) sollte nahe src[i] liegen.
        for (int i = 0; i < 4; i++)
        {
            Vector2 back = ApplyHomography(MUsed, dst[i]);
            Debug.Log($"[WARP] mapTest: MUsed(dst[{i}]) = {Fmt(back)}  vs src[{i}]={Fmt(src[i])}  dist={Vector2.Distance(back, src[i]):F2}");
        }

        // Teste außerdem Center der QR-Zielbox:
        Vector2 dstCenter = 0.25f * (dst[0] + dst[1] + dst[2] + dst[3]);
        Vector2 srcCenter = ApplyHomography(MUsed, dstCenter);
        Debug.Log($"[WARP] centerTest: dstCenter={Fmt(dstCenter)} -> srcCenter={Fmt(srcCenter)}");

        // Grobe Range: wo landen 5 Samplepunkte im Output im Input?
        Vector2[] samples =
        {
            new Vector2(0, 0),
            new Vector2(template.width-1, 0),
            new Vector2(template.width-1, template.height-1),
            new Vector2(0, template.height-1),
            new Vector2(template.width*0.5f, template.height*0.5f)
        };

        for (int i = 0; i < samples.Length; i++)
        {
            Vector2 s = ApplyHomography(MUsed, samples[i]);
            Debug.Log($"[WARP] sample[{i}] out={Fmt(samples[i])} -> in={Fmt(s)}");
        }

        Debug.Log($"[WARP] If most 'in=' are outside [0..{camW})x[0..{camH}), your flips/order/direction are still wrong.");
    }

    private static string Fmt(Vector2 v) => $"({v.x:F1},{v.y:F1})";

    private static void SavePng(Texture2D tex, string filename)
    {
        try
        {
            var png = tex.EncodeToPNG();
            var path = System.IO.Path.Combine(Application.persistentDataPath, filename);
            System.IO.File.WriteAllBytes(path, png);
            Debug.Log("[WARP] Saved PNG: " + path);
        }
        catch (Exception e)
        {
            Debug.LogWarning("[WARP] SavePng failed: " + e.Message);
        }
    }

    // --- Geometry utils -----------------------------------------------------

    /// <summary>
    /// Sortiert Punkte im Uhrzeigersinn und rotiert das Array so,
    /// dass Index 0 = "TopLeft" ist.
    /// Bei originBottomLeft=true gilt "TopLeft" = minX + maxY.
    /// </summary>
    private static Vector2[] NormalizeCornersClockwise(Vector2[] pts, bool originBottomLeft)
    {
        if (pts == null || pts.Length != 4) throw new Exception("Need exactly 4 corners.");

        // 1) Center
        Vector2 c = Vector2.zero;
        for (int i = 0; i < 4; i++) c += pts[i];
        c *= 0.25f;

        // 2) Sort by angle
        var ordered = pts
            .OrderBy(p => Mathf.Atan2(p.y - c.y, p.x - c.x))
            .ToArray();

        // 3) Rotate so that [0] is top-left in the given coordinate system
        int best = 0;
        float bestScore = ScoreTopLeft(ordered[0], originBottomLeft);
        for (int i = 1; i < 4; i++)
        {
            float sc = ScoreTopLeft(ordered[i], originBottomLeft);
            if (sc < bestScore)
            {
                bestScore = sc;
                best = i;
            }
        }

        var result = new Vector2[4];
        for (int i = 0; i < 4; i++)
            result[i] = ordered[(best + i) % 4];

        // 4) Ensure clockwise (optional): if area is negative, swap 1 and 3
        // (Schadet nicht, macht's stabiler.)
        if (SignedQuadArea(result) < 0)
        {
            (result[1], result[3]) = (result[3], result[1]);
        }

        return result;
    }

    private static float ScoreTopLeft(Vector2 p, bool originBottomLeft)
    {
        // "Top" heißt: y maximal (bei bottom-left origin)
        // Wir bauen einen Score, den wir minimieren:
        //  - kleiner x ist gut
        //  - größer y ist gut -> als -y
        return originBottomLeft ? (p.x - p.y) : (p.x + p.y);
    }

    private static float SignedQuadArea(Vector2[] q)
    {
        // Shoelace sum
        float a = 0;
        for (int i = 0; i < 4; i++)
        {
            Vector2 p = q[i];
            Vector2 n = q[(i + 1) % 4];
            a += p.x * n.y - n.x * p.y;
        }
        return a * 0.5f;
    }

    private static Vector2 ApplyHomography(Matrix4x4 H, Vector2 p)
    {
        float x = p.x;
        float y = p.y;

        float X = H.m00 * x + H.m01 * y + H.m03;
        float Y = H.m10 * x + H.m11 * y + H.m13;
        float W = H.m30 * x + H.m31 * y + H.m33;

        if (Mathf.Abs(W) < 1e-6f)
            return new Vector2(float.NaN, float.NaN);

        return new Vector2(X / W, Y / W);
    }

}
