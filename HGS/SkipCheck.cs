using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queues;
namespace HGS
{
    //滑动窗口取极值
    class SkipCheck
    {
        DequeSafe<double> qdata = new DequeSafe<double>();
        DequeSafe<int> qmax = new DequeSafe<int>();
        DequeSafe<int> qmin = new DequeSafe<int>();
        private int size = 64;//窗口size.

        FFTWReal fft = new FFTWReal();
        int p = -1;
        public enum skipstatus
        {
            skip,wave,error
        }

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
            
        public void add(double d)
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
                   double x =  qmax.PopLast();
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
        public double Max()
        {
            if (qdata.Count <= 0) throw new Exception("没有数据！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmax.PeekFirst() - start];
        }
        public double Min()
        {
            if (qdata.Count <= 0) throw new Exception("没有数据！");
            int start = p - size + 1;
            start = start >= 0 ? start : 0;
            return qdata[qmin.PeekFirst() - start];
        }
        public double[] Data()
        {
            if (qdata.Count <= 0) throw new Exception("没有数据！");
            return qdata.ToArray();
        }
        //返回极差
        public double DeltaP_P()
        {
            if (qdata.Count <= 0) throw new Exception("没有数据！");
            return Math.Abs(Max() - Min());
        }
        //数据跳变返回true，数据波动返回false;
        //调用前要保证数据极差异常，否则结果是错的。
        public skipstatus isSkip()
        {
            if (size < 64 || qdata.Count != size)
                return skipstatus.error;

            double[] Spectrum = fft.Spectrum(qdata.ToArray(), true);
            int imax = -1;
            double dmax = double.MinValue;
            for (int i = 1; i < Spectrum.Length; i++)
            {
                if (dmax - Spectrum[i] < -1e-6)
                {
                    dmax = Spectrum[i];
                    imax = i;
                }
            }
            return imax <= 3 ? skipstatus.skip : skipstatus.wave;
        }
    }
}
