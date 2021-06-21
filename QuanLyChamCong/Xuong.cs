using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyChamCong
{
    class Xuong
    {
        private string maxuong;
        private string tenxuong;
        public string Maxuong { get => maxuong; set => maxuong = value; }
        public string Tenxuong { get => tenxuong; set => tenxuong = value; }
        public Xuong()
        {
            maxuong = "";
            tenxuong = "";
        }
        public Xuong(string maxuong, string tenxuong)
        {
            this.maxuong = maxuong;
            this.tenxuong = tenxuong;
        }
        public override string ToString()
        {
            return $"• Mã xưởng: {maxuong}, Tên xưởng: {tenxuong}";
        }
        public void Input()
        {
            Program.WriteLine("------------THÊM XƯỞNG MỚI------------", ConsoleColor.Yellow);

            maxuong = Program.Read("• Nhập Mã xưởng: ");
            tenxuong = Program.Read("• Nhập Tên xưởng: ");
        }
        public void Change()
        {

            Program.WriteLine($"-----CHỈNH SỬA THÔNG TIN XƯỞNG {maxuong}-----", ConsoleColor.Yellow);
            Program.WriteLine("*Tips: Để trống nếu như không muốn thay đổi giá trị.", ConsoleColor.Green);

            maxuong = Program.Read($"• Mã xưởng ({maxuong}): ", maxuong);
            tenxuong = Program.Read($"• Tên xưởng ({tenxuong}): ", tenxuong);
        }
        public string Output()
        {
            return $"{maxuong}|{tenxuong}";
        }
    }
}
