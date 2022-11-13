using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGS
{
    public class DateValue
    {
        public DateValue(DateTime dt, float v)
        {
            Date = dt;
            Value = v;
        }
        public DateTime Date { get; set; }
        public float Value { get; set; }
    }
    public class PointData
    {
        //public int ID = -1;
        public float MaxAv = float.MinValue;
        public float MinAv = float.MaxValue;
        public float MeanAV = 0;
        public List<DateValue> data = new List<DateValue>();
    }
    static class SisConnect
    {
        public static OPAPI.Connect sisconn = null; //new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
            //Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        public static Dictionary<int, PointData> GetsisData(object[] keys, DateTime begin, DateTime end, int span = 1)
        {
            Dictionary<int, PointData> dic_data = new Dictionary<int, PointData>();
            if (keys.Length == 0) return dic_data;

            string[] colnames = new string[] { "ID", "TM", "DS", "AV" };

            Dictionary<string, object> options = new Dictionary<string, object>();
            span = (int)(end - begin).TotalSeconds / 120;
            options.Add("end", end);
            options.Add("begin", begin);
            options.Add("mode", "span");
            options.Add("interval", span);

            //object[] oo = {2053 };

            OPAPI.ResultSet resultSet = sisconn.select("Archive", colnames, keys, options);//options为条件            
            try
            {
                PointData PtData;
                while (resultSet.next())
                {
                    int id = resultSet.getInt(0);
                    if (!dic_data.TryGetValue(id, out PtData))
                    {
                        PtData = new PointData();
                        dic_data.Add(id, PtData);
                    }
                    PtData.data.Add(new DateValue(resultSet.getDateTime(1),resultSet.getFloat(3)));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
                //conn.close();
            }
            foreach (PointData dt in dic_data.Values)
            {
                double sum = 0;
                for (int m = 0; m < dt.data.Count; m++)
                {
                    dt.MaxAv = Math.Max(dt.MaxAv, dt.data[m].Value);
                    dt.MinAv = Math.Min(dt.MinAv, dt.data[m].Value);
                    sum += dt.data[m].Value;
                }
                dt.MeanAV = (float)sum / dt.data.Count;
            }
            return dic_data;
        }
        public static float GetDtw(PointData pd1, PointData pd2)
        {
            if (pd1.data.Count != pd2.data.Count)
                throw new ArgumentException("数组长度应相等！");
            double x2 = 0, y2 = 0, cost = 0; //矢量长度的平方。

            float[] x = new float[pd1.data.Count];
            float[] y = new float[pd1.data.Count];
            for (int i = 0; i < pd1.data.Count; i++)
            {
                x[i] = (pd1.data[i].Value - pd1.MinAv);
                y[i] = (pd2.data[i].Value - pd2.MinAv);
                x2 += x[i] * x[i];
                y2 += y[i] * y[i];
            }
            //向量模归一化.与z-normalization 比精度更高
            if (x2 > y2 && y2 >= 0.5)
            {
                float c = (float)Math.Sqrt(x2 / y2);
                for (int i = 0; i < x.Length; i++)
                {
                    y[i] *= c;
                }
                cost = Dtw.GetScoreF(x, y);
            }
            else if (y2 > x2 && x2 >= 0.5)
            {
                float c = (float)Math.Sqrt(y2 / x2);
                for (int i = 0; i < x.Length; i++)
                {
                    x[i] *= c;
                }
                cost = Dtw.GetScoreF(x, y);
            }
            return (float)Math.Sqrt(cost / x.Length);//均方差。
        }
        //dtaidistance.dtw
        public static double GetDtw_dd(PointData pd1, PointData pd2,double max_dist = 0, bool use_pruning = false)
        {
            if (pd1.data.Count != pd2.data.Count)
                throw new ArgumentException("数组长度应相等！");
            double x2 = 0, y2 = 0, cost = 0; //矢量长度的平方。

            double[] x = new double[pd1.data.Count];
            double[] y = new double[pd1.data.Count];
            for (int i = 0; i < pd1.data.Count; i++)
            {
                x[i] = (pd1.data[i].Value - pd1.MinAv);
                y[i] = (pd2.data[i].Value - pd2.MinAv);
                x2 += x[i] * x[i];
                y2 += y[i] * y[i];
            }
            //向量模归一化.与z-normalization 比精度更高
            if (x2 > y2 && y2 >= 0.5)
            {
                double c = Math.Sqrt(x2 / y2);
                for (int i = 0; i < x.Length; i++)
                {
                    y[i] *= c;
                }
                cost = dd_dtw.dtw_distance(x, y,max_dist,use_pruning);
            }
            else if (y2 > x2 && x2 >= 0.5)
            {
                double c = Math.Sqrt(y2 / x2);
                for (int i = 0; i < x.Length; i++)
                {
                    x[i] *= c;
                }
                cost = dd_dtw.dtw_distance(x, y,max_dist, use_pruning);
            }
            return cost;
        }
        //最小欧氏距离
        public static float GetED(PointData pd1, PointData pd2)
        {
            if (pd1.data.Count != pd2.data.Count)
                throw new ArgumentException("数组长度应相等！");
            double x2 = 0, y2 = 0, cost = 0; //矢量长度的平方。

            float[] x = new float[pd1.data.Count];
            float[] y = new float[pd1.data.Count];
            for (int i = 0; i < pd1.data.Count; i++)
            {
                x[i] = (pd1.data[i].Value - pd1.MinAv);
                y[i] = (pd2.data[i].Value - pd2.MinAv);
                x2 += x[i] * x[i];
                y2 += y[i] * y[i];
            }
            //用向量的模归一化
            if (x2 > y2 && y2 >= 0.1)
            {
                float c = (float)Math.Sqrt(x2 / y2);
                for (int i = 0; i < x.Length; i++)
                {
                    y[i] *= c;
                    double d = x[i] - y[i];
                    cost += d * d;
                }
            }
            else if (y2 > x2 && x2 >= 0.1)
            {
                float c = (float)Math.Sqrt(y2 / x2);
                for (int i = 0; i < x.Length; i++)
                {
                    x[i] *= c;
                    double d = x[i] - y[i];
                    cost += d * d;
                }
            }
            return (float)Math.Sqrt(cost);
        }
        public static double GetMain(Dictionary<int, PointData> dic_pd, double[] outmain)
        {
            int c = 0;
            foreach (PointData pd in dic_pd.Values)
            {
                for (int i = 0; i < pd.data.Count; i++)
                {
                    outmain[i] += pd.data[i].Value;
                }
                c++;
            }
            if (c == 0) throw new Exception("数据长度不能为0!");
            for (int i = 0; i < outmain.Length; i++)
            {
                outmain[i] /= c;
            }
           
            return 0;
        }
    }
}
