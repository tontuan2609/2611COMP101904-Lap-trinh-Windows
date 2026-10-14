using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            cboKhoa.Items.Add("Công nghệ Thông tin");
            cboKhoa.Items.Add("Toán - Tin học");
            cboKhoa.Items.Add("Vật lý");
            cboKhoa.Items.Add("Hóa học");
            cboKhoa.Items.Add("Sinh học");
        }

        // Nút Hiển thị kiểm tra dữ liệu và hiển thị thông tin cá nhân
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // Họ tên không được rỗng
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được rỗng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen.Focus();
                return;
            }

            // Năm sinh không được rỗng và phải là số nguyên
            if (string.IsNullOrEmpty(txtNamSinh.Text))
            {
                MessageBox.Show("Năm sinh không được rỗng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNamSinh.Focus();
                return;
            }
            int n;
            if (!int.TryParse(txtNamSinh.Text, out n))
            {
                MessageBox.Show("Năm sinh phải là số nguyên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNamSinh.Focus();
                return;
            }

            // Năm sinh phải nằm trong khoảng từ 1900 đến năm hiện tại
            if (n < 1900 || n > DateTime.Now.Year)
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoảng từ 1900 đến {DateTime.Now.Year}!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNamSinh.Focus();
                return;
            }

            // Email không được rỗng
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email không được rỗng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            // Phải chọn khoa hoặc lớp
            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Phải chọn khoa hoặc lớp!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Phải chọn giới tính
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Phải chọn giới tính!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string s;
            if (radNam.Checked)
            {
                s = "Nam";
            }
            else
            {
                s = "Nữ";
            }
            string x = "----- THÔNG TIN CÁ NHÂN -----\r\n";
            x = x + $"Họ tên sinh viên: {txtHoTen.Text.Trim()}\r\n";
            x = x + $"Năm sinh: {txtNamSinh.Text.Trim()}\r\n";
            x = x + $"Email: {txtEmail.Text.Trim()}\r\n";
            x = x + $"Khoa/ Lớp: {cboKhoa.Text}\r\n";
            x = x + $"Giới tính: {s}";
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
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}