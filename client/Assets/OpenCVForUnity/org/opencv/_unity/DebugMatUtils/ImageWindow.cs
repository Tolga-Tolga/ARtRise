using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.UI;

#if OPENCV_DONT_USE_UNSAFE_CODE
using System.Runtime.InteropServices;
#endif

namespace OpenCVForUnity.UnityUtils
{
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.ImageWindow class instead.")]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(RawImage))]
    [RequireComponent(typeof(LayoutElement))]
    public class ImageWindow : MonoBehaviour
    {
        public RawImage bigImage;

        public InputField dumpInputField;

        string winname;

        Mat mat;

        Texture2D texture2D;

        Texture texture;

        GraphicsBuffer graphicsBuffer;

        string dumpText;

        RectTransform rectTracsform;

        RawImage rawImage;

        LayoutElement layoutElement;

        AspectRatioFitter aspectRatioFilter;

        void Awake()
        {
            //Debug.Log("Awake " + this.name);

            rectTracsform = this.GetComponent<RectTransform>();
            rawImage = this.GetComponent<RawImage>();
            layoutElement = this.GetComponent<LayoutElement>();

            //texture2D = new Texture2D(100, 100, TextureFormat.RGBA32, false);
        }

        void OnDestroy()
        {
            //Debug.Log("OnDestroy " + this.name);

            if (mat != null) mat.Dispose();

            // Destroy the texture and set it to null
            if (texture2D != null)
            {
                if (bigImage.texture == texture2D)
                {
                    bigImage.transform.parent.gameObject.SetActive(false);
                    dumpInputField.gameObject.SetActive(false);
                }

                Texture2D.Destroy(texture2D);
                texture2D = null;
            }

            if (texture != null)
            {
                if (bigImage.texture == texture)
                {
                    bigImage.transform.parent.gameObject.SetActive(false);
                    dumpInputField.gameObject.SetActive(false);
                }

                Texture.Destroy(texture);
                texture2D = null;
            }

            if (graphicsBuffer != null)
            {
                graphicsBuffer.Dispose();
                graphicsBuffer = null;
            }

        }

        public void setWinname(string winname)
        {
            this.winname = winname;
        }

        public void setMat(Mat img, bool dump, CoreModule.Rect roi, string debugText, bool isDataSRGB)
        {
            if (texture2D != null && texture2D.isDataSRGB != isDataSRGB)
            {
                Texture2D.Destroy(texture2D);
                texture2D = null;
            }
            if (texture2D == null) texture2D = new Texture2D(100, 100, TextureFormat.RGBA32, false, !isDataSRGB);

            dumpText = "";
            dumpText += "<b>" + winname + "</b>";

            if (img == null || img.empty())
            {
                if (mat != null) mat.Dispose();

                mat = new Mat(100, 100, CvType.CV_8UC4, new Scalar(0, 0, 0, 255));

                Imgproc.line(mat, new Point(0, 0), new Point(mat.width(), mat.height()), new Scalar(0, 255, 0, 255));
                Imgproc.line(mat, new Point(mat.width(), 0), new Point(0, mat.height()), new Scalar(0, 255, 0, 255));
            }
            else
            {
                //Debug.Log(winname + " img.ToString() " + img.ToString());

                Mat roiImg;
                if (roi != null)
                {
                    roiImg = new Mat(img, roi);
                }
                else
                {
                    roiImg = img;
                }
                //Debug.Log(winname + " roiImg.ToString() " + roiImg.ToString());

                //https://github.com/opencv/opencv/blob/4.8.0/modules/highgui/src/precomp.hpp#L154-L175
                int imgDepth = roiImg.depth();
                int imgChannels = roiImg.channels();
                int imgDims = roiImg.dims();
                //Debug.Log("imgDepth "+ imgDepth+" imgChannels "+imgChannels +" imgDims "+imgDims);

                if (imgDepth != CvType.CV_16F && imgDepth != CvType.CV_32S && imgChannels != 2 && imgChannels <= 4 && imgDims < 3)
                {
                    if (mat != null && roiImg.width() == mat.width() && roiImg.height() == mat.height() && roiImg.type() == mat.type())
                    {
                        //Debug.Log(winname + " resuse mat.ToString() " + mat.ToString());
                        roiImg.copyTo(mat);
                    }
                    else
                    {
                        if (mat != null) mat.Dispose();

                        if (imgDepth == CvType.CV_8U)
                        {
                            mat = roiImg.clone();
                        }
                        else
                        {
                            mat = new Mat();
                            switch (imgDepth)
                            {
                                case CvType.CV_8S:
                                    //Core.convertScaleAbs(roiImg, mat, 1, 127);
                                    roiImg.convertTo(mat, CvType.CV_8U, 1, 127);
                                    break;
                                case CvType.CV_16S:
                                    //Core.convertScaleAbs(roiImg, mat, 1 / 255.0, 127);
                                    roiImg.convertTo(mat, CvType.CV_8U, 1 / 255.0, 127);
                                    break;
                                case CvType.CV_16U:
                                    //Core.convertScaleAbs(roiImg, mat, 1 / 255.0);
                                    roiImg.convertTo(mat, CvType.CV_8U, 1 / 255.0);
                                    break;
                                case CvType.CV_32F:
                                case CvType.CV_64F: // assuming image has values in range [0, 1)
                                    roiImg.convertTo(mat, CvType.CV_8U, 255.0, 0.0);
                                    break;
                            }
                        }
                    }
                }
                else
                {

                    //if (mat != null && roiImg.width() == mat.width() && roiImg.height() == mat.height())
                    //{
                    //    Debug.Log(winname + " blank resuse mat.ToString() " + mat.ToString());
                    //    mat.setTo(new Scalar(255, 255, 255, 255));
                    //}
                    //else
                    //{
                    if (mat != null) mat.Dispose();
                    //Debug.Log(winname + " blank new mat");
                    mat = new Mat(100, 100, CvType.CV_8UC4, new Scalar(255, 255, 255, 255));
                    //}
                    Imgproc.line(mat, new Point(0, 0), new Point(mat.width(), mat.height()), new Scalar(255, 0, 0, 255));
                    Imgproc.line(mat, new Point(mat.width(), 0), new Point(0, mat.height()), new Scalar(255, 0, 0, 255));

                    dumpText += "\nIf only imgDepth != CvType.CV_16F && imgDepth != CvType.CV_32S && imgChannels != 2 && imgChannels <= 4 && imgDims < 3, Mat image preview is supported. If the dump flag is enabled, the Mat value can be dumped.";

                }

                dumpText += "\n" + img.ToString();
                if (roi != null) dumpText += "\n<b>roi</b> : " + roi.ToString();
                if (dump)
                {
                    if (roiImg.width() * roiImg.height() <= 400)
                    {
                        dumpText += "\n<b>dump</b> : " + roiImg.dump();
                    }
                    else
                    {
                        dumpText += "\n<b>dump</b> : Mat larger than 400 pixels cannot be dumped, so specify roi in the imshow method.";
                    }
                }

                if (roi != null) roiImg.Dispose();
            }
            if (!string.IsNullOrEmpty(debugText)) dumpText += "\n<b>debugText</b> : " + debugText;

            //Debug.Log(winname + " mat.ToString() " + mat.ToString());

            if (mat.width() != texture2D.width || mat.height() != texture2D.height)
            {

#if UNITY_2021_1_OR_NEWER
                texture2D.Reinitialize(mat.width(), mat.height());
#else
                texture.Resize(mat.width(), mat.height());
#endif
            }

            if (bigImage.texture == texture2D)
            {
                aspectRatioFilter.aspectRatio = (float)texture2D.width / (float)texture2D.height;
                dumpInputField.text = dumpText;
            }

            Utils.matToTexture2D(mat, texture2D);

            rawImage.texture = texture2D;

            setNewDimension(mat.width(), mat.height());
        }

        public void setTexture(Texture img, bool dump, DebugMatUtils.DumpMode dumpMode, CoreModule.Rect roi, string debugText)
        {

            dumpText = "";
            dumpText += "<b>" + winname + "</b>";

            if (img == null)
            {
                if (mat != null) mat.Dispose();

                mat = new Mat(100, 100, CvType.CV_8UC4, new Scalar(0, 0, 0, 255));

                Imgproc.line(mat, new Point(0, 0), new Point(mat.width(), mat.height()), new Scalar(0, 255, 0, 255));
                Imgproc.line(mat, new Point(mat.width(), 0), new Point(0, mat.height()), new Scalar(0, 255, 0, 255));

                if (!string.IsNullOrEmpty(debugText)) dumpText += "\n<b>debugText</b> : " + debugText;

                //Debug.Log(winname + " mat.ToString() " + mat.ToString());

                if (mat.width() != texture2D.width || mat.height() != texture2D.height)
                {

#if UNITY_2021_1_OR_NEWER
                    texture2D.Reinitialize(mat.width(), mat.height());
#else
                    texture.Resize(mat.width(), mat.height());
#endif

                    if (bigImage.texture == texture2D)
                    {
                        aspectRatioFilter.aspectRatio = (float)texture2D.width / (float)texture2D.height;
                        dumpInputField.text = dumpText;
                    }
                }

                Utils.matToTexture2D(mat, texture2D);

                rawImage.texture = texture2D;

                setNewDimension(mat.width(), mat.height());
            }
            else
            {
                if (texture != null && img.graphicsFormat == texture.graphicsFormat && img.mipmapCount == texture.mipmapCount &&
                    (roi != null ? (img.width == roi.width && img.height == roi.height) : (img.width == texture.width && img.height == texture.height)))
                {
                    //Debug.Log("reuse texture " + winname);
                    if (roi != null)
                    {
                        if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
                        {
                            // If the Texture is a RenderTexture and useMipMap is true, then copying with the Graphics.CopyTexture method on a reused Texture will cause alpha values to be wrong, so GL.Clear is executed before copying.
                            if (texture is RenderTexture rt && rt.useMipMap)
                            {
                                RenderTexture.active = rt;
                                GL.Clear(true, true, Color.clear);
                                RenderTexture.active = null;
                            }
                            Graphics.CopyTexture(img, 0, 0, roi.x, img.height - (roi.y + roi.height), roi.width, roi.height, texture, 0, 0, 0, 0);
                        }
                        else
                        {
                            CopyTextureFallback(img, 0, 0, roi.x, img.height - (roi.y + roi.height), roi.width, roi.height, texture, 0, 0, 0, 0);
                        }
                    }
                    else
                    {
                        if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
                        {
                            // If the Texture is a RenderTexture and useMipMap is true, then copying with the Graphics.CopyTexture method on a reused Texture will cause alpha values to be wrong, so GL.Clear is executed before copying.
                            if (texture is RenderTexture rt && rt.useMipMap)
                            {
                                RenderTexture.active = rt;
                                GL.Clear(true, true, Color.clear);
                                RenderTexture.active = null;
                            }
                            Graphics.CopyTexture(img, texture);
                        }
                        else
                        {
                            CopyTextureFallback(img, texture);
                        }
                    }
                }
                else
                {
                    if (roi != null)
                    {
                        //Debug.Log("new roi texture " + winname);
                        texture = CreateSameFormatTexture(img, roi.width, roi.height);

                        if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
                        {
                            Graphics.CopyTexture(img, 0, 0, roi.x, img.height - (roi.y + roi.height), roi.width, roi.height, texture, 0, 0, 0, 0);
                        }
                        else
                        {
                            CopyTextureFallback(img, 0, 0, roi.x, img.height - (roi.y + roi.height), roi.width, roi.height, texture, 0, 0, 0, 0);
                        }
                    }
                    else
                    {
                        //Debug.Log("new texture " + winname);
                        texture = CreateSameFormatTexture(img, img.width, img.height);
                        if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
                        {
                            Graphics.CopyTexture(img, texture);
                        }
                        else
                        {
                            CopyTextureFallback(img, texture);
                        }
                    }
                }

                dumpText += "\n" + ToString(img);
                if (roi != null) dumpText += "\n<b>roi</b> : " + roi.ToString();
                if (dump)
                {
                    //if (!(img is Texture2D || img is RenderTexture))
                    //{
                    //    dumpText += "\n<b>dump</b> : This Texture class does not support dump.";
                    //}
                    //else
                    if (texture.width * texture.height <= 400)
                    {
                        dumpText += DumpTexture(texture, dumpMode);
                    }
                    else
                    {
                        dumpText += "\n<b>dump</b> : Mat larger than 400 pixels cannot be dumped, so specify roi in the imshow method.";
                    }
                }

                if (!string.IsNullOrEmpty(debugText)) dumpText += "\n<b>debugText</b> : " + debugText;

                if (bigImage.texture == texture)
                {
                    aspectRatioFilter.aspectRatio = (float)texture.width / (float)texture.height;
                    dumpInputField.text = dumpText;
                }

                rawImage.texture = texture;

                setNewDimension(texture.width, texture.height);

            }
        }

        public void setNewDimension​(int width, int height)
        {
            rectTracsform.sizeDelta = new Vector2(width, height);

            if (width < height)
            {
                layoutElement.preferredWidth = 100 * (float)width / (float)height;
                layoutElement.preferredHeight = 100;
            }
            else
            {
                layoutElement.preferredWidth = 100;
                layoutElement.preferredHeight = 100 * (float)height / (float)width;
            }
        }

        public void setNewPosition​(int x, int y)
        {
            rectTracsform.localPosition = new Vector3(x, -y, 0);
        }

        public void OnPointerClick()
        {
            //Debug.Log("OnPointerClick " + this.name);

            if (texture != null)
            {
                bigImage.texture = texture;
                aspectRatioFilter = bigImage.GetComponent<AspectRatioFitter>();
                aspectRatioFilter.aspectRatio = (float)texture.width / (float)texture.height;
            }
            else if (texture2D != null)
            {
                bigImage.texture = texture2D;
                aspectRatioFilter = bigImage.GetComponent<AspectRatioFitter>();
                aspectRatioFilter.aspectRatio = (float)texture2D.width / (float)texture2D.height;
            }

            bigImage.transform.parent.gameObject.SetActive(false);
            bigImage.transform.parent.gameObject.SetActive(true);

            dumpInputField.text = dumpText;
            dumpInputField.gameObject.SetActive(false);
            dumpInputField.gameObject.SetActive(true);
        }

        private void CopyTextureFallback(Texture src, Texture dst)
        {
            // Check if the source and destination can be cast to Texture2D
            if (src is Texture2D srcTexture2D && dst is Texture2D dstTexture2D)
            {
                // Get pixel data from the source texture
                Color[] pixels = srcTexture2D.GetPixels();

                // Write the pixel data to the target texture
                dstTexture2D.SetPixels(pixels);
                dstTexture2D.Apply();
            }
            else if (src is RenderTexture srcRenderTexture && dst is RenderTexture dstRenderTexture)
            {
                // If using RenderTexture, first copy the source to a Texture2D
                RenderTexture.active = srcRenderTexture;
                Texture2D tempTexture = new Texture2D(srcRenderTexture.width, srcRenderTexture.height, TextureFormat.RGBA32, false, !dstRenderTexture.isDataSRGB);
                tempTexture.ReadPixels(new UnityEngine.Rect(0, 0, srcRenderTexture.width, srcRenderTexture.height), 0, 0);
                tempTexture.Apply();

                // Activate the target RenderTexture and perform the write
                RenderTexture.active = dstRenderTexture;
                Graphics.Blit(tempTexture, dstRenderTexture);

                // Deactivate the RenderTexture
                RenderTexture.active = null;

                // Release the temporary texture from memory
                UnityEngine.Object.Destroy(tempTexture);
            }
            else
            {
                Debug.LogError("Unsupported texture types for manual copy.");
            }
        }

        private void CopyTextureFallback(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
        {
            if (src is Texture2D srcTexture2D && dst is Texture2D dstTexture2D)
            {
                // Get the pixels from the specified region of the source texture
                Color[] pixels = srcTexture2D.GetPixels(srcX, srcY, srcWidth, srcHeight, srcMip);

                // Write the pixel data to the specified position in the target texture
                dstTexture2D.SetPixels(dstX, dstY, srcWidth, srcHeight, pixels, dstMip);
                dstTexture2D.Apply();
            }
            else if (src is RenderTexture srcRenderTexture && dst is RenderTexture dstRenderTexture)
            {
                // If using RenderTexture, first copy the source to a Texture2D
                RenderTexture.active = srcRenderTexture;
                Texture2D tempSrcTexture = new Texture2D(srcRenderTexture.width, srcRenderTexture.height, TextureFormat.RGBA32, false, !dstRenderTexture.isDataSRGB);

                // Get the pixels from the specified region of the source
                tempSrcTexture.ReadPixels(new UnityEngine.Rect(srcX, srcY, srcWidth, srcHeight), 0, 0);
                tempSrcTexture.Apply();

                // To copy to the target RenderTexture, first activate the target RenderTexture and transfer the pixels to the specified position
                RenderTexture.active = dstRenderTexture;

                // Temporary texture to read the content of the target
                Texture2D tempDstTexture = new Texture2D(dstRenderTexture.width, dstRenderTexture.height, TextureFormat.RGBA32, false, !dstRenderTexture.isDataSRGB);
                tempDstTexture.ReadPixels(new UnityEngine.Rect(0, 0, dstRenderTexture.width, dstRenderTexture.height), 0, 0);

                // Write the pixel data
                tempDstTexture.SetPixels(dstX, dstY, srcWidth, srcHeight, tempSrcTexture.GetPixels(0, 0, srcWidth, srcHeight));
                tempDstTexture.Apply();

                // Blit the tempDstTexture onto dstRenderTexture to write
                Graphics.Blit(tempDstTexture, dstRenderTexture);

                // Deactivate the RenderTexture
                RenderTexture.active = null;

                // Release the memory for the temporary textures
                UnityEngine.Object.Destroy(tempSrcTexture);
                UnityEngine.Object.Destroy(tempDstTexture);
            }
            else
            {
                Debug.LogError("Unsupported texture types for manual copy.");
            }
        }

        private string ToString(Texture texture)
        {

            if (texture is Texture2D texture2D)
            {
                return "Texture2D [ " + GetTexturePropertiesText<Texture2D>(texture2D) + " ]";
            }
            else if (texture is RenderTexture renderTexture)
            {
                return "RenderTexture [ " + GetTexturePropertiesText<RenderTexture>(renderTexture) + " ]";
            }

            return "";

        }

        private string GetTexturePropertiesText<T>(T texture)
        {
            StringBuilder sb = new StringBuilder();

            // Only retrieve readable instance properties
            var instanceProperties = typeof(T).GetProperties().Where(p => p.CanRead && !p.GetGetMethod().IsStatic);

            foreach (var property in instanceProperties)
            {
                try
                {
                    sb.Append($"{property.Name}={property.GetValue(texture)}");
                    if (property != instanceProperties.Last())
                    {
                        sb.Append(", ");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning($"Failed to GetValue {property.Name}: {ex.Message}");
                }
            }

            return sb.ToString();
        }

        //private void CopyTextureProperties<T>(T srcTexture, T dstTexture)
        //{
        //    // Copy all properties
        //    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
        //    foreach (System.Reflection.PropertyInfo property in properties)
        //    {
        //        if (property.CanWrite && property.CanRead)
        //        {
        //            try
        //            {
        //                if (property.Name == "width" || property.Name == "height" || property.Name == "active" || property.Name == "descriptor") continue;

        //                Debug.Log("property.Name " + property.Name + " " + property.GetValue(srcTexture));
        //                property.SetValue(dstTexture, property.GetValue(srcTexture));
        //            }
        //            catch (System.Exception ex)
        //            {
        //                Debug.LogWarning($"Failed to copy property {property.Name}: {ex.Message}");
        //            }
        //        }
        //    }
        //}

        private Texture CreateSameFormatTexture(Texture texture, int width, int height)
        {
            if (texture is Texture2D srcTexture2D)
            {
                //Texture2D dstTexture2D = new Texture2D(width, height, ((Texture2D)texture).format, texture.mipmapCount, !((Texture2D)texture).isDataSRGB);
                //Texture2D dstTexture2D = new Texture2D(width, height, ((Texture2D)texture).format, ((Texture2D)texture).mipmapCount > 1, !((Texture2D)texture).isDataSRGB);
                bool mipChain = srcTexture2D.mipmapCount > 1;
                Texture2D dstTexture2D = new Texture2D(width, height, srcTexture2D.graphicsFormat, mipChain ? UnityEngine.Experimental.Rendering.TextureCreationFlags.MipChain : UnityEngine.Experimental.Rendering.TextureCreationFlags.None);

                dstTexture2D.filterMode = srcTexture2D.filterMode;
                dstTexture2D.wrapMode = srcTexture2D.wrapMode;
                dstTexture2D.wrapModeU = srcTexture2D.wrapModeU;
                dstTexture2D.wrapModeV = srcTexture2D.wrapModeV;
                dstTexture2D.wrapModeW = srcTexture2D.wrapModeW;
                dstTexture2D.anisoLevel = srcTexture2D.anisoLevel;
                dstTexture2D.mipMapBias = srcTexture2D.mipMapBias;

                dstTexture2D.requestedMipmapLevel = srcTexture2D.requestedMipmapLevel;
                dstTexture2D.minimumMipmapLevel = srcTexture2D.minimumMipmapLevel;
                dstTexture2D.ignoreMipmapLimit = srcTexture2D.ignoreMipmapLimit;

                dstTexture2D.name = srcTexture2D.name;

                //Debug.Log(GetTexturePropertiesText(dstTexture2D));

                return dstTexture2D;

            }
            else if (texture is RenderTexture srcRenderTexture)
            {

                //RenderTexture dstRenderTexture = new RenderTexture(width, height, ((RenderTexture)texture).depth, ((RenderTexture)texture).format, ((RenderTexture)texture).mipmapCount);

                //dstRenderTexture.filterMode = srcRenderTexture.filterMode;
                //dstRenderTexture.wrapMode = srcRenderTexture.wrapMode;
                //dstRenderTexture.wrapModeU = srcRenderTexture.wrapModeU;
                //dstRenderTexture.anisoLevel = srcRenderTexture.anisoLevel;
                //dstRenderTexture.mipMapBias = srcRenderTexture.mipMapBias;
                //dstRenderTexture.vrUsage = srcRenderTexture.vrUsage;
                //dstRenderTexture.useMipMap = srcRenderTexture.useMipMap;
                //dstRenderTexture.autoGenerateMips = srcRenderTexture.autoGenerateMips;
                //dstRenderTexture.dimension = srcRenderTexture.dimension;
                //dstRenderTexture.depth = srcRenderTexture.depth;
                //dstRenderTexture.useDynamicScale = srcRenderTexture.useDynamicScale;
                //dstRenderTexture.volumeDepth = srcRenderTexture.volumeDepth;
                //dstRenderTexture.name = srcRenderTexture.name;


                RenderTexture dstRenderTexture = new RenderTexture(srcRenderTexture.descriptor);
                dstRenderTexture.width = width;
                dstRenderTexture.height = height;
                dstRenderTexture.name = srcRenderTexture.name;
                dstRenderTexture.Create();

                //Debug.Log(GetTexturePropertiesText(dstRenderTexture));

                return dstRenderTexture;

            }

            return null;
        }

        private string DumpTexture(Texture texture, DebugMatUtils.DumpMode dumpMode)
        {
            string dumpText = "\n<b>dump</b> : ";

            if (texture is Texture2D texture2D)
            {

                if (dumpMode == DebugMatUtils.DumpMode.GetPixels32Mode)
                {
                    return dumpText + GetMatDump<Color32>(texture2D.GetPixels32(), texture.width, texture.height, CvType.CV_8UC4);
                }
                else
                {
                    int matType = GetChannelCount(texture2D.graphicsFormat);
                    if (matType == -1)
                    {
                        return dumpText + "As this TextureFormat is a compressed format, the result of GetRawTextureData() is dumped as is. : " + "[" + string.Join(", ", ((Texture2D)texture).GetRawTextureData()) + "]";
                    }
                    else
                    {
                        //return dumpText + GetMatDump<byte>(texture2D.GetRawTextureData(), texture.width, texture.height, GetChannelCount(texture2D.format));
                        return dumpText + GetMatDump<byte>(texture2D.GetRawTextureData(), texture.width, texture.height, GetChannelCount(texture2D.graphicsFormat));
                    }
                }
            }
            else if (texture is RenderTexture renderTexture)
            {

                //int matType = GetChannelCount(((RenderTexture)texture).format);

                //if (matType == -1)
                //{
                //    return dumpText + "This RenderTextureFormat is unsupported.";
                //}
                //else
                //{
                Mat mat = new Mat(texture.height, texture.width, CvType.CV_8UC4);
                int matCount = (int)mat.total();
                int matStride = (int)mat.elemSize();
                if (graphicsBuffer == null || (matCount != texture.width * texture.height))
                {
                    graphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, matCount, matStride);
                }

                try
                {
                    Utils.RenderTextureToMat(renderTexture, mat, graphicsBuffer);
                }
                catch (Exception ex)
                {
                    dumpText += ex.Message;
                    mat.Dispose();

                    return dumpText;
                }

                //if (matType == -1 || matType == CvType.CV_8UC4)
                //{
                dumpText += mat.dump();
                //}
                //else
                //{
                //    Mat partialMat = new Mat(mat.size(), matType);
                //    partialCopyUsingMixChannels(mat, partialMat);
                //    dumpText += partialMat.dump();
                //    partialMat.Dispose();
                //}

                mat.Dispose();

                return dumpText;
                //}
            }

            return "";
        }

        private static void partialCopyUsingMixChannels(Mat src, Mat dst)
        {
            int srcChannels = src.channels();
            int dstChannels = dst.channels();
            int copyChannels = Math.Min(srcChannels, dstChannels);

            // Array to store source channel indices
            int[] fromTo = new int[copyChannels * 2];

            // Set the indices
            for (int i = 0; i < copyChannels; ++i)
            {
                fromTo[i * 2] = i;
                fromTo[i * 2 + 1] = i;
            }

            MatOfInt fromToMat = new MatOfInt(fromTo);

            List<Mat> srcMats = new List<Mat>();
            srcMats.Add(src);
            List<Mat> dstMats = new List<Mat>();
            dstMats.Add(dst);

            // Copy using the mixChannels function
            Core.mixChannels(srcMats, dstMats, fromToMat);

            fromToMat.Dispose();
        }

        private string GetMatDump<T>(T[] dataArray, int width, int height, int type) where T : unmanaged
        {
            string dumpText = null;

#if !OPENCV_DONT_USE_UNSAFE_CODE
            unsafe
            {
                fixed (T* matDataPtr = dataArray)
                {
                    Mat mat = new Mat(height, width, type, (IntPtr)matDataPtr);
                    //Debug.Log("mat " + matMat.ToString());
                    //Debug.Log("mat " + matMat.dump());

                    Core.flip(mat, mat, 0);

                    dumpText = mat.dump();

                    mat.Dispose();

                }
            }
#else
            GCHandle matDataHandle = GCHandle.Alloc(dataArray, GCHandleType.Pinned);

            Mat mat = new Mat(height, width, type, matDataHandle.AddrOfPinnedObject());
            //Debug.Log("mat " + matMat.ToString());
            //Debug.Log("mat " + matMat.dump());

            Core.flip(mat, mat, 0);

            dumpText = mat.dump();

            mat.Dispose();

            matDataHandle.Free();

#endif
            return dumpText;
        }

        private static Dictionary<TextureFormat, int> TextureFormatToMatType = new Dictionary<TextureFormat, int>()
        {
            { TextureFormat.Alpha8, CvType.CV_8UC1 },
            // { TextureFormat.ARGB4444, -1 },
            // { TextureFormat.RGB24, CvType.CV_8UC3 },
            { TextureFormat.RGBA32, CvType.CV_8UC4 },
            { TextureFormat.ARGB32, CvType.CV_8UC4 },
            // { TextureFormat.RGB565, -1 },
            { TextureFormat.R16, CvType.CV_16UC1 },
            // { TextureFormat.DXT1, -1 },
            // { TextureFormat.DXT5, -1 },
            // { TextureFormat.RGBA4444, -1 },
            { TextureFormat.BGRA32, CvType.CV_8UC4 },
            { TextureFormat.RHalf, CvType.CV_16FC1 },
            { TextureFormat.RGHalf, CvType.CV_16FC2 },
            { TextureFormat.RGBAHalf, CvType.CV_16FC4 },
            { TextureFormat.RFloat, CvType.CV_32FC1 },
            { TextureFormat.RGFloat, CvType.CV_32FC2 },
            { TextureFormat.RGBAFloat, CvType.CV_32FC4 },
            // { TextureFormat.YUY2, -1 },
            // { TextureFormat.RGB9e5Float, -1 },
            // { TextureFormat.BC4, -1 },
            // { TextureFormat.BC5, -1 },
            // { TextureFormat.BC6H, -1 },
            // { TextureFormat.BC7, -1 },
            // { TextureFormat.DXT1Crunched, -1 },
            // { TextureFormat.DXT5Crunched, -1 },
            // { TextureFormat.PVRTC_RGB2, -1 },
            // { TextureFormat.PVRTC_RGBA2, -1 },
            // { TextureFormat.PVRTC_RGB4, -1 },
            // { TextureFormat.PVRTC_RGBA4, -1 },
            // { TextureFormat.ETC_RGB4, -1 },
            // { TextureFormat.EAC_R, -1 },
            // { TextureFormat.EAC_R_SIGNED, -1 },
            // { TextureFormat.EAC_RG, -1 },
            // { TextureFormat.EAC_RG_SIGNED, -1 },
            // { TextureFormat.ETC2_RGB, -1 },
            // { TextureFormat.ETC2_RGBA1, -1 },
            // { TextureFormat.ETC2_RGBA8, -1 },
            // { TextureFormat.ASTC_4x4, -1 },
            // { TextureFormat.ASTC_5x5, -1 },
            // { TextureFormat.ASTC_6x6, -1 },
            // { TextureFormat.ASTC_8x8, -1 },
            // { TextureFormat.ASTC_10x10, -1 },
            // { TextureFormat.ASTC_12x12, -1 },
            { TextureFormat.RG16, CvType.CV_8UC2 },
            { TextureFormat.R8, CvType.CV_8UC1 },
            // { TextureFormat.ETC_RGB4Crunched, -1 },
            // { TextureFormat.ETC2_RGBA8Crunched, -1 },
            // { TextureFormat.ASTC_HDR_4x4, -1 },
            // { TextureFormat.ASTC_HDR_5x5, -1 },
            // { TextureFormat.ASTC_HDR_6x6, -1 },
            // { TextureFormat.ASTC_HDR_8x8, -1 },
            // { TextureFormat.ASTC_HDR_10x10, -1 },
            // { TextureFormat.ASTC_HDR_12x12, -1 },
            { TextureFormat.RG32, CvType.CV_16UC2 },
            { TextureFormat.RGB48, CvType.CV_16UC3 },
            { TextureFormat.RGBA64, CvType.CV_16UC4 },
            // { TextureFormat.R8_SIGNED, CvType.CV_8SC1 },
            // { TextureFormat.RG16_SIGNED, CvType.CV_8SC2 },
            // { TextureFormat.RGB24_SIGNED, CvType.CV_8SC3 },
            // { TextureFormat.RGBA32_SIGNED, CvType.CV_8SC4 },
            // { TextureFormat.R16_SIGNED, CvType.CV_16SC1 },
            // { TextureFormat.RG32_SIGNED, CvType.CV_16SC2 },
            // { TextureFormat.RGB48_SIGNED, CvType.CV_16SC3 },
            // { TextureFormat.RGBA64_SIGNED, CvType.CV_16SC4 },
        };

        private static int GetChannelCount(TextureFormat format)
        {
            if (TextureFormatToMatType.TryGetValue(format, out int count))
            {
                return count;
            }
            else
            {
                //Debug.LogError("Unsupported TextureFormat: " + format);
                return -1;
            }
        }

        private static Dictionary<RenderTextureFormat, int> RenderTextureFormatToMatType = new Dictionary<RenderTextureFormat, int>()
        {
            { RenderTextureFormat.ARGB32, CvType.CV_8UC4 },
            // { RenderTextureFormat.Depth, -1 },
            // { RenderTextureFormat.ARGBHalf, CvType.CV_16FC4 },
            // { RenderTextureFormat.Shadowmap, -1 },
            // { RenderTextureFormat.RGB565, -1 },
            // { RenderTextureFormat.ARGB4444, -1 },
            // { RenderTextureFormat.ARGB1555, -1 },
            // { RenderTextureFormat.Default, -1 },
            // { RenderTextureFormat.ARGB2101010, -1 },
            // { RenderTextureFormat.DefaultHDR, -1 },
            // { RenderTextureFormat.ARGB64, CvType.CV_16UC4 },
            // { RenderTextureFormat.ARGBFloat, CvType.CV_32FC4 },
            // { RenderTextureFormat.RGFloat, CvType.CV_32FC2 },
            // { RenderTextureFormat.RGHalf, CvType.CV_16FC2 },
            // { RenderTextureFormat.RFloat, CvType.CV_32FC1 },
            // { RenderTextureFormat.RHalf, CvType.CV_16FC1 },
            { RenderTextureFormat.R8, CvType.CV_8UC1 },
            // { RenderTextureFormat.ARGBInt, -1 },
            // { RenderTextureFormat.RGInt, -1 },
            // { RenderTextureFormat.RInt, -1 },
            { RenderTextureFormat.BGRA32, CvType.CV_8UC4 },
            // { RenderTextureFormat.RGB111110Float, -1 },
            // { RenderTextureFormat.RG32, CvType.CV_16UC2 },
            // { RenderTextureFormat.RGBAUShort, CvType.CV_16UC4 },
            { RenderTextureFormat.RG16, CvType.CV_8UC2 },
            // { RenderTextureFormat.BGRA10101010_XR, -1 },
            // { RenderTextureFormat.BGR101010_XR, -1 },
            // { RenderTextureFormat.R16, CvType.CV_16UC1 },
        };

        private static int GetChannelCount(RenderTextureFormat format)
        {
            if (RenderTextureFormatToMatType.TryGetValue(format, out int count))
            {
                return count;
            }
            else
            {
                //Debug.LogError("Unsupported RenderTextureFormat: " + format);
                return -1;
            }
        }

        private static Dictionary<GraphicsFormat, int> GraphicsFormatToMatType = new Dictionary<GraphicsFormat, int>()
        {
            { GraphicsFormat.R8_SRGB, CvType.CV_8UC1 },                     // 8-bit single channel (sRGB)
            { GraphicsFormat.R8G8_SRGB, CvType.CV_8UC2 },                  // 8-bit 2 channels (sRGB)
            { GraphicsFormat.R8G8B8_SRGB, CvType.CV_8UC3 },                // 8-bit 3 channels (sRGB)
            { GraphicsFormat.R8G8B8A8_SRGB, CvType.CV_8UC4 },              // 8-bit 4 channels (sRGB)
            { GraphicsFormat.R8_UNorm, CvType.CV_8UC1 },                   // 8-bit single channel
            { GraphicsFormat.R8G8_UNorm, CvType.CV_8UC2 },                 // 8-bit 2 channels
            { GraphicsFormat.R8G8B8_UNorm, CvType.CV_8UC3 },               // 8-bit 3 channels
            { GraphicsFormat.R8G8B8A8_UNorm, CvType.CV_8UC4 },             // 8-bit 4 channels
            { GraphicsFormat.R8_SNorm, CvType.CV_8SC1 },                   // 8-bit signed single channel
            { GraphicsFormat.R8G8_SNorm, CvType.CV_8SC2 },                 // 8-bit signed 2 channels
            { GraphicsFormat.R8G8B8_SNorm, CvType.CV_8SC3 },               // 8-bit signed 3 channels
            { GraphicsFormat.R8G8B8A8_SNorm, CvType.CV_8SC4 },             // 8-bit signed 4 channels
            { GraphicsFormat.R8_UInt, CvType.CV_8UC1 },                     // 8-bit unsigned single channel
            { GraphicsFormat.R8G8_UInt, CvType.CV_8UC2 },                   // 8-bit unsigned 2 channels
            { GraphicsFormat.R8G8B8_UInt, CvType.CV_8UC3 },                 // 8-bit unsigned 3 channels
            { GraphicsFormat.R8G8B8A8_UInt, CvType.CV_8UC4 },               // 8-bit unsigned 4 channels
            { GraphicsFormat.R8_SInt, CvType.CV_8SC1 },                    // 8-bit signed integer single channel
            { GraphicsFormat.R8G8_SInt, CvType.CV_8SC2 },                  // 8-bit signed integer 2 channels
            { GraphicsFormat.R8G8B8_SInt, CvType.CV_8SC3 },                // 8-bit signed integer 3 channels
            { GraphicsFormat.R8G8B8A8_SInt, CvType.CV_8SC4 },              // 8-bit signed integer 4 channels
            { GraphicsFormat.R16_UNorm, CvType.CV_16UC1 },                 // 16-bit unsigned single channel
            { GraphicsFormat.R16G16_UNorm, CvType.CV_16UC2 },              // 16-bit unsigned 2 channels
            { GraphicsFormat.R16G16B16_UNorm, CvType.CV_16UC3 },           // 16-bit unsigned 3 channels
            { GraphicsFormat.R16G16B16A16_UNorm, CvType.CV_16UC4 },        // 16-bit unsigned 4 channels
            { GraphicsFormat.R16_SNorm, CvType.CV_16SC1 },                 // 16-bit signed single channel
            { GraphicsFormat.R16G16_SNorm, CvType.CV_16SC2 },              // 16-bit signed 2 channels
            { GraphicsFormat.R16G16B16_SNorm, CvType.CV_16SC3 },           // 16-bit signed 3 channels
            { GraphicsFormat.R16G16B16A16_SNorm, CvType.CV_16SC4 },        // 16-bit signed 4 channels
            { GraphicsFormat.R16_UInt, CvType.CV_16UC1 },                  // 16-bit unsigned integer single channel
            { GraphicsFormat.R16G16_UInt, CvType.CV_16UC2 },               // 16-bit unsigned integer 2 channels
            { GraphicsFormat.R16G16B16_UInt, CvType.CV_16UC3 },            // 16-bit unsigned integer 3 channels
            { GraphicsFormat.R16G16B16A16_UInt, CvType.CV_16UC4 },         // 16-bit unsigned integer 4 channels
            { GraphicsFormat.R16_SInt, CvType.CV_16SC1 },                  // 16-bit signed integer single channel
            { GraphicsFormat.R16G16_SInt, CvType.CV_16SC2 },               // 16-bit signed integer 2 channels
            { GraphicsFormat.R16G16B16_SInt, CvType.CV_16SC3 },            // 16-bit signed integer 3 channels
            { GraphicsFormat.R16G16B16A16_SInt, CvType.CV_16SC4 },         // 16-bit signed integer 4 channels
            { GraphicsFormat.R32_UInt, CvType.CV_32SC1 },                  // 32-bit unsigned single channel
            { GraphicsFormat.R32G32_UInt, CvType.CV_32SC2 },               // 32-bit unsigned 2 channels
            { GraphicsFormat.R32G32B32_UInt, CvType.CV_32SC3 },            // 32-bit unsigned 3 channels
            { GraphicsFormat.R32G32B32A32_UInt, CvType.CV_32SC4 },         // 32-bit unsigned 4 channels
            { GraphicsFormat.R32_SInt, CvType.CV_32SC1 },                  // 32-bit signed single channel
            { GraphicsFormat.R32G32_SInt, CvType.CV_32SC2 },               // 32-bit signed 2 channels
            { GraphicsFormat.R32G32B32_SInt, CvType.CV_32SC3 },            // 32-bit signed 3 channels
            { GraphicsFormat.R32G32B32A32_SInt, CvType.CV_32SC4 },         // 32-bit signed 4 channels
            { GraphicsFormat.R16_SFloat, CvType.CV_16FC1 },                // 16-bit floating-point single channel
            { GraphicsFormat.R16G16_SFloat, CvType.CV_16FC2 },             // 16-bit floating-point 2 channels
            { GraphicsFormat.R16G16B16_SFloat, CvType.CV_16FC3 },          // 16-bit floating-point 3 channels
            { GraphicsFormat.R16G16B16A16_SFloat, CvType.CV_16FC4 },       // 16-bit floating-point 4 channels
            { GraphicsFormat.R32_SFloat, CvType.CV_32FC1 },                // 32-bit floating-point single channel
            { GraphicsFormat.R32G32_SFloat, CvType.CV_32FC2 },             // 32-bit floating-point 2 channels
            { GraphicsFormat.R32G32B32_SFloat, CvType.CV_32FC3 },          // 32-bit floating-point 3 channels
            { GraphicsFormat.R32G32B32A32_SFloat, CvType.CV_32FC4 },       // 32-bit floating-point 4 channels

            { GraphicsFormat.B8G8R8_SRGB, CvType.CV_8UC3 },                  // 8-bit 3 channels (sRGB)
            { GraphicsFormat.B8G8R8A8_SRGB, CvType.CV_8UC4 },                // 8-bit 4 channels (sRGB)
            { GraphicsFormat.B8G8R8_UNorm, CvType.CV_8UC3 },                 // 8-bit 3 channels
            { GraphicsFormat.B8G8R8A8_UNorm, CvType.CV_8UC4 },               // 8-bit 4 channels
            { GraphicsFormat.B8G8R8_SNorm, CvType.CV_8SC3 },                 // 8-bit signed 3 channels
            { GraphicsFormat.B8G8R8A8_SNorm, CvType.CV_8SC4 },               // 8-bit signed 4 channels
            { GraphicsFormat.B8G8R8_UInt, CvType.CV_8UC3 },                  // 8-bit unsigned integer 3 channels
            { GraphicsFormat.B8G8R8A8_UInt, CvType.CV_8UC4 },                // 8-bit unsigned integer 4 channels
            { GraphicsFormat.B8G8R8_SInt, CvType.CV_8SC3 },                  // 8-bit signed integer 3 channels
            { GraphicsFormat.B8G8R8A8_SInt, CvType.CV_8SC4 },                // 8-bit signed integer 4 channels

            { GraphicsFormat.D16_UNorm, CvType.CV_16UC1 },                  // 16-bit unsigned normalized depth
            { GraphicsFormat.D32_SFloat, CvType.CV_32FC1 },                 // 32-bit signed floating-point depth
            { GraphicsFormat.S8_UInt, CvType.CV_8UC1 },                     // 8-bit unsigned integer stencil
        };

        private static int GetChannelCount(GraphicsFormat format)
        {
            if (GraphicsFormatToMatType.TryGetValue(format, out int count))
            {
                return count;
            }
            else
            {
                //Debug.LogError("Unsupported GraphicsFormat: " + format);
                return -1;
            }
        }

    }
}
