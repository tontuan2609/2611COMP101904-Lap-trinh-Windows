namespace Lab03
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }
        private double diemTrungBinh;
        public double DiemTrungBinh
        {
            get { return diemTrungBinh; }
            set
            {
                if (value >= 0 && value <= 10)
                    diemTrungBinh = value;
            }
        }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh) : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8)
                return "Giỏi";
            if (DiemTrungBinh >= 6.5)
                return "Khá";
            if (DiemTrungBinh >= 5)
                return "Trung bình";
            if (DiemTrungBinh >= 3.5)
                return "Yếu";
            return "Kém";
        }
        public override void LayThongTin()
        {
            Console.WriteLine($"Mã sinh viên: {MaSinhVien}");
            base.LayThongTin();
            Console.WriteLine($"Mã lớp: {MaLop}");
            Console.WriteLine($"Điểm trung bình: {DiemTrungBinh}");
            Console.WriteLine($"Xếp loại: {XepLoai()}");
        }
    }
}