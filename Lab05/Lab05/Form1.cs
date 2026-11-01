using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05
{
    public partial class Form1 : Form
    {
        private Dictionary<string, decimal> dsKhoaHoc = new Dictionary<string, decimal>()
        {
            { "C# WinForms cơ bản", 800000m },
            { "SQL Server cơ bản", 700000m },
            { "Web Frontend cơ bản", 750000m },
            { "Lập trình Python cơ bản", 650000m }
        };

        public Form1()
        {
            InitializeComponent();

            cboKhoaHoc.SelectedIndexChanged += TinhTongHocPhi;
            numSoThang.ValueChanged += TinhTongHocPhi;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Nạp danh sách khóa học vào ComboBox
            cboKhoaHoc.DataSource = new BindingSource(dsKhoaHoc, null);
            cboKhoaHoc.DisplayMember = "Key";
            cboKhoaHoc.ValueMember = "Value";

            // Chọn mặc định khóa học đầu tiên
            cboKhoaHoc.SelectedIndex = 0;

            // Chọn mặc định hình thức Online
            radOnline.Checked = true;

            // Thiết lập số tháng tối thiểu là 1, tối đa là 12
            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            // Hiển thị tổng học phí ban đầu
            TinhTongHocPhi(null, null);
        }

        private void TinhTongHocPhi(object sender, EventArgs e)
        {
            if (cboKhoaHoc.SelectedItem != null)
            {
                decimal hocPhi = ((KeyValuePair<string, decimal>)cboKhoaHoc.SelectedItem).Value;
                decimal tongTien = hocPhi * numSoThang.Value;

                lblTongTien.Text = $"{tongTien:#,##0} VND";
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra họ tên không được rỗng
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Kiểm tra số điện thoại không được rỗng
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            // Kiểm tra phải chọn khóa học
            if (cboKhoaHoc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenKhoaHoc = ((KeyValuePair<string, decimal>)cboKhoaHoc.SelectedItem).Key;

            // Tính tổng học phí = học phí một tháng x số tháng
            decimal hocPhi = ((KeyValuePair<string, decimal>)cboKhoaHoc.SelectedItem).Value;
            decimal tongTien = hocPhi * numSoThang.Value;

            string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp";
            string nhanEmail = chkNhanEmail.Checked ? "Có" : "Không";

            string ketQua = $"--- PHIẾU ĐĂNG KÝ ---\n\n" +
                            $"- Họ tên: {txtHoTen.Text.Trim()}\n" +
                            $"- Số điện thoại: {txtSoDienThoai.Text.Trim()}\n" +
                            $"- Ngày sinh: {dtpNgaySinh.Value:dd/MM/yyyy}\n" +
                            $"- Khóa học: {tenKhoaHoc}\n" +
                            $"- Hình thức học: {hinhThuc}\n" +
                            $"- Số tháng: {numSoThang.Value}\n" +
                            $"- Trạng thái nhận email: {nhanEmail}\n\n" +
                            $"TỔNG HỌC PHÍ: {tongTien:#,##0} VND";

            // Hiển thị phiếu đăng ký bằng MessageBox gồm: họ tên, số điện thoại, ngày sinh, khóa học, hình thức học, số tháng, tổng tiền và trạng thái nhận email
            MessageBox.Show(ketQua, "Thông báo Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa họ tên và số điện thoại
            txtHoTen.Clear();
            txtSoDienThoai.Clear();

            // Đưa ngày sinh về ngày hiện tại
            dtpNgaySinh.Value = DateTime.Now;

            // Bỏ chọn nhận email
            chkNhanEmail.Checked = false;

            // Chọn lại khóa học đầu tiên
            cboKhoaHoc.SelectedIndex = 0;

            // Chọn lại hình thức Online
            radOnline.Checked = true;

            // Đưa số tháng về 1
            numSoThang.Value = 1;

            // Đưa con trỏ về ô họ tên
            txtHoTen.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi thoát
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Chỉ đóng Form nếu người dùng chọn Yes
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}