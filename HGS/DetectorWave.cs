﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queues;
namespace HGS
{
    //滑动窗口取极值
    class SlideWindow
    {
        DequeSafe<float> qdata = new DequeSafe<float>();
        DequeSafe<int> qmax = new DequeSafe<int>();
        DequeSafe<int> qmin = new DequeSafe<int>();
        private int size = 30;//窗口size.
        private int downsamples = 1;
        private int totalsampls = 0;

        int p = -1;
        public SlideWindow(int DownSample ,int size)
        {
            downsamples = DownSample < 1 ? 1 : DownSample;
            this.size = size;
        }
        public float add(float d, bool bDS)
        {
            float first = 0;
            totalsampls++;
            if (!bDS || (totalsampls % downsamples == 0))
            {
                p++;               
                int im;
                qdata.Push(d);
                if (qdata.Count > size)
                {
                    first = qdata.PopFirst();

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
                        qmax.PopLast();
                    }
                    else break;
                qmax.Push(p);
                //
                while (qmin.TryPeekLast(out im))
                    if (qdata[im - start] - d >= 1e-6)
                    {
                        qmin.PopLast();
                    }
                    else break;
                qmin.Push(p);
            }
            return first;
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
        public float[] Data()
        {
            if (qdata.Count != size) return null;
            return qdata.ToArray();
        }
        //返回极差
        public float DeltaP_P()
        {
            if (p <= size + 20)
                return 0;
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
    public class DetectorWave
    {

        int size = 0;
        int p = -1;
        SlideWindow step1 , step2, step3;
        //滤波器用,x(n)=a*x(n-1)+b*y(n+1)+(1-a-b)*y(n) a+b要小于1;
        float a = 0.7f, b = 0.15f, x = 0, y1 = 0, y2 = 0;
        public DetectorWave(int DownSample,int size = 30)
        {
            this.size = size;
            step1 = new SlideWindow(DownSample,size);
            step2 = new SlideWindow(DownSample,size);
            step3 = new SlideWindow(DownSample,size);
        }
        public void add(float d, bool bDS)
        {
            p++;
            ///滤波，初始化时约需要120个数据
            y1 = y2;
            y2 = d;
            d = x = a * x + b * y1 + (1 - a - b) * y2;
            step3.add(step2.add(step1.add(d, bDS), bDS), bDS);
        }
        public float[] Data()
        {
            List<float> rsl = new List<float>();
            rsl.AddRange(step3.Data());
            rsl.AddRange(step2.Data());
            rsl.AddRange(step1.Data());
            return rsl.ToArray();
        }
        public void Clear()
        {
            step1.Clear();
            step2.Clear();
            step3.Clear();
            p = -1;
        }
        //th 为阈值
        public bool IsWave(float th)
        {
            if(p <=  3*size + 20) 
                return false;

            bool s1 = step1.DeltaP_P() > th;
            bool s2 = step2.DeltaP_P() > th;
            bool s3 = step3.DeltaP_P() > th;

            if (s1 && s2 && s3) 
                return true;
            return false;
        }
        public float Delta_pp()
        {
            if (p <= 3 * size + 20)
                return 0;
            return Math.Max(Math.Max(step1.DeltaP_P(), step2.DeltaP_P()), step3.DeltaP_P());
        }

    }
}