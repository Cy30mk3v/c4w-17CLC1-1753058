using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Exercise4
    {
        static void Main(string[] args)
        {
            int []n=CreateArray();
            LongestSubArray(n, n.Length);
        }

        static int[] CreateArray()
        {
            Random R1 = new Random();
            Random R2 = new Random();

            int n = R1.Next(100);
            int[] result = new int[n];
            for(int i=0;i<n;i++)
            {
                result[i] = R2.Next(9);
                Console.Write(result[i] + " ");
            }
            Console.WriteLine("\n");
            return result;
        }

        static void LongestSubArray(int []arr,int n)
        {
                int max = 1, len = 1, pos = 0; 
                for (int i = 1; i < n; i++)
                {   
                    //If next element is larger -> Increase len
                    if (arr[i] > arr[i - 1])
                        len++;
                    else
                    {
                        //Compare between current length & max position of current increase array
                        if (max < len)
                        {
                            max = len;
                            //Update position
                            pos = i - max;
                        }
                        len = 1;
                    }
                }
                if (max < len)
                {
                    max = len;
                    pos = n - max;
                }
                for (int i = pos; i < max + pos; i++)
                    Console.Write(arr[i] + " ");
            
        }
    }
}
