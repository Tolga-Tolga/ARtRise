using ZXing;

public class ByteArrayLuminanceSource : LuminanceSource
{
    private readonly byte[] _luminances;

    public ByteArrayLuminanceSource(byte[] luminances, int width, int height)
        : base(width, height)
    {
        _luminances = luminances;
    }

    public override byte[] Matrix => _luminances;

    public override byte[] getRow(int y, byte[] row)
    {
        int width = Width;
        if (row == null || row.Length < width)
            row = new byte[width];

        System.Buffer.BlockCopy(_luminances, y * width, row, 0, width);
        return row;
    }
}
