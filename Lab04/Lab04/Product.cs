namespace Lab04
{
    public class Product : IEntity
    {
        public string Id
        {
            get { return MaSP; }
        }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                    price = value;
            }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value >= 0)
                    quantity = value;
            }
        }

        public Product(string maSP, string tenSP, double price, int quantity)
        {
            MaSP = maSP;
            TenSP = tenSP;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Mã SP: {MaSP} - Tên SP: {TenSP} - Giá: {Price:N0} - Số lượng: {Quantity}";
        }
    }
}