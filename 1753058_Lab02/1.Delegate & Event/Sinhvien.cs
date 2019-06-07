using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Sinhvien
    {
        int _MSSV;
        string _hoTen, _queQuan;
        public int MSSV
        {
            get { return _MSSV; }
            set { _MSSV = value; }
        }

        public string QueQuan
        {
            get { return _hoTen; }
            set { _queQuan = value; }
        }

        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }

        public override string ToString()
        {
            return string.Format("<{0}>--{1}-[{2}]", _MSSV, _hoTen, _queQuan);
        }

    }
}
