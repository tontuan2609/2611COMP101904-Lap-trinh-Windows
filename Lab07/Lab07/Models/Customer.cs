namespace Lab07.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerType { get; set; }

        public Customer(string customerId, string fullName, string phoneNumber, string customerType)
        {
            CustomerId = customerId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            CustomerType = customerType;
        }
    }
}