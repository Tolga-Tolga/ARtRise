using System;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OpenCVForUnity.UnityIntegration
{
    public static class DebugMat
    {

#pragma warning disable 0414
        private static GameObject opencvDebugMatCanvas = null;

        private static GameObject imageWindows = null;

        private static RawImage bigImage = null;

        private static InputField dumpInputField = null;

        private static ImageWindow imageWindowTemplate = null;

        private static Dictionary<string, ImageWindow> imageWindowsDictionary = null;
#pragma warning restore 0414

        /// <summary>
        /// Enum representing layout positions for displaying a group of images using the imshow method.
        /// </summary>
        public enum LayoutType
        {
            /// <summary>
            /// Represents the top layout position.
            /// </summary>
            TOP,

            /// <summary>
            /// Represents the left layout position.
            /// </summary>
            LEFT,

            /// <summary>
            /// Represents the right layout position.
            /// </summary>
            RIGHT,

            /// <summary>
            /// Represents the bottom layout position.
            /// </summary>
            BOTTOM
        }

        /// <summary>
        /// Enum representing the methods for dumping texture data.
        /// </summary>
        public enum DumpMode
        {
            /// <summary>
            /// Mode for retrieving texture data using the GetRawTextureData method.
            /// </summary>
            GetRawTextureDataMode,

            /// <summary>
            /// Mode for retrieving pixel data using the GetPixels32 method.
            /// </summary>
            GetPixels32Mode
        }

        /// <summary>
        /// Destroys the DebugMat GameObjects.
        /// </summary>
        /// <remarks>
        /// The clear method destroys GameObjects related to DebugMat from the hierarchy.
        /// </remarks>
        public static void clear()
        {
            //Debug.Log("clear()");
            if (opencvDebugMatCanvas != null)
            {
                GameObject.Destroy(opencvDebugMatCanvas);
            }
            opencvDebugMatCanvas = null;
            imageWindows = null;
            bigImage = null;
            dumpInputField = null;
            imageWindowTemplate = null;
            imageWindowsDictionary = null;
        }

        /// <summary>
        /// Checks if an EventSystem exists in the active scene.
        /// Outputs a warning if it does not exist.
        /// </summary>
        private static void CheckForEventSystem()
        {
            // Get all EventSystems in the active scene
            EventSystem[] eventSystems;

#if UNITY_2023_1_OR_NEWER
            eventSystems = UnityEngine.Object.FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
#else
            eventSystems = UnityEngine.Object.FindObjectsOfType<EventSystem>();
#endif

            // If there are no EventSystems, display a warning
            if (eventSystems.Length == 0)
            {
                Debug.LogWarning("When using DebugMat, an EventSystem must be added to the scene.");
            }
        }

        /// <summary>
        /// Sets up the DebugMat GameObject.
        /// </summary>
        /// <remarks>
        /// The setup method adds DebugMat-related GameObjects to the hierarchy.
        /// By calling this method, the initialization process that normally occurs when the imshow() method is called for the first time can be performed in advance.
        /// </remarks>
        /// <param name="type">
        /// The layout type for the setup.
        /// </param>
        public static void setup(LayoutType type = LayoutType.RIGHT)
        {
            if (opencvDebugMatCanvas == null)
            {
                //Debug.Log("setup()");

                CheckForEventSystem();

                opencvDebugMatCanvas = GameObject.Instantiate(Resources.Load<GameObject>("OpenCVDebugMatCanvas"));
                opencvDebugMatCanvas.name = opencvDebugMatCanvas.name.Substring(0, opencvDebugMatCanvas.name.Length - 7);

                switch (type)
                {
                    case LayoutType.TOP:
                        imageWindows = opencvDebugMatCanvas.transform.Find("VerticalPanel/TopImageWindows").gameObject;
                        break;
                    case LayoutType.LEFT:
                        imageWindows = opencvDebugMatCanvas.transform.Find("VerticalPanel/HorizontalPanel/LeftImageWindows").gameObject;
                        break;
                    case LayoutType.RIGHT:
                    default:
                        imageWindows = opencvDebugMatCanvas.transform.Find("VerticalPanel/HorizontalPanel/RightImageWindows").gameObject;
                        break;
                    case LayoutType.BOTTOM:
                        imageWindows = opencvDebugMatCanvas.transform.Find("VerticalPanel/BottomImageWindows").gameObject;
                        break;
                }

                imageWindows.SetActive(true);

                bigImage = opencvDebugMatCanvas.transform.Find("VerticalPanel/HorizontalPanel/MainPanel/BigImageWindow/BigImage").gameObject.GetComponent<RawImage>();

                dumpInputField = opencvDebugMatCanvas.transform.Find("VerticalPanel/HorizontalPanel/MainPanel/DumpInputField").gameObject.GetComponent<InputField>();

                imageWindowTemplate = Resources.Load<GameObject>("ImageWindow").GetComponent<ImageWindow>();

                imageWindowsDictionary = new Dictionary<string, ImageWindow>();
            }
        }

        /// <summary>
        /// Displays a Mat image.
        /// </summary>
        /// <remarks>
        /// This method displays a Mat image.
        ///
        /// If the image is 8-bit unsigned, it is displayed as is.
        /// If the image is 16-bit unsigned, the pixel values are divided by 256, mapping the value range [0, 255*256] to [0, 255].
        /// If the image is 32-bit or 64-bit floating-point, the pixel values are multiplied by 255, mapping the value range [0, 1] to [0, 255].
        /// 32-bit integer images are not supported due to ambiguity in the required transformation.
        /// If necessary, please preprocess the image to convert it to an 8-bit unsigned matrix.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to display the result of the mat.dump() method.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshow(string winname, Mat img, bool dump = false, CoreModule.Rect roi = null, string debugText = null, bool isDataSRGB = true)
        {

            CoreModule.Rect newRoi = null;
            if (roi != null)
            {
                if (roi.empty()) return;

                newRoi = CoreModule.Rect.intersect(new CoreModule.Rect(0, 0, img.width(), img.height()), roi);
                if (newRoi.empty()) return;
                //Debug.Log("newRoi " + newRoi);
            }

            setup();

            if (imageWindowsDictionary.Count >= 50) return;

            if (string.IsNullOrWhiteSpace(winname))
            {
                int i = 0;
                while (imageWindowsDictionary.ContainsKey("Mat_" + i))
                {
                    i++;
                }
                winname = "Mat_" + i;
            }

            if (!imageWindowsDictionary.ContainsKey(winname))
            {
                //Debug.Log("init "+winname);

                ImageWindow newImageWindow = GameObject.Instantiate(imageWindowTemplate.gameObject).GetComponent<ImageWindow>();
                newImageWindow.transform.SetParent(imageWindows.transform);
                newImageWindow.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                newImageWindow.name = winname;
                newImageWindow.bigImage = bigImage;
                newImageWindow.dumpInputField = dumpInputField;
                imageWindowsDictionary.Add(winname, newImageWindow);
            }
            ImageWindow imageWindow = imageWindowsDictionary[winname];

            imageWindow.setWinname(winname);
            imageWindow.setMat(img, dump, newRoi, debugText, isDataSRGB);
        }

        /// <summary>
        /// Displays a Mat image.
        /// </summary>
        /// <remarks>
        /// This method displays a Mat image.
        ///
        /// If the image is 8-bit unsigned, it is displayed as is.
        /// If the image is 16-bit unsigned, the pixel values are divided by 256, mapping the value range [0, 255*256] to [0, 255].
        /// If the image is 32-bit or 64-bit floating-point, the pixel values are multiplied by 255, mapping the value range [0, 1] to [0, 255].
        /// 32-bit integer images are not supported due to ambiguity in the required transformation.
        /// If necessary, please preprocess the image to convert it to an 8-bit unsigned matrix.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshow(string winname, Mat img, bool isDataSRGB)
        {
            imshow(winname, img, false, null, null, isDataSRGB);
        }


        /// <summary>
        /// Displays a Mat image.
        /// </summary>
        /// <remarks>
        /// This method displays a Mat image.
        ///
        /// If the image is 8-bit unsigned, it is displayed as is.
        /// If the image is 16-bit unsigned, the pixel values are divided by 256, mapping the value range [0, 255*256] to [0, 255].
        /// If the image is 32-bit or 64-bit floating-point, the pixel values are multiplied by 255, mapping the value range [0, 1] to [0, 255].
        /// 32-bit integer images are not supported due to ambiguity in the required transformation.
        /// If necessary, please preprocess the image to convert it to an 8-bit unsigned matrix.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to display the result of the mat.dump() method.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshow(string winname, Mat img, bool dump, in (int x, int y, int width, int height) roi, string debugText = null, bool isDataSRGB = true)
        {
            imshow(winname, img, dump, new CoreModule.Rect(roi.x, roi.y, roi.width, roi.height), debugText, isDataSRGB);
        }

        /// <summary>
        /// Displays a Mat image.
        /// </summary>
        /// <remarks>
        /// This method displays a Mat image.
        ///
        /// If the image is 8-bit unsigned, it is displayed as is.
        /// If the image is 16-bit unsigned, the pixel values are divided by 256, mapping the value range [0, 255*256] to [0, 255].
        /// If the image is 32-bit or 64-bit floating-point, the pixel values are multiplied by 255, mapping the value range [0, 1] to [0, 255].
        /// 32-bit integer images are not supported due to ambiguity in the required transformation.
        /// If necessary, please preprocess the image to convert it to an 8-bit unsigned matrix.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to display the result of the mat.dump() method.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshow(string winname, Mat img, bool dump, in Vec4i roi, string debugText = null, bool isDataSRGB = true)
        {
            imshow(winname, img, dump, (CoreModule.Rect)roi, debugText, isDataSRGB);
        }

        /// <summary>
        /// Displays debug text.
        /// </summary>
        /// <remarks>
        /// The imshow method displays the provided debug text in the InputField.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window.
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        public static void imshow(string winname, string debugText)
        {
            imshow(winname, (Mat)null, false, null, debugText);
        }

        /// <summary>
        /// Displays a Texture image.
        /// </summary>
        /// <remarks>
        /// This method displays a Texture image.
        ///
        /// The supported texture classes are Texture2D and RenderTexture.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method to initialize before invoking the imshow method for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Texture to be displayed.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to dump the pixel data of the Texture.
        /// The pixel data will be reordered to match the format returned by the mat.dump() method, with the origin located at the top-right corner.
        /// </param>
        /// <param name="dumpMode">
        /// Specifies the dump mode (default is GetRawTextureDataMode).
        /// In GetRawTextureDataMode, if the TextureFormat is a compressed format, the raw byte data from the GetRawTextureData() method will be dumped as-is.
        /// Otherwise, the data will be reordered and dumped as pixel data with the origin in the top-right corner.
        /// In GetTexture32Mode, the Color32 array obtained via the GetPixels32() method will be reordered and dumped as pixel data with the origin in the top-right corner.
        /// If RenderTexture is specified for img, dumpMode is ignored and the data is always dumped in 4 channels per pixel.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        public static void imshow(string winname, Texture img, bool dump = false, DumpMode dumpMode = DumpMode.GetRawTextureDataMode, CoreModule.Rect roi = null, string debugText = null)
        {
            if (!(img is Texture2D || img is RenderTexture))
                throw new ArgumentException("Unsupported Texture Class. Expected Texture2D or RenderTexture.");

            CoreModule.Rect newRoi = null;
            if (roi != null)
            {
                if (roi.empty()) return;

                newRoi = CoreModule.Rect.intersect(new CoreModule.Rect(0, 0, img.width, img.height), roi);
                if (newRoi.empty()) return;
                //Debug.Log("newRoi " + newRoi);
            }

            setup();

            if (imageWindowsDictionary.Count >= 50) return;

            if (string.IsNullOrWhiteSpace(winname))
            {
                int i = 0;
                while (imageWindowsDictionary.ContainsKey("Texture_" + i))
                {
                    i++;
                }
                winname = "Texture_" + i;
            }

            if (!imageWindowsDictionary.ContainsKey(winname))
            {
                //Debug.Log("init "+winname);

                ImageWindow newImageWindow = GameObject.Instantiate(imageWindowTemplate.gameObject).GetComponent<ImageWindow>();
                newImageWindow.transform.SetParent(imageWindows.transform);
                newImageWindow.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                newImageWindow.name = winname;
                newImageWindow.bigImage = bigImage;
                newImageWindow.dumpInputField = dumpInputField;
                imageWindowsDictionary.Add(winname, newImageWindow);
            }
            ImageWindow imageWindow = imageWindowsDictionary[winname];

            imageWindow.setWinname(winname);
            imageWindow.setTexture(img, dump, dumpMode, newRoi, debugText);
        }

        /// <summary>
        /// Displays a Texture image.
        /// </summary>
        /// <remarks>
        /// This method displays a Texture image.
        ///
        /// The supported texture classes are Texture2D and RenderTexture.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method to initialize before invoking the imshow method for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Texture to be displayed.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to dump the pixel data of the Texture.
        /// The pixel data will be reordered to match the format returned by the mat.dump() method, with the origin located at the top-right corner.
        /// </param>
        /// <param name="dumpMode">
        /// Specifies the dump mode (default is GetRawTextureDataMode).
        /// In GetRawTextureDataMode, if the TextureFormat is a compressed format, the raw byte data from the GetRawTextureData() method will be dumped as-is.
        /// Otherwise, the data will be reordered and dumped as pixel data with the origin in the top-right corner.
        /// In GetTexture32Mode, the Color32 array obtained via the GetPixels32() method will be reordered and dumped as pixel data with the origin in the top-right corner.
        /// If RenderTexture is specified for img, dumpMode is ignored and the data is always dumped in 4 channels per pixel.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        public static void imshow(string winname, Texture img, bool dump, DumpMode dumpMode, in (int x, int y, int width, int height) roi, string debugText = null)
        {
            imshow(winname, img, dump, dumpMode, new CoreModule.Rect(roi.x, roi.y, roi.width, roi.height), debugText);
        }

        /// <summary>
        /// Displays a Texture image.
        /// </summary>
        /// <remarks>
        /// This method displays a Texture image.
        ///
        /// The supported texture classes are Texture2D and RenderTexture.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method to initialize before invoking the imshow method for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Texture to be displayed.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to dump the pixel data of the Texture.
        /// The pixel data will be reordered to match the format returned by the mat.dump() method, with the origin located at the top-right corner.
        /// </param>
        /// <param name="dumpMode">
        /// Specifies the dump mode (default is GetRawTextureDataMode).
        /// In GetRawTextureDataMode, if the TextureFormat is a compressed format, the raw byte data from the GetRawTextureData() method will be dumped as-is.
        /// Otherwise, the data will be reordered and dumped as pixel data with the origin in the top-right corner.
        /// In GetTexture32Mode, the Color32 array obtained via the GetPixels32() method will be reordered and dumped as pixel data with the origin in the top-right corner.
        /// If RenderTexture is specified for img, dumpMode is ignored and the data is always dumped in 4 channels per pixel.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        public static void imshow(string winname, Texture img, bool dump, DumpMode dumpMode, in Vec4i roi, string debugText = null)
        {
            imshow(winname, img, dump, dumpMode, (CoreModule.Rect)roi, debugText);
        }

        /// <summary>
        /// Displays a Mat image generated by Dnn.blobFromImage.
        /// </summary>
        /// <remarks>
        /// This method is specifically designed to display a Mat generated by Dnn.blobFromImage,
        /// which typically has an NCHW shape of [1, 3, h, w].
        ///
        /// The input Mat is converted from NCHW to HWC format before being displayed.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="scalefactor">
        /// The scaling factor to apply to the Mat values to normalize floating-point CV_Type values to the [0-1] range.
        /// Default is 1.0, which leaves values unchanged.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to display the result of the mat.dump() method.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshowDNNBlob(string winname, Mat img, double scalefactor = 1.0, bool dump = false, CoreModule.Rect roi = null, string debugText = null, bool isDataSRGB = true)
        {
            if (img == null)
            {
                imshow(winname, (Mat)null, false, null, debugText);
                return;
            }

            if (img.channels() != 1)
                throw new ArgumentException("Unsupported Mat. Expected single-channel Mat.");

            if ((img.dims() != 3 || img.size(0) != 3) && (img.dims() != 4 || img.size(0) != 1 || img.size(1) != 3))
                throw new ArgumentException("Unsupported Mat. Expected NCHW shape [1, 3, h, w] or [3, h, w].");

            Mat chwMat = (img.dims() == 4)
                ? img.reshape(1, new int[] { img.size(1), img.size(2), img.size(3) })
                : img;

            int height = chwMat.size(1);
            int width = chwMat.size(2);
            Mat hwcMat = new Mat(new int[] { height, width, 3 }, chwMat.type());
            Core.transposeND(chwMat, new MatOfInt(1, 2, 0), hwcMat);

            Mat hwMat = hwcMat.reshape(3, new int[] { height, width });

            if (scalefactor != 1.0)
            {
                Core.multiply(hwMat, (scalefactor, scalefactor, scalefactor, scalefactor), hwMat);
            }

            imshow(winname, hwMat, dump, roi, debugText, isDataSRGB);

            hwMat.Dispose();
            hwcMat.Dispose();
        }

        /// <summary>
        /// Displays a Mat image generated by Dnn.blobFromImage.
        /// </summary>
        /// <remarks>
        /// This method is specifically designed to display a Mat generated by Dnn.blobFromImage,
        /// which typically has an NCHW shape of [1, 3, h, w].
        ///
        /// The input Mat is converted from NCHW to HWC format before being displayed.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshowDNNBlob(string winname, Mat img, bool isDataSRGB)
        {
            imshowDNNBlob(winname, img, 1.0, false, null, null, isDataSRGB);
        }

        /// <summary>
        /// Displays a Mat image generated by Dnn.blobFromImage.
        /// </summary>
        /// <remarks>
        /// This method is specifically designed to display a Mat generated by Dnn.blobFromImage,
        /// which typically has an NCHW shape of [1, 3, h, w].
        ///
        /// The input Mat is converted from NCHW to HWC format before being displayed.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="scalefactor">
        /// The scaling factor to apply to the Mat values to normalize floating-point CV_Type values to the [0-1] range.
        /// Default is 1.0, which leaves values unchanged.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to display the result of the mat.dump() method.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshowDNNBlob(string winname, Mat img, double scalefactor, bool dump, in (int x, int y, int width, int height) roi, string debugText = null, bool isDataSRGB = true)
        {
            imshowDNNBlob(winname, img, scalefactor, dump, new CoreModule.Rect(roi.x, roi.y, roi.width, roi.height), debugText, isDataSRGB);
        }

        /// <summary>
        /// Displays a Mat image generated by Dnn.blobFromImage.
        /// </summary>
        /// <remarks>
        /// This method is specifically designed to display a Mat generated by Dnn.blobFromImage,
        /// which typically has an NCHW shape of [1, 3, h, w].
        ///
        /// The input Mat is converted from NCHW to HWC format before being displayed.
        /// A new window will not be created if the number of image windows exceeds 50.
        /// Additionally, if a LayoutType is specified, please call the setup method() before the imshow method() is invoked for the first time.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window. If the same name is already added, it will be overwritten.
        /// </param>
        /// <param name="img">
        /// The Mat image to be displayed.
        /// </param>
        /// <param name="scalefactor">
        /// The scaling factor to apply to the Mat values to normalize floating-point CV_Type values to the [0-1] range.
        /// Default is 1.0, which leaves values unchanged.
        /// </param>
        /// <param name="dump">
        /// Specifies whether to display the result of the mat.dump() method.
        /// </param>
        /// <param name="roi">
        /// The part of the image to be displayed (extracts the intersecting area of img and roi).
        /// </param>
        /// <param name="debugText">
        /// The debug text to be displayed in the InputField.
        /// </param>
        /// <param name="isDataSRGB">
        /// The isDataSRGB property of the Texture2D used when displaying the Mat image.
        /// If true, the data is assumed to be in sRGB color space.
        /// </param>
        public static void imshowDNNBlob(string winname, Mat img, double scalefactor, bool dump, in Vec4i roi, string debugText = null, bool isDataSRGB = true)
        {
            imshowDNNBlob(winname, img, scalefactor, dump, (CoreModule.Rect)roi, debugText, isDataSRGB);
        }

        /// <summary>
        /// Destroys the specified window.
        /// </summary>
        /// <remarks>
        /// The destroyWindow method removes the window identified by the provided name from the display.
        /// </remarks>
        /// <param name="winname">
        /// The name of the window to be destroyed.
        /// </param>
        public static void destroyWindow(string winname)
        {
            if (winname == null) return;

            if (imageWindowsDictionary == null) return;

            if (imageWindowsDictionary.ContainsKey(winname))
            {
                GameObject.Destroy(imageWindowsDictionary[winname].gameObject);
                imageWindowsDictionary.Remove(winname);
            }
        }

        /// <summary>
        /// Destroys all opened windows.
        /// </summary>
        /// <remarks>
        /// The destroyAllWindows method removes all windows that are currently open.
        /// This is useful for cleaning up the display when no longer needed.
        /// </remarks>
        public static void destroyAllWindows()
        {
            if (imageWindowsDictionary == null) return;

            foreach (var value in imageWindowsDictionary.Values)
            {
                GameObject.Destroy(value.gameObject);
            }
            imageWindowsDictionary.Clear();
        }
    }
}
