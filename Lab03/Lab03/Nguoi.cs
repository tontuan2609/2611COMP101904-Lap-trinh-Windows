namespace Lab03
{
    public class Nguoi
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }

        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        public virtual void LayThongTin()
        {
            Console.WriteLine($"Họ tên: {HoTen}");
            Console.WriteLine($"Ngày sinh: {NgaySinh:dd/MM/yyyy}");
        }
    }
}