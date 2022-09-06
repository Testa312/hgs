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
        public void Add(point pt,bool blastAlarm)
        {
            //没必要这样，直接即可，hashset速度极快。
            /*
            if (pt.AlarmCalc())
                AlarmSet.GetInst().ssAlarmPoint.Add(pt);
            else if (!pt.AlarmCalc() && blastAlarm)
                AlarmSet.GetInst().ssAlarmPoint.Remove(pt);
            */
            bool rsl = pt.AlarmCalc();
            if (rsl)
                AlarmSet.GetInst().ssAlarmPoint.Add(pt);
            else
                AlarmSet.GetInst().ssAlarmPoint.Remove(pt);
        }
        public void SaveAlarmInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (point pt in ssalarmPoint)
            {
                sb.AppendLine(string.Format(@"insert into alarmhistory (id,alarminfo,alarmav,datetime) values ({0},'{1}',{2},'{3}');",
                                    pt.id, pt.alarmininfo,pt.alarmingav, DateTime.Now));

            }
            try
            {
                if (sb.Length < 5) return;
                if (pgconn.State == System.Data.ConnectionState.Closed) 
                    pgconn.Open();
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();

            }
            catch(Exception e) { throw new Exception(string.Format("保存报警信息时发生错误！"),e); }
        }
    }
}
