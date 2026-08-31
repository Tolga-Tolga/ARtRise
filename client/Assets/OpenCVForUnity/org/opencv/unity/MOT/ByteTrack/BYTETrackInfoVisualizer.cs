using System;
using System.Text;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    /// <summary>
    /// BYTETrackInfoVisualizer
    /// A class that provides visualization functionality for BYTE tracking information.
    /// </summary>
    public class BYTETrackInfoVisualizer : TrackInfoVisualizerBase, ITrackInfoVisualizer<BYTETrackInfo>
    {
        /// <summary>
        /// Initializes a new instance of the BYTETrackInfoVisualizer class.
        /// </summary>
        /// <param name="classesFilepath">Path to the text file containing class names.</param>
        public BYTETrackInfoVisualizer(string classesFilepath = null) : base(classesFilepath)
        {
        }

        /// <summary>
        /// Visualizes BYTE tracking information on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="trackInfos">The BYTE tracking information to visualize.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the input image is RGB or BGR.</param>
#if NET_STANDARD_2_1
        public virtual void Visualize(Mat image, ReadOnlySpan<BYTETrackInfo> trackInfos, bool printResult = false, bool isRGB = false)
#else
        public virtual void Visualize(Mat image, BYTETrackInfo[] trackInfos, bool printResult = false, bool isRGB = false)
#endif
        {
            ThrowIfDisposed();

            if (image != null) image.ThrowIfDisposed();
            if (trackInfos == null || trackInfos.Length == 0)
                return;

            for (int i = 0; i < trackInfos.Length; ++i)
            {
                ref readonly var trackInfo = ref trackInfos[i];
                int trackId = trackInfo.TrackId;
                int trackletLength = trackInfo.TrackletLength;
                var bbox = trackInfo.BBox;
                float left = bbox.X;
                float top = bbox.Y;
                float right = bbox.X + bbox.Width;
                float bottom = bbox.Y + bbox.Height;
                float conf = bbox.Score;
                int classId = bbox.ClassId;

                var c = SCALAR_PALETTE[trackId % SCALAR_PALETTE.Length].ToValueTuple();
                var color = isRGB ? c : (c.v2, c.v1, c.v0, c.v3);

                Imgproc.rectangle(image, (left, top), (right, bottom), color, 2);

                string label = $"ID:{trackId} ({trackletLength}), {GetClassLabel(classId)}, {conf:F2}";

                int[] baseLine = new int[1];
                var labelSize = Imgproc.getTextSizeAsValueTuple(label, Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, 1, baseLine);

                top = Mathf.Max((float)top, (float)labelSize.height);
                Imgproc.rectangle(image, (left, top - labelSize.height),
                    (left + labelSize.width, top + baseLine[0]), color, Core.FILLED);
                Imgproc.putText(image, label, (left, top), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, SCALAR_WHITE.ToValueTuple(), 1, Imgproc.LINE_AA);
            }

            if (printResult)
            {
                StringBuilder sb = new StringBuilder(512);

                for (int i = 0; i < trackInfos.Length; ++i)
                {
                    ref readonly var trackInfo = ref trackInfos[i];
                    var bbox = trackInfo.BBox;
                    sb.AppendFormat("-----------track {0}-----------", i + 1);
                    sb.AppendLine();
                    sb.Append("TrackId: ").Append(trackInfo.TrackId);
                    sb.AppendLine();
                    sb.Append("State: ").Append(trackInfo.State);
                    sb.AppendLine();
                    sb.Append("TrackletLength: ").Append(trackInfo.TrackletLength);
                    sb.AppendLine();
                    sb.Append("Class: ").Append(GetClassLabel(bbox.ClassId));
                    sb.AppendLine();
                    sb.AppendFormat("Confidence: {0:F4}", bbox.Score);
                    sb.AppendLine();
                    sb.AppendFormat("Box: {0:F0} {1:F0} {2:F0} {3:F0}", bbox.X, bbox.Y, bbox.Width, bbox.Height);
                    sb.AppendLine();
                }

                Debug.Log(sb.ToString());
            }
        }

#if NET_STANDARD_2_1

        /// <summary>
        /// Visualizes BYTE tracking information on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="trackInfos">The BYTE tracking information to visualize.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the input image is RGB or BGR.</param>
        public virtual void Visualize(Mat image, BYTETrackInfo[] trackInfos, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (trackInfos == null)
                throw new ArgumentNullException(nameof(trackInfos));

            Visualize(image, trackInfos.AsSpan(), printResult, isRGB);
        }

#endif

    }
}
