namespace Lab03
{
    class Program
    {
        static QuanLySinhVien qlsv = new QuanLySinhVien();

        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            int chon = -1;

            do
            {
                Console.WriteLine("\n===== QUAN LY SINH VIEN =====");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Xuat danh sach");
                Console.WriteLine("3. Tim sinh vien theo ma");
                Console.WriteLine("4. Tim sinh vien theo ten");
                Console.WriteLine("5. Sua diem trung binh");
                Console.WriteLine("6. Xoa sinh vien");
                Console.WriteLine("7. Sap xep theo diem giam dan");
                Console.WriteLine("8. Loc sinh vien dat");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                if (int.TryParse(Console.ReadLine(), out chon))
                {
                    XuLyMenu(chon);
                }
                else
                {
                    Console.WriteLine("=> Lỗi: Vui lòng nhập một số hợp lệ!");
                    chon = -1;
                }
            } while (chon != 0);
        }

        static void XuLyMenu(int chon)
        {
            switch (chon)
            {
                case 1:
                    ThemSinhVien();
                    break;
                case 2:
                    qlsv.XuatDanhSach();
                    break;
                case 3:
                    Console.Write("Nhập mã SV cần tìm: ");
                    qlsv.TimTheoMa(Console.ReadLine() ?? "");
                    break;
                case 4:
                    Console.Write("Nhập từ khóa tên sinh viên: ");
                    qlsv.TimTheoTen(Console.ReadLine() ?? "");
                    break;
                case 5:
                    Console.Write("Nhập mã SV cần sửa điểm: ");
                    string maSua = Console.ReadLine() ?? "";
                    double diemMoi = NhapDiemTrungBinh();
                    qlsv.SuaDiem(maSua, diemMoi);
                    break;
                case 6:
                    Console.Write("Nhập mã SV cần xóa: ");
                    qlsv.Xoa(Console.ReadLine() ?? "");
                    break;
                case 7:
                    qlsv.SapXepTheoDiemGiamDan();
                    break;
                case 8:
                    qlsv.LocSinhVienDat();
                    break;
                case 0:
                    Console.WriteLine("=> Đã thoát chương trình.");
                    break;
                default:
                    Console.WriteLine("=> Chức năng không tồn tại!");
                    break;
            }
        }

        static void ThemSinhVien()
        {
            Console.Write("Nhập mã SV: ");
            string maSV = Console.ReadLine() ?? "";

            if (qlsv.KiemTraTonTai(maSV))
            {
                Console.WriteLine("=> Lỗi: Mã sinh viên đã tồn tại!");
                return;
            }

            Console.Write("Nhập họ tên: ");
            string hoTen = Console.ReadLine() ?? "";

            DateTime ngaySinh = NhapNgaySinh();

            Console.Write("Nhập mã lớp: ");
            string maLop = Console.ReadLine() ?? "";

            double diemTB = NhapDiemTrungBinh();

            SinhVien sv = new SinhVien(maSV, hoTen, ngaySinh, maLop, diemTB);
            qlsv.Them(sv);
        }

        static double NhapDiemTrungBinh()
        {
            double diem;
            while (true)
            {
                Console.Write("Nhập điểm trung bình (0 - 10): ");
                if (double.TryParse(Console.ReadLine(), out diem) && diem >= 0 && diem <= 10)
                {
                    return diem;
                }
                Console.WriteLine("=> Lỗi: Điểm không hợp lệ, vui lòng nhập số thực từ 0 đến 10!");
            }
        }

        static DateTime NhapNgaySinh()
        {
            DateTime ngaySinh;
            while (true)
            {
                Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                {
                    return ngaySinh;
                }
                Console.WriteLine("=> Lỗi: Ngày sinh phải nhập đúng định dạng dd/MM/yyyy!");
            }
        }
    }
}