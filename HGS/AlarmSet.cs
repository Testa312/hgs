using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Npgsql;
using Queues;
using System.Speech.Synthesis;
using System.Speech;
using System.Speech.Recognition;
namespace HGS
{
    public class AlarmInfo
    {
        public AlarmInfo(string sid,int sensorid,int deviceid,string nd,string pn,
            string ed,string eu,float? av,string info, string path,int sound)
        {
            _sid = sid;
            _Info = info;
            _nd = nd;
            _pn = pn;
            _ed = ed;
            _av = av;
            _eu = eu;
            _sensorid = sensorid;
            _deviceid = deviceid;
            _path = path;
            _sound = sound;
            //
        }
        public string _nd;
        public string _pn;
        public string _ed;
        public string _eu;
        public int _sensorid = -1;
        public int _deviceid = -1;
        public string _path;
        public int _sound;
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
        public DateTime stoptime = new DateTime(2000,1,1,0,0,0);
        public float? _av = null;
        public string _Info;
    }
    class AlarmSet
    {
        private static AlarmSet inst;
        private static NpgsqlConnection pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
        private static NpgsqlCommand cmd;

        private StringBuilder sb_alarmsql = new StringBuilder();
        //private StringBuilder sb_alarmsql_modified = new StringBuilder();
        //
        private LinkedList<AlarmInfo> linkAlarming = new LinkedList<AlarmInfo>();
        private Queue<AlarmInfo> q_alarm_history = new Queue<AlarmInfo>();
        //相当于索引
        private Dictionary<string, LinkedListNode<AlarmInfo>> dic_alarminfo = new Dictionary<string, LinkedListNode<AlarmInfo>>();
        //
        static SpeechSynthesizer speak = new SpeechSynthesizer();
        System.Media.SoundPlayer simpleSound = new System.Media.SoundPlayer();

        int TimeTick = 0;
        int sb_lines = 0;
        //
        private AlarmSet() { }

        public static AlarmSet GetInst()
        {
            if (inst == null)
            {
                inst = new AlarmSet();
                pgconn.Open();
                cmd = new NpgsqlCommand("", pgconn);
                speak.Rate = -2;
                speak.Volume = 100;

            }
            return inst;
        }
        ~AlarmSet(){ pgconn.Close(); }        
        
        public void AddNewSQL(AlarmInfo ai)
        {
            sb_lines++;
            sb_alarmsql.AppendLine(string.Format(@"insert into alarmhistory (sid,alarmtime,alarminfo,alarmav,stoptime,nd,ed,pn,eu,device_path) values " +
                                      "('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}');",
                                   ai.sid, ai._starttime,ai._Info, Functions.dtoNULL(ai._av), ai.stoptime,ai._nd,ai._ed,ai._pn,ai._eu,ai._path));
        }
        public void ModefiedSQL(AlarmInfo ai)
        {
            sb_lines++;
            sb_alarmsql.Append(string.Format(@"update alarmhistory set stoptime='{0}' where sid = '{1}' and alarmtime = '{2}';", 
                ai.stoptime, ai.sid,ai._starttime));
        }
        public void SaveAlarmInfoToPG()
        {
            TimeTick++;
            try
            {
                if (sb_lines >= 10 || (sb_lines > 0 && TimeTick % 30 == 0))
                {

                    if (pgconn.State == System.Data.ConnectionState.Closed)
                        pgconn.Open();
                    cmd.CommandText = sb_alarmsql.ToString();
                    cmd.ExecuteNonQuery();
                    sb_alarmsql.Clear();
                    sb_lines = 0;
                }

            }
            catch (Exception e) { throw new Exception(string.Format("保存报警信息时发生错误！"), e); }
        }
        //有线程安全问题
        //如果信号只发报警，不发报警消失，将溢出，数据库中将没有消失时间的记录.
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
                //lsNewAlarmInfo.Add(ai);
                AddNewSQL(ai);
                SounfPlay(ai);
            }
        }
        public void AlarmStop(string sid)
        {
            LinkedListNode<AlarmInfo> lai;
            if(dic_alarminfo.TryGetValue(sid,out lai))
            {
                lai.Value.stoptime = DateTime.Now;
                q_alarm_history.Enqueue(lai.Value);
                if (q_alarm_history.Count > 1000)
                {
                    q_alarm_history.Dequeue();
                }
                ModefiedSQL(lai.Value);
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
        private void SounfPlay(AlarmInfo ai)
        {
            switch (ai._sound)
            {
                case 0:
                    break;
                case 1:
                    simpleSound.SoundLocation = @"1.wav";
                    simpleSound.Play();
                    break;
                case 2:
                    simpleSound.SoundLocation = @"2.wav";
                    simpleSound.Play();
                    break;
                case 3:
                    simpleSound.SoundLocation = @"3.wav";
                    simpleSound.Play();
                    break;
                case 4:
                    speak.SpeakAsync(string.Format("{0}{1}",ai._ed,ai._Info));
                    break;
                default: { }break;
            }
        }
        public void SoundStop()
        {
            simpleSound.Stop();
            //speak.Pause();
        }
    }
}
