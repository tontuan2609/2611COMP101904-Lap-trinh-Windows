namespace Lab04
{
    public class ProductNotFoundException : Exception
    {
        public string ErrorCode { get; set; }
        public ProductNotFoundException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public override string ToString()
        {
            return $"[Lỗi không tìm thấy] {Message} (Mã lỗi: {ErrorCode})";
        }
    }
}