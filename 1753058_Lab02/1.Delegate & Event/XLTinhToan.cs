using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    //Xử lý tính toán
    class XLTinhToan
    {
        public static void PhepCong(decimal v1, decimal v2)
        {
            Console.WriteLine("{0} + {1} = {2}", v1, v2, v1 + v2);
        }

        public static void PhepTru(decimal v1, decimal v2)
        {
            Console.WriteLine("{0} - {1} = {2}", v1, v2, v1 - v2);
        }

        public static void PhepNhan(decimal v1, decimal v2)
        {
            Console.WriteLine("{0} x {1} = {2}", v1, v2, v1 * v2);
        }

        public static void PhepChia(decimal v1, decimal v2)
        {
            Console.WriteLine("{0} : {1} = {2}", v1, v2, v1 / v2);
        }
    }
}
