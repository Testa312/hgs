using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace HGS
{
    class dd_dtw
    {
        dd_dtw()
        {
        }
        public const string dll_name = "dd_dtw.dll";

        public struct DTWSettings
        {
            public long window ;
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
        public double dtw_distance_ndim(double[] s1, double[] s2, int ndim, double max_dist = 0, bool use_pruning = false)
        {
            DTWSettings Dtws = new DTWSettings();
            Dtws.use_pruning = use_pruning;
            Dtws.max_dist = max_dist;
            return dtw_distance_ndim(s1, s1.LongLength, s2, s2.LongLength, ndim,Dtws);
        }
    }
}
