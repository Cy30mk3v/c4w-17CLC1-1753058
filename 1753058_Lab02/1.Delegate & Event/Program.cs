using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        delegate decimal TinhToan(decimal v1, decimal v2);
        delegate void KQTinhToan(decimal v1, decimal v2);
        static void Main(string[] args)
        {
            Random rd = new Random();
            decimal v1, v2;
            char c = 'a', pt;
            int n = -1;
            while(n!=1 && n!=2 && n!=3 && n!=4 && n!=5)
            {
                Console.WriteLine("Chon vi du :");
                Console.WriteLine("1- Math Operations");
                Console.WriteLine("2- Xu ly tinh toan");
                Console.WriteLine("3- Sinh vien");
                Console.WriteLine("4- Khach hang");
                Console.WriteLine("5- Example Event (Print HelloUser)");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input:{0}", n);
            }
            switch (n)
            {
                case 1:
                    {
                        while (c != 'q')
                        {
                            TinhToan tt = LuaChonPhepToan(out pt);
                            v1 = rd.Next(1000);
                            v2 = rd.Next(1000);
                            Console.WriteLine("\n{0} {1} {2} = {3}", v1, pt, v2, tt(v1, v2));
                            Console.Write("Tiep tuc?");
                            c = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                        }
                        break;
                    }
                case 2:
                    {
                        KQTinhToan tt = XLTinhToan.PhepCong;
                        tt += XLTinhToan.PhepTru;
                        tt += XLTinhToan.PhepNhan;
                        tt += XLTinhToan.PhepChia;
                        while (c != 'q')
                        {
                            Console.Write("Nhap vao 2 so:");
                            v1 = Nhapso();
                            v2 = Nhapso();
                            tt(v1, v2);
                            Console.WriteLine("Tiep tuc?");
                            c = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                        }
                        break;
                    }
                case 3:
                    {
                        n = rd.Next(5, 10);
                        Sinhvien[] dsSV = new Sinhvien[n];
                        for(int i=0;i<n;i++)
                        {
                            dsSV[i] = new Sinhvien()
                            {
                                MSSV = 1364000 + rd.Next(999),
                                HoTen = string.Format("{0}HT", (char)rd.Next(0x41, 0x5A)),
                                QueQuan = string.Format("{0}QQ", (char)rd.Next(0x61, 0x7A))
                            };
                        }
                        char d = 'a';
                        while (d != 'q')
                        {
                            Console.Write("Sap xep theo tieu chi (1,2,3): ");
                            switch (Console.ReadKey().KeyChar)
                            {
                                case '1':
                                    {
                                        Array.Sort(dsSV, delegate(Sinhvien sv1, Sinhvien sv2)
                                        {
                                            return sv1.MSSV.CompareTo(sv2.MSSV);
                                        });
                                        break;
                                    }
                                case '2':
                                    {
                                        Array.Sort(dsSV, delegate (Sinhvien sv1, Sinhvien sv2)
                                        {
                                            return sv1.HoTen.CompareTo(sv2.HoTen);
                                        });
                                        break;
                                    }
                                case '3':
                                    {
                                        Array.Sort(dsSV, delegate (Sinhvien sv1, Sinhvien sv2)
                                        {
                                            return sv1.QueQuan.CompareTo(sv2.QueQuan);
                                        });
                                        break;
                                    }                                                         
                            }
                            Console.WriteLine();
                            foreach(var item in dsSV)
                            {
                                Console.WriteLine(item);
                            }
                            Console.Write("Tiep tuc?");
                            d = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                        }
                        break;
                    }
                case 4:
                    {
                        KhachHang kh = new KhachHang();
                        kh.DatMucBinhDan += new KhachHang.HangMucHandler(TTBinhDan);
                        kh.DatMucTrungCap += TTTrungCap;
                        kh.DatMucCaoCap += TTCaocap;
                        char d = 'a';
                        while (d!='q')
                        {
                            Console.Write("Nhan a de them thu nhap");
                            if(Console.ReadKey().Key == ConsoleKey.A)
                            {
                                kh.ThemThuNhap(rd.Next(1000000, 9000000));
                            }
                            Console.WriteLine();
                            Console.Write("Tiep tuc?");
                            c = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                        }
                        break;
                    }
                case 5:
                    {
                        string temp;
                        PrintEvent Event1 = new PrintEvent();
                        Console.Write("Input username: ");
                        temp = Console.ReadLine();
                        Console.WriteLine(Event1.HelloUser(temp));
                        break;
                    }
            }
        }

        static TinhToan LuaChonPhepToan(out char phepToan)
        {
            TinhToan tt = null;
            Console.Write("Lua chon phep toan: ");
            phepToan = Console.ReadKey().KeyChar;
            switch(phepToan)
            {
                case '+':
                    tt = new TinhToan(MathOperations.Cong);
                    break;
                case '-':
                    tt = new TinhToan(MathOperations.Tru);
                    break;
                case '*':
                    tt = new TinhToan(MathOperations.Nhan);
                    break;
                case '/':
                    tt = new TinhToan(MathOperations.Chia);
                    break;
            }
            return tt;
        }

        static decimal Nhapso()
        {
            decimal n = 0;
            string strLine = Console.ReadLine();
            if(!decimal.TryParse(strLine,out n))
            {
                n = -1;
            }
            return n;
        }

        static void TTBinhDan()
        {
            Console.WriteLine();
            Console.WriteLine("Tiep thi hang binh dan");
        }

        static void TTCaocap()
        {
            Console.WriteLine();
            Console.WriteLine("Tiep thi hang cao cap");
        }

        static void TTTrungCap()
        {
            Console.WriteLine();
            Console.WriteLine("Tiep thi hang trung cap");
        }
    }
}
