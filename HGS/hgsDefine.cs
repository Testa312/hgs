using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlacialComponents.Controls;

namespace HGS
{

    public class itemtag
    {
        public int sisid = 0;
        public int id = 0;
        public short fm = 2;
        public pointsrc PointSrc = pointsrc.sis;
        //public bool isNew = false;
        //public point Point;
    }
    public class subpoint
    {
        public int id {set;get;}
        public pointsrc PointSrc { set; get; }
        public string varname { set; get; }
    }
    public class point
    {
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
        public double? zh = null;//报警低2限
        public double? zl = null;//报警高2限
        public pointsrc pointsrc = 0;//点源0:sis,1:计算点
        public int ownerid = 0;//专业id
        public string orgformula;//计算公式
        public string expformula;//展开成点的计算公式
        public int id = -1;//点id
        public string eu;//点单位
        public bool iscalc;//是否计算
        public bool isalarm;//是否报警
        public int AS = 0;//点状态
        public double av = 1;//点值，实时或计算。
        public short fm = 0;//保留小数点位数。
        //计算子点id
        public List<subpoint> lsCalcOrgSubPoint = new List<subpoint>();//原始的能与计算点列表
        //
        //计算子点id用于进行计算点状态计算。
        public List<int> listSisCalaExpPointID = new List<int>();//展开成sis点的参与计算点列表。
    }
    //点来源。
    public enum pointsrc
    {
        sis,
        calc,
    }
}
