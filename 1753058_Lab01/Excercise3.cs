using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Excercise3
    {
        static void Main(String[] args)
        {
            int month = -1;
            while (month < 1 || month > 12)
            {
                Console.Write("Nhap thang: ");
                month = Convert.ToInt32(Console.ReadLine());
            }
            int year = -1;
            while (year < 0)
            {
                Console.Write("Nhap nam: ");
                year = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("So ngay trong thang {0} nam {1} la: {2}", month, year, DayInMonth(month, year));
        }

        static int DayInMonth(int month, int year)
        {
            if(month==2)
            {
                if (checkLeap(year))
                    return 29;
                return 28;
            }
            if(month <= 7)
            {
                if(month%2==0)
                {
                    return 30;
                }
                return 31;
            }
            if(month%2==0) 
            {
                return 31;
            }
            return 30;
        }

        static bool checkLeap(int year)
        {
            bool C1 = (year % 4) == 0;
            bool C2 = (year % 100) != 0;
            bool C3 = (year % 400) == 0;
            if(C1&&C2||C3)
            {
                return true;
            }
            return false;
        }
    }
}
