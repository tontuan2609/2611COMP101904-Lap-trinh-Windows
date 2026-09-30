using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab05_Nháp
{
    public partial class frmDangKyKhoaHoc : Form
    {
        private int hocPhi(string s)
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

        public frmDangKyKhoaHoc()
        {
            InitializeComponent();
        }

        private void frmDangKyKhoaHoc_Load(object sender, EventArgs e)
        {
            cboKhoaHoc.Items.Clear();
            cboKhoaHoc.Items.Add("C# WinForms cơ bản");
            cboKhoaHoc.Items.Add("SQL Server cơ bản");
            cboKhoaHoc.Items.Add("Web Frontend cơ bản");
            cboKhoaHoc.Items.Add("Lập trình Python cơ bản");
            cboKhoaHoc.SelectedIndex = 0;
            radOnline.Checked = true;
            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }
            if (cboKhoaHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khóa học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return;
            }
            int tongHocPhi = hocPhi(cboKhoaHoc.SelectedItem.ToString()) * (int)numSoThang.Value;
            lblTongTien.Text = $"{tongHocPhi} VNĐ";

        }
    }
}
