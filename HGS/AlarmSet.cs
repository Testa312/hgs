using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Npgsql;
namespace HGS
{
    /*/ Defines a comparer to create a sorted set
    // that is sorted by the file extensions.
    public class ByDateTime : IComparer<point>
    {
        public int Compare(point x, point y)
        {
            return x.lastalarmdatetime.CompareTo(y.lastalarmdatetime);
        }
    }*/
    class AlarmSet
    {
        private static AlarmSet inst;
        private static NpgsqlConnection pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
        private static NpgsqlCommand cmd;
        private StringBuilder sb_alarmsql = new StringBuilder();
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
                                   pt.Id, pt.Alarmininfo, Functions.dtoNULL(pt.Alarmingav), DateTime.Now));
                }
            }
            else
                ssAlarmPoint.Remove(pt);
        }
        public void Remove(point pt)
        {
            if(ssAlarmPoint.Contains(pt))
                sb_alarmsql.AppendLine(string.Format(@"insert into alarmhistory (id,alarminfo,alarmav,datetime) values ({0},'{1}',{2},'{3}');",
                                  pt.Id, "人工取消了报警！", Functions.dtoNULL(pt.Alarmingav), DateTime.Now));
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
    }
}
