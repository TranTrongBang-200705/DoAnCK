namespace DoAnCuoiKy
{
    partial class frmCapNhatKhoaHoc
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
            this.label1 = new System.Windows.Forms.Label();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.dataGridViewKhoaHoc = new System.Windows.Forms.DataGridView();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtMaKhoa = new System.Windows.Forms.TextBox();
            this.groupBoxThongTin = new System.Windows.Forms.GroupBox();
            this.comboGiangVien = new System.Windows.Forms.ComboBox();
            this.comboTrinhDo = new System.Windows.Forms.ComboBox();
            this.comboDanhMuc = new System.Windows.Forms.ComboBox();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.lblTrinhDo = new System.Windows.Forms.Label();
            this.lblGiangVien = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblTenKhoa = new System.Windows.Forms.Label();
            this.lblMaKhoa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.txtTenKhoaHoc = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.quảnLýChươngHọcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKhoaHoc)).BeginInit();
            this.groupBoxThongTin.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(420, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ KHÓA HỌC";
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // dataGridViewKhoaHoc
            // 
            this.dataGridViewKhoaHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKhoaHoc.Location = new System.Drawing.Point(403, 108);
            this.dataGridViewKhoaHoc.Name = "dataGridViewKhoaHoc";
            this.dataGridViewKhoaHoc.RowHeadersWidth = 51;
            this.dataGridViewKhoaHoc.RowTemplate.Height = 24;
            this.dataGridViewKhoaHoc.Size = new System.Drawing.Size(714, 295);
            this.dataGridViewKhoaHoc.TabIndex = 2;
            this.dataGridViewKhoaHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewKhoaHoc_CellClick);
            this.dataGridViewKhoaHoc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewKhoaHoc_CellContentClick_1);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(1018, 75);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(99, 27);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click_1);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(844, 77);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(158, 22);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(775, 80);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(63, 16);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm Kiếm";
            // 
            // txtMaKhoa
            // 
            this.txtMaKhoa.Location = new System.Drawing.Point(142, 21);
            this.txtMaKhoa.Name = "txtMaKhoa";
            this.txtMaKhoa.Size = new System.Drawing.Size(124, 22);
            this.txtMaKhoa.TabIndex = 3;
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.comboGiangVien);
            this.groupBoxThongTin.Controls.Add(this.comboTrinhDo);
            this.groupBoxThongTin.Controls.Add(this.comboDanhMuc);
            this.groupBoxThongTin.Controls.Add(this.lblDanhMuc);
            this.groupBoxThongTin.Controls.Add(this.lblTrinhDo);
            this.groupBoxThongTin.Controls.Add(this.lblGiangVien);
            this.groupBoxThongTin.Controls.Add(this.lblMoTa);
            this.groupBoxThongTin.Controls.Add(this.lblTenKhoa);
            this.groupBoxThongTin.Controls.Add(this.lblMaKhoa);
            this.groupBoxThongTin.Controls.Add(this.txtMoTa);
            this.groupBoxThongTin.Controls.Add(this.txtTenKhoaHoc);
            this.groupBoxThongTin.Controls.Add(this.txtMaKhoa);
            this.groupBoxThongTin.Location = new System.Drawing.Point(0, 101);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Size = new System.Drawing.Size(397, 271);
            this.groupBoxThongTin.TabIndex = 5;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông Tin";
            // 
            // comboGiangVien
            // 
            this.comboGiangVien.FormattingEnabled = true;
            this.comboGiangVien.Location = new System.Drawing.Point(142, 176);
            this.comboGiangVien.Name = "comboGiangVien";
            this.comboGiangVien.Size = new System.Drawing.Size(146, 24);
            this.comboGiangVien.TabIndex = 5;
            // 
            // comboTrinhDo
            // 
            this.comboTrinhDo.FormattingEnabled = true;
            this.comboTrinhDo.Location = new System.Drawing.Point(142, 215);
            this.comboTrinhDo.Name = "comboTrinhDo";
            this.comboTrinhDo.Size = new System.Drawing.Size(146, 24);
            this.comboTrinhDo.TabIndex = 5;
            // 
            // comboDanhMuc
            // 
            this.comboDanhMuc.FormattingEnabled = true;
            this.comboDanhMuc.Location = new System.Drawing.Point(142, 142);
            this.comboDanhMuc.Name = "comboDanhMuc";
            this.comboDanhMuc.Size = new System.Drawing.Size(146, 24);
            this.comboDanhMuc.TabIndex = 5;
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Location = new System.Drawing.Point(32, 145);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(67, 16);
            this.lblDanhMuc.TabIndex = 4;
            this.lblDanhMuc.Text = "Danh Mục";
            // 
            // lblTrinhDo
            // 
            this.lblTrinhDo.AutoSize = true;
            this.lblTrinhDo.Location = new System.Drawing.Point(32, 223);
            this.lblTrinhDo.Name = "lblTrinhDo";
            this.lblTrinhDo.Size = new System.Drawing.Size(57, 16);
            this.lblTrinhDo.TabIndex = 4;
            this.lblTrinhDo.Text = "Trình Độ";
            // 
            // lblGiangVien
            // 
            this.lblGiangVien.Location = new System.Drawing.Point(32, 179);
            this.lblGiangVien.Name = "lblGiangVien";
            this.lblGiangVien.Size = new System.Drawing.Size(73, 16);
            this.lblGiangVien.TabIndex = 4;
            this.lblGiangVien.Text = "Giảng Viên";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(32, 101);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(46, 16);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô Tả";
            // 
            // lblTenKhoa
            // 
            this.lblTenKhoa.AutoSize = true;
            this.lblTenKhoa.Location = new System.Drawing.Point(32, 63);
            this.lblTenKhoa.Name = "lblTenKhoa";
            this.lblTenKhoa.Size = new System.Drawing.Size(93, 16);
            this.lblTenKhoa.TabIndex = 4;
            this.lblTenKhoa.Text = "Tên Khóa Học";
            // 
            // lblMaKhoa
            // 
            this.lblMaKhoa.AutoSize = true;
            this.lblMaKhoa.Location = new System.Drawing.Point(32, 24);
            this.lblMaKhoa.Name = "lblMaKhoa";
            this.lblMaKhoa.Size = new System.Drawing.Size(60, 16);
            this.lblMaKhoa.TabIndex = 4;
            this.lblMaKhoa.Text = "Mã Khoa";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(142, 98);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(222, 22);
            this.txtMoTa.TabIndex = 3;
            // 
            // txtTenKhoaHoc
            // 
            this.txtTenKhoaHoc.Location = new System.Drawing.Point(142, 60);
            this.txtTenKhoaHoc.Name = "txtTenKhoaHoc";
            this.txtTenKhoaHoc.Size = new System.Drawing.Size(178, 22);
            this.txtTenKhoaHoc.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHuy);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Location = new System.Drawing.Point(3, 378);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 49);
            this.panel2.TabIndex = 6;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(287, 5);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(74, 29);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(164, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(72, 29);
            this.btnSua.TabIndex = 0;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(30, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(72, 29);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click_1);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1043, 421);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 29);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1129, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýChươngHọcToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1129, 28);
            this.menuStrip2.TabIndex = 8;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // quảnLýChươngHọcToolStripMenuItem
            // 
            this.quảnLýChươngHọcToolStripMenuItem.Name = "quảnLýChươngHọcToolStripMenuItem";
            this.quảnLýChươngHọcToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.quảnLýChươngHọcToolStripMenuItem.Text = "Quản Lý Chương Học";
            this.quảnLýChươngHọcToolStripMenuItem.Click += new System.EventHandler(this.quảnLýChươngHọcToolStripMenuItem_Click);
            // 
            // frmCapNhatKhoaHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 459);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.groupBoxThongTin);
            this.Controls.Add(this.dataGridViewKhoaHoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmCapNhatKhoaHoc";
            this.Text = "frmCapNhatKhoaHoc";
            this.Load += new System.EventHandler(this.frmCapNhatKhoaHoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKhoaHoc)).EndInit();
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private System.Windows.Forms.DataGridView dataGridViewKhoaHoc;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.TextBox txtMaKhoa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.GroupBox groupBoxThongTin;
        private System.Windows.Forms.Label lblMaKhoa;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.Label lblTrinhDo;
        private System.Windows.Forms.Label lblGiangVien;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblTenKhoa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.TextBox txtTenKhoaHoc;
        private System.Windows.Forms.ComboBox comboGiangVien;
        private System.Windows.Forms.ComboBox comboTrinhDo;
        private System.Windows.Forms.ComboBox comboDanhMuc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem quảnLýChươngHọcToolStripMenuItem;
    }
}