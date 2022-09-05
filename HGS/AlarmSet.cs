using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace HGS
{
    // Defines a comparer to create a sorted set
    // that is sorted by the file extensions.
    public class ByDateTime : IComparer<point>
    {
        public int Compare(point x, point y)
        {
            return x.lastalarmdatetime.CompareTo(y.lastalarmdatetime);
        }
    }
    class AlarmSet
    {
        private static AlarmSet inst;

        private AlarmSet() { }

        public static AlarmSet GetInst()
        {
            if (inst == null)
            {
                inst = new AlarmSet();
            }
            return inst;
        }
        //SortedSet<point> ssalarmPoint = new SortedSet<point>(new ByDateTime());//有问题，未查清，出现幻读。
        HashSet<point> ssalarmPoint = new HashSet<point>();
        public HashSet<point> ssAlarmPoint
        {
            //set { ssalarmPoint = value; }
            get { return ssalarmPoint; }
        }
  
    }
}
