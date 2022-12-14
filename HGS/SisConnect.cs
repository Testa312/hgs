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
        public string EU = "";
        public float MaxAv = float.MinValue;
        public float MinAv = float.MaxValue;
        public float MeanAV = 0;
        public float DifAV = 0;
        public List<DateValue> data = new List<DateValue>();
    }
    static class SisConnect
    {
        //public static OPAPI.Connect sisconn = null;
        public static OPAPI.Connect siscon_keep = null;//主连接由FormMain使用，负责建立、保活和销毁。
        private static Dictionary<int, PointData> GetsisData(OPAPI.Connect sisconn,object[] keys, 
            DateTime begin, DateTime end,int count = 100,int span = -1)
        {
            Dictionary<int, PointData> dic_data = new Dictionary<int, PointData>();
            if (keys.Length == 0) return dic_data;

            string[] colnames = new string[] { "ID", "TM", "DS", "AV","GN" };

            Dictionary<string, object> options = new Dictionary<string, object>();
            if (span <= 0)
            {
                span = (int)((end - begin).TotalSeconds / count);
                span = span <= 0 ? 1 : span;
                //span = span > 0 ? span : span++;
            }
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
                    if (count<=0 || PtData.data.Count < count)
                        PtData.data.Add(new DateValue(resultSet.getDateTime(1), resultSet.getFloat(3)));
                }
            }
            catch (Exception e)
            {
                string sise = string.Format("Sis连接出错！:\rsisconn:isAlive{0}\r{1}", sisconn.isAlive(),
                    sisconn.ToString());
                FormBugReport.ShowBug(e,sise);
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
        public static Dictionary<int, PointData> GetPointData_dic(OPAPI.Connect sisconn,HashSet<point> hsPoint, DateTime begin, 
            DateTime end, int count = 100 ,int span = -1)
        {
            Dictionary<int, PointData> dic_pd = new Dictionary<int, PointData>();
            if (hsPoint != null && hsPoint.Count > 0)
            {
                HashSet<object> sisid = new HashSet<object>();
                List<point> calcpt = new List<point>();
                foreach (point pt in hsPoint)
                {
                    //point pt = Data.inst().cd_Point[id];
                    if (pt.pointsrc == pointsrc.sis)
                        sisid.Add(Convert.ToInt64(pt.Id_sis));
                    else if (pt.pointsrc == pointsrc.calc)
                        calcpt.Add(pt);
                }

                Dictionary<int, PointData> dic_pd_sis = SisConnect.GetsisData(sisconn, sisid.ToArray(), begin, end, count, span);
                foreach (PointData pd in dic_pd_sis.Values)
                {
                    point pt = Data.inst().dic_SisIdtoPoint[pd.ID_sis];
                    pd.ED = pt.ed;
                    pd.ID = pt.id;
                    pd.EU = pt.eu;
                    dic_pd.Add(pd.ID, pd);
                }

                foreach (point pt in calcpt)
                {
                    PointData pdcalc = SisConnect.GetCalcPointData(sisconn,pt, begin, end, count);
                    pdcalc.ED = pt.ed;
                    pdcalc.EU = pt.eu;
                    dic_pd.Add(pdcalc.ID, pdcalc);
                }
            }
            return dic_pd;
        }
        private static PointData GetCalcPointData(OPAPI.Connect sisconn,point calcpt, DateTime begin, DateTime end, int count = 100,int span = -1)
        {
            if (calcpt.pointsrc != pointsrc.calc)
                throw new ArgumentException("必须为计算点！");

            List<object> siskeys = new List<object>();
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            foreach (point sispt in calcpt.listSisCalcExpPointID_main)
            {
                siskeys.Add(Convert.ToInt64(sispt.Id_sis));
            }
            //
            Dictionary<int, PointData> data = GetsisData(sisconn,siskeys.ToArray(), begin, end, count,span);
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
                if (calcpt.Sisformula_main.Length > 0)
                    dv.Value = (float)Convert.ToDouble(ce.Evaluate(calcpt.Sisformula_main));
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
        //返回值，设备ID，对应时段内一直满足报警备件
        private static HashSet<int>  GetDeiveAlarmStat(OPAPI.Connect sisconn, DateTime begin,DateTime end,int count,int span)
        {
            List<object> siskeys = new List<object>();
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            HashSet<int> hash_rsl = new HashSet<int>();
            foreach (DeviceInfo di in Data_Device.dic_Device.Values)
            {
                siskeys.Clear();

                if (di._listSisCalaExpPointID_If != null)
                {
                    foreach (point sispt in di._listSisCalaExpPointID_If)
                    {
                        siskeys.Add(Convert.ToInt64(sispt.Id_sis));
                    }
                    //
                    Dictionary<int, PointData> data = GetsisData(sisconn, siskeys.ToArray(), begin, end, count, span);
                    int c = 0;
                    if (data.Keys.Count >= 1)
                    {
                        PointData pt = data[Convert.ToInt32(siskeys[0])];
                        c = pt.data.Count;
                    }
                    //
                    bool rsl = true;
                    for (int i = 0; i < c; i++)
                    {
                        foreach (KeyValuePair<int, PointData> kvp in data)
                        {
                            ce.Variables["S" + Data.inst().dic_SisIdtoPoint[kvp.Key].id.ToString()] = kvp.Value.data[i].Value;
                        }
                        if (di.SisPointFormula_if.Length > 0 &&
                            !Convert.ToBoolean(ce.Evaluate(di.SisPointFormula_if)))
                        {
                            rsl = false;
                            break;
                        }
                    }
                    if (rsl) hash_rsl.Add(di.id);
                }
                else
                    hash_rsl.Add(di.id);

            }
            return hash_rsl;
        }
        public static void InitPointDtwQueues(Dictionary<int, DeviceInfo> dic_dev)
        {
            if (dic_dev == null)
                throw new ArgumentException("初始化队列的点列表不能为空！");
            int[] ScanSpan = new int[6] { 18, 36, 72, 144, 288, 576 };
            HashSet<point> lsob = new HashSet<point>();
            foreach (DeviceInfo di in dic_dev.Values)
            {
                foreach (int ptid in di.Sensors_set())
                {
                    point pt;
                    if (Data.inst().cd_Point.TryGetValue(ptid, out pt))
                    {
                        lsob.Add(pt);
                    }
                }
            }
            HashSet<point> lsrunob = new HashSet<point>();
            DateTime end = GetSisSystemTime(siscon_keep).AddSeconds(-5);
            for (int i = 0; i < ScanSpan.Length; i++)
            {
                //为了系统快速启动，统一为最长延时1小时，可能对近期启动设备投入时间延长。
                DateTime begin = end.AddMinutes(-ScanSpan[i] - 60);
                //
                HashSet<int> rsl = GetDeiveAlarmStat(siscon_keep, begin, end, 0, ScanSpan[i] / 2);
                //
                lsrunob.Clear();
                foreach (point pt in lsob)
                {
                    //foreach (int did in pt.Device_set())
                    {
                        if (rsl.Contains(pt.DeviceId))
                            lsrunob.Add(pt);
                        else
                        {
                            pt.initDeviceQ();
                        }
                    }
                }
               //
                Dictionary<int, PointData> dic_pd = GetPointData_dic(siscon_keep, lsrunob, begin, end, 0, ScanSpan[i] / 2);
                foreach (PointData pd in dic_pd.Values)
                {
                    float[] x = new float[pd.data.Count];
                    for (int m = 0; m < pd.data.Count; m++)
                    {
                        x[m] = pd.data[pd.data.Count - 1 - m].Value;
                    }
                    point pt;
                    if (Data.inst().cd_Point.TryGetValue(pd.ID, out pt))
                    {
                        pt.initDeviceQ(i, x);
                    }
                }
            }

        }
        private static bool GetSensorAlarmStat(OPAPI.Connect sisconn, point Point,DateTime begin, DateTime end, int count, int span)
        {
            List<object> siskeys = new List<object>();
            CalcEngine.CalcEngine ce = new CalcEngine.CalcEngine();
            // siskeys.Clear();
            if (Point.listSisCalcExpPointID_alarmif != null)
            {
                foreach (point sispt in Point.listSisCalcExpPointID_alarmif)
                {
                    siskeys.Add(Convert.ToInt64(sispt.Id_sis));
                }
                //
                Dictionary<int, PointData> data = GetsisData(sisconn, siskeys.ToArray(), begin, end, count, span);
                int c = 0;
                if (data.Keys.Count >= 1)
                {
                    PointData pt = data[Convert.ToInt32(siskeys[0])];
                    c = pt.data.Count;
                }
                for (int i = 0; i < c; i++)
                {
                    foreach (KeyValuePair<int, PointData> kvp in data)
                    {
                        ce.Variables["S" + Data.inst().dic_SisIdtoPoint[kvp.Key].id.ToString()] = kvp.Value.data[i].Value;
                    }
                    if (Point.Sisformula_Alarmif.Length > 0 &&
                        !Convert.ToBoolean(ce.Evaluate(Point.Sisformula_Alarmif)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static void InitSensorsWaveQueues(Dictionary<int, point> dic_pt)
        {

            if (dic_pt == null)
                throw new ArgumentException("初始化队列的点列表不能为空！");
            int[] ScanSpan = new int[7] { 120, 240, 480, 960, 1920, 3840,7680 };//秒数
            HashSet<point> lsob = new HashSet<point>();

            DateTime end = GetSisSystemTime(siscon_keep).AddSeconds(-5);
            for (int i = 0; i < ScanSpan.Length; i++)
            {
                //为了系统快速启动，统一为最长延时1小时，可能对近期启动设备投入时间延长。
                DateTime begin = end.AddSeconds(-ScanSpan[i] - 3600);
                //
                lsob.Clear();
                foreach (point pt in dic_pt.Values)
                {
                    if (GetSensorAlarmStat(siscon_keep, pt, begin, end, 0, ScanSpan[i] / 120))
                    {
                        lsob.Add(pt);
                    }
                }
                //
                Dictionary<int, PointData> dic_pd = GetPointData_dic(siscon_keep, lsob, begin, end, 0, ScanSpan[i] / 120);
                foreach (PointData pd in dic_pd.Values)
                {
                    float[] x = new float[pd.data.Count];
                    for (int m = 0; m < pd.data.Count; m++)
                    {
                        x[m] = pd.data[pd.data.Count - 1 - m].Value;
                    }
                    dic_pt[pd.ID].initWave3sQ(i, x);
                }
            }

        }
        public static Dictionary<int, PointData> GetsisStat(OPAPI.Connect sisconn,HashSet<point> hsPoint, DateTime begin, DateTime end,int span)
        {
            //
            Dictionary<int, PointData> dic_data_stat = new Dictionary<int, PointData>();
            if (hsPoint.Count == 0 || hsPoint == null) return dic_data_stat;
            //

            HashSet<object> sisid = new HashSet<object>();
            foreach (point pt in hsPoint)
            {
                if (pt.pointsrc == pointsrc.sis)
                    sisid.Add(Convert.ToInt64(pt.Id_sis));
            }
            if (sisid.Count <= 0) return dic_data_stat;
            string[] colnames = new string[] { "ID", "MAXV", "MINV" };

            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("end", end);
            options.Add("begin", begin);
            options.Add("mode", "stat");
            options.Add("interval", span);

            OPAPI.ResultSet resultSet = sisconn.select("Archive", colnames, sisid.ToArray(), options);//options为条件            
            try
            {
                PointData PtData;
                while (resultSet.next())
                {
                    int id = resultSet.getInt(0);
                    if (!dic_data_stat.TryGetValue(id, out PtData))
                    {
                        PtData = new PointData();                     
                        PtData.ID_sis = id;
                        dic_data_stat.Add(id, PtData);
                    }
                    PtData.DifAV = (float)Math.Max(resultSet.getDouble(1) - resultSet.getDouble(2), PtData.DifAV);
                }
            }
            catch (Exception e)
            {
                string sise = string.Format("Sis连接出错！:\rsisconn:isAlive{0}\r{1}", sisconn.isAlive(),
                    sisconn.ToString());
                FormBugReport.ShowBug(e,sise);
            }
            finally
            {
                if (resultSet != null)
                {
                    resultSet.close(); //释放内存
                }
                //conn.close();
            }
            Dictionary<int, PointData> dic_data_stat_id = new Dictionary<int, PointData>();
            foreach (PointData pd in dic_data_stat.Values)
            {
                point pt = Data.inst().dic_SisIdtoPoint[pd.ID_sis];
                pd.ED = pt.ed;
                pd.ID = pt.id;
                dic_data_stat_id.Add(pd.ID, pd);
            }
            return dic_data_stat_id;
        }
        //dtaidistance.dtw
        public static double GetDtw_dd(PointData pd1, PointData pd2,float max_dist = 0, bool use_pruning = false)
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
                float c = (float)Math.Sqrt(x2 / y2);
                for (int i = 0; i < x.Length; i++)
                {
                    y[i] *= c;
                }
                cost = dd_dtw.dtw_distance_cs(x, y,max_dist,use_pruning);
            }
            else if (y2 > x2 && x2 >= 0.5)
            {
                float c = (float)Math.Sqrt(y2 / x2);
                for (int i = 0; i < x.Length; i++)
                {
                    x[i] *= c;
                }
                cost = dd_dtw.dtw_distance_cs(x, y,max_dist, use_pruning);
            }
            return cost * Math.Sqrt(x.Length);//归一化
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
        public static double GetDtw_dd_diff(PointData pd1, PointData pd2, float max_dist = 0, bool use_pruning = false)
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
            return dd_dtw.dtw_distance_cs(x, y, max_dist, use_pruning) * Math.Sqrt(x.Length); //归一化
        }
        public static double Get_diff_integral(PointData pd1, PointData pd2)
        {
            if (pd1.data.Count != pd2.data.Count)
                throw new ArgumentException("数组长度应相等！");

            //double[] x = new double[pd1.data.Count];
            //double[] y = new double[pd1.data.Count];
            double dx, dy;
            double rmax =double.MinValue;
            for (int i = 0; i < pd1.data.Count; i++)
            {
                dx = pd1.data[i].Value - pd1.data[0].Value;
                dy = pd2.data[i].Value - pd2.data[0].Value;
                //
                //dmax += x[i] - y[i];
                rmax = Math.Max(Math.Abs(dx-dy), rmax);
            }
            return rmax;
        }

        public static double GetDtw_dd_diff(float[] x, float[] y, double max_dist = 0, bool use_pruning = false)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("数组长度应相等！");

            double[] xx = new double[x.Length];
            double[] yy = new double[x.Length];
            xx[0] = yy[0] = 0;
            for (int i = 1; i < x.Length; i++)
            {
                xx[i] = x[i] - x[i - 1];
                yy[i] = y[i] - y[i - 1];
            }
            return dd_dtw.dtw_distance_cs(xx, yy, max_dist, use_pruning) * Math.Sqrt(xx.Length); //归一化
        }
        public static double Get_diff_integral(float[] x, float[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("数组长度应相等！");

            //double[] xx = new double[x.Length];
            //double[] yy = new double[x.Length];
            double dx, dy;
            double rmax = double.MinValue;
            for (int i = 0; i < x.Length; i++)
            {
                dx = x[i] - x[0];
                dy = y[i] - y[0];
                //
                //dmax += xx[i] - yy[i];
                rmax = Math.Max(Math.Abs(dx-dy), rmax);
            }
            return rmax;
        }
        public static DateTime GetSisSystemTime(OPAPI.Connect sisconn)
        {
            DateTime sysdt = DateTime.Now.AddSeconds(-120);
            string sql = "select TM from Realtime where ID in (2053,9092,220865)";
            bool flag = false;

            OPAPI.ResultSet resultSet = sisconn.executeQuery(sql);//执行SQL
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
                string sise = string.Format("Sis连接出错！:\rsisconn:isAlive{0}\r{1}", sisconn.isAlive(), 
                    sisconn.ToString());
                FormBugReport.ShowBug(ee,sise);

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
