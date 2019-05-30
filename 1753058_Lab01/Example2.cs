using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Example1
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap n:");
            string strLine = Console.ReadLine();

            int n = 1;
            if(!int.TryParse(strLine,out n))
            {
                n = 1;
            }
            if(n<0)
            {
                n = -n;
            }

            Random rd = new Random();

            int[] arrI = new int[n];
            for(int i=0;i<n;i++)
            {
                arrI[i] = rd.Next(1000);
            }

            for(int i=0;i<n;i++)
            {
                Console.Write("{0},",arrI[i]);
            }

            double s = TinhTBCong(arrI);
            Console.WriteLine();
            Console.WriteLine("S={0:F2}", s);
        }
        static double TinhTBCong(int[] arrI)
        {
            double s = 0;

            int vOld = int.MinValue;

            int nCur = 0;
            double sCur = 0;

            int nS = 0;

            for(int i=0;i<arrI.Length;i++)
            {
                if (arrI[i] >= vOld)
                {
                    sCur += arrI[i];
                    nCur++;
                }
                else;
                {
                    if (nCur >1)
                    {
                        s += sCur;
                        nS += nCur;
                    }
                    sCur = 0;
                    nCur = 1;
                }
                vOld = arrI[i];
            }
            s /= nS;
            return s;
        }
    }
}
