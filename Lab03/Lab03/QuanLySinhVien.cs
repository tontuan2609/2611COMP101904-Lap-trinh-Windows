namespace Lab03
{
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSach;

        public QuanLySinhVien()
        {
            danhSach = new List<SinhVien>();
        }

        // Thêm sinh viên
        public bool Them(SinhVien sinhVien)
        {
            if (danhSach.Any(x => x.MaSinhVien.ToLower() == sinhVien.MaSinhVien.ToLower()))
                return false;
            danhSach.Add(sinhVien);
            return true;
        }

        // Xuất danh sách
        public List<SinhVien> LayDanhSach()
        {
            return danhSach.ToList();
        }

        // Tìm sinh viên theo mã
        public SinhVien? TimTheoMa(string maSinhVien)
        {
            return danhSach.FirstOrDefault(x => x.MaSinhVien.ToLower() == maSinhVien.ToLower());
        }

        // Tìm sinh viên theo tên
        public List<SinhVien> TimTheoTen(string ten)
        {
            return danhSach.Where(x => x.HoTen.ToLower().Contains(ten.ToLower())).ToList();
        }

        // Sửa điểm trung bình
        public bool Sua(string maSinhVien, double diemTrungBinh)
        {
            SinhVien? sinhVien = TimTheoMa(maSinhVien);
            if (sinhVien == null)
                return false;
            sinhVien.DiemTrungBinh = diemTrungBinh;
            return true;
        }

        // Xóa sinh viên
        public bool Xoa(string maSinhVien)
        {
            SinhVien? sinhVien = TimTheoMa(maSinhVien);
            if (sinhVien == null)
                return false;
            danhSach.Remove(sinhVien);
            return true;
        }

        // Sắp xếp theo điểm giảm dần
        public List<SinhVien> SapXepTheoDiem()
        {
            return danhSach.OrderByDescending(x => x.DiemTrungBinh).ToList();
        }

        // Lọc sinh viên đạt
        public List<SinhVien> LocSinhVienDat()
        {
            return danhSach.Where(x => x.DiemTrungBinh >= 5).ToList();
        }
    }
}