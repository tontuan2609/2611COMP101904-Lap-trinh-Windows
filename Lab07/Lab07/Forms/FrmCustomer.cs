using Lab07.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Lab07.Forms
{
    public partial class FrmCustomer : Form
    {
        private static BindingList<Customer> listCustomers = new BindingList<Customer>()
        {
            new Customer("C001", "Nguyễn Văn A", "0901234567", "Thân thiết"),
            new Customer("C002", "Trần Thị B", "0918765432", "Thường")
        };

        public FrmCustomer()
        {
            InitializeComponent();
            dgvCustomers.DataSource = listCustomers;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            string id = txtMaKH.Text.Trim();
            string name = txtHoTen.Text.Trim();
            string phone = txtSDT.Text.Trim();
            string type = txtLoaiKH.Text ?? "Thường";

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã và họ tên khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listCustomers.Any(c => c.CustomerId.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listCustomers.Add(new Customer(id, name, phone, type));
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
        }
    }
}