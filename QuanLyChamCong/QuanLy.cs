using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuanLyChamCong
{
    class QuanLy
    {
        public static List<NhanVien> NhanViens = new List<NhanVien>();
        public static List<Ca> Cas = new List<Ca>();
        public static List<Xuong> Xuongs = new List<Xuong>();
        public static List<Cong> Congs = new List<Cong>();
        public static List<Luong> Luongs = new List<Luong>();
        public static string[] sampleText = { ">> Nhập Mã nhân viên hoặc SĐT hoặc CMND: ", ">> Nhập Mã xưởng: ", ">> Nhập Mã ca: ", ">> Nhập Mã nhân viên hoặc Mã ca: ", ">> Nhập Mã nhân viên: " };
        /// <summary>
        /// <para>Đọc dữ liệu từ tệp</para>
        /// <para>Có thể đọc dữ liệu: Nhân viên, Ca, Xưởng, Công (lưu ý chỉ được có 4 định dạng này trong tệp cần đọc)</para>
        /// <para>Định dạng <b>Nhân Viên</b>: nhanvien|mã nhân viên|tên nhân viên|giới tính (nam: 0, nữ: 1)|số điện thoại|địa chỉ|mã xưởng|lương/1 công</para>
        /// <para>Định dạng <b>Ca</b>: ca|mã ca|thời gian bắt đầu|thời gian kết thúc</para>
        /// <para>Định dạng <b>Xưởng</b>: xuong|mã xưởng|tên xưởng</para>
        /// <para>Định dạng <b>Công</b>: cong|mã nhân viên|ngày giờ bắt đầu|ngày giờ kết thúc</para>
        /// </summary>
        /// <param name="path">Địa chỉ dẫn đến file data cần đọc</param>
        public static void DocDuLieu(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                while (!sr.EndOfStream)
                {
                    string[] str = sr.ReadLine().Split('|');
                    switch (str[0].ToLower())
                    {
                        case "xuong":
                            Xuong xuong = new Xuong(str[1], str[2]);
                            Xuongs.Add(xuong);
                            break;
                        case "ca":
                            TimeSpan batdau = TimeSpan.Parse(str[2]);
                            TimeSpan ketthuc = TimeSpan.Parse(str[3]);
                            Ca ca = new Ca(str[1], batdau, ketthuc);
                            Cas.Add(ca);
                            break;
                        case "cong":
                            DateTime start = DateTime.Parse(str[2]);
                            DateTime end = DateTime.Parse(str[3]);
                            Cong cong = new Cong(str[1], start, end);
                            Ca findca = FindCa(start, end);
                            cong.Maca = findca.Maca;
                            Congs.Add(cong);
                            break;
                        case "nhanvien":
                            NhanVien nv = new NhanVien(str[1], str[2], (str[3] == "0" ? true : false), str[4], str[5], str[6], str[7], int.Parse(str[8]));
                            NhanViens.Add(nv);
                            break;
                        default:
                            Program.WriteLine(">> Định dạng data không đúng", ConsoleColor.DarkRed);
                            return;
                    }
                }
                sr.Close();
                Program.WriteLine(">> Đọc dữ liệu thành công", ConsoleColor.DarkGreen);
            }
            else
            {
                Program.WriteLine(">> Không tìm thấy đường dẫn", ConsoleColor.DarkRed);
            }
        }
        public static DateTime MinMaxDate(DateTime dt1, DateTime dt2, bool findMin)
        {
            if (findMin) // tìm min Date
            {
                if (dt1.CompareTo(dt2) < 0) return dt1;
                return dt2;
            }
            else // tìm max Date
            {
                if (dt1.CompareTo(dt2) > 0) return dt1;
                return dt2;
            }
        }
        public static Ca FindCa(DateTime start, DateTime end)
        {
            // Xác định Công(start, end) thuộc về Ca nào
            DateTime cstart, cend;
            Ca result = new Ca();
            double maxValue = 0;
            double findValue;
            foreach(Ca ca in Cas)
            {
                cstart = end.Date;
                cend = end.Date;
                if (ca.Batdau > ca.Ketthuc) cstart = cstart.AddDays(-1);
                cstart = cstart.Add(ca.Batdau);
                cend = cend.Add(ca.Ketthuc);
                if (start < cend && end > cstart)
                {
                    findValue = (MinMaxDate(end, cend, true) - MinMaxDate(start, cstart, false)).TotalSeconds;
                    if (findValue > maxValue)
                    {
                        maxValue = findValue;
                        result = ca;
                    }
                }
            }
            return result;
        }
        public static void Them(int request)
        {
            object obj;
            switch (request)
            {
                case 0:
                    NhanVien nv = new NhanVien();
                    nv.Input();
                    NhanViens.Add(nv);
                    break;
                case 1:
                    Xuong xuong = new Xuong();
                    xuong.Input();
                    Xuongs.Add(xuong);
                    break;
                case 2:
                    Ca ca = new Ca();
                    ca.Input();
                    Cas.Add(ca);
                    break;
                case 3:
                    // tìm kiếm nhân viên
                    obj = TimKiem(0, true);
                    if (obj is null) return;

                    // chấm công cho nhân viên
                    nv = (NhanVien)obj;
                    Cong cong = new Cong();
                    cong.Input(nv.Manv);
                    Congs.Add(cong);

                    break;
                default:
                    Program.WriteLine(">> ERROR CODE NOT FOUND", ConsoleColor.DarkRed);
                    return;
            }
            Program.WriteLine(">> Thêm thành công", ConsoleColor.DarkGreen);
        }
        public static void Sua(int request)
        {
            object obj;
            switch (request)
            {
                case 0:
                    // tìm kiếm nhân viên
                    obj = TimKiem(0, true);
                    if (obj is null) return;

                    // sửa nhân viên
                    NhanVien nv = (NhanVien)obj;
                    nv.Change();

                    break;
                case 1:
                    // tìm kiếm xưởng
                    obj = TimKiem(1, true);
                    if (obj is null) return;

                    // sửa xưởng
                    Xuong xuong = (Xuong)obj;
                    xuong.Change();

                    break;
                case 2:
                    // tìm kiếm ca
                    obj = TimKiem(2, true);
                    if (obj is null) return;

                    // sửa ca
                    Ca ca = (Ca)obj;
                    ca.Change();

                    break;
                default:
                    Program.WriteLine(">> ERROR CODE NOT FOUND", ConsoleColor.DarkRed);
                    return;
            }
            Program.WriteLine(">> Sửa thành công", ConsoleColor.DarkGreen);
        }
        public static void Xoa(int request)
        {
            object obj;
            string text;
            switch (request)
            {
                case 0:
                    // tìm kiếm nhân viên
                    obj = TimKiem(0, true);
                    if (obj is null) return;

                    // xác nhận xóa nhân viên
                    NhanVien em = (NhanVien)obj;
                    text = Program.Read($">> Bạn có chắc muốn xóa nhân viên {em.Manv}? (y/n): ");

                    // xóa nhân viên
                    if (text == "y")
                    {
                        // xóa toàn bộ công của nhân viên đó
                        List<Cong> listcongs = (List<Cong>)TimKiem(3, false, em.Manv);
                        foreach (Cong cong in listcongs)
                            Congs.Remove(cong);
                        // xóa nhân viên
                        NhanViens.Remove(em);
                    }
                    else return;
                    break;
                case 1:
                    // tìm kiếm xưởng
                    obj = TimKiem(1, true);
                    if (obj is null) return;
                    
                    // xác nhận xóa xưởng
                    Xuong xuong = (Xuong)obj;
                    text = Program.Read($">> Bạn có chắc muốn xóa xưởng {xuong.Maxuong}? (y/n): ");

                    // xóa xưởng
                    if (text == "y")
                    {
                        // gỡ xưởng ra khỏi nhân viên
                        foreach (NhanVien nv in NhanViens)
                            if (nv.Maxuong == xuong.Maxuong)
                                nv.Maxuong = "";
                        // xóa xưởng
                        Xuongs.Remove(xuong);
                    }
                    else return;
                    break;
                case 2:
                    // tìm kiếm ca
                    obj = TimKiem(2, true);
                    if (obj is null) return;

                    // xác nhận xóa ca
                    Ca ca = (Ca)obj;
                    text = Program.Read($">> Bạn có chắc muốn xóa ca {ca.Maca}? (y/n): ");

                    // xóa ca
                    if (text == "y") Cas.Remove(ca);
                    else return;
                    break;
                default:
                    Program.WriteLine(">> ERROR CODE NOT FOUND", ConsoleColor.DarkRed);
                    return;
            }
            Program.WriteLine(">> Xóa thành công", ConsoleColor.DarkGreen);
        }
        public static void HienThi(int request)
        {
            switch (request)
            {
                case 0:
                    foreach (NhanVien nv in NhanViens)
                    {
                        Program.WriteLine(nv.ToString(), ConsoleColor.Cyan);
                    }
                    break;
                case 1:
                    foreach (Xuong xuong in Xuongs)
                    {
                        Program.WriteLine(xuong.ToString(), ConsoleColor.Cyan);
                    }
                    break;
                case 2:
                    foreach (Ca ca in Cas)
                    {
                        Program.WriteLine(ca.ToString(), ConsoleColor.Cyan);
                    }
                    break;
                case 3:
                    foreach (Cong cong in Congs)
                    {
                        Program.WriteLine(cong.ToString(), ConsoleColor.Cyan);
                    }
                    break;
                case 4:
                    foreach (Luong luong in Luongs)
                    {
                        Program.WriteLine(luong.ToString(), ConsoleColor.Cyan);
                    }
                    break;
                default:
                    Program.WriteLine(">> ERROR CODE NOT FOUND", ConsoleColor.DarkRed);
                    return;
            }
        }
        public static object TimKiem(int request, bool wannaPrint = false, string key = "")
        {
            if (key == "")
            {
                // Nếu key trống thì nhập key.
                key = Program.Read(sampleText[request]);
            }
            switch (request)
            {
                case 0:
                    foreach (NhanVien nv in NhanViens)
                        if (nv.Manv == key || nv.Sdt == key || nv.Cmnd == key || nv.Maxuong == key)
                        {
                            if (wannaPrint) Program.WriteLine(nv.ToString(), ConsoleColor.Cyan);
                            return nv;
                        }
                    break;
                case 1:
                    foreach (Xuong xuong in Xuongs)
                        if (xuong.Maxuong == key)
                        {
                            if (wannaPrint) Program.WriteLine(xuong.ToString(), ConsoleColor.Cyan);
                            return xuong;
                        }
                    break;
                case 2:
                    foreach (Ca ca in Cas)
                        if (ca.Maca == key)
                        {
                            if (wannaPrint) Program.WriteLine(ca.ToString(), ConsoleColor.Cyan);
                            return ca;
                        }
                    break;
                case 3:
                    List<Cong> ListCongs = new List<Cong>();
                    foreach(Cong cong in Congs)
                        if (cong.Manv == key || cong.Maca == key)
                        {
                            ListCongs.Add(cong);
                            if (wannaPrint) Program.WriteLine(cong.ToString(), ConsoleColor.Cyan);
                        }
                    return ListCongs;
                case 4:
                    foreach (Luong luong in Luongs)
                        if (luong.Manv == key)
                        {
                            if (wannaPrint) Program.WriteLine(luong.ToString(), ConsoleColor.Cyan);
                            return luong;
                        }
                    break;
                default:
                    Program.WriteLine(">> ERROR CODE NOT FOUND", ConsoleColor.DarkRed);
                    break;
            }
            if (wannaPrint) Program.WriteLine(">> Không tìm thấy", ConsoleColor.Red);
            return null;
        }
        public static void TinhLuong()
        {
            Luongs.Clear();
            Program.WriteLine("--------TÍNH LƯƠNG NHÂN VIÊN---------", ConsoleColor.Yellow);
            Program.WriteLine("*Tips: Định dạng ngày theo kiểu Year/Month/Day", ConsoleColor.Green);
            Program.WriteLine("*Tips: Chỉ tính lương cho công đủ (công đủ là công có số giờ làm >= số giờ của ca)", ConsoleColor.Green);
            Program.WriteLine("*Tips: Tính lương từ đầu tháng cho đến ngày nhập vào", ConsoleColor.Green);
            DateTime date = DateTime.Parse(Program.Read(">> Nhập Ngày tháng năm: "));
            foreach(NhanVien nv in NhanViens)
            {
                int total_cong = 0;
                foreach(Cong cong in Congs)
                {
                    if (   cong.Manv == nv.Manv 
                        && cong.Ketthuc.Month == date.Month
                        && cong.Ketthuc.Year == date.Year
                        && cong.Ketthuc <= date
                        )
                    {
                        // xác định công đủ hay không
                        Ca ca = (Ca)TimKiem(2, false, cong.Maca);
                        if (cong.Ketthuc-cong.Batdau >= ca.Ketthuc-ca.Batdau) ++total_cong;
                    }
                }
                Luong luong = new Luong(nv.Manv, date, total_cong, nv.Luong * total_cong);
                Luongs.Add(luong);
            }
            Program.WriteLine(">> Tính lương thành công", ConsoleColor.DarkGreen);
        }
        public static void GhiDuLieu(string path, string request)
        {
            if (File.Exists(path))
            {
                Program.WriteLine(">> Trùng file path", ConsoleColor.DarkRed);
                return;
            }

            StreamWriter sw = File.CreateText(path);
            if (request.ToLower().Contains("0"))
            {
                foreach(NhanVien nv in NhanViens)
                {
                    sw.WriteLine(nv.Output());
                }    
            }
            if (request.ToLower().Contains("1"))
            {
                foreach(Xuong xuong in Xuongs)
                {
                    sw.WriteLine(xuong.Output());
                }
            }
            if (request.ToLower().Contains("2"))
            {
                foreach(Ca ca in Cas)
                {
                    sw.WriteLine(ca.Output());
                }
            }
            if (request.ToLower().Contains("3"))
            {
                foreach(Cong cong in Congs)
                {
                    sw.WriteLine(cong.Output());
                }
            }
            if (request.ToLower().Contains("4"))
            {
                foreach(Luong luong in Luongs)
                {
                    sw.WriteLine(luong);
                }
            }
            sw.Close();
            Program.WriteLine(">> Ghi dữ liệu thành công", ConsoleColor.DarkGreen);
        }
        public static void ThongKe()
        {
            Program.WriteLine($">> Total: {NhanViens.Count} nhân viên, {Xuongs.Count} xưởng, {Cas.Count} ca làm, {Congs.Count} số công", ConsoleColor.Cyan); 
        }
    }
}
