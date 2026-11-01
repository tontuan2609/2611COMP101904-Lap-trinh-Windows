using System.Drawing.Drawing2D;

namespace Lab08
{
    public partial class FrmPaint : Form
    {
        private readonly DrawSurface canvas = new DrawSurface();
        private readonly ComboBox cboMode = new ComboBox();
        private readonly NumericUpDown nudWidth = new NumericUpDown();
        private readonly Button btnColor = new Button();
        private readonly Button btnUndo = new Button();
        private readonly Button btnClear = new Button();
        private readonly Button btnSave = new Button();
        private readonly Label lblStatus = new Label();

        private readonly List<DrawItem> items = new List<DrawItem>();
        private DrawItem currentItem;
        private bool isDrawing = false;
        private Color currentColor = Color.Black;

        public FrmPaint()
        {
            Text = "COMP1019 - Modern Simple Paint";
            Size = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            BuildUserInterface();
            RegisterEvents();
            this.KeyPreview = true;
            this.KeyDown += FrmPaint_KeyDown;
        }

        private void FrmPaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                BtnUndo_Click(btnUndo, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG Image|*.png";
                dialog.Title = "Lưu hình ảnh";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(canvas.Width, canvas.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.White);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            foreach (DrawItem item in items)
                                DrawOneItem(g, item);
                        }
                        bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        MessageBox.Show("Lưu ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void BuildUserInterface()
        {
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Size = new Size(1000, 60);
            topPanel.BackColor = Color.FromArgb(240, 244, 248);

            Label lblMode = new Label { Text = "Chế độ:", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" });
            cboMode.SelectedIndex = 0;
            cboMode.Location = new Point(85, 16);
            cboMode.Size = new Size(120, 25);

            Label lblWidth = new Label { Text = "Độ dày:", Location = new Point(230, 20), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            nudWidth.Minimum = 1;
            nudWidth.Maximum = 20;
            nudWidth.Value = 3;
            nudWidth.Location = new Point(295, 16);
            nudWidth.Size = new Size(60, 25);

            btnColor.Text = "Chọn Màu";
            btnColor.Location = new Point(380, 14);
            btnColor.Size = new Size(95, 32);
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.BackColor = Color.White;
            btnColor.Cursor = Cursors.Hand;

            btnUndo.Text = "Undo";
            btnUndo.Location = new Point(490, 14);
            btnUndo.Size = new Size(85, 32);
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnUndo.BackColor = Color.White;
            btnUndo.Cursor = Cursors.Hand;

            btnClear.Text = "Clear";
            btnClear.Location = new Point(585, 14);
            btnClear.Size = new Size(85, 32);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.BackColor = Color.FromArgb(255, 235, 238);
            btnClear.ForeColor = Color.DarkRed;
            btnClear.Cursor = Cursors.Hand;

            btnSave.Text = "Save";
            btnSave.Left = 685;
            btnSave.Top = 14;
            btnSave.Size = new Size(85, 32);

            Label lblModed = new Label { Text = "Mode", Left = 15, Top = 16, AutoSize = true };
            Label lblWidthd = new Label { Text = "Width", Left = 200, Top = 16, AutoSize = true };

            topPanel.Controls.Add(lblMode);
            topPanel.Controls.Add(cboMode);
            topPanel.Controls.Add(lblWidth);
            topPanel.Controls.Add(nudWidth);
            topPanel.Controls.Add(btnColor);
            topPanel.Controls.Add(btnUndo);
            topPanel.Controls.Add(btnClear);
            topPanel.Controls.Add(btnSave);

            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Size = new Size(1000, 30);
            lblStatus.Text = " Trạng thái: Sẵn sàng";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.BackColor = Color.FromArgb(226, 232, 240);

            Controls.Add(canvas);
            Controls.Add(lblStatus);
            Controls.Add(topPanel);
        }

        private void RegisterEvents()
        {
            canvas.Paint += Canvas_Paint;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;

            btnColor.Click += BtnColor_Click;
            btnUndo.Click += BtnUndo_Click;
            btnClear.Click += BtnClear_Click;
            btnSave.Click += BtnSave_Click;
        }

        private DrawMode CurrentMode
        {
            get
            {
                string mode = cboMode.SelectedItem.ToString();
                return (DrawMode)Enum.Parse(typeof(DrawMode), mode);
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog();
            dialog.Color = currentColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = dialog.Color;
                btnColor.BackColor = currentColor;
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (items.Count == 0) return;
            items.RemoveAt(items.Count - 1);
            canvas.Invalidate();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa toàn bộ vùng vẽ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                items.Clear();
                canvas.Invalidate();
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            isDrawing = true;
            currentItem = new DrawItem
            {
                Mode = CurrentMode,
                Start = e.Location,
                End = e.Location,
                Color = currentColor,
                Width = (float)nudWidth.Value
            };
            if (currentItem.Mode == DrawMode.Pen)
                currentItem.Points.Add(e.Location);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            lblStatus.Text = $" Tọa độ: X = {e.X}, Y = {e.Y} | Tổng số nét vẽ = {items.Count}";
            if (!isDrawing || currentItem == null) return;

            currentItem.End = e.Location;
            if (currentItem.Mode == DrawMode.Pen)
                currentItem.Points.Add(e.Location);

            canvas.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentItem == null) return;
            currentItem.End = e.Location;
            items.Add(currentItem);
            currentItem = null;
            isDrawing = false;
            canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (DrawItem item in items)
                DrawOneItem(e.Graphics, item);

            if (currentItem != null)
                DrawOneItem(e.Graphics, currentItem);
        }

        private void DrawOneItem(Graphics g, DrawItem item)
        {
            using Pen pen = new Pen(item.Color, item.Width);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;

            Rectangle rect = GetRectangle(item.Start, item.End);

            switch (item.Mode)
            {
                case DrawMode.Pen:
                    if (item.Points.Count >= 2)
                    {
                        for (int i = 1; i < item.Points.Count; i++)
                            g.DrawLine(pen, item.Points[i - 1], item.Points[i]);
                    }
                    break;
                case DrawMode.Line:
                    g.DrawLine(pen, item.Start, item.End);
                    break;
                case DrawMode.Rectangle:
                    g.DrawRectangle(pen, rect);
                    break;
                case DrawMode.Ellipse:
                    g.DrawEllipse(pen, rect);
                    break;
            }
        }

        private Rectangle GetRectangle(Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);
            return new Rectangle(x, y, width, height);
        }
    }
}