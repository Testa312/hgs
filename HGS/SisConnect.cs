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
        public int ID = -1;
        public int ID_sis = -1;
        public string GN = "";
        public string ED = "";
        public float MaxAv = float.MinValue;
        public float MinAv = float.MaxValue;
        public float MeanAV = 0;
        public List<DateValue> data = new List<DateValue>();
    }
    static class SisConnect
    {
        public static OPAPI.Connect sisconn = null; //new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
            //Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        public static Dictionary<int, PointData> GetsisData(object[] keys, DateTime begin, DateTime end,int count = 120)
        {
            Dictionary<int, PointData> dic_data = new Dictionary<int, PointData>();
            if (keys.Length == 0) return dic_data;

            string[] colnames = new string[] { "ID", "TM", "DS", "AV","GN" };

            Dictionary<string, object> options = new Dictionary<string, object>();
            int span = (int)((end - begin).TotalSeconds / count);
            span = span > 0 ? span : span++;
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
                        PtData.GN = resultSet.getString(4);
                        PtData.ID_sis = id;
                        dic_data.Add(id, PtData);
                    }
                    if (PtData.data.Count < count)
                        PtData.data.Add(new DateValue(resultSet.getDateTime(1), resultSet.getFloat(3)));
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
        public static Dictionary<int, PointData> GetPointData_dic(HashSet<int> hspid, DateTime begin, DateTime end, int count = 120)
        {
            Dictionary<int, PointData> dic_pd = new Dictionary<int, PointData>();
            if (hspid != null && hspid.Count > 0)
            {
                HashSet<object> sisid = new HashSet<object>();
                List<point> calcpt = new List<point>();
                foreach (int id in hspid)
                {
                    point pt = Data.inst().cd_Point[id];
                    if (pt.pointsrc == pointsrc.sis)
                        sisid.Add(Convert.ToInt64(pt.id_sis));
                    else if (pt.pointsrc == pointsrc.calc)
                        calcpt.Add(pt);
                }
                Dictionary<int, PointData>  dic_pd_sis = SisConnect.GetsisData(sisid.ToArray(), begin, end, count);
                foreach (PointData pd in dic_pd_sis.Values)
                {
                    point pt = Data.inst().dic_SisIdtoPoint[pd.ID_sis];
                    pd.ED =pt.ed;
                    pd.ID = pt.id;
                    dic_pd.Add(pd.ID, pd);
                }

                foreach (point pt in calcpt)
                {
                    PointData pdcalc = SisConnect.GetCalcPointData(pt, begin, end, count);
                    pdcalc.ED = pt.ed;
                    dic_pd.Add(pdcalc.ID, pdcalc);
                }
            }
            return dic_pd;
        }
        public static PointData GetCalcPointData(point calcpt, DateTime begin, DateTime end, int count = 120)
        {
            if (calcpt.pointsrc != pointsrc.calc)
                throw new ArgumentException("必须为计算点！");

            List<object> siskeys = new List<object>();
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            foreach (point sispt in calcpt.listSisCalaExpPointID_main)
            {
                siskeys.Add(Convert.ToInt64(sispt.id_sis));
            }
            //
            Dictionary<int, PointData> data = GetsisData(siskeys.ToArray(), begin, end, count);
            int c = 0;
            if (data.Keys.Count >= 1)
            {
                PointData pt = data[Convert.ToInt32(siskeys[0])];
                c = pt.data.Count;
            }
            PointData newpt = new PointData();
            newpt.GN = calcpt.ed;
            newpt.ID = calcpt.id;
            //string sisformula = Data.inst().ExpandOrgFormula_Main(calcpt);
            for (int i = 0; i < c; i++)
            {
                DateValue dv = new DateValue(DateTime.Now,0);
                foreach (KeyValuePair<int, PointData> kvp in data)
                {
                    ce.Variables["S" + Data.inst().dic_SisIdtoPoint[kvp.Key].id.ToString()] = kvp.Value.data[i].Value;
                    dv.Date = kvp.Value.data[i].Date;
                }
                if (calcpt.sisformula_main.Length > 0)
                    dv.Value = (float)Convert.ToDouble(ce.Evaluate(calcpt.sisformula_main));
                newpt.data.Add(dv);
            }
            double sum = 0;
            for (int m = 0; m < newpt.data.Count; m++)
            {
                newpt.MaxAv = Math.Max(newpt.MaxAv, newpt.data[m].Value);
                newpt.MinAv = Math.Min(newpt.MinAv, newpt.data[m].Value);
                sum += newpt.data[m].Value;
            }
            newpt.MeanAV = (float)sum / newpt.data.Count;

            return newpt;
        }
            public static float GetFastDtw(PointData pd1, PointData pd2)
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
        //用差分归一化
        public static double GetDtw_dd_diff(PointData pd1, PointData pd2, double max_dist = 0, bool use_pruning = false)
        {
            if (pd1.data.Count != pd2.data.Count)
                throw new ArgumentException("数组长度应相等！");

            double[] x = new double[pd1.data.Count];
            double[] y = new double[pd1.data.Count];
            x[0] = y[0] = 0;
            for (int i = 1; i < pd1.data.Count; i++)
            {
                x[i] = pd1.data[i].Value - pd1.data[i - 1].Value;
                y[i] = pd2.data[i].Value - pd2.data[i - 1].Value;
            }
            return dd_dtw.dtw_distance(x, y, max_dist, use_pruning); ;
        }
        public static DateTime GetSisSystemTime()
        {
            DateTime sysdt = DateTime.Now.AddSeconds(-120);
            string sql = "select TM from Realtime where ID in (2053,9092,220865)";
            bool flag = false;

            OPAPI.ResultSet resultSet = SisConnect.sisconn.executeQuery(sql);//执行SQL
            try
            {
                while (resultSet.next())//next()执行一次，游标下移一行
                {
                    sysdt = resultSet.getDateTime(0);
                    flag = true;
                }
                if (!flag) throw new Exception("没有得到sis系统时间！");
            }
            catch (Exception ee)
            {
                throw new Exception("取点系统时间出错！" + ee.ToString());

            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
            }
            return sysdt;
        }
    }
}
