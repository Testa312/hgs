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
        public double MaxAv = double.MinValue;
        public double MinAv = double.MaxValue;
        public float MeanAV = 0;
        public List<DateValue> data = new List<DateValue>();
    }
    static class SisConnect
    {
        public static OPAPI.Connect sisconn = new OPAPI.Connect(Pref.Inst().sisHost, Pref.Inst().sisPort, 60,
            Pref.Inst().sisUser, Pref.Inst().sisPassword);//建立连接
        public static Dictionary<int, PointData> GetsisData(object[] keys, DateTime begin, DateTime end, int span = 1)
        {
            Dictionary<int, PointData> dic_data = new Dictionary<int, PointData>();
            if (keys.Length == 0) return dic_data;

            string[] colnames = new string[] { "ID", "TM", "DS", "AV" };

            Dictionary<string, object> options = new Dictionary<string, object>();

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
    }
}
