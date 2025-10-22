namespace DoAnCuoiKy
{
    partial class frmCapNhatChuongHoc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.groupBoxThongTin = new System.Windows.Forms.GroupBox();
            this.cmbKhoaHoc = new System.Windows.Forms.ComboBox();
            this.lblGiangVien = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblTenKhoa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.txtTenChuongHoc = new System.Windows.Forms.TextBox();
            this.dataGridViewChuongHoc = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.cậpNhậtBàiHọcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChuongHoc)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(1028, 130);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(99, 27);
            this.btnTimKiem.TabIndex = 12;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1053, 476);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 29);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(854, 132);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(158, 22);
            this.txtTimKiem.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHuy);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Location = new System.Drawing.Point(13, 409);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 49);
            this.panel2.TabIndex = 15;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(280, 5);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(74, 29);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(158, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(72, 29);
            this.btnSua.TabIndex = 0;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(36, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(72, 29);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(785, 135);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(63, 16);
            this.lblTimKiem.TabIndex = 8;
            this.lblTimKiem.Text = "Tìm Kiếm";
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.cmbKhoaHoc);
            this.groupBoxThongTin.Controls.Add(this.lblGiangVien);
            this.groupBoxThongTin.Controls.Add(this.lblMoTa);
            this.groupBoxThongTin.Controls.Add(this.lblTenKhoa);
            this.groupBoxThongTin.Controls.Add(this.txtMoTa);
            this.groupBoxThongTin.Controls.Add(this.txtTenChuongHoc);
            this.groupBoxThongTin.Location = new System.Drawing.Point(10, 156);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Size = new System.Drawing.Size(397, 243);
            this.groupBoxThongTin.TabIndex = 14;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông Tin";
            // 
            // cmbKhoaHoc
            // 
            this.cmbKhoaHoc.FormattingEnabled = true;
            this.cmbKhoaHoc.Location = new System.Drawing.Point(152, 144);
            this.cmbKhoaHoc.Name = "cmbKhoaHoc";
            this.cmbKhoaHoc.Size = new System.Drawing.Size(146, 24);
            this.cmbKhoaHoc.TabIndex = 5;
            // 
            // lblGiangVien
            // 
            this.lblGiangVien.Location = new System.Drawing.Point(38, 152);
            this.lblGiangVien.Name = "lblGiangVien";
            this.lblGiangVien.Size = new System.Drawing.Size(73, 16);
            this.lblGiangVien.TabIndex = 4;
            this.lblGiangVien.Text = "Chọn Khóa Học";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(38, 104);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(46, 16);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô Tả";
            // 
            // lblTenKhoa
            // 
            this.lblTenKhoa.AutoSize = true;
            this.lblTenKhoa.Location = new System.Drawing.Point(38, 63);
            this.lblTenKhoa.Name = "lblTenKhoa";
            this.lblTenKhoa.Size = new System.Drawing.Size(108, 16);
            this.lblTenKhoa.TabIndex = 4;
            this.lblTenKhoa.Text = "Tên Chương Học";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(152, 98);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(222, 22);
            this.txtMoTa.TabIndex = 3;
            // 
            // txtTenChuongHoc
            // 
            this.txtTenChuongHoc.Location = new System.Drawing.Point(152, 60);
            this.txtTenChuongHoc.Name = "txtTenChuongHoc";
            this.txtTenChuongHoc.Size = new System.Drawing.Size(178, 22);
            this.txtTenChuongHoc.TabIndex = 3;
            // 
            // dataGridViewChuongHoc
            // 
            this.dataGridViewChuongHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChuongHoc.Location = new System.Drawing.Point(413, 163);
            this.dataGridViewChuongHoc.Name = "dataGridViewChuongHoc";
            this.dataGridViewChuongHoc.RowHeadersWidth = 51;
            this.dataGridViewChuongHoc.RowTemplate.Height = 24;
            this.dataGridViewChuongHoc.Size = new System.Drawing.Size(714, 295);
            this.dataGridViewChuongHoc.TabIndex = 13;
            this.dataGridViewChuongHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewChuongHoc_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(430, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 36);
            this.label1.TabIndex = 9;
            this.label1.Text = "QUẢN LÝ KHÓA HỌC";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1151, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cậpNhậtBàiHọcToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1151, 28);
            this.menuStrip2.TabIndex = 17;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // cậpNhậtBàiHọcToolStripMenuItem
            // 
            this.cậpNhậtBàiHọcToolStripMenuItem.Name = "cậpNhậtBàiHọcToolStripMenuItem";
            this.cậpNhậtBàiHọcToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.cậpNhậtBàiHọcToolStripMenuItem.Text = "Quản Lý Bài Học";
            this.cậpNhậtBàiHọcToolStripMenuItem.Click += new System.EventHandler(this.cậpNhậtBàiHọcToolStripMenuItem_Click);
            // 
            // frmCapNhatChuongHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 609);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.groupBoxThongTin);
            this.Controls.Add(this.dataGridViewChuongHoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "frmCapNhatChuongHoc";
            this.Text = "frmCapNhatChuongHoc";
            this.Load += new System.EventHandler(this.frmCapNhatChuongHoc_Load);
            this.panel2.ResumeLayout(false);
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChuongHoc)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.GroupBox groupBoxThongTin;
        private System.Windows.Forms.ComboBox cmbKhoaHoc;
        private System.Windows.Forms.Label lblGiangVien;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblTenKhoa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.TextBox txtTenChuongHoc;
        private System.Windows.Forms.DataGridView dataGridViewChuongHoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cậpNhậtBàiHọcToolStripMenuItem;
    }
}