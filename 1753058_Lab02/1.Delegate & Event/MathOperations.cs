using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class MathOperations
    {
        public static decimal Cong(decimal v1, decimal v2)
        {
            return v1 + v2;
        }

        public static decimal Tru(decimal v1, decimal v2)
        {
            return v1 - v2;
        }

        public static decimal Nhan(decimal v1, decimal v2)
        {
            return v1 * v2;
        }

        public static decimal Chia(decimal v1, decimal v2)
        {
            return v1 / v2;
        }
    }
}
