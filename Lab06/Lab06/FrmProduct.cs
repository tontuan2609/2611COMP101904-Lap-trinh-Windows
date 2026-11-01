using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab06
{
    public partial class FrmProduct : Form
    {
        private BindingList<Product> products = new BindingList<Product>();
        private BindingSource productSource = new BindingSource();
        private string selectedImagePath = string.Empty;

        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });
            cboLoaiSP.SelectedIndex = 0;

            products.Add(new Product { MaSP = "SP01", TenSP = "Chuột Logitech", LoaiSP = "Điện tử", DonGia = 250000, SoLuong = 50, NgayNhap = DateTime.Today.AddDays(-1), ConKinhDoanh = true });
            products.Add(new Product { MaSP = "SP02", TenSP = "Bàn phím cơ", LoaiSP = "Điện tử", DonGia = 850000, SoLuong = 15, NgayNhap = DateTime.Today, ConKinhDoanh = true });
            products.Add(new Product { MaSP = "SP03", TenSP = "Bút Thiên Long", LoaiSP = "Văn phòng", DonGia = 5000, SoLuong = 200, NgayNhap = DateTime.Today, ConKinhDoanh = true });

            productSource.DataSource = products;
            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.DataSource = productSource;

            UpdateStatus("Đã tải dữ liệu thành công");
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = $"{message} | Tổng số sản phẩm: {products.Count}";
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn hình ảnh sản phẩm";
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog1.FileName;
                picSanPham.Image = Image.FromFile(selectedImagePath);
                UpdateStatus("Đã tải ảnh lên");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(isEdit: false)) return;

            Product p = new Product
            {
                MaSP = txtMaSP.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                LoaiSP = cboLoaiSP.Text,
                DonGia = numDonGia.Value,
                SoLuong = (int)numSoLuong.Value,
                NgayNhap = dtpNgayNhap.Value.Date,
                ConKinhDoanh = chkConKinhDoanh.Checked,
                DuongDanAnh = selectedImagePath
            };

            products.Add(p);
            ClearInput();
            UpdateStatus($"Đã thêm mới: {p.TenSP}");
        }

        private bool ValidateInput(bool isEdit = false)
        {
            errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                errorProvider1.SetError(txtMaSP, "Vui lòng nhập mã sản phẩm.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                errorProvider1.SetError(txtTenSP, "Vui lòng nhập tên sản phẩm.");
                isValid = false;
            }
            if (numDonGia.Value <= 0)
            {
                errorProvider1.SetError(numDonGia, "Đơn giá phải lớn hơn 0.");
                isValid = false;
            }
            if (numSoLuong.Value < 0)
            {
                errorProvider1.SetError(numSoLuong, "Số lượng không được âm.");
                isValid = false;
            }
            if (dtpNgayNhap.Value.Date > DateTime.Today)
            {
                errorProvider1.SetError(dtpNgayNhap, "Ngày nhập không thể ở tương lai.");
                isValid = false;
            }

            if (!isEdit)
            {
                string ma = txtMaSP.Text.Trim();
                bool isDuplicate = products.Any(p => p.MaSP.Equals(ma, StringComparison.OrdinalIgnoreCase));
                if (isDuplicate)
                {
                    errorProvider1.SetError(txtMaSP, "Mã sản phẩm này đã tồn tại trong hệ thống.");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = 0;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            dtpNgayNhap.Value = DateTime.Today;
            chkConKinhDoanh.Checked = true;
            txtTimKiem.Clear();
            picSanPham.Image = null;
            selectedImagePath = string.Empty;
            errorProvider1.Clear();
            txtMaSP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;

            if (!ValidateInput(isEdit: true)) return;

            p.TenSP = txtTenSP.Text.Trim();
            p.LoaiSP = cboLoaiSP.Text;
            p.DonGia = numDonGia.Value;
            p.SoLuong = (int)numSoLuong.Value;
            p.NgayNhap = dtpNgayNhap.Value.Date;
            p.ConKinhDoanh = chkConKinhDoanh.Checked;
            p.DuongDanAnh = selectedImagePath;

            productSource.ResetBindings(false);
            UpdateStatus($"Đã cập nhật: {p.MaSP}");
        }

        private Product GetSelectedProduct()
        {
            if (productSource.Current == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return productSource.Current as Product;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;

            DialogResult confirm = MessageBox.Show($"Xác nhận xóa sản phẩm: {p.TenSP}?", "Hỏi đáp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                products.Remove(p);
                ClearInput();
                UpdateStatus("Đã xóa dữ liệu thành công");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dgvProducts.DataSource = productSource;
                UpdateStatus("Vui lòng nhập từ khóa tìm kiếm.");
                return;
            }

            var result = products.Where(p => p.MaSP.ToLower().Contains(keyword) || p.TenSP.ToLower().Contains(keyword)).ToList();
            dgvProducts.DataSource = result;
            lblStatus.Text = $"Kết quả lọc: Tìm thấy {result.Count} sản phẩm";
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Product p = productSource.Current as Product;
            if (p == null) return;

            txtMaSP.Text = p.MaSP;
            txtTenSP.Text = p.TenSP;
            cboLoaiSP.Text = p.LoaiSP;
            numDonGia.Value = p.DonGia;
            numSoLuong.Value = p.SoLuong;
            dtpNgayNhap.Value = p.NgayNhap;
            chkConKinhDoanh.Checked = p.ConKinhDoanh;
            selectedImagePath = p.DuongDanAnh;

            if (!string.IsNullOrWhiteSpace(p.DuongDanAnh) && File.Exists(p.DuongDanAnh))
            {
                picSanPham.Image = Image.FromFile(p.DuongDanAnh);
            }
            else
            {
                picSanPham.Image = null;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvProducts.DataSource = productSource;
            ClearInput();
            UpdateStatus("Đã khôi phục chế độ hiển thị toàn bộ");
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbThem_Click(object sender, EventArgs e) { btnThem.PerformClick(); }
        private void tsbSua_Click(object sender, EventArgs e) { btnSua.PerformClick(); }
        private void tsbXoa_Click(object sender, EventArgs e) { btnXoa.PerformClick(); }
        private void tsbLamMoi_Click(object sender, EventArgs e) { btnLamMoi.PerformClick(); }
        private void tsbTimKiem_Click(object sender, EventArgs e) { btnTimKiem.PerformClick(); }

        private void cmsSua_Click(object sender, EventArgs e) { btnSua.PerformClick(); }
        private void cmsXoa_Click(object sender, EventArgs e) { btnXoa.PerformClick(); }

        private void cmsXemChiTiet_Click(object sender, EventArgs e)
        {
            Product p = GetSelectedProduct();
            if (p == null) return;

            string detail = $"Mã SP: {p.MaSP}\nTên SP: {p.TenSP}\nLoại: {p.LoaiSP}\nĐơn giá: {p.DonGia:N0}\nSố lượng: {p.SoLuong}\nTổng tiền: {p.ThanhTien:N0}";
            MessageBox.Show(detail, "Chi tiết thông tin kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}