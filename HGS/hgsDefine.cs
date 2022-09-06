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
    //sw开关量。
    public enum alarmlevel
    {
        ok,tv,hl,zh,bv,ll,zl,sw
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
        Good, Timeout, Bad, Error,Force
    }
    
    public class point
    {
        //
        public int id_sis = 0;//sis点id
        public string nd = "";//节点名。
        public string pn = "";//点名
        //public int rt = 0;//点类型
        public string ed = "";//点描述
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
        public string eu = "";//点单位
        public bool iscalc= false;//是否进行计算
        public bool isavalarm = false;//是否报警
        public PointState ps = PointState.Error;//点状态
        public double av = -1;//点值，实时或计算。
        public short fm = 0;//保留小数点位数。
       
        public bool isboolv = false;
        public string boolalarminfo = "";//isbool 为真时的报警信息。
        //
        public Expression expression = new Expression();//优化计算速度。
        //计算子点id
        public List<subpoint> lsCalcOrgSubPoint = new List<subpoint>();//原始参能与计算点列表
        //
        //计算子点id用于进行计算点状态计算。
        public List<point> listSisCalaExpPointID = new List<point>();//展开成sis点的参与计算点列表。
        //
        //报警用，不存入数据库                                                            
        //
        //public bool calciserror = false;
        public alarmlevel alarmLevel = alarmlevel.ok;
        public DateTime lastalarmdatetime = DateTime.Now;
        public string alarmininfo = "";
        public double alarmingav = -1;
        //强制值，不存数据库
        public bool isforce = false;
        public double forceav = -1;//强制的数值。
        //-------------------
        private string AlarmPrx()
        {
            if (pointsrc == pointsrc.sis)
                return string.Format("SIS点[{0}]-{1}",pn,ed);
            return string.Format("计算点[{0]}]", ed);
        }
        public alarmlevel AlarmCalc()
        {
            alarmininfo = "";
            alarmlevel last_al = alarmLevel;
            alarmLevel = alarmlevel.ok;
            if (isboolv)
            {
                bool blv = Convert.ToBoolean(av);
                if (blv)
                {
                    alarmingav = Convert.ToDouble(blv);
                    alarmLevel = alarmlevel.sw;
                    alarmininfo = string.Format(" {0}", boolalarminfo);
                }
            }
            else if (isavalarm)
            {
                if (zh != null && av > zh)
                {
                    alarmLevel = alarmlevel.zh;
                    alarmininfo = string.Format("越报警高2限[{0}{1}]！", zh, eu);
                }
                else if (hl != null && av > hl)
                {
                    alarmLevel = alarmlevel.hl;
                    alarmininfo = string.Format("越报警高限[{0}{1}]！", hl, eu);
                }
                else if (tv != null && av > tv)
                {
                    alarmLevel = alarmlevel.tv;
                    alarmininfo = string.Format("越量程上限[{0}{1}]！", tv, eu);
                }

                else if (zl != null && av < zl)
                {
                    alarmLevel = alarmlevel.zl;
                    alarmininfo = string.Format("越报警低2限[{0}{1}]！", zl, eu);
                }         
                else if (ll != null && av < ll)
                {
                    alarmLevel = alarmlevel.ll;
                    alarmininfo = string.Format("越报警低限[{0}{1}]！", ll, eu);
                }
                else if (bv != null && av < bv)
                {
                    alarmLevel = alarmlevel.bv;
                    alarmininfo = string.Format("越量程下限[{0}{1}]！", bv, eu);
                }

                alarmingav = av;
            }
            if(alarmLevel == alarmlevel.ok && last_al != alarmLevel)
                alarmininfo = string.Format("报警消失！");
            if (last_al == alarmlevel.ok && alarmLevel != alarmlevel.ok)
            {
                lastalarmdatetime = DateTime.Now;
            }
            return alarmLevel;
        }
    }
   
}
