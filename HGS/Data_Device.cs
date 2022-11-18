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
        private float[] alarm_th_dis = null;
        public float[] alarm_th_dis_max = new float[6];
        public int sort = 0;
        public int CountofDTWCalc = 0;
        private HashSet<int> hs_Sensorsid = null;
        public float[] Alarm_th_dis
        {
            get { return alarm_th_dis; }
            set
            {
                alarm_th_dis = value;
                if (alarm_th_dis == null)
                {
                    Data_Device.dic_Device.Remove(id);
                    if (hs_Sensorsid != null)
                    {
                        foreach (int sid in hs_Sensorsid)
                        {
                            point pt;
                            if (Data.inst().cd_Point.TryGetValue(sid, out pt))
                            {
                                if (pt.Device_set().Count == 1)
                                    pt.Dtw_start_th = null;
                            }
                        }
                    }
                }
                else
                {
                    if (!Data_Device.dic_Device.ContainsKey(id))
                        Data_Device.dic_Device.Add(id, this);
                }
            }
        }
        public HashSet<int> Sensors_set()
        {
            HashSet<int> hs = new HashSet<int>();
            if (hs_Sensorsid != null)
                hs.UnionWith(hs_Sensorsid);
            return hs;
        }
        public void addSensor(int sid)
        {
            if (hs_Sensorsid == null)
                hs_Sensorsid = new HashSet<int>();
            hs_Sensorsid.Add(sid);
            Data.inst().cd_Point[sid].add_device(id);
        }
        public void RemoveSensor(int sid)
        {
            hs_Sensorsid.Remove(sid);
            point pt;
            if (Data.inst().cd_Point.TryGetValue(sid, out pt))
                pt.remove_device(id);
            if (hs_Sensorsid.Count <= 1)
            {
                Alarm_th_dis = null;
            }
            if (hs_Sensorsid.Count == 0)
                hs_Sensorsid = null;
        }
        public void ExceptWith(HashSet<int> sen_set)
        {
            if(sen_set == null )
                throw new Exception("参数不能为空！");
            foreach(int sid in sen_set)
            {
                point pt;
                if (Data.inst().cd_Point.TryGetValue(sid, out pt))
                    pt.remove_device(id);
            }
            hs_Sensorsid.ExceptWith(sen_set);
            if (hs_Sensorsid.Count <= 1)
            {
                Alarm_th_dis = null;
            }
            if (hs_Sensorsid.Count == 0)
                hs_Sensorsid = null;
        }
        public void UnionWith(HashSet<int> sen_set)
        {
            if (sen_set == null)
                throw new Exception("参数不能为空！");
            foreach (int sid in sen_set)
            {
                point pt;
                if (Data.inst().cd_Point.TryGetValue(sid, out pt))
                {
                    pt.add_device(id);
                }
                //else
                    //sen_set.Remove(id);
            }
            if (hs_Sensorsid == null)
                hs_Sensorsid = new HashSet<int>();
            hs_Sensorsid.UnionWith(sen_set);
        }
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
                        point pt;
                        if (Data.inst().cd_Point.TryGetValue(id, out pt))
                        {
                            if (pt.Dtw_Queues_Array != null)
                            {
                                double[] secdata = pt.Dtw_Queues_Array[Step].Data();
                                if (secdata != null)
                                {
                                    CountofDTWCalc++;
                                    double cost = SisConnect.GetDtw_dd_diff(maindata, secdata, alarm_th_dis[Step]);
                                    if (cost > alarm_th_dis[Step])
                                    {
                                        alarm_th_dis_max[Step] = (float)Math.Max(cost, alarm_th_dis_max[Step]);
                                        return true;
                                    }
                                }
                                count++;
                                if (count >= 2) return false;
                            }
                        }
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
                        di.Alarm_th_dis = (float[])ob;
                    }
                    ob = pgreader["pointid_array"];
                    if (ob != DBNull.Value)
                    {
                        di.UnionWith(new HashSet<int>((int[])ob));

                    }
                    //if (di.Alarm_th_dis != null)//去除大量不报警的设备.
                        //dic_Device.Add(di.id, di);                           
                }
            }
            catch (Exception e) { throw new Exception(string.Format("读入设备子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
    }
}
