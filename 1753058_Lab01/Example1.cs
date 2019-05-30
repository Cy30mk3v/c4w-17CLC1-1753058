using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap n: ");
            string strLine = Console.ReadLine();
            uint n = 1;
            if(!uint.TryParse(strLine,out n))
            {
                n = 1;
            }
            Console.WriteLine("S({0}))={1}", n, TinhSn(n));
        }

        static double TinhSn(uint n)
        {
            double s = 0;
            for (uint i = n; i > 0; i--)
            {
                s = Math.Sqrt(s + i);
            }
            return s;
        }

    }


}


