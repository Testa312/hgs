using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queues;
namespace HGS
{
    //滑动窗口取极值
    public class DetectionSkip
    {
        DequeSafe<float> qdata = new DequeSafe<float>();
        DequeSafe<int> qmax = new DequeSafe<int>();
        DequeSafe<int> qmin = new DequeSafe<int>();
        private int size = 30;//窗口size.

        //滤波器用,x(n)=a*x(n-1)+b*y(n+1)+(1-a-b)*y(n) a+b要小于1;
        float a = 0.6f, b = 0.2f, x = 0, y1 = 0, y2 = 0;
        //
       // FFTWReal fft = new FFTWReal();
        int p = -1;
        public DetectionSkip() { }
        /*
        public enum wavestatus
        {
            surge,wave,error
        }
        */
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
            
        public void add(float d)
        {
            if (p < 0)
                x = y2 = y1 = d;
            y1 = y2;
            y2 = d;
            d = x = a * x + b * y1 + (1 - a - b) * y2;
            //
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
                    float x =  qmax.PopLast();
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
        private float Max()
        {
            if (qdata.Count <= 0) 
                throw new Exception("没有数据！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmax.PeekFirst() - start];
        }
        private float Min()
        {
            if (qdata.Count <= 0) throw 
                    new Exception("没有数据！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmin.PeekFirst() - start];
        }
        private float[] Data()
        {
            if (qdata.Count < size) 
                return null;
            return qdata.ToArray();
        }
        //返回极差
        public float DeltaP_P()
        {
            if (p <= size + 1) 
                return 0;
            return Math.Abs(Max() - Min());
        }
        //数据跳变返回true，数据波动返回false;
        //调用前要保证数据极差异常，否则结果是错的。
        public bool isSkip(float th)
        {
            if (p <= size + 1)//有滤波，数据量要加30个左右才能稳定。
                return false;
            if (DeltaP_P() > th)
                return true;
            return false;
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
