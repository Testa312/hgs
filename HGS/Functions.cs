using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGS
{
    static class Functions
    {
        public  static double? CasttoDouble(object ob)
        {
            if (ob == DBNull.Value)
                return null;
            else
                return (double)ob;
        }
        //返回大于n的素数。
        public static int AbovePrimes(int n)
        {
            int i = (n % 2) == 0 ? ++n : n + 2;
            int j = 0;
            for (; i <= 100003; i = i + 2)
            {
                int k = (int)Math.Sqrt(i);
                for (j = 2; j <= k; j++)
                {
                    if ((i % j) == 0)
                    {
                        break;
                    }
                }

                if (j > k)
                {
                    return i;
                }
            }
            //最大100003
            return 100003;
        }
        public static string dtoNULL(double? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }
        public static string dtoNULL(float? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }
        public static  double? NullDoubleRount(double? d, int fm)
        {
            double? r = null;
            if (d != null)
            {
                double dAV = d ?? 0;
                r = Math.Round(dAV, fm);
            }
            return r;
        }
        public static float? NullFloatRount(float? d, int fm)
        {
            float? r = null;
            if (d != null)
            {
                float dAV = d ?? 0;
                r = (float)Math.Round(dAV, fm);
            }
            return r;
        }
        public static HashSet<point> set_idtopoint(HashSet<int> sid)
        {
            HashSet<point> hsp = new HashSet<point>();
            if (sid != null)
            {
                foreach (int id in sid)
                {
                    point pt;
                    if (Data.inst().cd_Point.TryGetValue(id, out pt))
                    {
                        hsp.Add(pt);
                    }
                }
            }
            return hsp;
        }
        public static PointState GetCalcPointState(List<point> hsp)
        {
            PointState ps = PointState.Good;
            if (hsp != null)
            {
                foreach (point pt in hsp)
                {
                    if (pt.ps == PointState.Bad || pt.ps == PointState.Timeout)
                    {
                        ps = PointState.Error;
                        break;
                    }
                }
            }
            return ps;
        }
    }
}
