using Lab12.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab12.Forms
{
    public partial class FrmNhapDiemTheoLop : Form
    {
        private readonly KetQuaDAL ketQuaDAL = new KetQuaDAL();
        private readonly BindingSource ketQuaSource = new BindingSource();

        public FrmNhapDiemTheoLop()
        {
            InitializeComponent();
        }

        private void FrmNhapDiemTheoLop_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            dgvKetQua.AutoGenerateColumns = true;
            dgvKetQua.DataSource = ketQuaSource;
        }

        private void LoadComboBox()
        {
            cboLop.DataSource = ketQuaDAL.GetLop();
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";

            cboMonHoc.DataSource = ketQuaDAL.GetMonHoc();
            cboMonHoc.DisplayMember = "TenMon";
            cboMonHoc.ValueMember = "MaMon";
        }

        private void btnTaiDanhSach_Click(object sender, EventArgs e)
        {
            string maLop = cboLop.SelectedValue?.ToString() ?? "";
            string maMon = cboMonHoc.SelectedValue?.ToString() ?? "";

            if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(maMon))
            {
                MessageBox.Show("Vui lòng chọn lớp và môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable table = ketQuaDAL.GetKetQuaByLopMon(maLop, maMon);
            ketQuaSource.DataSource = table;
            lblStatus.Text = $"Đã tải danh sách. Tổng số: {table.Rows.Count} sinh viên.";

            if (dgvKetQua.Columns.Count > 0)
            {
                dgvKetQua.Columns["MaSV"].ReadOnly = true;
                dgvKetQua.Columns["HoTen"].ReadOnly = true;
                dgvKetQua.Columns["Diem"].DefaultCellStyle.Format = "N1";
            }
        }

        private void btnLuuTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                ketQuaSource.EndEdit();
                DataTable table = (DataTable)ketQuaSource.DataSource;

                if (table == null || table.Rows.Count == 0) return;

                ValidateData(table);

                string maMon = cboMonHoc.SelectedValue?.ToString() ?? "";

                int count = ketQuaDAL.SaveAll(maMon, table);

                MessageBox.Show($"Đã lưu thành công {count} dòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = $"Lưu thành công lúc {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Lỗi khi lưu dữ liệu.";
            }
        }

        private void ValidateData(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                string maSV = row["MaSV"].ToString() ?? "";
                string diemText = row["Diem"].ToString() ?? "";

                if (!double.TryParse(diemText, out double diem))
                    throw new Exception($"Điểm của sinh viên {maSV} không hợp lệ (Phải là số).");
                if (diem < 0 || diem > 10)
                    throw new Exception($"Điểm của sinh viên {maSV} phải từ 0 đến 10.");
            }
        }
    }
}