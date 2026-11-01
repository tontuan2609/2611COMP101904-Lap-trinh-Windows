using Lab07.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Lab07.Forms
{
    public partial class FrmProduct : Form
    {
        private static BindingList<Product> listProducts = new BindingList<Product>()
        {
            new Product("P001", "Coca Cola", "Nước giải khát", 10000, 50),
            new Product("P002", "Bánh mì Kinh Đô", "Thực phẩm", 15000, 20)
        };

        public FrmProduct()
        {
            InitializeComponent();
            dgvProducts.DataSource = listProducts;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            string id = txtMaSP.Text.Trim();
            string name = txtTenSP.Text.Trim();
            string cat = txtLoaiSP.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã và tên sản phẩm không được trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listProducts.Any(p => p.ProductId.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(txtDonGia.Text, out decimal price) || price < 0 ||
                !int.TryParse(txtSoLuong.Text, out int qty) || qty < 0)
            {
                MessageBox.Show("Đơn giá và Số lượng phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listProducts.Add(new Product(id, name, cat, price, qty));
            ClearFields();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            string id = txtMaSP.Text.Trim();

            Product target = listProducts.FirstOrDefault(p => p.ProductId == id);
            if (target != null)
            {
                if (!decimal.TryParse(txtDonGia.Text, out decimal price) || price < 0 ||
                    !int.TryParse(txtSoLuong.Text, out int qty) || qty < 0)
                {
                    MessageBox.Show("Đơn giá và Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                target.ProductName = txtTenSP.Text.Trim();
                target.Category = txtLoaiSP.Text.Trim();
                target.Price = price;
                target.Quantity = qty;
                dgvProducts.Refresh();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            string id = dgvProducts.CurrentRow.Cells["ProductId"].Value.ToString();

            DialogResult res = MessageBox.Show($"Bạn chắc chắn muốn xóa sản phẩm mã {id}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Product target = listProducts.FirstOrDefault(p => p.ProductId == id);
                if (target != null)
                {
                    listProducts.Remove(target);
                    ClearFields();
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
            dgvProducts.DataSource = listProducts;
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            var filtered = listProducts.Where(p => p.ProductName.ToLower().Contains(keyword) || p.ProductId.ToLower().Contains(keyword)).ToList();
            dgvProducts.DataSource = new BindingList<Product>(filtered);
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["ProductId"].Value.ToString();
                txtTenSP.Text = row.Cells["ProductName"].Value.ToString();
                txtLoaiSP.Text = row.Cells["Category"].Value.ToString();
                txtDonGia.Text = row.Cells["Price"].Value.ToString();
                txtSoLuong.Text = row.Cells["Quantity"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtLoaiSP.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtMaSP.Focus();
        }
    }
}