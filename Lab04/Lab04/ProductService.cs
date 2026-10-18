namespace Lab04
{
    public class ProductService
    {
        private Repository<Product> khoLuuTru;
        public ProductService()
        {
            khoLuuTru = new Repository<Product>()!;
        }
        public event Action<string>? OnProductAdded;
        public event Action<string>? OnProductRemoved;
        // 1. Thêm sản phẩm
        public void AddProduct(Product sanPham)
        {
            if (string.IsNullOrEmpty(sanPham.MaSP))
                throw new ArgumentException("Mã sản phẩm không được rỗng!");
            if (khoLuuTru.FindById(sanPham.MaSP) == null)
            {
                khoLuuTru.Add(sanPham);
                OnProductAdded?.Invoke("Thêm sản phẩm thành công!");
            }
            else
                throw new DuplicateProductException("Mã sản phẩm đã tồn tại!");
        }
        // 2. Xuất danh sách
        public List<Product> GetAllProduct()
        {
            return khoLuuTru.GetAll().ToList();
        }
        // 3. Tìm theo mã
        public Product? SearchMaSP(string maSP)
        {
            return khoLuuTru.FindById(maSP);
        }
        // 4. Tìm theo tên
        public List<Product> SearchTenSP(string tenSP)
        {
            return khoLuuTru.Find(x => x.TenSP.Contains(tenSP)).ToList();
        }
        // 5. Lọc theo khoảng giá
        public List<Product> Filter(double minPrice, double maxPrice)
        {
            return khoLuuTru.Find(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }
        // 6. Xóa sản phẩm
        public void RemoveProduct(string maSP)
        {
            Product? sanPham = khoLuuTru.FindById(maSP);
            if (sanPham == null)
                throw new ProductNotFoundException("Mã sản phẩm không tồn tại!");
            else
            {
                khoLuuTru.Remove(sanPham);
                OnProductRemoved?.Invoke("Xóa sản phẩm thành công!");
            }
        }
        // 7. Tính tổng giá trị kho
        public double TinhTongGiaTriKho()
        {
            double tong = 0;
            foreach (Product sanPham in khoLuuTru.GetAll())
            {
                tong = tong + sanPham.Price * sanPham.Quantity;
            }
            return tong;
        }
    }
}