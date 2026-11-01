using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Lab11
{
    public partial class FrmLop : Form
    {
        private readonly string connectionString =
            @"Data Source=.\SQLEXPRESS; Initial Catalog=EduManagerDB;Integrated Security=True;TrustServerCertificate=True";

        public FrmLop()
        {
            InitializeComponent();
        }

        private void FrmLop_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string sql = @"SELECT l.MaLop, l.TenLop,
                                      COUNT(sv.MaSV) AS SoSinhVien
                               FROM LOP l
                               LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
                               GROUP BY l.MaLop, l.TenLop
                               ORDER BY l.MaLop";
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                using SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLop.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi load du lieu: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui long nhap ma lop");
                txtMaLop.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui long nhap ten lop");
                txtTenLop.Focus();
                return false;
            }
            return true;
        }

        private bool IsMaLopExists(string maLop)
        {
            string sql = "SELECT COUNT(*) FROM LOP WHERE MaLop = @ma";
            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ma", maLop);
            conn.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        private int CountSinhVienByLop(string maLop)
        {
            string sql = "SELECT COUNT(*) FROM SINHVIEN WHERE MaLop = @ma";
            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ma", maLop);
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            string ma = txtMaLop.Text.Trim();
            string ten = txtTenLop.Text.Trim();
            try
            {
                if (IsMaLopExists(ma))
                {
                    MessageBox.Show("Ma lop da ton tai");
                    return;
                }
                string sql = "INSERT INTO LOP(MaLop, TenLop) VALUES(@ma, @ten)";
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Them lop thanh cong");
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi them lop: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            string ma = txtMaLop.Text.Trim();
            string ten = txtTenLop.Text.Trim();
            try
            {
                string sql = "UPDATE LOP SET TenLop = @ten WHERE MaLop = @ma";
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ten", ten);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Cap nhat lop thanh cong");
                    LoadData();
                    ClearInput();
                }
                else
                {
                    MessageBox.Show("Khong tim thay lop can sua");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi cap nhat: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaLop.Text.Trim();
            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Vui long chon lop can xoa");
                return;
            }
            try
            {
                int soSinhVien = CountSinhVienByLop(ma);
                if (soSinhVien > 0)
                {
                    MessageBox.Show("Khong the xoa lop dang co sinh vien");
                    return;
                }
                DialogResult result = MessageBox.Show(
                    "Ban co chac muon xoa lop nay?",
                    "Xac nhan xoa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                string sql = "DELETE FROM LOP WHERE MaLop = @ma";
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", ma);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Xoa lop thanh cong");
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xoa lop: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                string sql = @"SELECT l.MaLop, l.TenLop,
                                      COUNT(sv.MaSV) AS SoSinhVien
                               FROM LOP l
                               LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
                               WHERE l.MaLop LIKE @kw OR l.TenLop LIKE @kw
                               GROUP BY l.MaLop, l.TenLop
                               ORDER BY l.MaLop";
                using SqlConnection conn = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                using SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLop.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tim kiem: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            LoadData();
        }

        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvLop.Rows[e.RowIndex];
            txtMaLop.Text = row.Cells["MaLop"].Value.ToString();
            txtTenLop.Text = row.Cells["TenLop"].Value.ToString();
        }

        private void ClearInput()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtTimKiem.Clear();
            txtMaLop.Focus();
        }
    }
}