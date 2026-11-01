namespace Lab04
{
    public class ProductService
    {
        private Repository<Product> repo;

        public event Action<string>? OnActionSuccess;

        public ProductService()
        {
            repo = new Repository<Product>();
        }

        public void AddProduct(Product p)
        {
            if (string.IsNullOrWhiteSpace(p.Id))
                throw new ArgumentException("Mã sản phẩm không được rỗng.");

            if (repo.FindById(p.Id) != null)
                throw new DuplicateProductException($"Sản phẩm có mã {p.Id} đã tồn tại.", "ERR_DUP_01");

            repo.Add(p);
            OnActionSuccess?.Invoke($"Đã THÊM thành công sản phẩm: {p.TenSP}");
        }

        public void RemoveProduct(string id)
        {
            var product = repo.FindById(id);
            if (product == null)
                throw new ProductNotFoundException($"Không tìm thấy sản phẩm có mã {id} để xóa.", "ERR_NOTFOUND_01");

            repo.Remove(product);
            OnActionSuccess?.Invoke($"Đã XÓA thành công sản phẩm mã: {id}");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return repo.GetAll();
        }

        public Product? FindById(string id)
        {
            return repo.FindById(id);
        }

        public IEnumerable<Product> SearchByName(string keyword)
        {
            return repo.Find(p => p.TenSP.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public IEnumerable<Product> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            return repo.Find(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        public decimal CalculateTotalInventoryValue()
        {
            return repo.GetAll().Sum(p => p.Price * p.Quantity);
        }
    }
}