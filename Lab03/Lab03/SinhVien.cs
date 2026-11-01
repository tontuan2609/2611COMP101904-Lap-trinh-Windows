namespace Lab03
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        private double _diemTrungBinh;
        public double DiemTrungBinh
        {
            get { return _diemTrungBinh; }
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentException("Điểm trung bình phải từ 0 đến 10.");
                _diemTrungBinh = value;
            }
        }

        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, string maLop, double diemTB)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSV;
            MaLop = maLop;
            DiemTrungBinh = diemTB;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.0) return "Giỏi";
            if (DiemTrungBinh >= 6.5) return "Khá";
            if (DiemTrungBinh >= 5.0) return "Trung bình";
            if (DiemTrungBinh >= 3.5) return "Yếu";
            return "Kém";
        }

        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien} | {base.LayThongTin()} | Lớp: {MaLop} | Điểm TB: {DiemTrungBinh} | Xếp loại: {XepLoai()}";
        }
    }
}