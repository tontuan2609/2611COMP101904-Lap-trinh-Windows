namespace Lab04
{
    public class Product : IEntity
    {
        public string Id { get; set; }
        public string TenSP { get; set; }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException("Đơn giá không được âm.");
                price = value;
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0) throw new ArgumentException("Số lượng không được âm.");
                quantity = value;
            }
        }

        public Product(string id, string tenSP, decimal price, int quantity)
        {
            Id = id;
            TenSP = tenSP;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Mã SP: {Id,-10} | Tên SP: {TenSP,-20} | Đơn giá: {Price,10:N0} | Số lượng: {Quantity,5}";
        }
    }
}