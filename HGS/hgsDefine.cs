using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlacialComponents.Controls;
using CalcEngine;
namespace HGS
{
    //Tag of GalcialList'item 
    public class itemtag
    {
        public int sisid = 0;
        public int id = 0;
        public short fm = 2;
        public pointsrc PointSrc = pointsrc.sis;
    }
    public class subpoint
    {
        public int id {set;get;}
        public pointsrc PointSrc { set; get; }
        public string varname { set; get; }
    }
    //点来源。
    public enum pointsrc
    {
        sis,
        calc
    }
    public enum PointState
    {
        Good, Timeout, Bad, Error
    }
    
    public class point
    {
        //
        public int id_sis = 0;//sis点id
        public string nd;//节点名。
        public string pn;//点名
        public int rt = 0;//点类类
        public string ed;//点描述
        //可null
        public double? tv = null;//量程上限
        public double? bv = null;//量程下限
        public double? ll = null;//报警低限
        public double? hl = null;//报警高限
        public double? zh = null;//报警高2限
        public double? zl = null;//报警低2限
        public pointsrc pointsrc = 0;//点源0:sis,1:计算点
        public int ownerid = 0;//专业id
        public string orgformula = "";//计算公式
        public string expformula = "";//展开成点的计算公式
        public int id = -1;//点id
        public string eu;//点单位
        public bool iscalc;//是否进行计算
        public bool isavalarm;//是否报警
        public PointState ps = PointState.Error;//点状态
        public double av = -1;//点值，实时或计算。
        public short fm = 0;//保留小数点位数。
        public bool calciserror = false;
        //
        public bool isboolv = false;
        public string boolalarminfo = "";//isbool 为真时的报警信息。
        //
        public Expression expression = new Expression();//优化计算速度。
        //计算子点id
        public List<subpoint> lsCalcOrgSubPoint = new List<subpoint>();//原始参能与计算点列表
        //
        //计算子点id用于进行计算点状态计算。
        public List<point> listSisCalaExpPointID = new List<point>();//展开成sis点的参与计算点列表。
        private string AlarmPrx()
        {
            if (pointsrc == pointsrc.sis)
                return string.Format("SIS点{[0]}-{1}",pn,ed);
            return string.Format("计算点{[0]}", ed);
        }
        public string AlarmCalc()
        {
            if (isboolv)
            {
                if (Convert.ToBoolean(av))
                    return string.Format("{0}------{1}！", AlarmPrx(), boolalarminfo);
            }
            else if(isavalarm)
            {
                if (zh != null && av > zh)
                    return string.Format("{0}------越报警高2限！", AlarmPrx());
                if (hl != null && av > hl)
                    return string.Format("{0}------越报警高限！", AlarmPrx());
                if (tv != null && av > tv)
                    return string.Format("{0}------越量程上限！", AlarmPrx());

                if (bv != null && av < bv)
                    return string.Format("{0}------越量程下限！", AlarmPrx());
                if (ll != null && av < ll)
                    return string.Format("{0}------越报警低限！", AlarmPrx());
                if (zl != null && av < zl)
                    return string.Format("{0}------越报警低2限！", AlarmPrx());
                else return "";
            }
            return "";
        }
    }
   
}
