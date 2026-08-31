using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Rendering;
using UnityEngine;
using Meta.XR;
using System;
using System.Linq;
using UnityEngine.Android;

#if ZXING_ENABLED
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Multi;
#endif

public enum QrCodeDetectionMode
{
    Single,
    Multiple
}

[Serializable]
public class QrCodeResult
{
    public string text;
    public Vector3[] corners;
    public Pose cameraPose;
    public PassthroughCameraAccess.CameraIntrinsics Intrinsics;
    public Vector2Int captureResolution;
}

public class QrCodeScanner : MonoBehaviour
{
#if ZXING_ENABLED
    [SerializeField] private int sampleFactor = 3;
    [SerializeField] private QrCodeDetectionMode detectionMode = QrCodeDetectionMode.Single;
    [Tooltip("Experimental Mode or Game Mode")]
    [SerializeField] private VRState state;

    private PassthroughCameraAccess _cameraAccess;
    private RenderTexture _downsampledTexture;
    private ComputeShader _downsampleShader;
    private QRCodeReader _qrReader;
    private bool _isScanning;
    private CardExtractor extractor;
    private CardArtworkExtractor artworkExtractor;
    private QrCodeDisplayManager manager;
    private bool _experimentalMode;

    private static readonly int Input1 = Shader.PropertyToID("_Input");
    private static readonly int Output = Shader.PropertyToID("_Output");
    private static readonly int InputWidth = Shader.PropertyToID("_InputWidth");
    private static readonly int InputHeight = Shader.PropertyToID("_InputHeight");
    private static readonly int OutputWidth = Shader.PropertyToID("_OutputWidth");
    private static readonly int OutputHeight = Shader.PropertyToID("_OutputHeight");

    CardTemplate template = new CardTemplate(
        width: 1000,
        height: 3200,

        qrCorners: new Vector2[]
        {
            new Vector2(0, 1100),     // QR oben links
            new Vector2(1000, 1100),  // QR oben rechts
            new Vector2(1000, 2100),  // QR unten rechts
            new Vector2(0, 2100)      // QR unten links
        },

        artworkRect: new RectInt(0, 0, 1000, 1000)
    );

    private struct CaptureFrame
    {
        public Texture Texture;
        public Pose Pose;
        public PassthroughCameraAccess.CameraIntrinsics Intrinsics;
        public Vector2Int Resolution;
    }
    private const string HeadsetCameraPermission = "horizonos.permission.HEADSET_CAMERA";
    private void Awake()
    {
        _cameraAccess = gameObject.AddComponent<PassthroughCameraAccess>();
        _cameraAccess.CameraPosition = PassthroughCameraAccess.CameraPositionType.Left;
        _cameraAccess.RequestedResolution = new Vector2Int(1280, 960);
        _downsampleShader = Resources.Load<ComputeShader>($"Downsample");
        extractor = GetComponent<CardExtractor>();
        artworkExtractor = GetComponent<CardArtworkExtractor>();
        
        _qrReader = new QRCodeReader();
        manager = GetComponent<QrCodeDisplayManager>();
        _experimentalMode = state.experimentalMode;

        Debug.Log("[cameraaccess] cameraAccess created: "+ (_cameraAccess != null));
        Debug.Log("[cameraaccess] component enabled: " + _cameraAccess.enabled);
        Debug.Log("[cameraaccess] gameObject activeInHierarchy: " + gameObject.activeInHierarchy);

        Debug.Log("[perm] Has HEADSET_CAMERA: " + Permission.HasUserAuthorizedPermission(HeadsetCameraPermission));

        if (!Permission.HasUserAuthorizedPermission(HeadsetCameraPermission))
        {
            Permission.RequestUserPermission(HeadsetCameraPermission);
            Debug.Log("[perm] Requested HEADSET_CAMERA permission");
        }
        Debug.Log("[perm] OVR granted: " + 
            OVRPermissionsRequester.IsPermissionGranted(OVRPermissionsRequester.Permission.PassthroughCameraAccess));
        }
    // private async void Start()
    // {
    //     await Task.Yield(); // einen Frame warten

    //     _cameraAccess = gameObject.AddComponent<PassthroughCameraAccess>();
    //     _cameraAccess.CameraPosition = PassthroughCameraAccess.CameraPositionType.Left;
    //     _cameraAccess.RequestedResolution = new Vector2Int(1280, 960);

    //     _downsampleShader = Resources.Load<ComputeShader>("Downsample");
    //     extractor = GetComponent<CardExtractor>();
    //     artworkExtractor = GetComponent<CardArtworkExtractor>();
    //     _qrReader = new QRCodeReader();
    //     manager = GetComponent<QrCodeDisplayManager>();
    //     _experimentalMode = state.experimentalMode;
    // }

    private void OnDestroy()
    {
        if (_downsampledTexture == null) return;
        _downsampledTexture.Release();
        Destroy(_downsampledTexture);
    }
    
    public async Task<Tuple<QrCodeResult, Texture2D>[]> ScanFrameAsync()
    {
        // Debug.Log("[SCAN] ScanFrameAsync START (isScanning=" + _isScanning + ")");
        if (_downsampleShader == null)
            return Array.Empty<Tuple<QrCodeResult, Texture2D>>();

        if (_isScanning || !_downsampleShader)
            return Array.Empty<Tuple<QrCodeResult, Texture2D>>();

        _isScanning = true;
        try
        {
            var frame = await AcquireFrameAsync();
            if (frame == null){
                Debug.Log("[SCAN] frame is null!");
                return Array.Empty<Tuple<QrCodeResult, Texture2D>>();
            }
            
            var (targetWidth, targetHeight) = GetTargetDimensions(frame.Value.Texture);
            if (!EnsureDownsampleTarget(targetWidth, targetHeight))
                return Array.Empty<Tuple<QrCodeResult, Texture2D>>();
            DispatchDownsample(frame.Value.Texture, targetWidth, targetHeight);
            var grayBytes = await ReadPixelsAsync(_downsampledTexture);
            if (grayBytes == null || grayBytes.Length == 0)
                return Array.Empty<Tuple<QrCodeResult, Texture2D>>();
            // optional:
            // SaveDebugTexture(grayBytes, targetWidth, targetHeight);
            // Debug.Log(string.Join(" ", grayBytes.Take(200).Select(b => b.ToString("X2"))));
            Debug.Log("[SCAN] thread task.run = " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            var decoded = await Task.Run(() =>
                DecodeFrame(frame.Value, grayBytes, targetWidth, targetHeight));
            // Debug.Log("[SCAN] 6");
            // OUTPUT LIST
            var results = new List<Tuple<QrCodeResult, Texture2D>>();
            // Debug.Log("[SCAN] 7");
            if (decoded != null && decoded.Length > 0)
            {
                // Debug.Log("[SCAN] 9");
                foreach (var qr in decoded)
                {
                    // Debug.Log("[SCAN] 10");
                    // Debug.Log("[SCAN] thread before extract artwork= " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                    if(!manager.HasQrAlreadyPictures(qr.text) && _experimentalMode){
                        UnityEngine.Texture2D fullResTexture = CloneTexture(frame.Value.Texture as UnityEngine.Texture2D);
                        // Debug.Log("[SCAN] 13");
                        if (fullResTexture == null){
                            Debug.Log("[Texture] camera texture runtime type = " + frame.Value.Texture.GetType().FullName);
                            Debug.LogError("[Texture] Texture is not a RenderTexture!");
                        }
                        Texture2D art = extractor.ExtractArtwork(fullResTexture, qr, template);
                        // Texture2D art;
                        // bool found = artworkExtractor.TryExtractArtwork(
                        //     fullResTexture,
                        //     out Texture2D artwork,
                        //     out Texture2D warpedCard,
                        //     out OpenCVForUnity.CoreModule.Point[] corners
                        // );
                        // if (found)
                        // {
                        //     art = artwork;
                        // }
                        // else
                        // {
                        //     art = warpedCard;
                        // }
                        results.Add(Tuple.Create(qr,art));
                        // Debug.Log("[SCAN] 14");
                        Destroy(fullResTexture);
                    }
                    else{
                        // Debug.Log("[SCAN] 11");
                        results.Add(Tuple.Create(qr, (Texture2D)null));
                        // Debug.Log("[SCAN] 12");
                    }
                }
            }
            // Debug.Log("[SCAN] ScanFrameAsync RETURNING " + results.Count + " items");
            return results.ToArray();
        }
        finally
        {
            _isScanning = false;
        }
    }
    Texture2D CloneTexture(Texture2D original)
    {
        Texture2D copy = new Texture2D(
            original.width,
            original.height,
            original.format,
            original.mipmapCount > 1
        );

        copy.SetPixels(original.GetPixels());
        copy.Apply();

        return copy;
    }


    // Debug-Helfer: Gray8 → sichtbares PNG
    private void SaveDebugTexture(byte[] bytes, int width, int height)
    {
        // wir machen ein RGB-Texture, damit PNG korrekt aussieht
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        var rgb = new byte[width * height * 3];
        for (int i = 0; i < width * height; i++)
        {
            byte g = bytes[i];
            rgb[i * 3 + 0] = g;
            rgb[i * 3 + 1] = g;
            rgb[i * 3 + 2] = g;
        }

        tex.LoadRawTextureData(rgb);
        tex.Apply();

        var png = tex.EncodeToPNG();
        var path = Application.persistentDataPath + "/qr_debug.png";
        System.IO.File.WriteAllBytes(path, png);

        Debug.Log("DEBUG QR IMAGE SAVED: " + path);
    }

    private QrCodeResult ProcessDecodeResult(Result decodeResult, int downW, int downH, CaptureFrame frame)
    {
        var points = decodeResult.ResultPoints;
        var uvCorners = new Vector3[points.Length];

        float fullW = frame.Resolution.x;
        float fullH = frame.Resolution.y;

        float scaleX = fullW / downW;
        float scaleY = fullH / downH;

        // Debug.Log(
        //     $"[ZXING] ProcessDecodeResult():\n" +
        //     $"   text = {decodeResult.Text}\n" +
        //     $"   downsampleSize = {downW}x{downH}\n" +
        //     $"   fullResolution = {fullW}x{fullH}\n" +
        //     $"   scale = ({scaleX:F4}, {scaleY:F4})\n" +
        //     $"   cornerCount = {points.Length}"
        // );

        for (int i = 0; i < points.Length; i++)
        {
            float zx = points[i].X;
            float zy = points[i].Y;

            float px = zx * scaleX;     // zurückskaliert
            float py = zy * scaleY;

            float u = px / fullW;       // UV 0..1
            float v = py / fullH;

            uvCorners[i] = new Vector3(u, v, 0);

            // Debug.Log(
            //     $"[ZXING] Corner {i}:\n" +
            //     $"   ZXing raw      = ({zx:F2}, {zy:F2})\n" +
            //     $"   fullRes pixel  = ({px:F2}, {py:F2})   (inside {fullW}x{fullH}?)\n" +
            //     $"   UV (0..1)      = ({u:F4}, {v:F4})"
            // );
        }

        return new QrCodeResult
        {
            text = decodeResult.Text,
            corners = uvCorners,
            cameraPose = frame.Pose,
            Intrinsics = frame.Intrinsics,
            captureResolution = frame.Resolution
        };
    }



    private Task<byte[]> ReadPixelsAsync(RenderTexture rt)
    {
        var tcs = new TaskCompletionSource<byte[]>();

        AsyncGPUReadback.Request(rt, 0, TextureFormat.R8, request =>
        {
            if (request.hasError)
            {
                tcs.SetException(new Exception("GPU readback error."));
            }
            else
            {
                tcs.SetResult(request.GetData<byte>().ToArray());
            }
        });
        return tcs.Task;
    }
    private Texture textureAsync;
    private async Task<CaptureFrame?> AcquireFrameAsync()
    {
        // Debug.Log("[SCAN] AcquireFrameAsync entered");
        while (true)
        {
            // if (!_cameraAccess.IsPlaying)
                // Debug.Log("[SCAN] CameraAccess not playing");

            // var tex = _cameraAccess.GetTexture();
            // if (!tex)
                // Debug.Log("[SCAN] No texture yet...");
            if (_cameraAccess && _cameraAccess.IsPlaying)
            {
                textureAsync = _cameraAccess.GetTexture();
                // Debug.Log("[SCAN] texture extracted!");
                if (textureAsync)
                {
                    // Debug.Log("[SCAN] texture extracted not null!");
                    return new CaptureFrame
                    {
                        Texture = textureAsync,
                        Pose = _cameraAccess.GetCameraPose(),
                        Intrinsics = _cameraAccess.Intrinsics,
                        Resolution = _cameraAccess.CurrentResolution
                    };
                }
            }
            await Task.Delay(16);
        }
    }

    private (int width, int height) GetTargetDimensions(Texture texture)
    {
        var divisor = Mathf.Max(1, sampleFactor);
        return (Mathf.Max(1, texture.width / divisor), Mathf.Max(1, texture.height / divisor));
    }

    private bool EnsureDownsampleTarget(int width, int height)
    {
        if (_downsampledTexture && _downsampledTexture.width == width && _downsampledTexture.height == height)
        {
            return true;
        }

        if (_downsampledTexture)
        {
            _downsampledTexture.Release();
        }

        _downsampledTexture = new RenderTexture(width, height, 0, RenderTextureFormat.R8)
        {
            enableRandomWrite = true
        };
        _downsampledTexture.Create();
        return true;
    }

    private void DispatchDownsample(Texture source, int targetWidth, int targetHeight)
    {
        var kernel = _downsampleShader.FindKernel("CSMain");
        _downsampleShader.SetTexture(kernel, Input1, source);
        _downsampleShader.SetTexture(kernel, Output, _downsampledTexture);
        _downsampleShader.SetInt(InputWidth, source.width);
        _downsampleShader.SetInt(InputHeight, source.height);
        _downsampleShader.SetInt(OutputWidth, targetWidth);
        _downsampleShader.SetInt(OutputHeight, targetHeight);

        var threadGroupsX = Mathf.CeilToInt(targetWidth / 8f);
        var threadGroupsY = Mathf.CeilToInt(targetHeight / 8f);
        _downsampleShader.Dispatch(kernel, threadGroupsX, threadGroupsY, 1);
    }

    private QrCodeResult[] DecodeFrame(CaptureFrame frame, byte[] grayBytes, int width, int height)
    {
        // Debug.Log($"[ZXING] DecodeFrame start   size={width}x{height}  bytes={grayBytes.Length}");

        try
        {
            // Gray8 → RGB24
            int pixelCount = width * height;
            var rgb = new byte[pixelCount * 3];

            for (int i = 0; i < pixelCount; i++)
            {
                byte g = grayBytes[i];
                int idx = i * 3;
                rgb[idx + 0] = g;
                rgb[idx + 1] = g;
                rgb[idx + 2] = g;
            }

            var luminance = new RGBLuminanceSource(rgb, width, height, RGBLuminanceSource.BitmapFormat.RGB24);

            var hints = new Dictionary<DecodeHintType, object>
            {
                { DecodeHintType.TRY_HARDER, true },
                { DecodeHintType.POSSIBLE_FORMATS, new List<BarcodeFormat> { BarcodeFormat.QR_CODE } },
                { DecodeHintType.CHARACTER_SET, "UTF-8" }
            };

            // ------------------------------------------------------------
            // SINGLE QR CODE
            // ------------------------------------------------------------
            if (detectionMode == QrCodeDetectionMode.Single)
            {
                // Debug.Log("[ZXING] MODE = Single");

                // 1) Hybrid
                try
                {
                    // Debug.Log("[ZXING] HybridBinarizer pass...");
                    var binary = new BinaryBitmap(new HybridBinarizer(luminance));
                    var result = _qrReader.decode(binary, hints);

                    if (result != null)
                    {
                        PrintZXingResult("[ZXING] Hybrid SUCCESS", result);
                        return new[] { ProcessDecodeResult(result, width, height, frame) };
                    }
                    // Debug.Log("[ZXING] Hybrid failed");
                }
                catch (Exception e)
                {
                    Debug.LogWarning("[ZXING] Hybrid error: " + e.Message);
                }

                // 2) GlobalHistogram
                try
                {
                    // Debug.Log("[ZXING] GlobalHistogram pass...");
                    var binary2 = new BinaryBitmap(new GlobalHistogramBinarizer(luminance));
                    var result2 = _qrReader.decode(binary2, hints);

                    if (result2 != null)
                    {
                        PrintZXingResult("[ZXING] Global SUCCESS", result2);
                        return new[] { ProcessDecodeResult(result2, width, height, frame) };
                    }
                    // Debug.Log("[ZXING] Global failed");
                }
                catch (Exception e)
                {
                    // Debug.LogWarning("[ZXING] Global error: " + e.Message);
                }

                // 3) Invert + Hybrid
                try
                {
                    // Debug.Log("[ZXING] Invert pass...");
                    var inverted = luminance.invert();
                    var binary3 = new BinaryBitmap(new HybridBinarizer(inverted));
                    var result3 = _qrReader.decode(binary3, hints);

                    if (result3 != null)
                    {
                        // PrintZXingResult("[ZXING] INVERT SUCCESS", result3);
                        return new[] { ProcessDecodeResult(result3, width, height, frame) };
                    }
                    Debug.Log("[ZXING] Invert failed");
                }
                catch (Exception e)
                {
                    Debug.LogWarning("[ZXING] Invert error: " + e.Message);
                }
            }

            // ------------------------------------------------------------
            // MULTI QR CODE
            // ------------------------------------------------------------
            else
            {
                // Debug.Log("[ZXING] MODE = Multi");

                var multiReader = new GenericMultipleBarcodeReader(_qrReader);

                Result[] results = null;

                // Try 1
                try
                {
                    // Debug.Log("[ZXING-MULTI] Hybrid pass...");
                    var binary = new BinaryBitmap(new HybridBinarizer(luminance));
                    results = multiReader.decodeMultiple(binary, hints);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("[ZXING-MULTI] Hybrid error: " + e.Message);
                }

                // Try 2
                if (results == null)
                {
                    try
                    {
                        // Debug.Log("[ZXING-MULTI] GlobalHistogram pass...");
                        var binary2 = new BinaryBitmap(new GlobalHistogramBinarizer(luminance));
                        results = multiReader.decodeMultiple(binary2, hints);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning("[ZXING-MULTI] Global error: " + e.Message);
                    }
                }

                // Try 3
                if (results == null)
                {
                    try
                    {
                        // Debug.Log("[ZXING-MULTI] Invert pass...");
                        var inverted = luminance.invert();
                        var binary3 = new BinaryBitmap(new HybridBinarizer(inverted));
                        results = multiReader.decodeMultiple(binary3, hints);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning("[ZXING-MULTI] Invert error: " + e.Message);
                    }
                }

                // Result handling
                if (results != null && results.Length > 0)
                {
                    // Debug.Log($"[ZXING-MULTI] SUCCESS count={results.Length}");
                    foreach (var r in results)
                        PrintZXingResult("[ZXING-MULTI] Found", r);

                    return results.Select(r => ProcessDecodeResult(r, width, height, frame)).ToArray();
                }

                // Debug.Log("[ZXING-MULTI] No QR codes detected.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("[ZXING] DecodeFrame EXCEPTION: " + ex);
        }

        Debug.Log("[ZXING] DecodeFrame end: NO RESULTS");
        return Array.Empty<QrCodeResult>();
    }

    private void PrintZXingResult(string prefix, Result result)
    {
        if (result == null)
        {
            // Debug.Log(prefix + " -> NULL");
            return;
        }

        // Debug.Log($"{prefix}: text=\"{result.Text}\" points={result.ResultPoints?.Length}");

        if (result.ResultPoints != null)
        {
            for (int i = 0; i < result.ResultPoints.Length; i++)
            {
                var p = result.ResultPoints[i];
                // Debug.Log($"   Corner {i}: X={p.X:F2}, Y={p.Y:F2}");
            }
        }
    }

#endif
}
