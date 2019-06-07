using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class KhachHang
    {
        public delegate void HangMucHandler();

        public event HangMucHandler DatMucBinhDan;
        public event HangMucHandler DatMucTrungCap;
        public event HangMucHandler DatMucCaoCap;

        static long mucTrungCap = 10000000;
        static long mucCaoCap = 100000000;
        long _taiKhoan = 0;

        public long TaiKhoan
        {
            get {return _taiKhoan;}
            set
            {
                _taiKhoan = value;

                if(_taiKhoan <mucTrungCap)
                {
                    if(DatMucBinhDan !=null)
                    {
                        DatMucBinhDan();
                    }
                }
                else
                {
                    if (_taiKhoan < mucCaoCap)
                    {
                        if (DatMucTrungCap != null)
                        {
                            DatMucTrungCap();
                        }
                    }
                    else
                    {
                        if(DatMucCaoCap!=null)
                        {
                            DatMucCaoCap();
                        }
                    }
                }
            }
        }
        public void ThemThuNhap(long soTien)
        {
            TaiKhoan += soTien;
        }
    }
}
