namespace Lab04
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string thongDiep) : base(thongDiep) { }
    }
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string thongDiep) : base(thongDiep) { }
    }
}