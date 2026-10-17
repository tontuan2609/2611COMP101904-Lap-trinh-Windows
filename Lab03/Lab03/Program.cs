namespace Lab03
{
    internal class Program
    {
        static QuanLySinhVien quanLySinhVien = new QuanLySinhVien();
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== QUẢN LÝ SINH VIÊN =====");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Xuất danh sách");
                Console.WriteLine("3. Tìm sinh viên theo mã");
                Console.WriteLine("4. Tìm sinh viên theo tên");
                Console.WriteLine("5. Sửa điểm trung bình");
                Console.WriteLine("6. Xóa sinh viên");
                Console.WriteLine("7. Sắp xếp theo điểm giảm dần");
                Console.WriteLine("8. Lọc sinh viên đạt");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");
                if (int.TryParse(Console.ReadLine(), out int chucNang))
                {
                    if (chucNang >= 0 && chucNang <= 8)
                    {
                        switch (chucNang)
                        {
                            case 1:
                                ChucNangThem();
                                break;
                            case 2:
                                ChucNangXuat(quanLySinhVien.LayDanhSach(), "DANH SÁCH SINH VIÊN");
                                break;
                            case 3:
                                ChucNangTimTheoMa();
                                break;
                            case 4:
                                ChucNangTimTheoTen();
                                break;
                            case 5:
                                ChucNangSua();
                                break;
                            case 6:
                                ChucNangXoa();
                                break;
                            case 7:
                                ChucNangXuat(quanLySinhVien.SapXepTheoDiem(), "DANH SÁCH SINH VIÊN CÓ ĐIỂM TRUNG BÌNH GIẢM DẦN");
                                break;
                            case 8:
                                ChucNangXuat(quanLySinhVien.LocSinhVienDat(), "DANH SÁCH SINH VIÊN CÓ ĐIỂM TRUNG BÌNH ĐẠT");
                                break;
                            case 0:
                                Console.WriteLine("Thoát");
                                return;
                        }
                    }
                    else
                        Console.WriteLine("Chức năng phải từ 0 đến 8!");
                }
                else
                    Console.WriteLine("Chức năng phải là một số nguyên!");
                Console.WriteLine();
                Console.WriteLine("Nhấn phím bất kỳ để tiếp tục!");
                Console.ReadKey();
            }
        }
        static void ChucNangXuat(List<SinhVien> danhSach, string tieuDe)
        {
            Console.WriteLine($"===== {tieuDe} =====");
            if (danhSach.Count == 0)
                Console.WriteLine("Danh sách rỗng!");
            else
            {
                foreach (SinhVien i in danhSach)
                {
                    i.LayThongTin();
                    Console.WriteLine("--------------------");
                }
            }
        }

        // Thêm sinh viên
        static void ChucNangThem()
        {
            Console.Write("Nhập mã sinh viên cần thêm: ");
            string maSinhVien = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Nhập họ tên: ");
            string hoTen = Console.ReadLine()?.Trim() ?? "";
            DateTime ngaySinh;
            while (true)
            {
                Console.Write("Nhập ngày sinh: ");
                if (DateTime.TryParseExact(Console.ReadLine(), "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                    break;
                Console.WriteLine("Ngày sinh không hợp lệ!");
            }
            Console.Write("Nhập mã lớp: ");
            string maLop = Console.ReadLine()?.Trim() ?? "";
            double diemTrungBinh;
            while (true)
            {
                Console.Write("Nhập điểm trung bình: ");
                if (double.TryParse(Console.ReadLine(), out diemTrungBinh) && (diemTrungBinh >= 0 && diemTrungBinh <= 10))
                    break;
                Console.WriteLine("Điểm trung bình phải từ 0 đến 10!");
            }
            SinhVien sinhVien = new SinhVien(maSinhVien, hoTen, ngaySinh, maLop, diemTrungBinh);
            if (quanLySinhVien.Them(sinhVien))
                Console.WriteLine("Thêm sinh viên thành công!");
            else
                Console.WriteLine("Mã sinh viên đã tồn tại!");
        }

        // Tìm sinh viên theo mã
        static void ChucNangTimTheoMa()
        {
            Console.Write("Nhập mã sinh viên cần tìm: ");
            string maSinhVien = Console.ReadLine()?.Trim() ?? "";
            SinhVien? sinhVien = quanLySinhVien.TimTheoMa(maSinhVien);
            if (sinhVien == null)
                Console.WriteLine("Tìm sinh viên không thành công!");
            else
            {
                Console.WriteLine("Tìm sinh viên thành công!");
                sinhVien.LayThongTin();
            }
        }

        // Tìm sinh viên theo tên
        static void ChucNangTimTheoTen()
        {
            Console.Write("Nhập tên sinh viên cần tìm: ");
            string ten = Console.ReadLine()?.Trim() ?? "";
            List<SinhVien> danhSach = quanLySinhVien.TimTheoTen(ten);
            ChucNangXuat(danhSach, $"Danh sách sinh viên có tên {ten}");
        }

        // Sửa điểm trung bình
        static void ChucNangSua()
        {
            Console.Write("Nhập mã sinh viên cần sửa: ");
            string maSinhVien = Console.ReadLine()?.Trim() ?? "";
            SinhVien? sinhVien = quanLySinhVien.TimTheoMa(maSinhVien);
            if (sinhVien == null)
                Console.WriteLine("Tìm sinh viên không thành công!");
            else
            {
                double diemTrungBinh;
                while (true)
                {
                    Console.Write("Nhập điểm trung bình cần sửa: ");
                    if (double.TryParse(Console.ReadLine(), out diemTrungBinh) && (diemTrungBinh >= 0 && diemTrungBinh <= 10))
                        break;
                    Console.WriteLine("Điểm trung bình phải từ 0 đến 10!");
                }
                sinhVien.DiemTrungBinh = diemTrungBinh;
                Console.WriteLine("Sửa điểm trung bình thành công!");
            }
        }

        // Xóa sinh viên
        static void ChucNangXoa()
        {
            Console.Write("Nhập mã sinh viên cần xóa: ");
            string maSinhVien = Console.ReadLine()?.Trim() ?? "";
            if (quanLySinhVien.Xoa(maSinhVien))
                Console.WriteLine("Xóa sinh viên thành công!");
            else
                Console.WriteLine("Xóa sinh viên không thành công!");
        }
    }
}