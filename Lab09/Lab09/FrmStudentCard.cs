using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab09
{
    public partial class FrmStudentCard : Form
    {
        public FrmStudentCard()
        {
            InitializeComponent();
            InitializeData();
            RegisterEvents();
        }

        private void InitializeData()
        {
            cboClass.Items.AddRange(new object[] { "K49.CNTT.A", "K50.CNTT.A", "K51.CNTT.A" });
            cboFaculty.Items.AddRange(new object[] { "Công nghệ thông tin", "Toán - Tin học" });
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });

            cboClass.SelectedIndex = 0;
            cboFaculty.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void RegisterEvents()
        {
            btnChoosePhoto.Click += btnChoosePhoto_Click;
            btnPreviewPrint.Click += btnPreviewPrint_Click;
            btnPrint.Click += btnPrint_Click;
            btnClear.Click += btnClear_Click;
            pnlCardPreview.Paint += pnlCardPreview_Paint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            txtStudentId.TextChanged += InputChanged;
            txtFullName.TextChanged += InputChanged;
            txtCourse.TextChanged += InputChanged;
            cboClass.SelectedIndexChanged += InputChanged;
            cboFaculty.SelectedIndexChanged += InputChanged;
            cboGender.SelectedIndexChanged += InputChanged;
            dtpBirthDate.ValueChanged += InputChanged;
        }

        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Chọn ảnh sinh viên";
            dlg.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using Image temp = Image.FromFile(dlg.FileName);
                    Image copy = new Bitmap(temp);
                    picAvatar.Image?.Dispose();
                    picAvatar.Image = copy;
                    pnlCardPreview.Invalidate();
                }
                catch
                {
                    MessageBox.Show("File được chọn không phải ảnh hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private StudentCardInfo GetStudentInfo()
        {
            return new StudentCardInfo
            {
                StudentId = txtStudentId.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                ClassName = cboClass.Text,
                Faculty = cboFaculty.Text,
                Course = txtCourse.Text.Trim(),
                BirthDate = dtpBirthDate.Value,
                Gender = cboGender.Text,
                Photo = picAvatar.Image
            };
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.");
                txtStudentId.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.");
                txtFullName.Focus();
                return false;
            }
            if (picAvatar.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh sinh viên.");
                return false;
            }
            return true;
        }

        private void InputChanged(object sender, EventArgs e)
        {
            pnlCardPreview.Invalidate();
        }

        private void pnlCardPreview_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle card = new Rectangle(20, 20, pnlCardPreview.Width - 40, pnlCardPreview.Height - 40);
            DrawStudentCard(e.Graphics, card, GetStudentInfo());
        }

        private void DrawStudentCard(Graphics g, Rectangle card, StudentCardInfo s)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using Font schoolFont = new Font("Arial", 11, FontStyle.Bold);
            using Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            using Font labelFont = new Font("Arial", 10, FontStyle.Bold);
            using Font normalFont = new Font("Arial", 10, FontStyle.Regular);
            using Pen borderPen = new Pen(Color.Navy, 2);
            using SolidBrush blueBrush = new SolidBrush(Color.Navy);
            using SolidBrush bgBrush = new SolidBrush(Color.FromArgb(238, 247, 247));

            g.FillRectangle(bgBrush, card);
            g.DrawRectangle(borderPen, card);

            int x = card.Left;
            int y = card.Top;

            g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH", schoolFont, blueBrush, x + 25, y + 20);
            g.DrawString("THẺ SINH VIÊN", titleFont, blueBrush, x + 150, y + 50);

            Rectangle photoBox = new Rectangle(x + 25, y + 100, 120, 160);
            g.FillRectangle(Brushes.White, photoBox);
            g.DrawRectangle(Pens.Gray, photoBox);

            if (s.Photo != null)
                DrawImageZoom(g, s.Photo, photoBox);
            else
                g.DrawString("Ảnh", normalFont, Brushes.Gray, photoBox.Left + 45, photoBox.Top + 70);

            int infoX = x + 180;
            int infoY = y + 105;
            int spacing = 32;

            DrawInfoLine(g, "MSSV:", s.StudentId, labelFont, normalFont, infoX, infoY);
            DrawInfoLine(g, "Họ tên:", s.FullName, labelFont, normalFont, infoX, infoY + spacing);
            DrawInfoLine(g, "Lớp:", s.ClassName, labelFont, normalFont, infoX, infoY + spacing * 2);
            DrawInfoLine(g, "Khoa:", s.Faculty, labelFont, normalFont, infoX, infoY + spacing * 3);
            DrawInfoLine(g, "Khóa:", s.Course, labelFont, normalFont, infoX, infoY + spacing * 4);
            DrawInfoLine(g, "Ngày sinh:", s.BirthDate.ToString("dd/MM/yyyy"), labelFont, normalFont, infoX, infoY + spacing * 5);
        }

        private void DrawInfoLine(Graphics g, string label, string value, Font labelFont, Font normalFont, int x, int y)
        {
            g.DrawString(label, labelFont, Brushes.Black, x, y);
            g.DrawString(value, normalFont, Brushes.Black, x + 90, y);
        }

        private void DrawImageZoom(Graphics g, Image image, Rectangle box)
        {
            float ratio = Math.Min((float)box.Width / image.Width, (float)box.Height / image.Height);
            int width = (int)(image.Width * ratio);
            int height = (int)(image.Height * ratio);
            int x = box.Left + (box.Width - width) / 2;
            int y = box.Top + (box.Height - height) / 2;
            g.DrawImage(image, new Rectangle(x, y, width, height));
        }

        private void btnPreviewPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Width = 900;
            printPreviewDialog1.Height = 700;
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            StudentCardInfo info = GetStudentInfo();
            Rectangle card = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, 520, 320);
            DrawStudentCard(e.Graphics, card, info);
            e.HasMorePages = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtCourse.Clear();
            if (cboClass.Items.Count > 0) cboClass.SelectedIndex = 0;
            if (cboFaculty.Items.Count > 0) cboFaculty.SelectedIndex = 0;
            if (cboGender.Items.Count > 0) cboGender.SelectedIndex = 0;
            dtpBirthDate.Value = DateTime.Today;

            picAvatar.Image?.Dispose();
            picAvatar.Image = null;
            pnlCardPreview.Invalidate();
            txtStudentId.Focus();
        }
    }
}