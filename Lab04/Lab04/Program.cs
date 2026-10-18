namespace Lab04
{
    internal class Program
    {
        static ProductService dichVuSanPham = new ProductService();
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            dichVuSanPham.OnProductAdded += thongDiep => InThanhCong(thongDiep);
            dichVuSanPham.OnProductRemoved += thongdiep => InLoi(thongdiep);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== PRODUCT MANAGER =====");
                Console.WriteLine("1. Thêm sản phẩm");
                Console.WriteLine("2. Xuất danh sách");
                Console.WriteLine("3. Tìm theo mã");
                Console.WriteLine("4. Tìm theo tên");
                Console.WriteLine("5. Lọc theo khoảng giá");
                Console.WriteLine("6. Xóa sản phẩm");
                Console.WriteLine("7. Tính tổng giá trị kho");
                Console.WriteLine("8. Thoát");
                Console.Write("Chọn: ");
                if (int.TryParse(Console.ReadLine(), out int chon))
                {
                    try
                    {
                        switch (chon)
                        {
                            case 1: ThemSanPham(); break;
                            case 2: XuatDanhSach(dichVuSanPham.GetAllProduct(), "DANH SÁCH SẢN PHẨM"); break;
                            case 3: TimTheoMa(); break;
                            case 4: TimTheoTen(); break;
                            case 5: LocTheoKhoangGia(); break;
                            case 6: XoaSanPham(); break;
                            case 7: TinhTongGiaTriKho(); break;
                            case 8: Console.WriteLine("Thoát"); return;
                            default: Console.WriteLine("Chọn phải từ 1 đến 8!"); break;
                        }
                    }
                    catch (DuplicateProductException ex)
                    {
                        InLoi(ex.Message);
                    }
                    catch (ProductNotFoundException ex)
                    {
                        InLoi(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        InLoi(ex.Message);
                    }
                }
                else
                    Console.WriteLine("Chọn phải là số nguyên!");
                Console.WriteLine();
                Console.Write("Nhấn phím bất kì để tiếp tục...");
                Console.ReadKey();
            }
        }
        // 1. Thêm sản phẩm
        static void ThemSanPham()
        {
            Console.Write("Nhập mã SP cần thêm: ");
            string maSP = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Nhập tên SP: ");
            string tenSP = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Nhập giá SP: ");
            double giaSP;
            if (double.TryParse(Console.ReadLine(), out double price))
                giaSP = price;
            else
                throw new ArgumentException("Giá SP phải là số thực!");
            Console.Write("Nhập số lượng: ");
            int soLuong;
            if (int.TryParse(Console.ReadLine(), out int quantity))
                soLuong = quantity;
            else
                throw new ArgumentException("Số lượng phải là số nguyên!");
            Product sanPham = new Product(maSP, tenSP, giaSP, soLuong);
            dichVuSanPham.AddProduct(sanPham);
        }
        // 2. Xuất danh sách
        static void XuatDanhSach(List<Product> danhSach, string tieuDe)
        {
            Console.WriteLine($"===== {tieuDe} =====");
            if (danhSach.Count == 0)
                Console.WriteLine("Danh sách rỗng!");
            else
            {
                foreach (Product sanPham in danhSach)
                {
                    Console.WriteLine(sanPham.ToString());
                }
            }
        }
        // 3. Tìm theo mã
        static void TimTheoMa()
        {
            Console.Write("Nhập mã sản phẩm cần tìm: ");
            string maSP = Console.ReadLine()?.Trim() ?? "";
            Product? sanPham = dichVuSanPham.SearchMaSP(maSP);
            if (sanPham == null)
                Console.WriteLine("Tìm sản phẩm không thành công!");
            else
            {
                Console.WriteLine("Tìm sản phẩm thành công!");
                Console.WriteLine(sanPham.ToString());
            }
        }
        // 4. Tìm theo tên
        static void TimTheoTen()
        {
            Console.Write("Nhập tên sản phẩm cần tìm: ");
            string tenSP = Console.ReadLine()?.Trim() ?? "";
            List<Product> danhSach = dichVuSanPham.SearchTenSP(tenSP);
            XuatDanhSach(danhSach, $"DANH SACH SẢN PHẨM CÓ TÊN {tenSP}");
        }
        // 5. Lọc theo khoảng giá
        static void LocTheoKhoangGia()
        {
            Console.Write("Nhập giá sản phẩm thấp nhất cần lọc: ");
            if (!double.TryParse(Console.ReadLine(), out double min))
                throw new ArgumentException("Giá sản phẩm thấp nhất không hợp lệ!");
            if (!double.TryParse(Console.ReadLine(), out double max))
                throw new ArgumentException("Giá sản phẩm lớn nhất không hợp lệ!");
            List<Product> danhSach = dichVuSanPham.Filter(min, max);
            XuatDanhSach(danhSach, $"DANH SÁCH SẢN PHẨM CÓ GIÁ TỪ {min} ĐẾN {max}");
        }
        // 6. Xóa sản phẩm
        static void XoaSanPham()
        {
            Console.WriteLine("Nhập mã sản phẩm cần xóa: ");
            string maSP = Console.ReadLine()?.Trim() ?? "";
            dichVuSanPham.RemoveProduct(maSP);
        }
        // 7. Tính tổng giá trị kho
        static void TinhTongGiaTriKho()
        {
            double tong = dichVuSanPham.TinhTongGiaTriKho();
            Console.WriteLine($"Tổng giá trị kho = {tong}");
        }
        static void InThanhCong(string thongDiep)
        {
            Console.WriteLine($"[THÀNH CÔNG] {thongDiep}");
        }
        static void InLoi(string thongDiep)
        {
            Console.WriteLine($"[LỖI] {thongDiep}");
        }
    }
}