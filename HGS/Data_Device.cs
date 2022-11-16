using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace HGS
{
    public class DeviceInfo
    {
        public string Name = "";
        public int id = -1;
        public string path = "";
        public float[] alarm_th_dis = null;
        public int sort = 0;
        public HashSet<int> hs_Sensorsid = null;
        public bool dtw_alarm(int Sensorid, int Step)
        {
            if (alarm_th_dis == null || hs_Sensorsid == null) 
                return false;
            if (alarm_th_dis.Length != 6) 
                throw new Exception("设备dtw报警阈值必须为6个！");
            if(!hs_Sensorsid.Contains(Sensorid)) 
                throw new Exception("设备没有这个传感器！");
            if (Step < 0 || Step >= 6) 
                throw new Exception("设备没有采集这些数据！");
            //
            double[] maindata = Data.inst().cd_Point[Sensorid].Dtw_Queues_Array[Step].Data();
            if (maindata != null)
            {
                int count = 0;
                foreach (int id in hs_Sensorsid)
                {
                    if (id != Sensorid)
                    {
                        double[] secdata = Data.inst().cd_Point[id].Dtw_Queues_Array[Step].Data();
                        if (secdata != null)
                        {
                            //double textxx = SisConnect.GetDtw_dd_diff(maindata, secdata);//???????????????
                            if (SisConnect.GetDtw_dd_diff(maindata, secdata, alarm_th_dis[Step]) > alarm_th_dis[Step])
                                return true ;
                        }
                        count++;
                        if (count >= 2) return false;
                    }
                }
            }
            return false;
        }
    }
    static class Data_Device
    {
        //所有报警设备的字典,id-DeviceInfo-----------------------------------
        public static Dictionary<int, DeviceInfo> dic_Device = new Dictionary<int, DeviceInfo>();
        public static void GetAllAlarmDevice()
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string sql = string.Format(@"SELECT * FROM devicetree  order by sort;");
                var cmd = new NpgsqlCommand(sql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                while (pgreader.Read())
                {
                    DeviceInfo di = new DeviceInfo();

                    di.id = (int)pgreader["id"];
                    di.Name = pgreader["nodename"].ToString();
                    di.sort = (int)pgreader["sort"];
                    di.path = pgreader["path"].ToString();
                    object ob = pgreader["alarm_th_dis"];
                    if (ob != DBNull.Value)
                    {
                        di.alarm_th_dis = (float[])ob;
                    }
                    ob = pgreader["pointid_array"];
                    if (ob != DBNull.Value)
                    {
                        di.hs_Sensorsid = new HashSet<int>((int[])ob);

                    }
                    if (di.alarm_th_dis != null && di.hs_Sensorsid != null)
                    {
                        dic_Device.Add(di.id, di);
                        //传感器关联设备
                        foreach(int id in di.hs_Sensorsid)
                        {
                            point pt = Data.inst().cd_Point[id];
                            pt.add_device(di.id);
                        }
                    }
                            
                }
            }
            catch (Exception e) { throw new Exception(string.Format("读入设备子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
    }
}
