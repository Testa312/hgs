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
        /*
        const int initdicCapacity = 1009;
        //报警点----------------------------------
        public ConcurrentDictionary<int, point> cd_Point { set; get; }
        private void Initcd_dicCapacity()
        {
            //-------------------------------------------------------------
            //点字典,用于计算
            // We know how many items we want to insert into the ConcurrentDictionary.
            // So set the initial capacity to some prime number above that, to ensure that
            // the ConcurrentDictionary does not need to be resized while initializing it.
            //static int NUMITEMS = 20000;
            int initialCapacity = Functions.inst().AbovePrimes(initdicCapacity);//素数
            initialCapacity = initialCapacity > 1009 ? initialCapacity : 1009;

            // The higher the concurrencyLevel, the higher the theoretical number of operations
            // that could be performed concurrently on the ConcurrentDictionary.  However, global
            // operations like resizing the dictionary take longer as the concurrencyLevel rises.
            // For the purposes of this example, we'll compromise at numCores * 2.
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;
            cd_Point = new ConcurrentDictionary<int, point>(concurrencyLevel, initialCapacity);
            //         
        }
        */
        SortedSet<point> ssalarmPoint = new SortedSet<point>(new ByDateTime());
        public SortedSet<point> ssAlarmPoint
        {
            //set { ssalarmPoint = value; }
            get { return ssalarmPoint; }
        }
  
    }
}
