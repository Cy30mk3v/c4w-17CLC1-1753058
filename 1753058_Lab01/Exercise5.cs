using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Exercise5
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap: ");
            string strIn = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Chuoi in hoa chu dau: {0}", Correct(strIn));
        }

        static string Correct(string strIn)  
        {
            StringBuilder sb = new StringBuilder();
            string[] arrStr = strIn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrStr.Length; i++)
            {
                sb.AppendFormat("{0} ", arrStr[i]);
            }
            strIn = sb.ToString();
            char[] temp = strIn.ToCharArray();
            if(char.IsLower(temp[0]))
            {
                temp[0]=char.ToUpper(temp[0]);
            }
            for(int i=temp.Length-1;i>0;i--)
            {
                if (temp[i - 1] == ' ')
                {
                    temp[i] = char.ToUpper(temp[i]);
                }
                else
                    temp[i] = char.ToLower(temp[i]);
            }
            return new string(temp);
        }
    }
}
