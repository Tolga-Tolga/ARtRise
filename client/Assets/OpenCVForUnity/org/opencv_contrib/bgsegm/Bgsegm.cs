
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using OpenCVForUnity.VideoModule;

namespace OpenCVForUnity.BgsegmModule
{
    // C++: class Bgsegm


    public class Bgsegm
    {

        /// <summary>
        /// C++: enum LSBPCameraMotionCompensation (cv.bgsegm.LSBPCameraMotionCompensation)
        /// </summary>
        public const int LSBP_CAMERA_MOTION_COMPENSATION_NONE = 0;

        /// <summary>
        /// C++: enum LSBPCameraMotionCompensation (cv.bgsegm.LSBPCameraMotionCompensation)
        /// </summary>
        public const int LSBP_CAMERA_MOTION_COMPENSATION_LK = 0 + 1;


        //
        // C++:  Ptr_BackgroundSubtractorMOG cv::bgsegm::createBackgroundSubtractorMOG(int history = 200, int nmixtures = 5, double backgroundRatio = 0.7, double noiseSigma = 0)
        //

        /// <summary>
        ///  Creates mixture-of-gaussian background subtractor
        /// </summary>
        /// <param name="history">
        /// Length of the history.
        /// </param>
        /// <param name="nmixtures">
        /// Number of Gaussian mixtures.
        /// </param>
        /// <param name="backgroundRatio">
        /// Background ratio.
        /// </param>
        /// <param name="noiseSigma">
        /// Noise strength (standard deviation of the brightness or each color channel). 0
        ///  means some automatic value.
        /// </param>
        public static BackgroundSubtractorMOG createBackgroundSubtractorMOG(int history, int nmixtures, double backgroundRatio, double noiseSigma)
        {


            return BackgroundSubtractorMOG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorMOG_10(history, nmixtures, backgroundRatio, noiseSigma)));


        }

        /// <summary>
        ///  Creates mixture-of-gaussian background subtractor
        /// </summary>
        /// <param name="history">
        /// Length of the history.
        /// </param>
        /// <param name="nmixtures">
        /// Number of Gaussian mixtures.
        /// </param>
        /// <param name="backgroundRatio">
        /// Background ratio.
        /// </param>
        /// <param name="noiseSigma">
        /// Noise strength (standard deviation of the brightness or each color channel). 0
        ///  means some automatic value.
        /// </param>
        public static BackgroundSubtractorMOG createBackgroundSubtractorMOG(int history, int nmixtures, double backgroundRatio)
        {


            return BackgroundSubtractorMOG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorMOG_11(history, nmixtures, backgroundRatio)));


        }

        /// <summary>
        ///  Creates mixture-of-gaussian background subtractor
        /// </summary>
        /// <param name="history">
        /// Length of the history.
        /// </param>
        /// <param name="nmixtures">
        /// Number of Gaussian mixtures.
        /// </param>
        /// <param name="backgroundRatio">
        /// Background ratio.
        /// </param>
        /// <param name="noiseSigma">
        /// Noise strength (standard deviation of the brightness or each color channel). 0
        ///  means some automatic value.
        /// </param>
        public static BackgroundSubtractorMOG createBackgroundSubtractorMOG(int history, int nmixtures)
        {


            return BackgroundSubtractorMOG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorMOG_12(history, nmixtures)));


        }

        /// <summary>
        ///  Creates mixture-of-gaussian background subtractor
        /// </summary>
        /// <param name="history">
        /// Length of the history.
        /// </param>
        /// <param name="nmixtures">
        /// Number of Gaussian mixtures.
        /// </param>
        /// <param name="backgroundRatio">
        /// Background ratio.
        /// </param>
        /// <param name="noiseSigma">
        /// Noise strength (standard deviation of the brightness or each color channel). 0
        ///  means some automatic value.
        /// </param>
        public static BackgroundSubtractorMOG createBackgroundSubtractorMOG(int history)
        {


            return BackgroundSubtractorMOG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorMOG_13(history)));


        }

        /// <summary>
        ///  Creates mixture-of-gaussian background subtractor
        /// </summary>
        /// <param name="history">
        /// Length of the history.
        /// </param>
        /// <param name="nmixtures">
        /// Number of Gaussian mixtures.
        /// </param>
        /// <param name="backgroundRatio">
        /// Background ratio.
        /// </param>
        /// <param name="noiseSigma">
        /// Noise strength (standard deviation of the brightness or each color channel). 0
        ///  means some automatic value.
        /// </param>
        public static BackgroundSubtractorMOG createBackgroundSubtractorMOG()
        {


            return BackgroundSubtractorMOG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorMOG_14()));


        }


        //
        // C++:  Ptr_BackgroundSubtractorGMG cv::bgsegm::createBackgroundSubtractorGMG(int initializationFrames = 120, double decisionThreshold = 0.8)
        //

        /// <summary>
        ///  Creates a GMG Background Subtractor
        /// </summary>
        /// <param name="initializationFrames">
        /// number of frames used to initialize the background models.
        /// </param>
        /// <param name="decisionThreshold">
        /// Threshold value, above which it is marked foreground, else background.
        /// </param>
        public static BackgroundSubtractorGMG createBackgroundSubtractorGMG(int initializationFrames, double decisionThreshold)
        {


            return BackgroundSubtractorGMG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGMG_10(initializationFrames, decisionThreshold)));


        }

        /// <summary>
        ///  Creates a GMG Background Subtractor
        /// </summary>
        /// <param name="initializationFrames">
        /// number of frames used to initialize the background models.
        /// </param>
        /// <param name="decisionThreshold">
        /// Threshold value, above which it is marked foreground, else background.
        /// </param>
        public static BackgroundSubtractorGMG createBackgroundSubtractorGMG(int initializationFrames)
        {


            return BackgroundSubtractorGMG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGMG_11(initializationFrames)));


        }

        /// <summary>
        ///  Creates a GMG Background Subtractor
        /// </summary>
        /// <param name="initializationFrames">
        /// number of frames used to initialize the background models.
        /// </param>
        /// <param name="decisionThreshold">
        /// Threshold value, above which it is marked foreground, else background.
        /// </param>
        public static BackgroundSubtractorGMG createBackgroundSubtractorGMG()
        {


            return BackgroundSubtractorGMG.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGMG_12()));


        }


        //
        // C++:  Ptr_BackgroundSubtractorCNT cv::bgsegm::createBackgroundSubtractorCNT(int minPixelStability = 15, bool useHistory = true, int maxPixelStability = 15*60, bool isParallel = true)
        //

        /// <summary>
        ///  Creates a CNT Background Subtractor
        /// </summary>
        /// <param name="minPixelStability">
        /// number of frames with same pixel color to consider stable
        /// </param>
        /// <param name="useHistory">
        /// determines if we're giving a pixel credit for being stable for a long time
        /// </param>
        /// <param name="maxPixelStability">
        /// maximum allowed credit for a pixel in history
        /// </param>
        /// <param name="isParallel">
        /// determines if we're parallelizing the algorithm
        /// </param>
        public static BackgroundSubtractorCNT createBackgroundSubtractorCNT(int minPixelStability, bool useHistory, int maxPixelStability, bool isParallel)
        {


            return BackgroundSubtractorCNT.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorCNT_10(minPixelStability, useHistory, maxPixelStability, isParallel)));


        }

        /// <summary>
        ///  Creates a CNT Background Subtractor
        /// </summary>
        /// <param name="minPixelStability">
        /// number of frames with same pixel color to consider stable
        /// </param>
        /// <param name="useHistory">
        /// determines if we're giving a pixel credit for being stable for a long time
        /// </param>
        /// <param name="maxPixelStability">
        /// maximum allowed credit for a pixel in history
        /// </param>
        /// <param name="isParallel">
        /// determines if we're parallelizing the algorithm
        /// </param>
        public static BackgroundSubtractorCNT createBackgroundSubtractorCNT(int minPixelStability, bool useHistory, int maxPixelStability)
        {


            return BackgroundSubtractorCNT.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorCNT_11(minPixelStability, useHistory, maxPixelStability)));


        }

        /// <summary>
        ///  Creates a CNT Background Subtractor
        /// </summary>
        /// <param name="minPixelStability">
        /// number of frames with same pixel color to consider stable
        /// </param>
        /// <param name="useHistory">
        /// determines if we're giving a pixel credit for being stable for a long time
        /// </param>
        /// <param name="maxPixelStability">
        /// maximum allowed credit for a pixel in history
        /// </param>
        /// <param name="isParallel">
        /// determines if we're parallelizing the algorithm
        /// </param>
        public static BackgroundSubtractorCNT createBackgroundSubtractorCNT(int minPixelStability, bool useHistory)
        {


            return BackgroundSubtractorCNT.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorCNT_12(minPixelStability, useHistory)));


        }

        /// <summary>
        ///  Creates a CNT Background Subtractor
        /// </summary>
        /// <param name="minPixelStability">
        /// number of frames with same pixel color to consider stable
        /// </param>
        /// <param name="useHistory">
        /// determines if we're giving a pixel credit for being stable for a long time
        /// </param>
        /// <param name="maxPixelStability">
        /// maximum allowed credit for a pixel in history
        /// </param>
        /// <param name="isParallel">
        /// determines if we're parallelizing the algorithm
        /// </param>
        public static BackgroundSubtractorCNT createBackgroundSubtractorCNT(int minPixelStability)
        {


            return BackgroundSubtractorCNT.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorCNT_13(minPixelStability)));


        }

        /// <summary>
        ///  Creates a CNT Background Subtractor
        /// </summary>
        /// <param name="minPixelStability">
        /// number of frames with same pixel color to consider stable
        /// </param>
        /// <param name="useHistory">
        /// determines if we're giving a pixel credit for being stable for a long time
        /// </param>
        /// <param name="maxPixelStability">
        /// maximum allowed credit for a pixel in history
        /// </param>
        /// <param name="isParallel">
        /// determines if we're parallelizing the algorithm
        /// </param>
        public static BackgroundSubtractorCNT createBackgroundSubtractorCNT()
        {


            return BackgroundSubtractorCNT.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorCNT_14()));


        }


        //
        // C++:  Ptr_BackgroundSubtractorGSOC cv::bgsegm::createBackgroundSubtractorGSOC(int mc = LSBP_CAMERA_MOTION_COMPENSATION_NONE, int nSamples = 20, float replaceRate = 0.003f, float propagationRate = 0.01f, int hitsThreshold = 32, float alpha = 0.01f, float beta = 0.0022f, float blinkingSupressionDecay = 0.1f, float blinkingSupressionMultiplier = 0.1f, float noiseRemovalThresholdFacBG = 0.0004f, float noiseRemovalThresholdFacFG = 0.0008f)
        //

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay, float blinkingSupressionMultiplier, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_10(mc, nSamples, replaceRate, propagationRate, hitsThreshold, alpha, beta, blinkingSupressionDecay, blinkingSupressionMultiplier, noiseRemovalThresholdFacBG, noiseRemovalThresholdFacFG)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay, float blinkingSupressionMultiplier, float noiseRemovalThresholdFacBG)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_11(mc, nSamples, replaceRate, propagationRate, hitsThreshold, alpha, beta, blinkingSupressionDecay, blinkingSupressionMultiplier, noiseRemovalThresholdFacBG)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay, float blinkingSupressionMultiplier)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_12(mc, nSamples, replaceRate, propagationRate, hitsThreshold, alpha, beta, blinkingSupressionDecay, blinkingSupressionMultiplier)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_13(mc, nSamples, replaceRate, propagationRate, hitsThreshold, alpha, beta, blinkingSupressionDecay)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_14(mc, nSamples, replaceRate, propagationRate, hitsThreshold, alpha, beta)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_15(mc, nSamples, replaceRate, propagationRate, hitsThreshold, alpha)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_16(mc, nSamples, replaceRate, propagationRate, hitsThreshold)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate, float propagationRate)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_17(mc, nSamples, replaceRate, propagationRate)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples, float replaceRate)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_18(mc, nSamples, replaceRate)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc, int nSamples)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_19(mc, nSamples)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC(int mc)
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_110(mc)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorGSOC algorithm.
        /// </summary>
        /// <remarks>
        ///  Implementation of the different yet better algorithm which is called GSOC, as it was implemented during GSOC and was not originated from any paper.
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="replaceRate">
        /// Probability of replacing the old sample - how fast the model will update itself.
        /// </param>
        /// <param name="propagationRate">
        /// Probability of propagating to neighbors.
        /// </param>
        /// <param name="hitsThreshold">
        /// How many positives the sample must get before it will be considered as a possible replacement.
        /// </param>
        /// <param name="alpha">
        /// Scale coefficient for threshold.
        /// </param>
        /// <param name="beta">
        /// Bias coefficient for threshold.
        /// </param>
        /// <param name="blinkingSupressionDecay">
        /// Blinking supression decay factor.
        /// </param>
        /// <param name="blinkingSupressionMultiplier">
        /// Blinking supression multiplier.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        public static BackgroundSubtractorGSOC createBackgroundSubtractorGSOC()
        {


            return BackgroundSubtractorGSOC.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorGSOC_111()));


        }


        //
        // C++:  Ptr_BackgroundSubtractorLSBP cv::bgsegm::createBackgroundSubtractorLSBP(int mc = LSBP_CAMERA_MOTION_COMPENSATION_NONE, int nSamples = 20, int LSBPRadius = 16, float Tlower = 2.0f, float Tupper = 32.0f, float Tinc = 1.0f, float Tdec = 0.05f, float Rscale = 10.0f, float Rincdec = 0.005f, float noiseRemovalThresholdFacBG = 0.0004f, float noiseRemovalThresholdFacFG = 0.0008f, int LSBPthreshold = 8, int minCount = 2)
        //

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG, int LSBPthreshold, int minCount)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_10(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec, Rscale, Rincdec, noiseRemovalThresholdFacBG, noiseRemovalThresholdFacFG, LSBPthreshold, minCount)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG, int LSBPthreshold)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_11(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec, Rscale, Rincdec, noiseRemovalThresholdFacBG, noiseRemovalThresholdFacFG, LSBPthreshold)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_12(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec, Rscale, Rincdec, noiseRemovalThresholdFacBG, noiseRemovalThresholdFacFG)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_13(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec, Rscale, Rincdec, noiseRemovalThresholdFacBG)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_14(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec, Rscale, Rincdec)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_15(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec, Rscale)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_16(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc, Tdec)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_17(mc, nSamples, LSBPRadius, Tlower, Tupper, Tinc)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_18(mc, nSamples, LSBPRadius, Tlower, Tupper)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius, float Tlower)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_19(mc, nSamples, LSBPRadius, Tlower)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples, int LSBPRadius)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_110(mc, nSamples, LSBPRadius)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc, int nSamples)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_111(mc, nSamples)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP(int mc)
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_112(mc)));


        }

        /// <summary>
        ///  Creates an instance of BackgroundSubtractorLSBP algorithm.
        /// </summary>
        /// <remarks>
        ///  Background Subtraction using Local SVD Binary Pattern. More details about the algorithm can be found at @cite LGuo2016
        /// </remarks>
        /// <param name="mc">
        /// Whether to use camera motion compensation.
        /// </param>
        /// <param name="nSamples">
        /// Number of samples to maintain at each point of the frame.
        /// </param>
        /// <param name="LSBPRadius">
        /// LSBP descriptor radius.
        /// </param>
        /// <param name="Tlower">
        /// Lower bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tupper">
        /// Upper bound for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tinc">
        /// Increase step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Tdec">
        /// Decrease step for T-values. See @cite LGuo2016 for details.
        /// </param>
        /// <param name="Rscale">
        /// Scale coefficient for threshold values.
        /// </param>
        /// <param name="Rincdec">
        /// Increase/Decrease step for threshold values.
        /// </param>
        /// <param name="noiseRemovalThresholdFacBG">
        /// Strength of the noise removal for background points.
        /// </param>
        /// <param name="noiseRemovalThresholdFacFG">
        /// Strength of the noise removal for foreground points.
        /// </param>
        /// <param name="LSBPthreshold">
        /// Threshold for LSBP binary string.
        /// </param>
        /// <param name="minCount">
        /// Minimal number of matches for sample to be considered as foreground.
        /// </param>
        public static BackgroundSubtractorLSBP createBackgroundSubtractorLSBP()
        {


            return BackgroundSubtractorLSBP.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createBackgroundSubtractorLSBP_113()));


        }


        //
        // C++:  Ptr_SyntheticSequenceGenerator cv::bgsegm::createSyntheticSequenceGenerator(Mat background, Mat _object, double amplitude = 2.0, double wavelength = 20.0, double wavespeed = 0.2, double objspeed = 6.0)
        //

        /// <summary>
        ///  Creates an instance of SyntheticSequenceGenerator.
        /// </summary>
        /// <param name="background">
        /// Background image for object.
        /// </param>
        /// <param name="object">
        /// Object image which will move slowly over the background.
        /// </param>
        /// <param name="amplitude">
        /// Amplitude of wave distortion applied to background.
        /// </param>
        /// <param name="wavelength">
        /// Length of waves in distortion applied to background.
        /// </param>
        /// <param name="wavespeed">
        /// How fast waves will move.
        /// </param>
        /// <param name="objspeed">
        /// How fast object will fly over background.
        /// </param>
        public static SyntheticSequenceGenerator createSyntheticSequenceGenerator(Mat background, Mat _object, double amplitude, double wavelength, double wavespeed, double objspeed)
        {
            if (background != null) background.ThrowIfDisposed();
            if (_object != null) _object.ThrowIfDisposed();

            return SyntheticSequenceGenerator.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createSyntheticSequenceGenerator_10(background.nativeObj, _object.nativeObj, amplitude, wavelength, wavespeed, objspeed)));


        }

        /// <summary>
        ///  Creates an instance of SyntheticSequenceGenerator.
        /// </summary>
        /// <param name="background">
        /// Background image for object.
        /// </param>
        /// <param name="object">
        /// Object image which will move slowly over the background.
        /// </param>
        /// <param name="amplitude">
        /// Amplitude of wave distortion applied to background.
        /// </param>
        /// <param name="wavelength">
        /// Length of waves in distortion applied to background.
        /// </param>
        /// <param name="wavespeed">
        /// How fast waves will move.
        /// </param>
        /// <param name="objspeed">
        /// How fast object will fly over background.
        /// </param>
        public static SyntheticSequenceGenerator createSyntheticSequenceGenerator(Mat background, Mat _object, double amplitude, double wavelength, double wavespeed)
        {
            if (background != null) background.ThrowIfDisposed();
            if (_object != null) _object.ThrowIfDisposed();

            return SyntheticSequenceGenerator.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createSyntheticSequenceGenerator_11(background.nativeObj, _object.nativeObj, amplitude, wavelength, wavespeed)));


        }

        /// <summary>
        ///  Creates an instance of SyntheticSequenceGenerator.
        /// </summary>
        /// <param name="background">
        /// Background image for object.
        /// </param>
        /// <param name="object">
        /// Object image which will move slowly over the background.
        /// </param>
        /// <param name="amplitude">
        /// Amplitude of wave distortion applied to background.
        /// </param>
        /// <param name="wavelength">
        /// Length of waves in distortion applied to background.
        /// </param>
        /// <param name="wavespeed">
        /// How fast waves will move.
        /// </param>
        /// <param name="objspeed">
        /// How fast object will fly over background.
        /// </param>
        public static SyntheticSequenceGenerator createSyntheticSequenceGenerator(Mat background, Mat _object, double amplitude, double wavelength)
        {
            if (background != null) background.ThrowIfDisposed();
            if (_object != null) _object.ThrowIfDisposed();

            return SyntheticSequenceGenerator.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createSyntheticSequenceGenerator_12(background.nativeObj, _object.nativeObj, amplitude, wavelength)));


        }

        /// <summary>
        ///  Creates an instance of SyntheticSequenceGenerator.
        /// </summary>
        /// <param name="background">
        /// Background image for object.
        /// </param>
        /// <param name="object">
        /// Object image which will move slowly over the background.
        /// </param>
        /// <param name="amplitude">
        /// Amplitude of wave distortion applied to background.
        /// </param>
        /// <param name="wavelength">
        /// Length of waves in distortion applied to background.
        /// </param>
        /// <param name="wavespeed">
        /// How fast waves will move.
        /// </param>
        /// <param name="objspeed">
        /// How fast object will fly over background.
        /// </param>
        public static SyntheticSequenceGenerator createSyntheticSequenceGenerator(Mat background, Mat _object, double amplitude)
        {
            if (background != null) background.ThrowIfDisposed();
            if (_object != null) _object.ThrowIfDisposed();

            return SyntheticSequenceGenerator.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createSyntheticSequenceGenerator_13(background.nativeObj, _object.nativeObj, amplitude)));


        }

        /// <summary>
        ///  Creates an instance of SyntheticSequenceGenerator.
        /// </summary>
        /// <param name="background">
        /// Background image for object.
        /// </param>
        /// <param name="object">
        /// Object image which will move slowly over the background.
        /// </param>
        /// <param name="amplitude">
        /// Amplitude of wave distortion applied to background.
        /// </param>
        /// <param name="wavelength">
        /// Length of waves in distortion applied to background.
        /// </param>
        /// <param name="wavespeed">
        /// How fast waves will move.
        /// </param>
        /// <param name="objspeed">
        /// How fast object will fly over background.
        /// </param>
        public static SyntheticSequenceGenerator createSyntheticSequenceGenerator(Mat background, Mat _object)
        {
            if (background != null) background.ThrowIfDisposed();
            if (_object != null) _object.ThrowIfDisposed();

            return SyntheticSequenceGenerator.__fromPtr__(DisposableObject.ThrowIfNullIntPtr(bgsegm_Bgsegm_createSyntheticSequenceGenerator_14(background.nativeObj, _object.nativeObj)));


        }


#if (UNITY_IOS || UNITY_VISIONOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  Ptr_BackgroundSubtractorMOG cv::bgsegm::createBackgroundSubtractorMOG(int history = 200, int nmixtures = 5, double backgroundRatio = 0.7, double noiseSigma = 0)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorMOG_10(int history, int nmixtures, double backgroundRatio, double noiseSigma);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorMOG_11(int history, int nmixtures, double backgroundRatio);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorMOG_12(int history, int nmixtures);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorMOG_13(int history);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorMOG_14();

        // C++:  Ptr_BackgroundSubtractorGMG cv::bgsegm::createBackgroundSubtractorGMG(int initializationFrames = 120, double decisionThreshold = 0.8)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGMG_10(int initializationFrames, double decisionThreshold);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGMG_11(int initializationFrames);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGMG_12();

        // C++:  Ptr_BackgroundSubtractorCNT cv::bgsegm::createBackgroundSubtractorCNT(int minPixelStability = 15, bool useHistory = true, int maxPixelStability = 15*60, bool isParallel = true)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorCNT_10(int minPixelStability, [MarshalAs(UnmanagedType.U1)] bool useHistory, int maxPixelStability, [MarshalAs(UnmanagedType.U1)] bool isParallel);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorCNT_11(int minPixelStability, [MarshalAs(UnmanagedType.U1)] bool useHistory, int maxPixelStability);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorCNT_12(int minPixelStability, [MarshalAs(UnmanagedType.U1)] bool useHistory);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorCNT_13(int minPixelStability);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorCNT_14();

        // C++:  Ptr_BackgroundSubtractorGSOC cv::bgsegm::createBackgroundSubtractorGSOC(int mc = LSBP_CAMERA_MOTION_COMPENSATION_NONE, int nSamples = 20, float replaceRate = 0.003f, float propagationRate = 0.01f, int hitsThreshold = 32, float alpha = 0.01f, float beta = 0.0022f, float blinkingSupressionDecay = 0.1f, float blinkingSupressionMultiplier = 0.1f, float noiseRemovalThresholdFacBG = 0.0004f, float noiseRemovalThresholdFacFG = 0.0008f)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_10(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay, float blinkingSupressionMultiplier, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_11(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay, float blinkingSupressionMultiplier, float noiseRemovalThresholdFacBG);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_12(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay, float blinkingSupressionMultiplier);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_13(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta, float blinkingSupressionDecay);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_14(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha, float beta);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_15(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold, float alpha);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_16(int mc, int nSamples, float replaceRate, float propagationRate, int hitsThreshold);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_17(int mc, int nSamples, float replaceRate, float propagationRate);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_18(int mc, int nSamples, float replaceRate);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_19(int mc, int nSamples);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_110(int mc);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorGSOC_111();

        // C++:  Ptr_BackgroundSubtractorLSBP cv::bgsegm::createBackgroundSubtractorLSBP(int mc = LSBP_CAMERA_MOTION_COMPENSATION_NONE, int nSamples = 20, int LSBPRadius = 16, float Tlower = 2.0f, float Tupper = 32.0f, float Tinc = 1.0f, float Tdec = 0.05f, float Rscale = 10.0f, float Rincdec = 0.005f, float noiseRemovalThresholdFacBG = 0.0004f, float noiseRemovalThresholdFacFG = 0.0008f, int LSBPthreshold = 8, int minCount = 2)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_10(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG, int LSBPthreshold, int minCount);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_11(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG, int LSBPthreshold);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_12(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG, float noiseRemovalThresholdFacFG);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_13(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec, float noiseRemovalThresholdFacBG);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_14(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale, float Rincdec);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_15(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec, float Rscale);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_16(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc, float Tdec);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_17(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper, float Tinc);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_18(int mc, int nSamples, int LSBPRadius, float Tlower, float Tupper);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_19(int mc, int nSamples, int LSBPRadius, float Tlower);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_110(int mc, int nSamples, int LSBPRadius);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_111(int mc, int nSamples);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_112(int mc);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createBackgroundSubtractorLSBP_113();

        // C++:  Ptr_SyntheticSequenceGenerator cv::bgsegm::createSyntheticSequenceGenerator(Mat background, Mat _object, double amplitude = 2.0, double wavelength = 20.0, double wavespeed = 0.2, double objspeed = 6.0)
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createSyntheticSequenceGenerator_10(IntPtr background_nativeObj, IntPtr _object_nativeObj, double amplitude, double wavelength, double wavespeed, double objspeed);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createSyntheticSequenceGenerator_11(IntPtr background_nativeObj, IntPtr _object_nativeObj, double amplitude, double wavelength, double wavespeed);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createSyntheticSequenceGenerator_12(IntPtr background_nativeObj, IntPtr _object_nativeObj, double amplitude, double wavelength);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createSyntheticSequenceGenerator_13(IntPtr background_nativeObj, IntPtr _object_nativeObj, double amplitude);
        [DllImport(LIBNAME)]
        private static extern IntPtr bgsegm_Bgsegm_createSyntheticSequenceGenerator_14(IntPtr background_nativeObj, IntPtr _object_nativeObj);

    }
}
