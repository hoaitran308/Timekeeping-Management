using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyChamCong
{
    class Luong
    {
        private string manv;
        private DateTime date;
        private int socong;
        private double tienluong;

        public string Manv { get => manv; set => manv = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Tienluong { get => tienluong; set => tienluong = value; }
        public int Socong { get => socong; set => socong = value; }

        public Luong(string manv, DateTime date, int socong, double tienluong)
        {
            Manv = manv;
            Date = date;
            Socong = socong;
            Tienluong = tienluong;
        }
        public override string ToString()
        {
            return $"Mã nhân viên: {manv}, Ngày tính lương: {date.ToShortDateString()}, Số công đã làm: {socong} công, Tiền lương: {tienluong} VNĐ";
        }
    }
}
