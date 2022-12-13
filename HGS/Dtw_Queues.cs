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
        Deque<float> qdata = new Deque<float>();
        Deque<int> qmax = new Deque<int>();
        Deque<int> qmin = new Deque<int>();
        //
        //DequeSafe<float> qdata = new DequeSafe<float>();
        //DequeSafe<int> qmax = new DequeSafe<int>();
        //DequeSafe<int> qmin = new DequeSafe<int>();
        private int size = 100;//窗口size.
        private int downsample = 1;
        private int totalsampls = 1;
        const int delay = 3600;//s
        int p = -1;
        //滤波器用,x(n)=a*x(n-1)+b*y(n+1)+(1-a-b)*y(n) a+b要小于1;
        float a = 0.8f, b = 0.1f, x = 0, y1 = 0, y2 = 0;
        public Dtw_queues(int DownSample) 
        {
            //this.size = size;
            downsample = DownSample < 1 ? 1 : DownSample;
            //
            Random rnd = new Random();
            totalsampls = rnd.Next(0, downsample+1);//平均分配CPU负荷。
        }
        /*
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
        */
        public void add(float d,bool bDS)
        {
            totalsampls++;
            if (!bDS || (totalsampls % downsample == 0))
            {
                ///滤波，初始化时约需要120个数据
                y1 = y2;
                y2 = d;
                d = x = a * x + b * y1 + (1 - a - b) * y2;
                
                p++;
                int im;
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
                        float x = qmax.PopLast();
                    }
                    else break;
                qmax.Push(p);
                //
                while (qmin.TryPeekLast(out im))
                    if (qdata[im - start] - d >= 1e-6)
                    {
                        float x = qmin.PopLast();
                    }
                    else break;
                qmin.Push(p);
            }
        }
        private float Max()
        {
            if (qdata.Count != size) throw new Exception("数据量不对！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmax.PeekFirst() - start];
        }
        private float Min()
        {
            if (qdata.Count != size) throw new Exception("数据量不对！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmin.PeekFirst() - start];
        }
        public float[] Data()
        {
            if (qdata.Count != size) return null;
            return qdata.ToArray();
        }
        //返回极差
        //设备启动后，延迟一定时间
        public float DeltaP_P()
        {
            //if (qdata.Count != size) 
            if (p <= size + delay / downsample + 20)
                return 0;
            return Max() - Min();
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
