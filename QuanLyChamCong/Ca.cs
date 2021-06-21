using System;

namespace QuanLyChamCong
{
    class Ca
    {
        private string maca;
        private TimeSpan batdau;
        private TimeSpan ketthuc;

        public string Maca { get => maca; set => maca = value; }
        public TimeSpan Batdau { get => batdau; set => batdau = value; }
        public TimeSpan Ketthuc { get => ketthuc; set => ketthuc = value; }

        public Ca()
        {
            maca = "";
            batdau = DateTime.Now.TimeOfDay;
            ketthuc = DateTime.Now.TimeOfDay;
        }
        public Ca(string maca, TimeSpan batdau, TimeSpan ketthuc)
        {
            this.maca = maca;
            this.batdau = batdau;
            this.ketthuc = ketthuc;
        }
        public override string ToString()
        {
            return $"• Mã ca: {maca}, Thời gian bắt đầu: {batdau}, Thời gian kết thúc: {ketthuc}";
        }
        public void Input()
        {
            Program.WriteLine("-------------THÊM CA MỚI--------------", ConsoleColor.Yellow);
            Program.WriteLine("*Tips: Định dạng thời gian theo kiểu 21:32:56 (21 giờ, 32 phút, 56 giây).", ConsoleColor.Green);

            maca = Program.Read("• Nhập Mã ca: ");
            batdau = TimeSpan.Parse(Program.Read("• Nhập Thời gian bắt đầu: "));
            ketthuc = TimeSpan.Parse(Program.Read("• Nhập Thời gian kết thúc: "));
        }
        public void Change()
        {
            Program.WriteLine($"-----CHỈNH SỬA THÔNG TIN CA LÀM VIỆC {maca}-----", ConsoleColor.Yellow);
            Program.WriteLine("*Tips: Định dạng thời gian theo kiểu 21:32:56 (21 giờ, 32 phút, 56 giây).", ConsoleColor.Green);
            Program.WriteLine("*Tips: Để trống nếu như không muốn thay đổi giá trị.", ConsoleColor.Green);

            maca = Program.Read($"• Mã ca ({maca}): ", maca);
            batdau = TimeSpan.Parse(Program.Read($"• Nhập Thời gian bắt đầu ({batdau}): ", batdau.ToString()));
            ketthuc = TimeSpan.Parse(Program.Read($"• Nhập Thời gian kết thúc ({ketthuc}): ",ketthuc.ToString()));
        }
        public string Output()
        {
            return $"{maca}|{batdau}|{ketthuc}";
        }
    }
}
