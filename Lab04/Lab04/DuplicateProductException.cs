namespace Lab04
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string thongDiep) : base(thongDiep) { }
    }
}