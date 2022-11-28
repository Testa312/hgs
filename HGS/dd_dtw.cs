using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace HGS
{
    public struct DTWSettings
    {
        public long window;
        public double max_dist;
        public double max_step;
        public long max_length_diff;
        public double penalty;
        public long psi_1b;  // series 1, begin psi
        public long psi_1e;  // series 1, end psi
        public long psi_2b;
        public long psi_2e;
        public bool use_pruning;
        public bool only_ub;
    };
    class dd_dtw
    {
        dd_dtw()
        {
        }
        public const string dll_name = "dd_dtw.dll";

       

        [DllImport(dll_name, EntryPoint = "dtw_distance")]
        public static extern double dtw_distance(double[] s1,long l1,double[] s2,long l2, DTWSettings settings);
        [DllImport(dll_name, EntryPoint = "dtw_distance_ndim")]
        public static extern double dtw_distance_ndim(double[] s1, long l1, double[] s2, long l2,int ndim, DTWSettings settings);
        //
        public static double dtw_distance(double[] s1, double[] s2,double max_dist = 0 , bool use_pruning = false)
        {
            DTWSettings Dtws = new DTWSettings();
            Dtws.use_pruning = use_pruning;
            Dtws.max_dist = max_dist;
            return dtw_distance(s1, s1.LongLength, s2, s2.LongLength, Dtws);
        }
        public static double dtw_distance_ndim(double[] s1, double[] s2, int ndim, double max_dist = 0, bool use_pruning = false)
        {
            DTWSettings Dtws = new DTWSettings();
            Dtws.use_pruning = use_pruning;
            Dtws.max_dist = max_dist;
            return dtw_distance_ndim(s1, s1.LongLength, s2, s2.LongLength, ndim,Dtws);
        }
        //CS版,比C要慢20%------------------------------
        public static double  dtw_distance_new(double[] s1, long l1,
                      double[] s2, long l2,
                      DTWSettings settings)
        {
            Debug.Assert(settings.psi_1b < l1 && settings.psi_1e < l1 &&
                   settings.psi_2b < l2 && settings.psi_2e < l2);
            long ldiff;
            long dl;
            // DTWPruned
            long sc = 0;
            long ec = 0;
            bool smaller_found;
            long ec_next;
            // signal(SIGINT, dtw_int_handler); // not compatible with OMP

            long window = settings.window;
            double max_step = settings.max_step;
            double max_dist = settings.max_dist;
            double penalty = settings.penalty;

            if (settings.use_pruning || settings.only_ub)
            {
                max_dist = Math.Pow(ub_euclidean(s1, l1, s2, l2), 2) + 1e-5;
                if (settings.only_ub)
                {
                    return max_dist;
                }
            }
            else if (max_dist == 0)
            {
                max_dist = double.MaxValue;
            }
            else
            {
                max_dist = Math.Pow(max_dist, 2);
            }
            if (l1 > l2)
            {
                ldiff = l1 - l2;
                dl = ldiff;
            }
            else
            {
                ldiff = l2 - l1;
                dl = 0;
            }
            if (settings.max_length_diff != 0 && ldiff > settings.max_length_diff)
            {
                return double.MaxValue;
            }
            if (window == 0)
            {
                window = Math.Max(l1, l2);
            }
            if (max_step == 0)
            {
                max_step = double.MaxValue;
            }
            else
            {
                max_step = Math.Pow(max_step, 2);
            }
            penalty = Math.Pow(penalty, 2);
            // rows is for series 1, columns is for series 2
            long length = Math.Min(l2 + 1, ldiff + 2 * window + 1);
            Debug.Assert(length > 0);
            double[] dtw = new double[length * 2];

            long i;
            long j;
            for (j = 0; j < length * 2; j++)
            {
                dtw[j] = double.MaxValue;
            }
            // Deal with psi-relaxation in first row
            for (i = 0; i < settings.psi_2b + 1; i++)
            {
                dtw[i] = 0;
            }
            long skip = 0;
            long skipp = 0;
            long i0 = 1;
            long i1 = 0;
            long minj;
            long maxj = 0;
            long curidx = 0;
            long dl_window = dl + window - 1;
            long ldiff_window = window;
            if (l2 > l1)
            {
                ldiff_window += ldiff;
            }
            double minv;
            double d;
            double tempv;
            double psi_shortest = double.MaxValue;
            // keepRunning = 1;
            for (i = 0; i < l1; i++)
            {
                // if (!keepRunning){  // not compatible with OMP
                //     free(dtw);
                //     printf("Stop computing DTW...\n");
                //     return double.MaxValue;
                // }
                // maxj = i;
                // if (maxj > dl_window) {
                //     maxj -= dl_window;
                // } else {
                //     maxj = 0;
                // }
                maxj = (i - dl_window) * Convert.ToInt32((i > dl_window));
                // No risk for overflow/modulo because we also need to store dtw of size
                // MIN(l2+1, ldiff + 2*window + 1) ?
                minj = i + ldiff_window;
                if (minj > l2)
                {
                    minj = l2;
                }
                skipp = skip;
                skip = maxj;
                i0 = 1 - i0;
                i1 = 1 - i1;
                // Reset new line i1
                for (j = 0; j < length; j++)
                {
                    dtw[length * i1 + j] = double.MaxValue;
                }
                // if (length == l2 + 1) {
                //     skip = 0;
                // }
                skip = skip * Convert.ToInt32(length != l2 + 1);
                // PrunedDTW
                if (sc > maxj)
                {
                    maxj = sc;
                }
                smaller_found = false;
                ec_next = i;
                // Deal with psi-relaxation in first column
                if (settings.psi_1b != 0 && maxj == 0 && i < settings.psi_1b)
                {
                    dtw[i1 * length + 0] = 0;
                }
                for (j = maxj; j < minj; j++)
                {
                    d = (s1[i] - s2[j]) * (s1[i] - s2[j]);
                    if (d > max_step)
                    {
                        // Let the value be double.MaxValue as initialized
                        continue;
                    }
                    curidx = i0 * length + j - skipp;
                    minv = dtw[curidx];
                    curidx += 1;
                    tempv = dtw[curidx] + penalty;
                    if (tempv < minv)
                    {
                        minv = tempv;
                    }
                    curidx = i1 * length + j - skip;
                    tempv = dtw[curidx] + penalty;
                    if (tempv < minv)
                    {
                        minv = tempv;
                    }
                    curidx += 1;
                    dtw[curidx] = d + minv;

                    // PrunedDTW
                    if (dtw[curidx] > max_dist)
                    {
                        if (!smaller_found)
                        {
                            sc = j + 1;
                        }
                        if (j >= ec)
                        {
                            break;
                        }
                    }
                    else
                    {
                        smaller_found = true;
                        ec_next = j + 1;
                    }
                }
                ec = ec_next;
                // Deal with Psi-relaxation in last column
                if (settings.psi_1e != 0 && minj == l2 && l1 - 1 - i <= settings.psi_1e)
                {
                    Debug.Assert((i1 + 1) * length - 1 == curidx);
                    if (dtw[curidx] < psi_shortest)
                    {
                        // curidx is the last value
                        psi_shortest = dtw[curidx];
                    }
                }

            }
            if (window - 1 < 0)
            {
                l2 += window - 1;
            }
            double result = Math.Sqrt(dtw[length * i1 + l2 - skip]);
            // Deal with psi-relaxation in the last row
            if (settings.psi_2e != 0)
            {
                for (i = l2 - skip - settings.psi_2e; i < l2 - skip + 1; i++)
                { // iterate over vci
                    if (dtw[i1 * length + i] < psi_shortest)
                    {
                        psi_shortest = dtw[i1 * length + i];
                    }
                }
                result = Math.Sqrt(psi_shortest);
            }
            // signal(SIGINT, SIG_DFL);  // not compatible with OMP
            if (settings.max_dist != 0 && result > settings.max_dist)
            {
                // DTWPruned keeps the last value larger than max_dist. Correct for this.
                result = double.MaxValue;
            }
            return result;
        }
        static double ub_euclidean(double[] s1, long l1, double[] s2, long l2)
        {
            long n = Math.Min(l1, l2);
            double ub = 0;
            for (long i = 0; i < n; i++)
            {
                //ub += EDIST(s1[i], s2[i]);
                ub += (s1[i] - s2[i]) * (s1[i] - s2[i]);
            }
            // If the two series differ in length, compare the last element of the shortest series
            // to the remaining elements in the longer series
            if (l1 > l2)
            {
                for (long i = n; i < l1; i++)
                {
                    //ub += EDIST(s1[i], s2[n - 1]);
                    ub += (s1[i] - s2[n-1]) * (s1[i] - s2[n-1]);
                }
            }
            else if (l1 < l2)
            {
                for (long i = n; i < l2; i++)
                {
                    //ub += EDIST(s1[n - 1], s2[i]);
                    ub += (s1[n-1] - s2[i]) * (s1[n-1] - s2[i]);
                }
            }
            return Math.Sqrt(ub);
        }


    }
}
