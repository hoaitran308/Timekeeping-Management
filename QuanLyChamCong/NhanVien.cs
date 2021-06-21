using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyChamCong
{
    class NhanVien
    {
        private string manv;
        private string hoten;
        private bool ismale;
        private string sdt;
        private string cmnd;
        private string diachi;
        private string maxuong;
        private int luong;

        public string Manv { get => manv; set => manv = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public bool Ismale { get => ismale; set => ismale = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Cmnd { get => cmnd; set => cmnd = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Maxuong { get => maxuong; set => maxuong = value; }
        public int Luong { get => luong; set => luong = value; }

        public NhanVien()
        {
            Manv = "";
            Hoten = "";
            Ismale = true;
            Sdt = "";
            Cmnd = "";
            Diachi = "";
            Maxuong = "";
            Luong = 0;
        }
        public NhanVien(string manv, string hoten, bool ismale, string sdt, string cmnd, string diachi, string maxuong, int luong)
        {
            Manv = manv;
            Hoten = hoten;
            Ismale = ismale;
            Sdt = sdt;
            Cmnd = cmnd;
            Diachi = diachi;
            Maxuong = maxuong;
            Luong = luong;
        }
        public override string ToString()
        {
            return $"• Mã nhân viên: {manv}, Họ và tên: {hoten}, {(ismale ? "Nam" : "Nữ")}, SĐT: {sdt}, CMND: {cmnd}, Địa chỉ: {diachi}, Mã xưởng: {Maxuong}, Số tiền/1 công: {luong}";
        }
        public void Input()
        {
            object obj;
            Program.WriteLine("----------THÊM NHÂN VIÊN MỚI----------", ConsoleColor.Yellow);

            do // Kiểm tra trùng mã nv
            {
                manv = Program.Read("• Nhập Mã nhân viên: ");
                obj = QuanLy.TimKiem(0, false, manv);
                if (obj != null) { Program.WriteLine(">> MÃ NHÂN VIÊN TRÙNG!", ConsoleColor.DarkRed); }
            }
            while (obj != null);
            hoten = Program.Read("• Họ tên nhân viên: ");

            ismale = Program.Read("• Giới tính (nam - 0, nữ - 1): ") == "0";
            sdt = Program.Read("• Số điện thoại: ");
            cmnd = Program.Read("• Chứng minh nhân dân: ");
            diachi = Program.Read("• Địa chỉ: ");

            do // kiểm tra xưởng
            {
                maxuong = Program.Read("• Mã xưởng: ");
                if (maxuong == "") break;
                obj = QuanLy.TimKiem(1, false, maxuong);
                if (obj == null) { Program.WriteLine(">> XƯỞNG KHÔNG TỒN TẠI!", ConsoleColor.DarkRed); }
            }
            while (obj == null);

            luong = int.Parse(Program.Read("• Số lương/1 công làm (VND): "));
        }
        public void Change()
        {
            Program.WriteLine($"-----CHỈNH SỬA THÔNG TIN NHÂN VIÊN {manv}-----", ConsoleColor.Yellow);
            Program.WriteLine("*Tips: Để trống nếu như không muốn thay đổi giá trị.", ConsoleColor.Green);

            object obj;
            string getText;

            do // kiểm tra trùng nhân viên
            {
                getText = Program.Read($"• Mã nhân viên ({manv}): ", manv);
                if (getText == manv) { break; }
                obj = QuanLy.TimKiem(0, false, getText);
                if (obj != null) { Program.WriteLine(">> MÃ NHÂN VIÊN TRÙNG!", ConsoleColor.DarkRed); }
            }
            while (obj != null);
            manv = getText;

            hoten = Program.Read($"• Họ tên nhân viên ({hoten}): ", hoten);
            ismale = Program.Read($"• Giới tính (nam - 0, nữ - 1) ({(ismale ? 0 : 1)}): ", ismale ? "0" : "1") == "0";
            sdt = Program.Read($"• Số điện thoại ({sdt}): ", sdt);
            cmnd = Program.Read($"• Chứng minh nhân dân ({cmnd}): ", cmnd);
            diachi = Program.Read($"• Địa chỉ ({diachi}): ", diachi);

            do // kiểm tra xưởng tồn tại
            {
                getText = Program.Read($"• Mã xưởng ({maxuong}): ", maxuong);
                if (getText == maxuong) { break; }
                obj = QuanLy.TimKiem(1, false, getText);
                if (obj == null) { Program.WriteLine(">> KHÔNG TÌM THẤY XƯỞNG!", ConsoleColor.DarkRed); }
            }
            while (obj == null);
            maxuong = getText;

            luong = int.Parse(Program.Read($"• Số lương/1 công làm ({luong} VND): ", Convert.ToString(luong)));
        }
        public string Output()
        {
            return $"{manv}|{hoten}|{(ismale ? "0" : "1")}|{sdt}|{cmnd}|{diachi}|{maxuong}|{luong}";
        }

    }
}
