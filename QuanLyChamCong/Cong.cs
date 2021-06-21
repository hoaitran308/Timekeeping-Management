using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyChamCong
{
    class Cong
    {
        private string manv;
        private DateTime batdau;
        private DateTime ketthuc;
        private string maca;

        public string Manv { get => manv; set => manv = value; }
        public DateTime Batdau { get => batdau; set => batdau = value; }
        public DateTime Ketthuc { get => ketthuc; set => ketthuc = value; }
        public string Maca { get => maca; set => maca = value; }

        public Cong()
        {
            manv = "";
            batdau = DateTime.Now;
            ketthuc = DateTime.Now;
        }
        public Cong(string manv, DateTime batdau, DateTime ketthuc)
        {
            Manv = manv;
            Batdau = batdau;
            Ketthuc = ketthuc;
        }
        public override string ToString()
        {
            return $"• Mã nhân viên: {manv}, Thời gian bắt đầu: {batdau}, Thời gian kết thúc: {ketthuc}, Mã ca: {maca}";
        }
        public void Input(string manv)
        {
            Program.WriteLine("----------CHẤM CÔNG NHÂN VIÊN---------", ConsoleColor.Yellow);
            Program.WriteLine("*Tips: Định dạng thời gian theo kiểu Year/Month/Day Hour:Minute:Second", ConsoleColor.Green);
            
            this.manv = manv;
            batdau = DateTime.Parse(Program.Read("• Nhập Thời gian chấm công bắt đầu: "));
            ketthuc = DateTime.Parse(Program.Read("• Nhập Thời gian chấm công kết thúc: "));
            Ca ca = QuanLy.FindCa(batdau, ketthuc);
            this.maca = ca.Maca;
        }
        public string Output()
        {
            return $"{manv}|{batdau}|{ketthuc}";
        }

    }
}
