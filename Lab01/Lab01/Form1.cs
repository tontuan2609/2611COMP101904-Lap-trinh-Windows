using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Khi mở chương trình, ComboBox khoa/lớp có sẵn ít nhất 3 lựa chọn
        private void Form1_Load(object sender, EventArgs e)
        {
            cboKhoa.Items.Add("Công nghệ thông tin");
            cboKhoa.Items.Add("Toán - Tin học");
            cboKhoa.Items.Add("Vật lý");
            cboKhoa.Items.Add("Hóa học");
            cboKhoa.Items.Add("Sinh học");
            cboKhoa.Items.Add("Ngữ văn");
            cboKhoa.Items.Add("Lịch sử");
            cboKhoa.Items.Add("Địa lý");
            cboKhoa.Items.Add("Tiếng Anh");
            cboKhoa.Items.Add("Tiếng Pháp");
        }

        // Nút Hiển thị kiểm tra dữ liệu và hiển thị thông tin cá nhân
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // Họ tên không được rỗng
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Năm sinh không được rỗng và phải là số nguyên
            if (string.IsNullOrEmpty(txtNamSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập năm sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }
            int namSinh;
            if (!int.TryParse(txtNamSinh.Text, out namSinh))
            {
                MessageBox.Show("Năm sinh phải là số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // Năm sinh phải nằm trong khoảng từ 1900 đến năm hiện tại
            if (namSinh < 1900 || namSinh > DateTime.Now.Year)
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoảng từ 1900 đến {DateTime.Now.Year}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // Email không được rỗng
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Phải chọn khoa hoặc lớp
            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa/lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Phải chọn giới tính
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Có thể hiển thị kết quả bằng MessageBox hoặc bằng một Label/TextBox trên Form
            string s = "";
            if (radNam.Checked)
            {
                s = "Nam";
            }
            if (radNu.Checked)
            {
                s = "Nữ";
            }
            string x = "THÔNG TIN SINH VIÊN\r\n\r\n";
            x += $"Họ tên sinh viên: {txtHoTen.Text}\r\n";
            x += $"Năm sinh: {txtNamSinh.Text}\r\n";
            x += $"Email: {txtEmail.Text}\r\n";
            x += $"Khoa/Lớp: {cboKhoa.SelectedItem}\r\n";
            x += $"Giới tính: {s}";
            txtKetQua.Text = x;
        }

        // Nút Xóa đưa các TextBox về rỗng, bỏ chọn giới tính, đưa ComboBox về lựa chọn đầu tiên hoặc không chọn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();
            cboKhoa.SelectedIndex = -1;
            radNam.Checked = false;
            radNu.Checked = false;
            txtKetQua.Clear();
        }

        // Nút Thoát hỏi xác nhận trước khi đóng chương trình
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}