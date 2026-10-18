namespace Lab04
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string thongDiep) : base(thongDiep) { }
    }
}