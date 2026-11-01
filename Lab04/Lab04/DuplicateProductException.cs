namespace Lab04
{
    public class DuplicateProductException : Exception
    {
        public string ErrorCode { get; set; }
        public DuplicateProductException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        public override string ToString()
        {
            return $"[Lỗi trùng lặp] {Message} (Mã lỗi: {ErrorCode})";
        }
    }
}