﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using CalcEngine;
using System.Text.RegularExpressions;
namespace HGS
{
    public class DeviceInfo
    {
        public DeviceInfo()
        {
            Random rnd = new Random();
            TimeTick = rnd.Next(1, 100);
        }
        public string Name = "";
        public int id = -1;
        public string path = "";
        private float[] alarm_th_dis = null;
        public float[] alarm_th_dis_max = new float[6];
        public int sort = 0;
        public int CountofDTWCalc = 0;
        public int Sound = 0;
        private HashSet<int> hs_Sensorsid = null;
        //
        private int TimeTick;
        private uint lastAlarmBit = 0, curAlarmBit = 0;
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
        //
        private string _Orgformula_device = "";//原始计算公式
        public string Orgformula_device
        {
            set
            {
                if (_Orgformula_device != value)
                {
                    //Data.inst().Update(this);//??????????????
                }
                _Orgformula_device = value;
            }
            get { return _Orgformula_device; }
        }
        private string _Sisformula_device = "";//已展开成SIS点的计算公式，不保存
        public string Sisformula_device
        {
            set { _Sisformula_device = value; }
            get { return _Sisformula_device; }
        }
        private Expression _Expression_device = null;//已解析为表达式树,优化计算速度。
        public Expression Expression_device
        {
            set { _Expression_device = value; }
            get { return _Expression_device; }
        }
        //计算子点id
        private List<varlinktopoint> _lsCalcOrgSubPoint_device = null;//变量与点Id的对应列表
        public List<varlinktopoint> lsCalcOrgSubPoint_device
        {
            set { _lsCalcOrgSubPoint_device = value; }
            get { return _lsCalcOrgSubPoint_device; }
        }
        //
        //计算子点id用于进行计算点状态计算。
        private List<point> _listSisCalaExpPointID_device = null;//参与公式计算的sis点列表。;
        public List<point> listSisCalaExpPointID_device
        {
            set { _listSisCalaExpPointID_device = value; }
            get { return _listSisCalaExpPointID_device; }
        }
        //
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
            Data.inst().cd_Point[sid].add_device(id,path);
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
                    pt.add_device(id,path);
                }
                //else
                    //sen_set.Remove(id);
            }
            if (hs_Sensorsid == null)
                hs_Sensorsid = new HashSet<int>();
            hs_Sensorsid.UnionWith(sen_set);
        }
        //--------------------
        static string[,] _Alarm = {
            {"15m",  "15分钟内参数异常！"    },
            {"30m",  "30分钟内参数异常！"    },
            {"60m",  "1小时内参数异常！"    },
            {"120m",  "2小时内段参数异常！"  },
            {"240m",  "4小时内参数异常！"  },
            {"480m",  "8小时内参数异常！"  } };
        //-----
        private void AlarmCalc_dtw(point pt,int Step)
        {
            if (alarm_th_dis == null || hs_Sensorsid == null) 
                return;
            if (alarm_th_dis.Length != 6) 
                throw new Exception("设备dtw报警阈值必须为6个！");
            if (Step < 0 || Step >= 6) 
                throw new Exception("设备没有采集这些数据！");
            //
            Queue<int> q_s = new Queue<int>();
            foreach (int s in hs_Sensorsid)
                q_s.Enqueue(s);
            double[] maindata = pt.Dtw_Queues_Array[Step].Data();
            if (maindata != null)
            {
                int count = 0;
                while (q_s.Count > 0)
                {
                    int sid = q_s.Dequeue();
                    if (sid == pt.id) continue;
                    point ptx;
                    if (Data.inst().cd_Point.TryGetValue(sid, out ptx))
                    {
                        if (ptx.Dtw_Queues_Array != null)
                        {
                            double[] secdata = ptx.Dtw_Queues_Array[Step].Data();
                            if (secdata != null)
                            {
                                CountofDTWCalc++;
                                double cost = SisConnect.GetDtw_dd_diff(maindata, secdata, alarm_th_dis[Step] * 1.414);
                                if (cost > alarm_th_dis[Step])
                                {
                                    curAlarmBit = (uint)1 << Step;
                                    if (!double.IsInfinity(cost))
                                        alarm_th_dis_max[Step] = (float)Math.Round(Math.Max(cost, alarm_th_dis_max[Step]),3);
                                    return;
                                }
                            }
                            count++;
                            if (count >= 2) return;
                        }
                    }
                }
            }
        }
        private point Dtw_th_h(int step)
        {
            foreach (int sid in hs_Sensorsid)
            {
                point pt;
                if (Data.inst().cd_Point.TryGetValue(sid, out pt))
                {
                    if (pt.Dtw_Queues_Array != null)
                    {
                        double p_p = pt.Dtw_Queues_Array[step].DeltaP_P();
                        if (p_p > pt.Dtw_start_th[step])
                            return pt;
                    }
                }
            }
            return null;
        }
        private string CreateAlarmSid(int bitnum)
        {
            return string.Format("D{0}-{1}", id, _Alarm[bitnum, 0]);
        }
        //-------------
        public AlarmInfo CreateAlarmInfo(int bitnum)
        {
            return new AlarmInfo(CreateAlarmSid(bitnum), -1, id, "设备", "", Name,"Ed",
                alarm_th_dis_max[bitnum],
                _Alarm[bitnum, 1],
                path,Sound);
        }
        //素数181，347,727,1373,2801,5711
        static int[] prime = { 181,347, 727, 1373, 2801, 5711 };
        public void AlarmCalc()
        {
            TimeTick++;        
            for (int i = 0; i < prime.Length; i++)
            {
                if (TimeTick % prime[i] == 0)
                {
                    uint temp = ~((uint)1 << i);
                    curAlarmBit &= temp;

                    point pt = Dtw_th_h(i);
                    if (pt != null)
                        AlarmCalc_dtw(pt, i);

                    uint lastBit = lastAlarmBit & ((uint)1 << i);
                    uint curBit = curAlarmBit & ((uint)1 << i);
                    if (lastBit > curBit)
                    {
                        AlarmSet.GetInst().AlarmStop(CreateAlarmSid(i));
                    }
                    else if (lastBit < curBit)
                    {
                        AlarmSet.GetInst().AddAlarming(CreateAlarmInfo(i));
                    }
                    lastAlarmBit = curAlarmBit;
                }             
            }            
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
                    di.Sound = (int)pgreader["sound"];
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
        public static DeviceInfo GetDevice(int DeviceId)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            DeviceInfo di = null;
            try
            {
                pgconn.Open();
                string strsql = string.Format("select * from devicetree where id = {0} limit 1", DeviceId);
                var cmd = new NpgsqlCommand(strsql, pgconn);

                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                while (pgreader.Read())
                {
                    di = new DeviceInfo();
                    di.Name = pgreader["nodename"].ToString();
                    di.path = pgreader["path"].ToString();
                    di.Sound = (int)pgreader["sound"];
                    di.sort = (int)pgreader["sort"];
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

                }
                pgconn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                pgconn.Close();
            }
            return di;
        }
        public static void AlarmCalc_All_Device()
        {
            foreach (DeviceInfo dv in dic_Device.Values)
            {
                dv.AlarmCalc();
            }
        }

        //-------------------
        //展开公式为Sis点-------------------------------------------------
        static HashSet<int> loopvar = new HashSet<int>();
        public static string ExpandOrgFormula_Device(point pt)
        {
            if (pt.orgformula_main.Length == 0) return "";
            if (loopvar.Contains(pt.id))
            {
                StringBuilder sb = new StringBuilder();
                foreach (int sid in loopvar)
                {
                    point Point = Data.inst().cd_Point[sid];
                    sb.Append(string.Format("[{0}]:{1},", Point.id, Point.ed));
                }
                throw new ArgumentException(sb.Append("循环变量引用！").ToString());
            }
            loopvar.Add(pt.id);
            string orgf = pt.orgformula_main;
            if (pt.lsCalcOrgSubPoint_main != null)
            {
                ExpandOrgFormulaSub(ref orgf, pt.lsCalcOrgSubPoint_main);
            }
            loopvar.Clear();
            return orgf;
        }
        //展开device_if公式为Sis点-------------------------------------------------
        private static void ExpandOrgFormulaSub(ref string orgf, List<varlinktopoint> lscosp)
        {
            foreach (varlinktopoint subpt in lscosp)
            {
                point Point = Data.inst().cd_Point[subpt.sub_id];
                if (Point.pointsrc == pointsrc.calc)
                {
                    string rpl = string.Format("({0})", ExpandOrgFormula_Device(Point));
                    string pat = string.Format(@"\b{0}\b(?=[^(])|\b{0}$", subpt.varname);

                    orgf = Regex.Replace(orgf, pat, rpl);
                }
                else
                {
                    string rpl = string.Format("{0}", Pref.Inst().GetVarName(Point));
                    string pat = string.Format(@"\b{0}\b(?=[^(])|\b{0}$", subpt.varname);
                    orgf = Regex.Replace(orgf, pat, rpl);
                }
            }
        }
        public static string ExpandOrgFormula_AlarmIf(point pt)
        {
            if (pt.Alarmif.Length == 0) return "";
            string orgf = pt.Alarmif;
            if (pt.lsCalcOrgSubPoint_alarmif != null)
            {
                foreach (varlinktopoint subpt in pt.lsCalcOrgSubPoint_alarmif)
                {
                    ExpandOrgFormulaSub(ref orgf, pt.lsCalcOrgSubPoint_alarmif);
                }
            }
            return orgf;
        }
        private static void ExpandOrgPointToSisPoinSub(List<point> exppt, List<varlinktopoint> lcosp)
        {
            foreach (varlinktopoint subpt in lcosp)
            {
                point Pointx = Data.inst().cd_Point[subpt.sub_id];
                if (Pointx.pointsrc == pointsrc.calc)
                {
                    List<point> lsexp = ExpandOrgPointToSisPoint_Main(Pointx);
                    if (lsexp != null)
                        exppt.AddRange(lsexp);
                }
                else
                {
                    exppt.Add(Pointx);
                }
            }
        }
        //返回AlarmIf公式展开成sis点的列表。
        public static List<point> ExpandOrgPointToSisPoint_AlarmIf(point pt)
        {
            if (pt.lsCalcOrgSubPoint_alarmif == null) return null;
            List<point> ExpandPoint = new List<point>();
            ExpandOrgPointToSisPoinSub(ExpandPoint, pt.lsCalcOrgSubPoint_alarmif);
            return ExpandPoint;
        }
        static HashSet<int> xloopvar = new HashSet<int>();
        //返回计算点展开成sis点的列表,同时用于检查循环引用问题。
        public static List<point> ExpandOrgPointToSisPoint_Main(point pt)
        {
            if (pt.lsCalcOrgSubPoint_main == null) return null;
            List<point> ExpandPoint = new List<point>();
            if (xloopvar.Contains(pt.id))
            {
                StringBuilder sb = new StringBuilder();
                foreach (int sid in xloopvar)
                {
                    point Point = Data.inst().cd_Point[sid];
                    sb.Append(string.Format("[{0}]:{1},", Point.id, Point.ed));
                }
                throw new ArgumentException(sb.Append("循环变量引用！").ToString());
            }
            xloopvar.Add(pt.id);
            ExpandOrgPointToSisPoinSub(ExpandPoint, pt.lsCalcOrgSubPoint_main);
            xloopvar.Clear();
            return ExpandPoint;
        }

    }
}
