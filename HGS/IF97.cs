using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace IF97
{
    class IF97
    {
        public const string dll_name = "IF97.dll";

        [DllImport(dll_name, EntryPoint = "hmass_Tp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern double H(double T, double p);
    }
}
