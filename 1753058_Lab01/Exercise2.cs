using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Exercise_2
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap so dien: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Tien dien:{1} VND", n, TienDien(n));
        }

        static double TienDien(int n)
        {
            double result = 0;
            if (n < 51)
            {
                result = (n * 1388);
            }
            if (n < 101 && n >= 51)
            {
                result = (n - 50) * 1433 + (50 * 1388);
            }
            if (n < 201 && n >= 101)
            {
                result = (n - 100) * 1660 + 50 * (1433 + 1388);
            }
            if (n < 301 && n >= 201)
            {
                result = (n - 200) * 2082 + 100 * 1660 + 50 * (1433 + 1388);
            }
            if (n < 401 && n >= 301)
            {
                result = (n - 300) * 2324 + 100 * (2082 + 1660) + 50 * (1433 + 1388);
            }
            if (n >= 401)
            {
                result = (n - 400) * 2399 + 100 * (2324 + 2082 + 1660) + 50 * (1388 + 1433);
            }
            return result *= 1.1;
        }
    }
}
