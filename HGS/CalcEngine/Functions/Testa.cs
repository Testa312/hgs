using System;
using System.Diagnostics;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using IF97;
namespace CalcEngine
{
    static class Testa
    {
        public static void Register(CalcEngine ce)
        {
            //ce.RegisterFunction("H", 2, _h);
            ce.RegisterFunction("BinTwoThirdsLog", 3, _BinTwoThirdsLog);
            ce.RegisterFunction("BinOneLog", 1, _BinOneLog);
            ce.RegisterFunction("BinbitOflong", 2, _BinbitOflong);
            ce.RegisterFunction("DMidOfThree", 3, _DMidOfThree);
            ce.RegisterFunction("LogicbitOflong", 2, _bitOflong);
            ce.RegisterFunction("MotorPF", 4, _MotorPF);
            ce.RegisterFunction("IsBz", 2, _IsBz);
        }
#if DEBUG
        public static void Test(CalcEngine ce)
        {
        }
#endif
        /*
        static object _h(List<Expression> p)
        {
            FreeSteam.SteamState s = FreeSteam.freesteam_set_pT((double)p[0] * 1e6, (double)p[1] + 273.15);

            return FreeSteam.freesteam_h(s) / 1000.0;//kJ/kg
        }*/
        //三逻辑值转双精度1和0；
        static object _BinTwoThirdsLog(List<Expression> p)
        {
            bool p0 = (bool)p[0];
            bool p1 = (bool)p[1];
            bool p2 = (bool)p[2];
            return (p0 && p1 || p0 && p2 || p1 && p2) ? 1.0 : 0.0;
        }
        //单逻辑值转双精度1和0；
        static object _BinOneLog(List<Expression> p)
        {
            //bool p0 = (bool)p[0].Evaluate();
            return ((bool)p[0].Evaluate()) ? 1.0 : 0.0;
        }
        ///三取中
        static object _DMidOfThree(List<Expression> p)
        {
            double p0 = (double)p[0];
            double p1 = (double)p[1];
            double p2 = (double)p[2];
            return Math.Min(Math.Max(p0, p1), Math.Max(p1, p2));
        }
        ///测试长整型数的第n位是否为零
        static object _bitOflong(List<Expression> p)
        {
            UInt64 p0 = Convert.ToUInt64((double)p[0]);
            UInt32 p1 = Convert.ToUInt32((double)p[1]);
            UInt64 tmp = Convert.ToUInt64(Math.Pow(2, p1));
            return (p0 & tmp) > 0;
        }
        ///测试长整型数的第n位,返回0，1；
        static object _BinbitOflong(List<Expression> p)
        {
            UInt64 p0 = Convert.ToUInt64((double)p[0]);
            UInt32 p1 = Convert.ToUInt32((double)p[1]);
            UInt64 tmp = Convert.ToUInt64(Math.Pow(2, p1));
            return (p0 & tmp) > 0 ? 1.0 : 0.0;
        }
        ///计算异步动动机的功率因数，输入当前运行电流I，额定电流Ie、额定功率因数PF。不能用于变频电机。
        ///参考：徐志强 电气开关 异步电动机的功率因数和补偿容量的近似计算 1995 No.5
        static object _MotorPF(List<Expression> p)
        {
            double I = (double)p[0];
            double I0 = (double)p[1];
            double Ie = (double)p[2];
            double PF = (double)p[3];
            //I0 = 2*Ie*(1-PF);
            if (I0 <= 0)
                I0 = 5.5 * Ie * (1 - PF) * Math.Sqrt(1 - Math.Pow(PF, 2));
            double B = Math.Sqrt((Math.Pow(I, 2) - Math.Pow(I0, 2)) / (Math.Pow(Ie, 2) - Math.Pow(I0, 2)));
            double k = 0;
            if (B > 0.85)
                k = 1;
            else if (B >= 0.8)
                k = 0.95;
            else
                k = 0.9;
            return 1 - k * (1 - PF) / B;
        }
        //
        static  byte GetCurBz(int ctime)
        {
            DateTime m_BaseDateTime = new DateTime(2011, 12, 6, 8, 0, 0);
            DateTime dt = (new DateTime(1970, 1, 1)).AddSeconds(ctime).ToLocalTime();
            byte[] BzIndex = { 1, 2, 3, 4, 5, 1, 2 };
            long it = dt.Ticks - m_BaseDateTime.Ticks;
            it /= (10000000L * 3600);//小时
            long itmod = it % 240;
            for (int i = 9; i >= 0; i--)
            {
                int index = i / 2;
                if (itmod >= 0 && itmod <= 7)
                    return BzIndex[index];
                else if (itmod > 7 && itmod <= 16)
                    return BzIndex[index + 1];
                else if (itmod > 16 && itmod <= 23)
                    return BzIndex[index + 2];
                itmod -= 24;
            }
            return byte.MinValue;
        }
        //
        ///测试时刻是否是某值上班：是返回1，不是返回0，错误返回最小值；
        static object _IsBz(List<Expression> p)
        {
            int p0 = (int)p[0];//时间，1970.1.1以来的秒数
            byte p1 = (byte)p[1];
            if (p1 <= 0 || p1 > 5)
                return byte.MaxValue;
            byte cbz = GetCurBz(p0);
            if (cbz == p1)
                return 1;
            else
                return 0;
        }

    }
}
