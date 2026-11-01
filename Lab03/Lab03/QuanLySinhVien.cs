namespace Lab03
{
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSach = new List<SinhVien>();

        public bool KiemTraTonTai(string maSV)
        {
            return danhSach.Any(sv => sv.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        public void Them(SinhVien sv)
        {
            if (KiemTraTonTai(sv.MaSinhVien))
            {
                Console.WriteLine("=> Lỗi: Mã sinh viên đã tồn tại trong danh sách!");
                return;
            }
            danhSach.Add(sv);
            Console.WriteLine("=> Thêm sinh viên thành công!");
        }

        public void XuatDanhSach()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("=> Danh sách sinh viên đang trống.");
                return;
            }
            foreach (var sv in danhSach)
            {
                Console.WriteLine(sv.LayThongTin());
            }
        }

        public void TimTheoMa(string maSV)
        {
            var sv = danhSach.FirstOrDefault(s => s.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
            if (sv != null)
                Console.WriteLine(sv.LayThongTin());
            else
                Console.WriteLine("=> Không tìm thấy sinh viên có mã này.");
        }

        public void TimTheoTen(string tuKhoa)
        {
            tuKhoa = tuKhoa ?? "";

            var ketQua = danhSach.Where(s =>
                s.HoTen != null &&
                s.HoTen.ToLower().Contains(tuKhoa.ToLower())
            ).ToList();

            if (ketQua.Count == 0)
                Console.WriteLine("=> Không tìm thấy sinh viên nào khớp với từ khóa.");
            else
                ketQua.ForEach(s => Console.WriteLine(s.LayThongTin()));
        }

        public void SuaDiem(string maSV, double diemMoi)
        {
            var sv = danhSach.FirstOrDefault(s => s.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
            if (sv != null)
            {
                sv.DiemTrungBinh = diemMoi;
                Console.WriteLine("=> Cập nhật điểm thành công.");
            }
            else
            {
                Console.WriteLine("=> Không tìm thấy sinh viên cần sửa điểm.");
            }
        }

        public void Xoa(string maSV)
        {
            var sv = danhSach.FirstOrDefault(s => s.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
            if (sv != null)
            {
                danhSach.Remove(sv);
                Console.WriteLine("=> Đã xóa sinh viên khỏi danh sách.");
            }
            else
            {
                Console.WriteLine("=> Không tìm thấy sinh viên để xóa.");
            }
        }

        public void SapXepTheoDiemGiamDan()
        {
            var danhSachSapXep = danhSach.OrderByDescending(s => s.DiemTrungBinh).ToList();
            if (danhSachSapXep.Count == 0)
                Console.WriteLine("=> Danh sách trống.");
            else
                danhSachSapXep.ForEach(s => Console.WriteLine(s.LayThongTin()));
        }

        public void LocSinhVienDat()
        {
            var danhSachDat = danhSach.Where(s => s.DiemTrungBinh >= 5.0).ToList();
            if (danhSachDat.Count == 0)
                Console.WriteLine("=> Không có sinh viên nào đạt.");
            else
                danhSachDat.ForEach(s => Console.WriteLine(s.LayThongTin()));
        }
    }
}