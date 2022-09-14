using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlacialComponents.Controls;
using System.Data;
namespace ImportFromSisStat
{
    //Tag of GalcialList'item 
    //点来源。
    public enum pointsrc
    {
        sis,
        calc
    }
    public class point
    {
        public int id = -1;
        public int id_sis = -1;
        public string nd = "";
        public string pn = "";
        public string ed = "";
        public string eu = "";
        public pointsrc Pointsrc = pointsrc.sis;
        public int ownerid = 0;
        public short fm = 1;
        public DateTime mt = DateTime.Now;
        public double? hl = null;
        public double? ll = null;
    }

}
