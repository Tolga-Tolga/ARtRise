using UnityEngine;

public static class Homography
{
    public static Matrix4x4 Compute(Vector2[] src, Vector2[] dst)
    {
        if (src.Length != 4 || dst.Length != 4)
            throw new System.Exception("Need exactly 4 points");

        float[,] A = new float[8, 8];
        float[] b = new float[8];

        for (int i = 0; i < 4; i++)
        {
            float x = src[i].x;
            float y = src[i].y;
            float X = dst[i].x;
            float Y = dst[i].y;

            int r = i * 2;

            A[r, 0] = x;  A[r, 1] = y;  A[r, 2] = 1;
            A[r, 6] = -x * X; A[r, 7] = -y * X;
            b[r] = X;

            A[r + 1, 3] = x; A[r + 1, 4] = y; A[r + 1, 5] = 1;
            A[r + 1, 6] = -x * Y; A[r + 1, 7] = -y * Y;
            b[r + 1] = Y;
        }

        float[] h = SolveWithPivoting(A, b);

        Matrix4x4 H = Matrix4x4.identity;
        H.m00 = h[0]; H.m01 = h[1]; H.m02 = h[2];
        H.m10 = h[3]; H.m11 = h[4]; H.m12 = h[5];
        H.m20 = h[6]; H.m21 = h[7]; H.m22 = 1f;

        return H;
    }

    private static float[] SolveWithPivoting(float[,] A, float[] b)
    {
        int n = 8;
        float[,] M = new float[n, n + 1];

        for (int r = 0; r < n; r++)
        {
            for (int c = 0; c < n; c++)
                M[r, c] = A[r, c];
            M[r, n] = b[r];
        }

        for (int i = 0; i < n; i++)
        {
            // 🔑 Pivot
            int maxRow = i;
            float maxVal = Mathf.Abs(M[i, i]);
            for (int r = i + 1; r < n; r++)
            {
                float v = Mathf.Abs(M[r, i]);
                if (v > maxVal)
                {
                    maxVal = v;
                    maxRow = r;
                }
            }

            if (maxVal < 1e-6f)
                throw new System.Exception("Degenerate homography");

            // swap
            if (maxRow != i)
                for (int c = i; c <= n; c++)
                    (M[i, c], M[maxRow, c]) = (M[maxRow, c], M[i, c]);

            float diag = M[i, i];
            for (int c = i; c <= n; c++)
                M[i, c] /= diag;

            for (int r = 0; r < n; r++)
            {
                if (r == i) continue;
                float f = M[r, i];
                for (int c = i; c <= n; c++)
                    M[r, c] -= f * M[i, c];
            }
        }

        float[] x = new float[n];
        for (int i = 0; i < n; i++)
            x[i] = M[i, n];

        return x;
    }
}
