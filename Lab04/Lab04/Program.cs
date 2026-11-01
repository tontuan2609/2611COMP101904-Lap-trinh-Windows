namespace Lab04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ProductService productService = new ProductService();

            productService.OnActionSuccess += (message) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[THÔNG BÁO] {message}");
                Console.ResetColor();
            };

            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("\n===== PRODUCT MANAGER =====");
                Console.WriteLine("1. Them san pham");
                Console.WriteLine("2. Xuat danh sach");
                Console.WriteLine("3. Tim theo ma");
                Console.WriteLine("4. Tim theo ten");
                Console.WriteLine("5. Loc theo khoang gia");
                Console.WriteLine("6. Xoa san pham");
                Console.WriteLine("7. Tinh tong gia tri kho");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    ShowError("Vui lòng nhập số hợp lệ!");
                    choice = -1;
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddProductUI(productService);
                            break;
                        case 2:
                            ShowAllProductsUI(productService);
                            break;
                        case 3:
                            FindByIdUI(productService);
                            break;
                        case 4:
                            FindByNameUI(productService);
                            break;
                        case 5:
                            FilterByPriceUI(productService);
                            break;
                        case 6:
                            RemoveProductUI(productService);
                            break;
                        case 7:
                            CalculateTotalUI(productService);
                            break;
                        case 0:
                            Console.WriteLine("Đang thoát chương trình...");
                            break;
                        default:
                            ShowError("Chức năng không tồn tại!");
                            break;
                    }
                }
                catch (DuplicateProductException ex)
                {
                    ShowError(ex.ToString());
                }
                catch (ProductNotFoundException ex)
                {
                    ShowError(ex.ToString());
                }
                catch (ArgumentException ex)
                {
                    ShowError($"Lỗi dữ liệu: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ShowError($"Lỗi hệ thống: {ex.Message}");
                }
            }
        }

        static void AddProductUI(ProductService service)
        {
            Console.Write("Nhập mã SP: ");
            string id = Console.ReadLine() ?? "";
            Console.Write("Nhập tên SP: ");
            string name = Console.ReadLine() ?? "";

            decimal price = GetValidDecimal("Nhập đơn giá: ");
            int quantity = GetValidInt("Nhập số lượng: ");

            Product p = new Product(id, name, price, quantity);
            service.AddProduct(p);
        }

        static void ShowAllProductsUI(ProductService service)
        {
            var products = service.GetAllProducts().ToList();
            if (products.Count == 0)
            {
                Console.WriteLine("Danh sách sản phẩm đang rỗng.");
                return;
            }
            Console.WriteLine("\n--- DANH SÁCH SẢN PHẨM ---");
            foreach (var p in products)
            {
                Console.WriteLine(p.ToString());
            }
        }

        static void FindByIdUI(ProductService service)
        {
            Console.Write("Nhập mã SP cần tìm: ");
            string id = Console.ReadLine() ?? "";
            var p = service.FindById(id);
            if (p != null)
                Console.WriteLine(p.ToString());
            else
                Console.WriteLine("Không tìm thấy sản phẩm!");
        }

        static void FindByNameUI(ProductService service)
        {
            Console.Write("Nhập từ khóa tên SP: ");
            string keyword = Console.ReadLine() ?? "";
            var products = service.SearchByName(keyword).ToList();

            if (products.Count == 0)
                Console.WriteLine("Không có sản phẩm nào khớp với từ khóa.");
            else
            {
                Console.WriteLine($"\n--- KẾT QUẢ TÌM KIẾM ({products.Count}) ---");
                products.ForEach(p => Console.WriteLine(p.ToString()));
            }
        }

        static void FilterByPriceUI(ProductService service)
        {
            decimal min = GetValidDecimal("Nhập giá nhỏ nhất: ");
            decimal max = GetValidDecimal("Nhập giá lớn nhất: ");

            var products = service.FilterByPrice(min, max).ToList();
            if (products.Count == 0)
                Console.WriteLine("Không có sản phẩm nào trong khoảng giá này.");
            else
            {
                Console.WriteLine($"\n--- KẾT QUẢ LỌC ({products.Count}) ---");
                products.ForEach(p => Console.WriteLine(p.ToString()));
            }
        }

        static void RemoveProductUI(ProductService service)
        {
            Console.Write("Nhập mã SP cần xóa: ");
            string id = Console.ReadLine() ?? "";
            service.RemoveProduct(id);
        }

        static void CalculateTotalUI(ProductService service)
        {
            decimal total = service.CalculateTotalInventoryValue();
            Console.WriteLine($"\nTổng giá trị kho hàng: {total:N0} VNĐ");
        }

        static decimal GetValidDecimal(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out value))
                    return value;
                ShowError("Vui lòng nhập một số hợp lệ!");
            }
        }

        static int GetValidInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;
                ShowError("Vui lòng nhập một số nguyên hợp lệ!");
            }
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}