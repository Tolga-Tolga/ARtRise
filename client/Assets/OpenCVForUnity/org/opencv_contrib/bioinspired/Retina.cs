
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;

namespace OpenCVForUnity.BioinspiredModule
{

    // C++: class Retina
    /// <summary>
    ///  class which allows the Gipsa/Listic Labs model to be used with OpenCV.
    /// </summary>
    /// <remarks>
    ///  This retina model allows spatio-temporal image processing (applied on still images, video sequences).
    ///  As a summary, these are the retina model properties:
    ///  - It applies a spectral whithening (mid-frequency details enhancement)
    ///  - high frequency spatio-temporal noise reduction
    ///  - low frequency luminance to be reduced (luminance range compression)
    ///  - local logarithmic luminance compression allows details to be enhanced in low light conditions
    ///  
    ///  USE : this model can be used basically for spatio-temporal video effects but also for :
    ///       _using the getParvo method output matrix : texture analysiswith enhanced signal to noise ratio and enhanced details robust against input images luminance ranges
    ///       _using the getMagno method output matrix : motion analysis also with the previously cited properties
    ///  
    ///  for more information, reer to the following papers :
    ///  Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
    ///  Vision: Images, Signals and Neural Networks: Models of Neural Processing in Visual Perception (Progress in Neural Processing),By: Jeanny Herault, ISBN: 9814273686. WAPI (Tower ID): 113266891.
    ///  
    ///  The retina filter includes the research contributions of phd/research collegues from which code has been redrawn by the author :
    ///  take a look at the retinacolor.hpp module to discover Brice Chaix de Lavarene color mosaicing/demosaicing and the reference paper:
    ///  B. Chaix de Lavarene, D. Alleysson, B. Durette, J. Herault (2007). "Efficient demosaicing through recursive filtering", IEEE International Conference on Image Processing ICIP 2007
    ///  take a look at imagelogpolprojection.hpp to discover retina spatial log sampling which originates from Barthelemy Durette phd with Jeanny Herault. A Retina / V1 cortex projection is also proposed and originates from Jeanny's discussions.
    ///  more informations in the above cited Jeanny Heraults's book.
    /// </remarks>
    public partial class Retina : Algorithm
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        bioinspired_Retina_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal Retina(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new Retina __fromPtr__(IntPtr addr) { return new Retina(addr); }

        //
        // C++:  Size cv::bioinspired::Retina::getInputSize()
        //

        /// <summary>
        ///  Retreive retina input buffer size
        /// </summary>
        /// <returns>
        ///  the retina input buffer size
        /// </returns>
        public Size getInputSize()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            bioinspired_Retina_getInputSize_10(nativeObj, tmpArray);
            Size retVal = new Size(tmpArray);

            return retVal;
        }


        //
        // C++:  Size cv::bioinspired::Retina::getOutputSize()
        //

        /// <summary>
        ///  Retreive retina output buffer size that can be different from the input if a spatial log
        ///      transformation is applied
        /// </summary>
        /// <returns>
        ///  the retina output buffer size
        /// </returns>
        public Size getOutputSize()
        {
            ThrowIfDisposed();

            double[] tmpArray = new double[2];
            bioinspired_Retina_getOutputSize_10(nativeObj, tmpArray);
            Size retVal = new Size(tmpArray);

            return retVal;
        }


        //
        // C++:  void cv::bioinspired::Retina::setup(String retinaParameterFile = "", bool applyDefaultSetupOnFailure = true)
        //

        /// <summary>
        ///  Try to open an XML retina parameters file to adjust current retina instance setup
        /// </summary>
        /// <remarks>
        ///      - if the xml file does not exist, then default setup is applied
        ///      - warning, Exceptions are thrown if read XML file is not valid
        /// </remarks>
        /// <param name="retinaParameterFile">
        /// the parameters filename
        /// </param>
        /// <param name="applyDefaultSetupOnFailure">
        /// set to true if an error must be thrown on error
        /// </param>
        /// <remarks>
        ///      You can retrieve the current parameters structure using the method Retina::getParameters and update
        ///      it before running method Retina::setup.
        /// </remarks>
        public void setup(string retinaParameterFile, bool applyDefaultSetupOnFailure)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setup_10(nativeObj, retinaParameterFile, applyDefaultSetupOnFailure);


        }

        /// <summary>
        ///  Try to open an XML retina parameters file to adjust current retina instance setup
        /// </summary>
        /// <remarks>
        ///      - if the xml file does not exist, then default setup is applied
        ///      - warning, Exceptions are thrown if read XML file is not valid
        /// </remarks>
        /// <param name="retinaParameterFile">
        /// the parameters filename
        /// </param>
        /// <param name="applyDefaultSetupOnFailure">
        /// set to true if an error must be thrown on error
        /// </param>
        /// <remarks>
        ///      You can retrieve the current parameters structure using the method Retina::getParameters and update
        ///      it before running method Retina::setup.
        /// </remarks>
        public void setup(string retinaParameterFile)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setup_11(nativeObj, retinaParameterFile);


        }

        /// <summary>
        ///  Try to open an XML retina parameters file to adjust current retina instance setup
        /// </summary>
        /// <remarks>
        ///      - if the xml file does not exist, then default setup is applied
        ///      - warning, Exceptions are thrown if read XML file is not valid
        /// </remarks>
        /// <param name="retinaParameterFile">
        /// the parameters filename
        /// </param>
        /// <param name="applyDefaultSetupOnFailure">
        /// set to true if an error must be thrown on error
        /// </param>
        /// <remarks>
        ///      You can retrieve the current parameters structure using the method Retina::getParameters and update
        ///      it before running method Retina::setup.
        /// </remarks>
        public void setup()
        {
            ThrowIfDisposed();

            bioinspired_Retina_setup_12(nativeObj);


        }


        //
        // C++:  String cv::bioinspired::Retina::printSetup()
        //

        /// <summary>
        ///  Outputs a string showing the used parameters setup
        /// </summary>
        /// <returns>
        ///  a string which contains formated parameters information
        /// </returns>
        public string printSetup()
        {
            ThrowIfDisposed();

            IntPtr strPtr = DisposableObject.ThrowIfNullIntPtr(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_printSetup_10(nativeObj)));
            string retVal = Marshal.PtrToStringAnsi(strPtr);
            UnityIntegration.OpenCVInternalUtils.FreeStringCopy(strPtr);

            return retVal;
        }


        //
        // C++:  void cv::bioinspired::Retina::write(String fs)
        //

        /// <summary>
        ///  Write xml/yml formated parameters information
        /// </summary>
        /// <param name="fs">
        /// the filename of the xml file that will be open and writen with formatted parameters
        ///      information
        /// </param>
        public void write(string fs)
        {
            ThrowIfDisposed();

            bioinspired_Retina_write_10(nativeObj, fs);


        }


        //
        // C++:  void cv::bioinspired::Retina::setupOPLandIPLParvoChannel(bool colorMode = true, bool normaliseOutput = true, float photoreceptorsLocalAdaptationSensitivity = 0.7f, float photoreceptorsTemporalConstant = 0.5f, float photoreceptorsSpatialConstant = 0.53f, float horizontalCellsGain = 0.f, float HcellsTemporalConstant = 1.f, float HcellsSpatialConstant = 7.f, float ganglionCellsSensitivity = 0.7f)
        //

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain, float HcellsTemporalConstant, float HcellsSpatialConstant, float ganglionCellsSensitivity)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_10(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity, photoreceptorsTemporalConstant, photoreceptorsSpatialConstant, horizontalCellsGain, HcellsTemporalConstant, HcellsSpatialConstant, ganglionCellsSensitivity);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain, float HcellsTemporalConstant, float HcellsSpatialConstant)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_11(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity, photoreceptorsTemporalConstant, photoreceptorsSpatialConstant, horizontalCellsGain, HcellsTemporalConstant, HcellsSpatialConstant);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain, float HcellsTemporalConstant)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_12(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity, photoreceptorsTemporalConstant, photoreceptorsSpatialConstant, horizontalCellsGain, HcellsTemporalConstant);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_13(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity, photoreceptorsTemporalConstant, photoreceptorsSpatialConstant, horizontalCellsGain);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_14(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity, photoreceptorsTemporalConstant, photoreceptorsSpatialConstant);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_15(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity, photoreceptorsTemporalConstant);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_16(nativeObj, colorMode, normaliseOutput, photoreceptorsLocalAdaptationSensitivity);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode, bool normaliseOutput)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_17(nativeObj, colorMode, normaliseOutput);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel(bool colorMode)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_18(nativeObj, colorMode);


        }

        /// <summary>
        ///  Setup the OPL and IPL parvo channels (see biologocal model)
        /// </summary>
        /// <remarks>
        ///      OPL is referred as Outer Plexiform Layer of the retina, it allows the spatio-temporal filtering
        ///      which withens the spectrum and reduces spatio-temporal noise while attenuating global luminance
        ///      (low frequency energy) IPL parvo is the OPL next processing stage, it refers to a part of the
        ///      Inner Plexiform layer of the retina, it allows high contours sensitivity in foveal vision. See
        ///      reference papers for more informations.
        ///      for more informations, please have a look at the paper Benoit A., Caplier A., Durette B., Herault, J., "USING HUMAN VISUAL SYSTEM MODELING FOR BIO-INSPIRED LOW LEVEL IMAGE PROCESSING", Elsevier, Computer Vision and Image Understanding 114 (2010), pp. 758-773, DOI: http://dx.doi.org/10.1016/j.cviu.2010.01.011
        /// </remarks>
        /// <param name="colorMode">
        /// specifies if (true) color is processed of not (false) to then processing gray
        ///      level image
        /// </param>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="photoreceptorsLocalAdaptationSensitivity">
        /// the photoreceptors sensitivity renage is 0-1
        ///      (more log compression effect when value increases)
        /// </param>
        /// <param name="photoreceptorsTemporalConstant">
        /// the time constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high temporal frequencies (noise or fast motion), unit is
        ///      frames, typical value is 1 frame
        /// </param>
        /// <param name="photoreceptorsSpatialConstant">
        /// the spatial constant of the first order low pass filter of
        ///      the photoreceptors, use it to cut high spatial frequencies (noise or thick contours), unit is
        ///      pixels, typical value is 1 pixel
        /// </param>
        /// <param name="horizontalCellsGain">
        /// gain of the horizontal cells network, if 0, then the mean value of
        ///      the output is zero, if the parameter is near 1, then, the luminance is not filtered and is
        ///      still reachable at the output, typicall value is 0
        /// </param>
        /// <param name="HcellsTemporalConstant">
        /// the time constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low temporal frequencies (local luminance variations), unit is
        ///      frames, typical value is 1 frame, as the photoreceptors
        /// </param>
        /// <param name="HcellsSpatialConstant">
        /// the spatial constant of the first order low pass filter of the
        ///      horizontal cells, use it to cut low spatial frequencies (local luminance), unit is pixels,
        ///      typical value is 5 pixel, this value is also used for local contrast computing when computing
        ///      the local contrast adaptation at the ganglion cells level (Inner Plexiform Layer parvocellular
        ///      channel model)
        /// </param>
        /// <param name="ganglionCellsSensitivity">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.7
        /// </param>
        public void setupOPLandIPLParvoChannel()
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupOPLandIPLParvoChannel_19(nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::setupIPLMagnoChannel(bool normaliseOutput = true, float parasolCells_beta = 0.f, float parasolCells_tau = 0.f, float parasolCells_k = 7.f, float amacrinCellsTemporalCutFrequency = 1.2f, float V0CompressionParameter = 0.95f, float localAdaptintegration_tau = 0.f, float localAdaptintegration_k = 7.f)
        //

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency, float V0CompressionParameter, float localAdaptintegration_tau, float localAdaptintegration_k)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_10(nativeObj, normaliseOutput, parasolCells_beta, parasolCells_tau, parasolCells_k, amacrinCellsTemporalCutFrequency, V0CompressionParameter, localAdaptintegration_tau, localAdaptintegration_k);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency, float V0CompressionParameter, float localAdaptintegration_tau)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_11(nativeObj, normaliseOutput, parasolCells_beta, parasolCells_tau, parasolCells_k, amacrinCellsTemporalCutFrequency, V0CompressionParameter, localAdaptintegration_tau);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency, float V0CompressionParameter)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_12(nativeObj, normaliseOutput, parasolCells_beta, parasolCells_tau, parasolCells_k, amacrinCellsTemporalCutFrequency, V0CompressionParameter);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_13(nativeObj, normaliseOutput, parasolCells_beta, parasolCells_tau, parasolCells_k, amacrinCellsTemporalCutFrequency);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_14(nativeObj, normaliseOutput, parasolCells_beta, parasolCells_tau, parasolCells_k);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta, float parasolCells_tau)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_15(nativeObj, normaliseOutput, parasolCells_beta, parasolCells_tau);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput, float parasolCells_beta)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_16(nativeObj, normaliseOutput, parasolCells_beta);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel(bool normaliseOutput)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_17(nativeObj, normaliseOutput);


        }

        /// <summary>
        ///  Set parameters values for the Inner Plexiform Layer (IPL) magnocellular channel
        /// </summary>
        /// <remarks>
        ///      this channel processes signals output from OPL processing stage in peripheral vision, it allows
        ///      motion information enhancement. It is decorrelated from the details channel. See reference
        ///      papers for more details.
        /// </remarks>
        /// <param name="normaliseOutput">
        /// specifies if (true) output is rescaled between 0 and 255 of not (false)
        /// </param>
        /// <param name="parasolCells_beta">
        /// the low pass filter gain used for local contrast adaptation at the
        ///      IPL level of the retina (for ganglion cells local adaptation), typical value is 0
        /// </param>
        /// <param name="parasolCells_tau">
        /// the low pass filter time constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is frame, typical
        ///      value is 0 (immediate response)
        /// </param>
        /// <param name="parasolCells_k">
        /// the low pass filter spatial constant used for local contrast adaptation
        ///      at the IPL level of the retina (for ganglion cells local adaptation), unit is pixels, typical
        ///      value is 5
        /// </param>
        /// <param name="amacrinCellsTemporalCutFrequency">
        /// the time constant of the first order high pass fiter of
        ///      the magnocellular way (motion information channel), unit is frames, typical value is 1.2
        /// </param>
        /// <param name="V0CompressionParameter">
        /// the compression strengh of the ganglion cells local adaptation
        ///      output, set a value between 0.6 and 1 for best results, a high value increases more the low
        ///      value sensitivity... and the output saturates faster, recommended value: 0.95
        /// </param>
        /// <param name="localAdaptintegration_tau">
        /// specifies the temporal constant of the low pas filter
        ///      involved in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        /// <param name="localAdaptintegration_k">
        /// specifies the spatial constant of the low pas filter involved
        ///      in the computation of the local "motion mean" for the local adaptation computation
        /// </param>
        public void setupIPLMagnoChannel()
        {
            ThrowIfDisposed();

            bioinspired_Retina_setupIPLMagnoChannel_18(nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::run(Mat inputImage)
        //

        /// <summary>
        ///  Method which allows retina to be applied on an input image,
        /// </summary>
        /// <remarks>
        ///      after run, encapsulated retina module is ready to deliver its outputs using dedicated
        ///      acccessors, see getParvo and getMagno methods
        /// </remarks>
        /// <param name="inputImage">
        /// the input Mat image to be processed, can be gray level or BGR coded in any
        ///      format (from 8bit to 16bits)
        /// </param>
        public void run(Mat inputImage)
        {
            ThrowIfDisposed();
            if (inputImage != null) inputImage.ThrowIfDisposed();

            bioinspired_Retina_run_10(nativeObj, inputImage.nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::applyFastToneMapping(Mat inputImage, Mat& outputToneMappedImage)
        //

        /// <summary>
        ///  Method which processes an image in the aim to correct its luminance correct
        ///      backlight problems, enhance details in shadows.
        /// </summary>
        /// <remarks>
        ///      This method is designed to perform High Dynamic Range image tone mapping (compress &gt;8bit/pixel
        ///      images to 8bit/pixel). This is a simplified version of the Retina Parvocellular model
        ///      (simplified version of the run/getParvo methods call) since it does not include the
        ///      spatio-temporal filter modelling the Outer Plexiform Layer of the retina that performs spectral
        ///      whitening and many other stuff. However, it works great for tone mapping and in a faster way.
        ///  
        ///      Check the demos and experiments section to see examples and the way to perform tone mapping
        ///      using the original retina model and the method.
        /// </remarks>
        /// <param name="inputImage">
        /// the input image to process (should be coded in float format : CV_32F,
        ///      CV_32FC1, CV_32F_C3, CV_32F_C4, the 4th channel won't be considered).
        /// </param>
        /// <param name="outputToneMappedImage">
        /// the output 8bit/channel tone mapped image (CV_8U or CV_8UC3 format).
        /// </param>
        public void applyFastToneMapping(Mat inputImage, Mat outputToneMappedImage)
        {
            ThrowIfDisposed();
            if (inputImage != null) inputImage.ThrowIfDisposed();
            if (outputToneMappedImage != null) outputToneMappedImage.ThrowIfDisposed();

            bioinspired_Retina_applyFastToneMapping_10(nativeObj, inputImage.nativeObj, outputToneMappedImage.nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::getParvo(Mat& retinaOutput_parvo)
        //

        /// <summary>
        ///  Accessor of the details channel of the retina (models foveal vision).
        /// </summary>
        /// <remarks>
        ///      Warning, getParvoRAW methods return buffers that are not rescaled within range [0;255] while
        ///      the non RAW method allows a normalized matrix to be retrieved.
        /// </remarks>
        /// <param name="retinaOutput_parvo">
        /// the output buffer (reallocated if necessary), format can be :
        ///      -   a Mat, this output is rescaled for standard 8bits image processing use in OpenCV
        ///      -   RAW methods actually return a 1D matrix (encoding is R1, R2, ... Rn, G1, G2, ..., Gn, B1,
        ///      B2, ...Bn), this output is the original retina filter model output, without any
        ///      quantification or rescaling.
        ///      @see getParvoRAW
        /// </param>
        public void getParvo(Mat retinaOutput_parvo)
        {
            ThrowIfDisposed();
            if (retinaOutput_parvo != null) retinaOutput_parvo.ThrowIfDisposed();

            bioinspired_Retina_getParvo_10(nativeObj, retinaOutput_parvo.nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::getParvoRAW(Mat& retinaOutput_parvo)
        //

        /// <summary>
        ///  Accessor of the details channel of the retina (models foveal vision).
        ///      @see getParvo
        /// </summary>
        public void getParvoRAW(Mat retinaOutput_parvo)
        {
            ThrowIfDisposed();
            if (retinaOutput_parvo != null) retinaOutput_parvo.ThrowIfDisposed();

            bioinspired_Retina_getParvoRAW_10(nativeObj, retinaOutput_parvo.nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::getMagno(Mat& retinaOutput_magno)
        //

        /// <summary>
        ///  Accessor of the motion channel of the retina (models peripheral vision).
        /// </summary>
        /// <remarks>
        ///      Warning, getMagnoRAW methods return buffers that are not rescaled within range [0;255] while
        ///      the non RAW method allows a normalized matrix to be retrieved.
        /// </remarks>
        /// <param name="retinaOutput_magno">
        /// the output buffer (reallocated if necessary), format can be :
        ///      -   a Mat, this output is rescaled for standard 8bits image processing use in OpenCV
        ///      -   RAW methods actually return a 1D matrix (encoding is M1, M2,... Mn), this output is the
        ///      original retina filter model output, without any quantification or rescaling.
        ///      @see getMagnoRAW
        /// </param>
        public void getMagno(Mat retinaOutput_magno)
        {
            ThrowIfDisposed();
            if (retinaOutput_magno != null) retinaOutput_magno.ThrowIfDisposed();

            bioinspired_Retina_getMagno_10(nativeObj, retinaOutput_magno.nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::getMagnoRAW(Mat& retinaOutput_magno)
        //

        /// <summary>
        ///  Accessor of the motion channel of the retina (models peripheral vision).
        ///      @see getMagno
        /// </summary>
        public void getMagnoRAW(Mat retinaOutput_magno)
        {
            ThrowIfDisposed();
            if (retinaOutput_magno != null) retinaOutput_magno.ThrowIfDisposed();

            bioinspired_Retina_getMagnoRAW_10(nativeObj, retinaOutput_magno.nativeObj);


        }


        //
        // C++:  Mat cv::bioinspired::Retina::getMagnoRAW()
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public Mat getMagnoRAW()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_getMagnoRAW_11(nativeObj)));


        }


        //
        // C++:  Mat cv::bioinspired::Retina::getParvoRAW()
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public Mat getParvoRAW()
        {
            ThrowIfDisposed();

            return new Mat(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_getParvoRAW_11(nativeObj)));


        }


        //
        // C++:  void cv::bioinspired::Retina::setColorSaturation(bool saturateColors = true, float colorSaturationValue = 4.0f)
        //

        /// <summary>
        ///  Activate color saturation as the final step of the color demultiplexing process -&gt; this
        ///      saturation is a sigmoide function applied to each channel of the demultiplexed image.
        /// </summary>
        /// <param name="saturateColors">
        /// boolean that activates color saturation (if true) or desactivate (if false)
        /// </param>
        /// <param name="colorSaturationValue">
        /// the saturation factor : a simple factor applied on the chrominance
        ///      buffers
        /// </param>
        public void setColorSaturation(bool saturateColors, float colorSaturationValue)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setColorSaturation_10(nativeObj, saturateColors, colorSaturationValue);


        }

        /// <summary>
        ///  Activate color saturation as the final step of the color demultiplexing process -&gt; this
        ///      saturation is a sigmoide function applied to each channel of the demultiplexed image.
        /// </summary>
        /// <param name="saturateColors">
        /// boolean that activates color saturation (if true) or desactivate (if false)
        /// </param>
        /// <param name="colorSaturationValue">
        /// the saturation factor : a simple factor applied on the chrominance
        ///      buffers
        /// </param>
        public void setColorSaturation(bool saturateColors)
        {
            ThrowIfDisposed();

            bioinspired_Retina_setColorSaturation_11(nativeObj, saturateColors);


        }

        /// <summary>
        ///  Activate color saturation as the final step of the color demultiplexing process -&gt; this
        ///      saturation is a sigmoide function applied to each channel of the demultiplexed image.
        /// </summary>
        /// <param name="saturateColors">
        /// boolean that activates color saturation (if true) or desactivate (if false)
        /// </param>
        /// <param name="colorSaturationValue">
        /// the saturation factor : a simple factor applied on the chrominance
        ///      buffers
        /// </param>
        public void setColorSaturation()
        {
            ThrowIfDisposed();

            bioinspired_Retina_setColorSaturation_12(nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::clearBuffers()
        //

        /// <summary>
        ///  Clears all retina buffers
        /// </summary>
        /// <remarks>
        ///      (equivalent to opening the eyes after a long period of eye close ;o) whatchout the temporal
        ///      transition occuring just after this method call.
        /// </remarks>
        public void clearBuffers()
        {
            ThrowIfDisposed();

            bioinspired_Retina_clearBuffers_10(nativeObj);


        }


        //
        // C++:  void cv::bioinspired::Retina::activateMovingContoursProcessing(bool activate)
        //

        /// <summary>
        ///  Activate/desactivate the Magnocellular pathway processing (motion information extraction), by
        ///      default, it is activated
        /// </summary>
        /// <param name="activate">
        /// true if Magnocellular output should be activated, false if not... if activated,
        ///      the Magnocellular output can be retrieved using the **getMagno** methods
        /// </param>
        public void activateMovingContoursProcessing(bool activate)
        {
            ThrowIfDisposed();

            bioinspired_Retina_activateMovingContoursProcessing_10(nativeObj, activate);


        }


        //
        // C++:  void cv::bioinspired::Retina::activateContoursProcessing(bool activate)
        //

        /// <summary>
        ///  Activate/desactivate the Parvocellular pathway processing (contours information extraction), by
        ///      default, it is activated
        /// </summary>
        /// <param name="activate">
        /// true if Parvocellular (contours information extraction) output should be
        ///      activated, false if not... if activated, the Parvocellular output can be retrieved using the
        ///      Retina::getParvo methods
        /// </param>
        public void activateContoursProcessing(bool activate)
        {
            ThrowIfDisposed();

            bioinspired_Retina_activateContoursProcessing_10(nativeObj, activate);


        }


        //
        // C++: static Ptr_Retina cv::bioinspired::Retina::create(Size inputSize)
        //

        /// <remarks>
        ///  @overload
        /// </remarks>
        public static Retina create(Size inputSize)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_10(inputSize.width, inputSize.height)));


        }


        //
        // C++: static Ptr_Retina cv::bioinspired::Retina::create(Size inputSize, bool colorMode, int colorSamplingMethod = RETINA_COLOR_BAYER, bool useRetinaLogSampling = false, float reductionFactor = 1.0f, float samplingStrength = 10.0f)
        //

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(Size inputSize, bool colorMode, int colorSamplingMethod, bool useRetinaLogSampling, float reductionFactor, float samplingStrength)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_11(inputSize.width, inputSize.height, colorMode, colorSamplingMethod, useRetinaLogSampling, reductionFactor, samplingStrength)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(Size inputSize, bool colorMode, int colorSamplingMethod, bool useRetinaLogSampling, float reductionFactor)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_12(inputSize.width, inputSize.height, colorMode, colorSamplingMethod, useRetinaLogSampling, reductionFactor)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(Size inputSize, bool colorMode, int colorSamplingMethod, bool useRetinaLogSampling)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_13(inputSize.width, inputSize.height, colorMode, colorSamplingMethod, useRetinaLogSampling)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(Size inputSize, bool colorMode, int colorSamplingMethod)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_14(inputSize.width, inputSize.height, colorMode, colorSamplingMethod)));


        }

        /// <summary>
        ///  Constructors from standardized interfaces : retreive a smart pointer to a Retina instance
        /// </summary>
        /// <param name="inputSize">
        /// the input frame size
        /// </param>
        /// <param name="colorMode">
        /// the chosen processing mode : with or without color processing
        /// </param>
        /// <param name="colorSamplingMethod">
        /// specifies which kind of color sampling will be used :
        ///      -   cv::bioinspired::RETINA_COLOR_RANDOM: each pixel position is either R, G or B in a random choice
        ///      -   cv::bioinspired::RETINA_COLOR_DIAGONAL: color sampling is RGBRGBRGB..., line 2 BRGBRGBRG..., line 3, GBRGBRGBR...
        ///      -   cv::bioinspired::RETINA_COLOR_BAYER: standard bayer sampling
        /// </param>
        /// <param name="useRetinaLogSampling">
        /// activate retina log sampling, if true, the 2 following parameters can
        ///      be used
        /// </param>
        /// <param name="reductionFactor">
        /// only usefull if param useRetinaLogSampling=true, specifies the reduction
        ///      factor of the output frame (as the center (fovea) is high resolution and corners can be
        ///      underscaled, then a reduction of the output is allowed without precision leak
        /// </param>
        /// <param name="samplingStrength">
        /// only usefull if param useRetinaLogSampling=true, specifies the strength of
        ///      the log scale that is applied
        /// </param>
        public static Retina create(Size inputSize, bool colorMode)
        {


            return Retina.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bioinspired_Retina_create_15(inputSize.width, inputSize.height, colorMode)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  Size cv::bioinspired::Retina::getInputSize()
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_getInputSize_10(IntPtr nativeObj, double[] retVal);

        // C++:  Size cv::bioinspired::Retina::getOutputSize()
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_getOutputSize_10(IntPtr nativeObj, double[] retVal);

        // C++:  void cv::bioinspired::Retina::setup(String retinaParameterFile = "", bool applyDefaultSetupOnFailure = true)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setup_10(IntPtr nativeObj, string retinaParameterFile, [MarshalAs(UnmanagedType.U1)] bool applyDefaultSetupOnFailure);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setup_11(IntPtr nativeObj, string retinaParameterFile);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setup_12(IntPtr nativeObj);

        // C++:  String cv::bioinspired::Retina::printSetup()
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_printSetup_10(IntPtr nativeObj);

        // C++:  void cv::bioinspired::Retina::write(String fs)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_write_10(IntPtr nativeObj, string fs);

        // C++:  void cv::bioinspired::Retina::setupOPLandIPLParvoChannel(bool colorMode = true, bool normaliseOutput = true, float photoreceptorsLocalAdaptationSensitivity = 0.7f, float photoreceptorsTemporalConstant = 0.5f, float photoreceptorsSpatialConstant = 0.53f, float horizontalCellsGain = 0.f, float HcellsTemporalConstant = 1.f, float HcellsSpatialConstant = 7.f, float ganglionCellsSensitivity = 0.7f)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain, float HcellsTemporalConstant, float HcellsSpatialConstant, float ganglionCellsSensitivity);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_11(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain, float HcellsTemporalConstant, float HcellsSpatialConstant);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_12(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain, float HcellsTemporalConstant);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_13(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant, float horizontalCellsGain);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_14(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant, float photoreceptorsSpatialConstant);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_15(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity, float photoreceptorsTemporalConstant);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_16(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float photoreceptorsLocalAdaptationSensitivity);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_17(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_18(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool colorMode);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupOPLandIPLParvoChannel_19(IntPtr nativeObj);

        // C++:  void cv::bioinspired::Retina::setupIPLMagnoChannel(bool normaliseOutput = true, float parasolCells_beta = 0.f, float parasolCells_tau = 0.f, float parasolCells_k = 7.f, float amacrinCellsTemporalCutFrequency = 1.2f, float V0CompressionParameter = 0.95f, float localAdaptintegration_tau = 0.f, float localAdaptintegration_k = 7.f)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency, float V0CompressionParameter, float localAdaptintegration_tau, float localAdaptintegration_k);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_11(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency, float V0CompressionParameter, float localAdaptintegration_tau);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_12(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency, float V0CompressionParameter);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_13(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k, float amacrinCellsTemporalCutFrequency);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_14(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta, float parasolCells_tau, float parasolCells_k);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_15(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta, float parasolCells_tau);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_16(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput, float parasolCells_beta);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_17(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool normaliseOutput);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setupIPLMagnoChannel_18(IntPtr nativeObj);

        // C++:  void cv::bioinspired::Retina::run(Mat inputImage)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_run_10(IntPtr nativeObj, IntPtr inputImage_nativeObj);

        // C++:  void cv::bioinspired::Retina::applyFastToneMapping(Mat inputImage, Mat& outputToneMappedImage)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_applyFastToneMapping_10(IntPtr nativeObj, IntPtr inputImage_nativeObj, IntPtr outputToneMappedImage_nativeObj);

        // C++:  void cv::bioinspired::Retina::getParvo(Mat& retinaOutput_parvo)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_getParvo_10(IntPtr nativeObj, IntPtr retinaOutput_parvo_nativeObj);

        // C++:  void cv::bioinspired::Retina::getParvoRAW(Mat& retinaOutput_parvo)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_getParvoRAW_10(IntPtr nativeObj, IntPtr retinaOutput_parvo_nativeObj);

        // C++:  void cv::bioinspired::Retina::getMagno(Mat& retinaOutput_magno)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_getMagno_10(IntPtr nativeObj, IntPtr retinaOutput_magno_nativeObj);

        // C++:  void cv::bioinspired::Retina::getMagnoRAW(Mat& retinaOutput_magno)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_getMagnoRAW_10(IntPtr nativeObj, IntPtr retinaOutput_magno_nativeObj);

        // C++:  Mat cv::bioinspired::Retina::getMagnoRAW()
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_getMagnoRAW_11(IntPtr nativeObj);

        // C++:  Mat cv::bioinspired::Retina::getParvoRAW()
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_getParvoRAW_11(IntPtr nativeObj);

        // C++:  void cv::bioinspired::Retina::setColorSaturation(bool saturateColors = true, float colorSaturationValue = 4.0f)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setColorSaturation_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool saturateColors, float colorSaturationValue);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setColorSaturation_11(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool saturateColors);
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_setColorSaturation_12(IntPtr nativeObj);

        // C++:  void cv::bioinspired::Retina::clearBuffers()
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_clearBuffers_10(IntPtr nativeObj);

        // C++:  void cv::bioinspired::Retina::activateMovingContoursProcessing(bool activate)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_activateMovingContoursProcessing_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool activate);

        // C++:  void cv::bioinspired::Retina::activateContoursProcessing(bool activate)
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_activateContoursProcessing_10(IntPtr nativeObj, [MarshalAs(UnmanagedType.U1)] bool activate);

        // C++: static Ptr_Retina cv::bioinspired::Retina::create(Size inputSize)
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_create_10(double inputSize_width, double inputSize_height);

        // C++: static Ptr_Retina cv::bioinspired::Retina::create(Size inputSize, bool colorMode, int colorSamplingMethod = RETINA_COLOR_BAYER, bool useRetinaLogSampling = false, float reductionFactor = 1.0f, float samplingStrength = 10.0f)
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_create_11(double inputSize_width, double inputSize_height, [MarshalAs(UnmanagedType.U1)] bool colorMode, int colorSamplingMethod, [MarshalAs(UnmanagedType.U1)] bool useRetinaLogSampling, float reductionFactor, float samplingStrength);
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_create_12(double inputSize_width, double inputSize_height, [MarshalAs(UnmanagedType.U1)] bool colorMode, int colorSamplingMethod, [MarshalAs(UnmanagedType.U1)] bool useRetinaLogSampling, float reductionFactor);
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_create_13(double inputSize_width, double inputSize_height, [MarshalAs(UnmanagedType.U1)] bool colorMode, int colorSamplingMethod, [MarshalAs(UnmanagedType.U1)] bool useRetinaLogSampling);
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_create_14(double inputSize_width, double inputSize_height, [MarshalAs(UnmanagedType.U1)] bool colorMode, int colorSamplingMethod);
        [DllImport(LIBNAME)]
        private static extern IntPtr bioinspired_Retina_create_15(double inputSize_width, double inputSize_height, [MarshalAs(UnmanagedType.U1)] bool colorMode);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void bioinspired_Retina_delete(IntPtr nativeObj);

    }
}
