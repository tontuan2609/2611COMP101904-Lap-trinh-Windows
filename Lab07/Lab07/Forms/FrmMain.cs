using Lab07.Forms;
using Lab07.Models;
using System;
using System.Windows.Forms;

namespace Lab07
{
    public partial class FrmMain : Form
    {
        private User currentUser;

        public FrmMain(User user)
        {
            InitializeComponent();
            this.IsMdiContainer = true; 
            this.currentUser = user;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblStatusName.Text = $"Người dùng: {currentUser.FullName} | Vai trò: {currentUser.Role}";
            toolStripStatusLabel3.Text = $"Thời gian: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
        }

        private void OpenMdiChildForm(Type formType)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.GetType() == formType)
                {
                    child.Activate(); 
                    return;
                }
            }

            Form newChild = (Form)Activator.CreateInstance(formType);
            newChild.MdiParent = this;
            newChild.Show();
        }

        private void MenuQLSanPham_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(typeof(FrmProduct));
        }

        private void MenuQLKhachHang_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(typeof(FrmCustomer));
        }

        private void MenuGioiThieu_Click(object sender, EventArgs e)
        {
            OpenMdiChildForm(typeof(FrmAbout));
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin login = new FrmLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                this.currentUser = login.AuthenticatedUser;
                lblStatusName.Text = $"Người dùng: {currentUser.FullName} | Vai trò: {currentUser.Role}";
                this.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private void MenuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}