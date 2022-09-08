using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGS
{
    class Functions
    {
        /*
        private static Functions instance;

        private Functions() { }

        public static Functions inst()
        {
            if (instance == null)
            {
                instance = new Functions();
            }
            return instance;
        }
        */
        //返回大于n的素数。
        public static int AbovePrimes(int n)
        {
            int i = (n % 2) == 0 ? ++n : n + 2;
            int j = 0;
            for (; i <= 100003; i = i + 2)
            {
                int k = (int)Math.Sqrt(i);
                for (j = 2; j <= k; j++)
                {
                    if ((i % j) == 0)
                    {
                        break;
                    }
                }

                if (j > k)
                {
                    return i;
                }
            }
            //最大100003
            return 100003;
        }
        public static string dtoNULL(double? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }
    }
}
