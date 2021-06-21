using System;
using System.Text;

namespace QuanLyChamCong
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            WriteLine("--------------------------------------");
            WriteLine("------PHẦN MỀM QUẢN LÝ CHẤM CÔNG------");
            WriteLine("------------NHÓM 10 - ST001-----------");
            WriteLine("--------------------------------------");
            Menu();
        }
        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void Write(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static string Read(string text)
        {
            Write(text, ConsoleColor.Red);
            return Console.ReadLine();
        }
        public static string Read(string text, string data)
        {
            string output = Read(text);
            if (output == "") return data;
            return output;
        }
        static void Menu()
        {
            Present:
            try
            {
                do
                {
                    WriteLine("\n------------CONTROL PANEL------------- ", ConsoleColor.Yellow);
                    QuanLy.ThongKe();
                    WriteLine("\n 0. Đọc dữ liệu \n 1. Nhân viên \n 2. Xưởng \n 3. Ca làm việc \n 4. Chấm công \n 5. Lương \n 6. Ghi dữ liệu \n 7. Thoát");
                    int code = int.Parse(Read(">> Code: "));
                    string text;
                    switch (code)
                    {
                        case 0: // đọc dữ liệu
                            WriteLine("------------ĐỌC DỮ LIỆU--------------", ConsoleColor.Yellow);
                            WriteLine("*Tips: Dữ liệu trong tệp không cần tuân theo thứ tự nhưng bắt buộc phải đúng định dạng đề ra.", ConsoleColor.Green);
                            text = Read(">> Đường dẫn data file: ");
                            QuanLy.DocDuLieu(text);
                            break;
                        case 1: // nhân viên
                            do
                            {
                                WriteLine("\n----------QUẢN LÝ NHÂN VIÊN----------- ", ConsoleColor.Yellow);
                                QuanLy.ThongKe();
                                WriteLine("\n 0. Xem \n 1. Sửa \n 2. Xóa \n 3. Tìm kiếm \n 4. Thêm");
                                code = int.Parse(Read(">> Code: "));
                                switch (code)
                                {
                                    case 0:
                                        QuanLy.HienThi(0);
                                        break;
                                    case 1:
                                        QuanLy.Sua(0);
                                        break;
                                    case 2:
                                        QuanLy.Xoa(0);
                                        break;
                                    case 3:
                                        QuanLy.TimKiem(0, true);
                                        break;
                                    case 4:
                                        QuanLy.Them(0);
                                        break;
                                    default:
                                        goto Present;
                                }
                            }
                            while (true);
                        case 2: // xưởng
                            do
                            {
                                WriteLine("\n------------QUẢN LÝ XƯỞNG------------- ", ConsoleColor.Yellow);
                                QuanLy.ThongKe();
                                WriteLine("\n 0. Xem \n 1. Sửa \n 2. Xóa \n 3. Tìm kiếm \n 4. Thêm");
                                code = int.Parse(Read(">> Code: "));
                                switch (code)
                                {
                                    case 0:
                                        QuanLy.HienThi(1);
                                        break;
                                    case 1:
                                        QuanLy.Sua(1);
                                        break;
                                    case 2:
                                        QuanLy.Xoa(1);
                                        break;
                                    case 3:
                                        QuanLy.TimKiem(1, true);
                                        break;
                                    case 4:
                                        QuanLy.Them(1);
                                        break;
                                    default:
                                        goto Present;
                                }
                            }
                            while (true);
                        case 3: // ca làm việc
                            do
                            {
                                WriteLine("\n-----------QUẢN LÝ CA LÀM------------- ", ConsoleColor.Yellow);
                                QuanLy.ThongKe();
                                WriteLine("\n 0. Xem \n 1. Sửa \n 2. Xóa \n 3. Tìm kiếm \n 4. Thêm");
                                code = int.Parse(Read(">> Code: "));
                                switch (code)
                                {
                                    case 0:
                                        QuanLy.HienThi(2);
                                        break;
                                    case 1:
                                        QuanLy.Sua(2);
                                        break;
                                    case 2:
                                        QuanLy.Xoa(2);
                                        break;
                                    case 3:
                                        QuanLy.TimKiem(2, true);
                                        break;
                                    case 4:
                                        QuanLy.Them(2);
                                        break;
                                    default:
                                        goto Present;
                                }
                            }
                            while (true);
                        case 4: // chấm công
                            do
                            {
                                WriteLine("\n----------QUẢN LÝ CHẤM CÔNG----------- ", ConsoleColor.Yellow);
                                QuanLy.ThongKe();
                                WriteLine("\n 0. Xem \n 1. Chấm công \n 2. Tìm kiếm");
                                code = int.Parse(Read(">> Code: "));
                                switch (code)
                                {
                                    case 0:
                                        QuanLy.HienThi(3);
                                        break;
                                    case 1:
                                        QuanLy.Them(3);
                                        break;
                                    case 2:
                                        QuanLy.TimKiem(3, true);
                                        break;
                                    default:
                                        goto Present;
                                }
                            }
                            while (true);
                        case 5: // lương
                            do
                            {
                                WriteLine("\n------------QUẢN LÝ LƯƠNG------------- ", ConsoleColor.Yellow);
                                QuanLy.ThongKe();
                                WriteLine("\n 0. Xem \n 1. Tính lương \n 2. Tìm kiếm");
                                code = int.Parse(Read(">> Code: "));
                                switch (code)
                                {
                                    case 0:
                                        QuanLy.HienThi(4);
                                        break;
                                    case 1:
                                        QuanLy.TinhLuong();
                                        break;
                                    case 2:
                                        QuanLy.TimKiem(4, true);
                                        break;
                                    default:
                                        goto Present;
                                }
                            }
                            while (true);
                        case 6: // ghi dữ liệu
                            WriteLine("------------GHI DỮ LIỆU--------------", ConsoleColor.Yellow);
                            WriteLine("*Tips: Không ghi dữ liệu của Lương nếu như tệp dữ liệu sẽ được dùng trong chức năng Đọc dữ liệu", ConsoleColor.Green);

                            WriteLine(" 0. Nhân viên \n 1. Xưởng \n 2. Ca \n 3. Số công \n 4. Lương");
                            text = Read(">> Các Code muốn ghi (ví dụ: 1, 2, 4): ");
                            string url = Read(">> Đường dẫn data file (không trùng): ");
                            QuanLy.GhiDuLieu(url, text);
                            break;
                        case 7: // thoát
                            text = Read(">> Bạn có chắc muốn thoát? (y/n): ").ToLower();
                            if (text == "y") return;
                            break;
                        default:
                            WriteLine(">> ERROR CODE NOT FOUND", ConsoleColor.DarkRed);
                            break;
                    }
                }
                while (true);
            }
            catch(Exception e)
            {
                WriteLine("ERROR: " + e.Message, ConsoleColor.DarkRed);
                goto Present;
            }

        }
    }
}
