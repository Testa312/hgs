using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Npgsql;
using Queues;
namespace HGS
{
    public class AlarmInfo
    {
        public AlarmInfo(string sid,int sensorid,int deviceid,string nd,string pn,string ed,float? av,string info)
        {
            _sid = sid;
            _Info = info;
            _nd = nd;
            _pn = pn;
            _ed = ed;
            _av = av;
            _sensorid = sensorid;
            _deviceid = deviceid;
            //
        }
        public string _nd;
        public string _pn;
        public string _ed;
        public int _sensorid = -1;
        public int _deviceid = -1;
        string _sid;

        public string sid
        {
            set { }
            get
            {
                return _sid;
            }
        }

        public DateTime _starttime = DateTime.Now;
        public DateTime stoptime;
        public float? _av = null;
        public string _Info;
    }
    class AlarmSet
    {
        private static AlarmSet inst;
        private static NpgsqlConnection pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
        private static NpgsqlCommand cmd;
        private StringBuilder sb_alarmsql = new StringBuilder();
        //
        private LinkedList<AlarmInfo> linkAlarming = new LinkedList<AlarmInfo>();
        private Queue<AlarmInfo> q_alarm_history = new Queue<AlarmInfo>();
        //相当于索引
        private Dictionary<string, LinkedListNode<AlarmInfo>> dic_alarminfo = new Dictionary<string, LinkedListNode<AlarmInfo>>();
        //避免频繁保存。
        private List<AlarmInfo> lsNewAlarmInfo = new List<AlarmInfo>();
        private List<AlarmInfo> lsModifiedAlarmInfo = new List<AlarmInfo>();
        //
        private AlarmSet() { }

        public static AlarmSet GetInst()
        {
            if (inst == null)
            {
                inst = new AlarmSet();
                pgconn.Open();
                cmd = new NpgsqlCommand("", pgconn);
            }
            return inst;
        }
        ~AlarmSet(){ pgconn.Close(); }

        //SortedSet<point> ssalarmPoint = new SortedSet<point>(new ByDateTime());//有问题，未查清，出现幻读。
        /*
        HashSet<point> ssalarmPoint = new HashSet<point>();
        public HashSet<point> ssAlarmPoint
        {
            //set { ssalarmPoint = value; }
            get { return ssalarmPoint; }
        }
        public void Add(point pt)
        {          
            if ((pt.Alarmifav && (pt.isAvalarm || pt.isboolvAlarm))|| pt.Dtw_Queues_Array != null)
            {
                alarmlevel la_stal = pt.AlarmLevel;
                alarmlevel al = pt.AlarmCalc();

                if (al != alarmlevel.no)
                    ssAlarmPoint.Add(pt);
                else
                    ssAlarmPoint.Remove(pt);
                //
                if (al != la_stal)
                {

                    sb_alarmsql.AppendLine(string.Format(@"insert into alarmhistory (id,alarminfo,alarmav,datetime) values ({0},'{1}',{2},'{3}');",
                                   pt.id, pt.Alarmininfo, Functions.dtoNULL(pt.Alarmingav), DateTime.Now));
                }
            }
            else
                ssAlarmPoint.Remove(pt);
        }
        public void Remove(point pt)
        {
            if(ssAlarmPoint.Contains(pt))
                sb_alarmsql.AppendLine(string.Format(@"insert into alarmhistory (id,alarminfo,alarmav,datetime) values ({0},'{1}',{2},'{3}');",
                                  pt.id, "人工取消了报警！", Functions.dtoNULL(pt.Alarmingav), DateTime.Now));
            ssAlarmPoint.Remove(pt);
        }
            public void SaveAlarmInfo()
        {
           
            try
            {
                if (sb_alarmsql.Length < 5) return;
                if (pgconn.State == System.Data.ConnectionState.Closed) 
                    pgconn.Open();
                cmd.CommandText = sb_alarmsql.ToString();
                cmd.ExecuteNonQuery();
                sb_alarmsql.Clear();

            }
            catch(Exception e) { throw new Exception(string.Format("保存报警信息时发生错误！"),e); }
        }
        */
        //有线程安全问题
        public void AddAlarming(AlarmInfo ai)
        {
            if (!dic_alarminfo.ContainsKey(ai.sid))
            {
                dic_alarminfo.Add(ai.sid,linkAlarming.AddLast(ai));
                if (linkAlarming.Count > 1000)
                {
                    linkAlarming.RemoveFirst();
                    dic_alarminfo.Remove(ai.sid);
                }
                lsNewAlarmInfo.Add(ai);
                
            }
        }
        public void AlarmStop(string sid)
        {
            LinkedListNode<AlarmInfo> lai;
            if(dic_alarminfo.TryGetValue(sid,out lai))
            {
                lai.Value.stoptime = DateTime.Now;
                q_alarm_history.Enqueue(lai.Value);
                lsModifiedAlarmInfo.Add(lai.Value);
                dic_alarminfo.Remove(sid);
                linkAlarming.Remove(lai);
            }
        }
        public List<AlarmInfo> GetAlarmRealTimeInfo()
        {
            return linkAlarming.ToList();
        }
        public List<AlarmInfo> GetAlarmHistoryInfo()
        {
            return q_alarm_history.ToList();
        }
    }
}
