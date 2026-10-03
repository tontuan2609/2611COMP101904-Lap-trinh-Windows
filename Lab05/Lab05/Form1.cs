using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Lab05
{
    public partial class Form1 : Form
    {
        private int HocPhi(string s)
        {
            if (s == "C# WinForms cơ bản")
                return 800000;
            if (s == "SQL Server cơ bản")
                return 700000;
            if (s == "Web Frontend cơ bản")
                return 750000;
            if (s == "Lập trình Python cơ bản")
                return 650000;
            return 0;
        }

        private void TongTien()
        {
            int n = HocPhi(cboKhoaHoc.Text) * (int)numSoThang.Value;
            lblTongTien.Text = $"{n:N0} VNĐ";
        }

        public Form1()
        {
            InitializeComponent();
        }

        // Khi Form Load
        private void frmDangKyKhoaHoc_Load(object sender, EventArgs e)
        {
            // Nạp danh sách khóa học vào ComboBox
            cboKhoaHoc.Items.Add("C# WinForms cơ bản");
            cboKhoaHoc.Items.Add("SQL Server cơ bản");
            cboKhoaHoc.Items.Add("Web Frontend cơ bản");
            cboKhoaHoc.Items.Add("Lập trình Python cơ bản");

            // Chọn mặc định khóa học đầu tiên
            cboKhoaHoc.SelectedIndex = 0;

            // Chọn mặc định hình thức Online
            radOnline.Checked = true;

            // Thiết lập số tháng tối thiểu là 1, tối đa là 12
            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;

            // Hiển thị tổng học phí ban đầu
            TongTien();
        }

        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            TongTien();
        }

        private void numSoThang_ValueChanged(object sender, EventArgs e)
        {
            TongTien();
        }

        // Nút Đăng ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra họ tên không được rỗng
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được rỗng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Kiểm tra số điện thoại không được rỗng
            if (string.IsNullOrEmpty(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không được rỗng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            // Kiểm tra phải chọn khóa học
            if (cboKhoaHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return;
            }

            // Tính tổng học phí = học phí một tháng x số tháng
            TongTien();

            // Hiển thị phiếu đăng ký bằng MessageBox
            string s;
            if (chkNhanEmail.Checked)
                s = "Có";
            else
                s = "Không";
            string x;
            if (radOnline.Checked)
                x = "Online";
            else
                x = "Trực tiếp";
            string t = "THÔNG TIN ĐĂNG KÝ KHÓA HỌC\n";
            t += "\n";
            t += $"Họ tên học viên: {txtHoTen.Text.Trim()}\n";
            t += $"Số điện thoại: {txtSoDienThoai.Text.Trim()}\n";
            t += $"Ngày sinh: {dtpNgaySinh.Value:dd/MM/yyyy}\n";
            t += $"Nhận email thông báo: {s}\n";
            t += $"Khóa học: {cboKhoaHoc.Text}\n";
            t += $"Hình thức: {x}\n";
            t += $"Số tháng: {numSoThang.Value}\n";
            t += $"Tổng học phí: {lblTongTien.Text}\n";
            MessageBox.Show(t, "Phiếu đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Nút Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa họ tên và số điện thoại
            txtHoTen.Clear(); ;
            txtSoDienThoai.Clear();

            // Đưa ngày sinh về ngày hiện tại
            dtpNgaySinh.Value = DateTime.Today;

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

        // Nút Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi thoát
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            // Chỉ đóng Form nếu người dùng chọn Yes
            if (d == DialogResult.Yes)
                this.Close();
        }
    }
}