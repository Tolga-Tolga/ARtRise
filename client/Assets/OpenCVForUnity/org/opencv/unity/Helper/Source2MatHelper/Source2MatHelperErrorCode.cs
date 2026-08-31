namespace OpenCVForUnity.UnityIntegration.Helper.Source2Mat
{
    public enum Source2MatHelperErrorCode : int
    {
        UNKNOWN = 0,
        CAMERA_DEVICE_NOT_EXIST,
        CAMERA_PERMISSION_DENIED,
        VIDEO_FILE_NOT_EXIST,
        VIDEO_FILE_CANT_OPEN,
        IMAGE_FILE_NOT_EXIST,
        IMAGE_FILE_CANT_OPEN,
        SOURCE_TEXTURE_IS_NULL,
        SOURCE_TEXTURE_FORMAT_IS_NOT_SPPORTED,
        ASYNC_GPU_READBACK_IS_NOT_SPPORTED,
        RENDERTEXTURE_GRAPHICS_FORMAT_IS_NOT_SPPORTED,
        TIMEOUT,
    }
}
