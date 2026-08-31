using System;
using System.Collections.Generic;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// Data structure for salient point detectors.
    /// </summary>
    /// <remarks>
    /// The class instance stores a keypoint, which is a point feature found by one of many available 
    /// keypoint detectors, such as Harris corner detector, FAST, StarDetector, SURF, SIFT, etc.
    /// 
    /// The keypoint is characterized by the 2D position, scale (proportional to the diameter of the 
    /// neighborhood that needs to be taken into account), orientation, and some other parameters. The 
    /// keypoint neighborhood is then analyzed by another algorithm that builds a descriptor (usually 
    /// represented as a feature vector). Keypoints representing the same object in different images 
    /// can then be matched using KDTree or another method.
    /// 
    /// <para>
    /// C++: cv::KeyPoint Class Reference @see https://docs.opencv.org/4.10.0/d2/d29/classcv_1_1KeyPoint.html
    /// </para>
    /// </remarks>
    [Serializable]
    public partial class KeyPoint : IEquatable<KeyPoint>
    {

        /// <remarks>
        /// Coordinates of the keypoint.
        /// </remarks>
        public Point pt;
        /// <remarks>
        /// Diameter of the useful keypoint adjacent area.
        /// </remarks>
        public float size;
        /// <remarks>
        /// Computed orientation of the keypoint (-1 if not applicable).
        /// </remarks>
        public float angle;
        /// <remarks>
        /// The response, by which the strongest keypoints have been selected. Can
        /// be used for further sorting or subsampling.
        /// </remarks>
        public float response;
        /// <remarks>
        /// Octave (pyramid layer), from which the keypoint has been extracted.
        /// </remarks>
        public int octave;
        /// <remarks>
        /// Object ID, that can be used to cluster keypoints by an object they
        /// belong to.
        /// </remarks>
        public int class_id;

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        /// <param name="_angle">
        /// keypoint orientation
        /// </param>
        /// <param name="_response">
        /// keypoint detector response on the keypoint (that is, strength of the keypoint)
        /// </param>
        /// <param name="_octave">
        /// pyramid octave in which the keypoint has been detected
        /// </param>
        /// <param name="_class_id">
        /// object id
        /// </param>
        public KeyPoint(float x, float y, float _size, float _angle, float _response, int _octave, int _class_id)
        {
            pt = new Point(x, y);
            size = _size;
            angle = _angle;
            response = _response;
            octave = _octave;
            class_id = _class_id;
        }


        /// <remarks>
        /// default constructor
        /// </remarks>
        public KeyPoint()
                    : this(0, 0, 0, -1, 0, 0, -1)
        {

        }

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        /// <param name="_angle">
        /// keypoint orientation
        /// </param>
        /// <param name="_response">
        /// keypoint detector response on the keypoint (that is, strength of the keypoint)
        /// </param>
        /// <param name="_octave">
        /// pyramid octave in which the keypoint has been detected
        /// </param>
        public KeyPoint(float x, float y, float _size, float _angle, float _response, int _octave)
                    : this(x, y, _size, _angle, _response, _octave, -1)
        {

        }

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        /// <param name="_angle">
        /// keypoint orientation
        /// </param>
        /// <param name="_response">
        /// keypoint detector response on the keypoint (that is, strength of the keypoint)
        /// </param>
        public KeyPoint(float x, float y, float _size, float _angle, float _response)
                    : this(x, y, _size, _angle, _response, 0, -1)
        {

        }

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        /// <param name="_angle">
        /// keypoint orientation
        /// </param>
        public KeyPoint(float x, float y, float _size, float _angle)
                    : this(x, y, _size, _angle, 0, 0, -1)
        {

        }

        /// <param name="x">
        /// x-coordinate of the keypoint
        /// </param>
        /// <param name="y">
        /// y-coordinate of the keypoint
        /// </param>
        /// <param name="_size">
        /// keypoint diameter
        /// </param>
        public KeyPoint(float x, float y, float _size)
                    : this(x, y, _size, -1, 0, 0, -1)
        {

        }

        //@Override
        public override string ToString()
        {
            return "KeyPoint [pt=" + pt + ", size=" + size + ", angle=" + angle
                + ", response=" + response + ", octave=" + octave
                + ", class_id=" + class_id + "]";
        }

    }
}
