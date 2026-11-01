namespace Lab11
{
    partial class FrmLop
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtMaLop = new TextBox();
            label2 = new Label();
            txtTenLop = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            txtTimKiem = new TextBox();
            button5 = new Button();
            dgvLop = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLop).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(53, 17);
            label1.TabIndex = 0;
            label1.Text = "Mã lớp:";
            // 
            // txtMaLop
            // 
            txtMaLop.Location = new Point(120, 27);
            txtMaLop.Name = "txtMaLop";
            txtMaLop.Size = new Size(200, 25);
            txtMaLop.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 70);
            label2.Name = "label2";
            label2.Size = new Size(54, 17);
            label2.TabIndex = 2;
            label2.Text = "Tên lớp:";
            // 
            // txtTenLop
            // 
            txtTenLop.Location = new Point(120, 67);
            txtTenLop.Name = "txtTenLop";
            txtTenLop.Size = new Size(430, 25);
            txtTenLop.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(120, 110);
            button1.Name = "button1";
            button1.Size = new Size(90, 35);
            button1.TabIndex = 4;
            button1.Text = "Thêm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnThem_Click;
            // 
            // button2
            // 
            button2.Location = new Point(220, 110);
            button2.Name = "button2";
            button2.Size = new Size(90, 35);
            button2.TabIndex = 5;
            button2.Text = "Sửa";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnSua_Click;
            // 
            // button3
            // 
            button3.Location = new Point(320, 110);
            button3.Name = "button3";
            button3.Size = new Size(90, 35);
            button3.TabIndex = 6;
            button3.Text = "Xóa";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnXoa_Click;
            // 
            // button4
            // 
            button4.Location = new Point(420, 110);
            button4.Name = "button4";
            button4.Size = new Size(90, 35);
            button4.TabIndex = 7;
            button4.Text = "Làm mới";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnLamMoi_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(30, 170);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập mã hoặc tên lớp";
            txtTimKiem.Size = new Size(400, 25);
            txtTimKiem.TabIndex = 8;
            // 
            // button5
            // 
            button5.Location = new Point(450, 165);
            button5.Name = "button5";
            button5.Size = new Size(100, 35);
            button5.TabIndex = 9;
            button5.Text = "Tìm kiếm";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnTimKiem_Click;
            // 
            // dgvLop
            // 
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLop.BackgroundColor = Color.White;
            dgvLop.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLop.Location = new Point(30, 220);
            dgvLop.Name = "dgvLop";
            dgvLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLop.Size = new Size(520, 220);
            dgvLop.TabIndex = 10;
            // 
            // FrmLop
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(584, 461);
            Controls.Add(dgvLop);
            Controls.Add(button5);
            Controls.Add(txtTimKiem);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtTenLop);
            Controls.Add(label2);
            Controls.Add(txtMaLop);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "FrmLop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += FrmLop_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLop).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtMaLop;
        private Label label2;
        private TextBox txtTenLop;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox txtTimKiem;
        private Button button5;
        private DataGridView dgvLop;
    }
}
