using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HGS
{
    class TimerState
    {
        //定义委托
        public delegate void SetControlValue(object value);
        //
        public long timeconsum = 0;
        public System.Threading.Timer threadTimer;
        public OPAPI.Connect siscon_keep = null;
        public SetControlValue setControlValue;
        public Stopwatch sW = new Stopwatch();
        public int Formula_error_nums = 0;
    }
}
