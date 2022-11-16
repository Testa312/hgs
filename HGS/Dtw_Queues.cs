using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queues;
namespace HGS
{
    //滑动窗口取极值,可降采样
    public class Dtw_queues
    {
        DequeSafe<double> qdata = new DequeSafe<double>();
        DequeSafe<int> qmax = new DequeSafe<int>();
        DequeSafe<int> qmin = new DequeSafe<int>();
        private int size = 100;//窗口size.
        private int downsamples = 1;
        private int totalsampls = 0; 
        int p = -1;
        public Dtw_queues() { }
        public int Size
        {
            get
            { 
                return size;
            }
            set
            {
                if(value < 1)
                    throw new Exception("窗口太小或为负值！");
                qdata.Clear();
                qmax.Clear();
                qmin.Clear();
                p = -1;
                size = value;
            }
        }
        public int DownSamples
        {
            get { return downsamples; }
            set { downsamples = value; }
        }
        public void add(double d)
        {
            if (totalsampls % downsamples == 0)
            {
                p++;
                int im = 0;
                qdata.Push(d);
                if (qdata.Count > size)
                {
                    qdata.PopFirst();

                    if (qmax.TryPeekFirst(out im))
                        if (p - im >= size) qmax.PopFirst();
                    //
                    if (qmin.TryPeekFirst(out im))
                        if (p - im >= size) qmin.PopFirst();
                }
                int start = p - size + 1;
                start = start >= 0 ? start : 0;
                while (qmax.TryPeekLast(out im))
                    if (d - qdata[im - start] >= 1e-6)
                    {
                        double x = qmax.PopLast();
                    }
                    else break;
                qmax.Push(p);
                //
                while (qmin.TryPeekLast(out im))
                    if (qdata[im - start] - d >= 1e-6)
                    {
                        double x = qmin.PopLast();
                    }
                    else break;
                qmin.Push(p);
            }
        }
        private double Max()
        {
            if (qdata.Count != size) throw new Exception("数据量不对！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmax.PeekFirst() - start];
        }
        private double Min()
        {
            if (qdata.Count != size) throw new Exception("数据量不对！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmin.PeekFirst() - start];
        }
        public double[] Data()
        {
            if (qdata.Count != size) return null;
            return qdata.ToArray();
        }
        //返回极差
        public double DeltaP_P()
        {
            if (qdata.Count != size) return 0;
            return Math.Abs(Max() - Min());
        }
        public void Clear()
        {
            qdata.Clear();
            qmax.Clear();
            qmin.Clear();
            p = -1;
        }
    }
}
