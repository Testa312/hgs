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
        public int sisID = 0;
        public int id = 0;
        //public int sisAS = 0;
        public int FM = 2;
        public int ownerid = 0;
        public pointsrc PointSrc = pointsrc.sis;
        public string formula = "";
        public bool isAlarm = false;
        public bool isCalc = false;
        public bool isNew = false;
        //计算点id
        public List<int> listcalcpointid = new List<int>();//参与计算的点。
        public string Formula
        {
            set { formula = value; }
            get { return formula; }
        }
        public point Point;
    }
    public class point
    {
        public int id_sis = 0;
        public string nd;//
        public string pn;
        public int rt = 0;
        public string ed;
        //可null
        public double? tv = null;
        public double? bv = null;
        public double? ll = null;
        public double? hl = null;
        public double? zh = null;
        public double? zl = null;
        public pointsrc pointsrc = 0;
        public int ownerid = 0;
        public string formula;
        public int id = -1;
        public string eu;
        public bool iscalc;
        public bool isalarm;
        public int AS = 0;//点状态
        public double av = 0;//点值，实时或计算。
        //计算子点id
        public List<int> lsOrgCalcPointID= new List<int>();//编辑时也用
        //
        //计算子点id
        public List<int> listSisCalaPointID = new List<int>();
    }
    //点来源。
    public enum pointsrc
    {
        sis,
        calc,
    }
}
