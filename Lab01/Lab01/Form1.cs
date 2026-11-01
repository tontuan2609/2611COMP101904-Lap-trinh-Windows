using System;
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
            cboKhoa.Items.Add("Toán - Tin");
            cboKhoa.Items.Add("Vật lý");
            cboKhoa.Items.Add("Hóa học");
            cboKhoa.Items.Add("Sinh học");

            cboKhoa.SelectedIndex = -1;
        }

        // Nút Hiển thị kiểm tra dữ liệu và hiển thị thông tin cá nhân
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // Họ tên không được rỗng
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Năm sinh không được rỗng và phải là số nguyên
            if (string.IsNullOrWhiteSpace(txtNamSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập năm sinh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            int namSinh;
            if (!int.TryParse(txtNamSinh.Text, out namSinh))
            {
                MessageBox.Show("Năm sinh phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // Năm sinh phải nằm trong khoảng từ 1900 đến năm hiện tại
            int namHienTai = DateTime.Now.Year;
            if (namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoảng từ 1900 đến {namHienTai}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // Email không được rỗng
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Phải chọn giới tính
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Phải chọn khoa hoặc lớp
            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khoa hoặc Lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoa.Focus();
                return;
            }

            int tuoi = namHienTai - namSinh;
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            string khoa = cboKhoa.SelectedItem.ToString();

            string ketQua = "THÔNG TIN SINH VIÊN\r\n" +
                            $"Họ tên: {txtHoTen.Text.Trim()}\r\n" +
                            $"Tuổi: {tuoi}\r\n" +
                            $"Email: {txtEmail.Text.Trim()}\r\n" +
                            $"Giới tính: {gioiTinh}\r\n" +
                            $"Khoa/Lớp: {khoa}";

            // Có thể hiển thị kết quả bằng MessageBox hoặc bằng một Label/TextBox trên Form
            txtKetQua.Text = ketQua;
        }

        // Nút Xóa đưa các TextBox về rỗng, bỏ chọn giới tính, đưa ComboBox về lựa chọn đầu tiên hoặc không chọn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();
            txtKetQua.Clear();

            radNam.Checked = false;
            radNu.Checked = false;

            cboKhoa.SelectedIndex = -1;

            txtHoTen.Focus();
        }

        // Nút Thoát hỏi xác nhận trước khi đóng chương trình
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                                                  "Xác nhận thoát",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}