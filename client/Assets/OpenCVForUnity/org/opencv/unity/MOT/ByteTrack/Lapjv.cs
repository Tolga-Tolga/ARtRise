using System;
using System.Buffers;

namespace OpenCVForUnity.UnityIntegration.MOT.ByteTrack
{
    internal static class Lapjv
    {
        private const int LARGE = 1000000;

        private enum FpType
        {
            FP_1 = 1,
            FP_2 = 2,
            FP_DYNAMIC = 3
        }

        /// <remarks>
        ///  Column-reduction and reduction transfer for a dense cost matrix.
        /// </remarks>
        private static int CcrrtDense(int n, float[] cost, int[] free_rows,
                                         int[] x, int[] y, double[] v)
        {
            int n_free_rows;
#if NET_STANDARD_2_1
            bool[] unique = ArrayPool<bool>.Shared.Rent(n);
#else
            bool[] unique = new bool[n];
#endif
            try
            {
                // Initialize
                Array.Fill(x, -1, 0, n);
                Array.Fill(v, LARGE, 0, n);
                Array.Fill(y, 0, 0, n);
                Array.Fill(unique, true, 0, n);

                // Process the cost matrix
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double c = cost[i * n + j];
                        if (c < v[j])
                        {
                            v[j] = c;
                            y[j] = i;
                        }
                    }
                }

                // Column reduction
                for (int j = n - 1; j >= 0; j--)
                {
                    int i = y[j];
                    if (x[i] < 0)
                    {
                        x[i] = j;
                    }
                    else
                    {
                        unique[i] = false;
                        y[j] = -1;
                    }
                }

                // Process free rows
                n_free_rows = 0;
                for (int i = 0; i < n; i++)
                {
                    if (x[i] < 0)
                    {
                        free_rows[n_free_rows++] = i;
                    }
                    else if (unique[i])
                    {
                        int j = x[i];
                        double min = LARGE;
                        for (int j2 = 0; j2 < n; j2++)
                        {
                            if (j2 == j)
                            {
                                continue;
                            }
                            double c = cost[i * n + j2] - v[j2];
                            if (c < min)
                            {
                                min = c;
                            }
                        }
                        v[j] -= min;
                    }
                }

                return n_free_rows;
            }
            finally
            {
#if NET_STANDARD_2_1
                ArrayPool<bool>.Shared.Return(unique);
#endif
            }
        }

        /// <remarks>
        ///  Augmenting row reduction for a dense cost matrix.
        /// </remarks>
        private static int CarrDense(int n, float[] cost, int n_free_rows,
                                  int[] free_rows, int[] x, int[] y,
                                  double[] v)
        {
            int current = 0;
            int new_free_rows = 0;
            int rr_cnt = 0;

            while (current < n_free_rows)
            {
                int i0;
                int j1, j2;
                double v1, v2, v1_new;
                bool v1_lowers;

                rr_cnt++;
                int free_i = free_rows[current++];
                j1 = 0;
                v1 = cost[free_i * n] - v[0];
                j2 = -1;
                v2 = LARGE;

                for (int j = 1; j < n; j++)
                {
                    double c = cost[free_i * n + j] - v[j];
                    if (c < v2)
                    {
                        if (c >= v1)
                        {
                            v2 = c;
                            j2 = j;
                        }
                        else
                        {
                            v2 = v1;
                            v1 = c;
                            j2 = j1;
                            j1 = j;
                        }
                    }
                }

                i0 = y[j1];
                v1_new = v[j1] - (v2 - v1);
                v1_lowers = v1_new < v[j1];

                if (rr_cnt < current * n)
                {
                    if (v1_lowers)
                    {
                        v[j1] = v1_new;
                    }
                    else if (i0 >= 0 && j2 >= 0)
                    {
                        j1 = j2;
                        i0 = y[j2];
                    }

                    if (i0 >= 0)
                    {
                        if (v1_lowers)
                        {
                            free_rows[--current] = i0;
                        }
                        else
                        {
                            free_rows[new_free_rows++] = i0;
                        }
                    }
                }
                else
                {
                    if (i0 >= 0)
                    {
                        free_rows[new_free_rows++] = i0;
                    }
                }

                x[free_i] = j1;
                y[j1] = free_i;
            }

            return new_free_rows;
        }

        /// <remarks>
        ///  Find columns with minimum d[j] and put them on the SCAN list.
        /// </remarks>
        private static int FindDense(int n, int lo, double[] d, int[] cols)
        {
            int hi = lo + 1;
            double mind = d[cols[lo]];

            for (int k = hi; k < n; k++)
            {
                int j = cols[k];
                if (d[j] <= mind)
                {
                    if (d[j] < mind)
                    {
                        hi = lo;
                        mind = d[j];
                    }
                    cols[k] = cols[hi];
                    cols[hi++] = j;
                }
            }

            return hi;
        }

        // Scan all columns in TODO starting from arbitrary column in SCAN
        // and try to decrease d of the TODO columns using the SCAN column.
        private static int ScanDense(int n, float[] cost, ref int plo, ref int phi,
                          double[] d, int[] cols, int[] pred,
                          int[] y, double[] v)
        {
            int lo = plo;
            int hi = phi;
            double h, cred_ij;

            while (lo != hi)
            {
                int j = cols[lo++];
                int i = y[j];
                double mind = d[j];
                h = cost[i * n + j] - v[j] - mind;
                // For all columns in TODO
                for (int k = hi; k < n; k++)
                {
                    j = cols[k];
                    cred_ij = cost[i * n + j] - v[j] - h;
                    if (cred_ij < d[j])
                    {
                        d[j] = cred_ij;
                        pred[j] = i;
                        if (cred_ij == mind)
                        {
                            if (y[j] < 0)
                            {
                                return j;
                            }
                            cols[k] = cols[hi];
                            cols[hi++] = j;
                        }
                    }
                }
            }

            plo = lo;
            phi = hi;
            return -1;
        }

        /// <remarks>
        ///  Single iteration of modified Dijkstra shortest path algorithm as explained
        ///    in the JV paper.
        ///
        ///    This is a dense matrix version.
        ///
        ///    \return The closest free column index.
        /// </remarks>
        private static int FindPathDense(int n, float[] cost, int start_i,
                                      int[] y, double[] v, int[] pred)
        {
            int lo = 0, hi = 0;
            int final_j = -1;
            int n_ready = 0;
#if NET_STANDARD_2_1
            int[] cols = ArrayPool<int>.Shared.Rent(n);
            double[] d = ArrayPool<double>.Shared.Rent(n);
#else
            int[] cols = new int[n];
            double[] d = new double[n];
#endif
            try
            {
                for (int i = 0; i < n; i++)
                {
                    cols[i] = i;
                }
                Array.Fill(pred, start_i, 0, n);
                for (int i = 0; i < n; i++)
                {
                    d[i] = cost[start_i * n + i] - v[i];
                }

                while (final_j == -1)
                {
                    // No columns left on the SCAN list.
                    if (lo == hi)
                    {
                        n_ready = lo;
                        hi = FindDense(n, lo, d, cols);
                        for (int k = lo; k < hi; k++)
                        {
                            int j = cols[k];
                            if (y[j] < 0)
                            {
                                final_j = j;
                            }
                        }
                    }
                    if (final_j == -1)
                    {
                        final_j = ScanDense(n, cost, ref lo, ref hi, d, cols, pred, y, v);
                    }
                }

                double mind = d[cols[lo]];
                for (int k = 0; k < n_ready; k++)
                {
                    int j = cols[k];
                    v[j] += d[j] - mind;
                }

                return final_j;
            }
            finally
            {
#if NET_STANDARD_2_1
                ArrayPool<int>.Shared.Return(cols);
                ArrayPool<double>.Shared.Return(d);
#endif
            }
        }

        /// <remarks>
        ///  Augment for a dense cost matrix.
        /// </remarks>
        private static int CaDense(int n, float[] cost, int n_free_rows,
                                int[] free_rows, int[] x, int[] y,
                                double[] v)
        {
#if NET_STANDARD_2_1
            int[] pred = ArrayPool<int>.Shared.Rent(n);
#else
            int[] pred = new int[n];
#endif

            try
            {
                for (int row_n = 0; row_n < n_free_rows; ++row_n)
                {
                    int free_row = free_rows[row_n];
                    int i = -1;
                    int k = 0;

                    int j = FindPathDense(n, cost, free_row, y, v, pred);
                    if (j < 0)
                    {
                        throw new Exception("Error occured in _ca_dense(): j < 0");
                    }
                    if (j >= n)
                    {
                        throw new Exception("Error occured in _ca_dense(): j >= n");
                    }
                    while (i != free_row)
                    {
                        i = pred[j];
                        y[j] = i;
                        int temp = x[i];
                        x[i] = j;
                        j = temp;
                        k++;
                        if (k >= n)
                        {
                            throw new Exception("Error occured in _ca_dense(): k >= n");
                        }
                    }
                }
                return 0;
            }
            finally
            {
#if NET_STANDARD_2_1
                ArrayPool<int>.Shared.Return(pred);
#endif
            }
        }

        /// <remarks>
        ///  Solve dense sparse LAP.
        /// </remarks>
        public static int LapjvInternal(int n, float[] cost, int[] x, int[] y)
        {
            int ret;
#if NET_STANDARD_2_1
            int[] free_rows = ArrayPool<int>.Shared.Rent(n);
            double[] v = ArrayPool<double>.Shared.Rent(n);
#else
            int[] free_rows = new int[n];
            double[] v = new double[n];
#endif
            try
            {
                ret = CcrrtDense(n, cost, free_rows, x, y, v);
                int i = 0;
                while (ret > 0 && i < 2)
                {
                    ret = CarrDense(n, cost, ret, free_rows, x, y, v);
                    i++;
                }
                if (ret > 0)
                {
                    ret = CaDense(n, cost, ret, free_rows, x, y, v);
                }

                return ret;
            }
            finally
            {
#if NET_STANDARD_2_1
                ArrayPool<int>.Shared.Return(free_rows);
                ArrayPool<double>.Shared.Return(v);
#endif
            }
        }
    }
}
