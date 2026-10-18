namespace Lab04
{
    public class ProductService
    {
        private Repository<Product> khoLuuTru;

        public ProductService()
        {
            khoLuuTru = new Repository<Product>();
        }

        public event Action<string> OnProductAdded;
        public event Action<string> OnProductRemoved;

        public void AddProduct(Product sanPham)
        {
            if (string.IsNullOrEmpty(sanPham.MaSP))
                throw new ArgumentException("Mã sản phẩm không được rỗng!");
            if (khoLuuTru.FindById(sanPham.MaSP) == null)
            {
                khoLuuTru.Add(sanPham);
                OnProductAdded.Invoke("Thêm sản phẩm thành công!");
            }
            else
                throw new DuplicateProductException("Mã sản phẩm đã tồn tại!");
        }
        public List<Product> GetAllProduct()
        {
            return khoLuuTru.GetAll().ToList();
        }
        public Product? SearchMaSP(string maSP)
        {
            return khoLuuTru.FindById(maSP);
        }
        public List<Product> SearchTenSP(string tenSP)
        {
            return khoLuuTru.Find(x => x.TenSP.Contains(tenSP)).ToList();
        }
        public List<Product> Filter(double minPrice, double maxPrice)
        {
            return khoLuuTru.Find(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
        }
        // 6. Xóa sản phẩm
        public void XoaSanPham(string maSP)
        {
            Product? sanPham = khoLuuTru.FindById(maSP);
            if (sanPham == null)
                throw new ProductNotFoundException("Xóa sản phẩm không thành công!");
            else
            {
                khoLuuTru.Remove(sanPham);
                OnProductRemoved.Invoke("Xóa sản phẩm thành công!");
            }
        }
        // 7. Tính tổng giá trị kho
        public double TinhTongGiaTriKho()
        {
            double x = 0;
            foreach (Product i in khoLuuTru.GetAll())
            {
                x = x + i.Price * i.Quantity;
            }
            return x;
        }
    }
}