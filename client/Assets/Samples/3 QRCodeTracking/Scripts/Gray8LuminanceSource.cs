#if ZXING_ENABLED
using System;
using ZXing;

public class Gray8LuminanceSource : LuminanceSource
{
    private readonly byte[] buffer;

    public Gray8LuminanceSource(byte[] buffer, int width, int height)
        : base(width, height)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (buffer.Length != width * height)
            throw new ArgumentException("Gray8 buffer size must be width * height", nameof(buffer));

        this.buffer = buffer;
    }

    private Gray8LuminanceSource(byte[] buffer, int width, int height, int left, int top, int cropWidth, int cropHeight)
        : base(cropWidth, cropHeight)
    {
        this.buffer = new byte[cropWidth * cropHeight];

        for (int y = 0; y < cropHeight; y++)
        {
            Buffer.BlockCopy(
                buffer,
                (top + y) * width + left,
                this.buffer,
                y * cropWidth,
                cropWidth
            );
        }
    }

    public override byte[] Matrix => buffer;

    public override byte[] getRow(int y, byte[] row)
    {
        if (y < 0 || y >= Height)
            throw new ArgumentOutOfRangeException(nameof(y));

        int width = Width;
        if (row == null || row.Length < width)
            row = new byte[width];

        Buffer.BlockCopy(buffer, y * width, row, 0, width);
        return row;
    }

    public override LuminanceSource crop(int left, int top, int width, int height)
    {
        return new Gray8LuminanceSource(buffer, Width, Height, left, top, width, height);
    }

    public override LuminanceSource invert()
    {
        byte[] inverted = new byte[buffer.Length];
        for (int i = 0; i < buffer.Length; i++)
            inverted[i] = (byte)(255 - buffer[i]);

        return new Gray8LuminanceSource(inverted, Width, Height);
    }

    public override bool CropSupported => true;
    public override bool InversionSupported => true;
}
#endif
