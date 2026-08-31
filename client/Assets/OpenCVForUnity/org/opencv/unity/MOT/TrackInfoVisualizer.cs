using System;
using System.Collections.Generic;
using System.Text;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityIntegration.Worker.Utils;
using UnityEngine;

namespace OpenCVForUnity.UnityIntegration.MOT
{
    /// <summary>
    /// TrackInfoVisualizerBase
    /// A base class that provides common visualization functionality for tracking information.
    /// </summary>
    public abstract class TrackInfoVisualizerBase : IDisposable
    {
        protected static readonly Scalar SCALAR_WHITE = new Scalar(255, 255, 255, 255);
        protected static readonly Scalar[] SCALAR_PALETTE = new Scalar[]
        {
            new(255, 56, 56, 255),
            new(255, 157, 151, 255),
            new(255, 112, 31, 255),
            new(255, 178, 29, 255),
            new(207, 210, 49, 255),
            new(72, 249, 10, 255),
            new(146, 204, 23, 255),
            new(61, 219, 134, 255),
            new(26, 147, 52, 255),
            new(0, 212, 187, 255),
            new(44, 153, 168, 255),
            new(0, 194, 255, 255),
            new(52, 69, 147, 255),
            new(100, 115, 255, 255),
            new(0, 24, 236, 255),
            new(132, 56, 255, 255),
            new(82, 0, 133, 255),
            new(203, 56, 255, 255),
            new(255, 149, 200, 255),
            new(255, 55, 199, 255)
        };

        protected List<string> _classNames;
        protected bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the TrackInfoVisualizerBase class.
        /// </summary>
        /// <param name="classesFilepath">Path to the text file containing class names.</param>
        protected TrackInfoVisualizerBase(string classesFilepath = null)
        {
            if (!string.IsNullOrEmpty(classesFilepath))
            {
                _classNames = ReadClassNames(classesFilepath);
            }
            else
            {
                _classNames = new List<string>();
            }
        }

        /// <summary>
        /// Gets the class label for the given class ID.
        /// </summary>
        /// <param name="id">Class ID.</param>
        /// <returns>Class label string. Returns the ID as string if no label is found.</returns>
        protected virtual string GetClassLabel(float id)
        {
            ThrowIfDisposed();

            return ClassLabelUtils.GetClassLabel(id, _classNames);
        }

        /// <summary>
        /// Gets all class labels.
        /// </summary>
        /// <returns>Array of class label strings.</returns>
        public virtual string[] GetClassLabels()
        {
            ThrowIfDisposed();

            return _classNames.ToArray();
        }

        /// <summary>
        /// Reads class names from a text file.
        /// </summary>
        /// <param name="filename">Path to the text file containing class names.</param>
        /// <returns>List of class names.</returns>
        protected virtual List<string> ReadClassNames(string filename)
        {
            return ClassLabelUtils.ReadClassNames(filename);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _classNames?.Clear(); _classNames = null;
            }

            _disposed = true;
        }

        ~TrackInfoVisualizerBase()
        {
            Dispose(false);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }

    /// <summary>
    /// TrackInfoVisualizer
    /// A class that provides visualization functionality for tracking information.
    /// </summary>
    public class TrackInfoVisualizer : TrackInfoVisualizerBase, ITrackInfoVisualizer<TrackInfo>
    {
        /// <summary>
        /// Initializes a new instance of the TrackInfoVisualizer class.
        /// </summary>
        /// <param name="classesFilepath">Path to the text file containing class names.</param>
        public TrackInfoVisualizer(string classesFilepath = null) : base(classesFilepath)
        {
        }

        /// <summary>
        /// Visualizes tracking information on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="trackInfos">The tracking information to visualize.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the input image is RGB or BGR.</param>
#if NET_STANDARD_2_1
        public virtual void Visualize(Mat image, ReadOnlySpan<TrackInfo> trackInfos, bool printResult = false, bool isRGB = false)
#else
        public virtual void Visualize(Mat image, TrackInfo[] trackInfos, bool printResult = false, bool isRGB = false)
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

                string label = $"ID:{trackId}, {GetClassLabel(classId)}, {conf:F2}";

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
        /// Visualizes tracking information on the input image.
        /// </summary>
        /// <param name="image">The input image to draw on.</param>
        /// <param name="trackInfos">The tracking information to visualize.</param>
        /// <param name="printResult">Whether to print result to console.</param>
        /// <param name="isRGB">Whether the input image is RGB or BGR.</param>
        public virtual void Visualize(Mat image, TrackInfo[] trackInfos, bool printResult = false, bool isRGB = false)
        {
            ThrowIfDisposed();

            if (trackInfos == null)
                throw new ArgumentNullException(nameof(trackInfos));

            Visualize(image, trackInfos.AsSpan(), printResult, isRGB);
        }

#endif

    }
}
