namespace DoAnCuoiKy
{
    partial class frmMainGiangVien
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuKhoaGiangDay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLyBaiTap = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmBàiTậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chấmBàiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHoSo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThongTinCaNhan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.mnuKhoaGiangDay,
            this.menuQuanLyBaiTap,
            this.menuHoSo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1169, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuKhoaGiangDay
            // 
            this.mnuKhoaGiangDay.Name = "mnuKhoaGiangDay";
            this.mnuKhoaGiangDay.Size = new System.Drawing.Size(130, 24);
            this.mnuKhoaGiangDay.Text = "Khóa Giảng Dạy";
            this.mnuKhoaGiangDay.Click += new System.EventHandler(this.mnuKhoaGiangDay_Click);
            // 
            // menuQuanLyBaiTap
            // 
            this.menuQuanLyBaiTap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thêmBàiTậpToolStripMenuItem,
            this.chấmBàiToolStripMenuItem});
            this.menuQuanLyBaiTap.Name = "menuQuanLyBaiTap";
            this.menuQuanLyBaiTap.Size = new System.Drawing.Size(129, 24);
            this.menuQuanLyBaiTap.Text = "Quản Lý Bài Tập";
            this.menuQuanLyBaiTap.Click += new System.EventHandler(this.menuQuanLyBaiTap_Click);
            // 
            // thêmBàiTậpToolStripMenuItem
            // 
            this.thêmBàiTậpToolStripMenuItem.Name = "thêmBàiTậpToolStripMenuItem";
            this.thêmBàiTậpToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.thêmBàiTậpToolStripMenuItem.Text = "Thêm Bài Tập";
            this.thêmBàiTậpToolStripMenuItem.Click += new System.EventHandler(this.thêmBàiTậpToolStripMenuItem_Click);
            // 
            // chấmBàiToolStripMenuItem
            // 
            this.chấmBàiToolStripMenuItem.Name = "chấmBàiToolStripMenuItem";
            this.chấmBàiToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.chấmBàiToolStripMenuItem.Text = "Chấm Bài";
            this.chấmBàiToolStripMenuItem.Click += new System.EventHandler(this.chấmBàiToolStripMenuItem_Click);
            // 
            // menuHoSo
            // 
            this.menuHoSo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuThongTinCaNhan,
            this.menuDoiMatKhau,
            this.toolStripSeparator1,
            this.menuDangXuat});
            this.menuHoSo.Name = "menuHoSo";
            this.menuHoSo.Size = new System.Drawing.Size(64, 24);
            this.menuHoSo.Text = "Hồ Sơ";
            // 
            // menuThongTinCaNhan
            // 
            this.menuThongTinCaNhan.Name = "menuThongTinCaNhan";
            this.menuThongTinCaNhan.Size = new System.Drawing.Size(210, 26);
            this.menuThongTinCaNhan.Text = "Thông tin cá nhân";
            this.menuThongTinCaNhan.Click += new System.EventHandler(this.menuThongTinCaNhan_Click);
            // 
            // menuDoiMatKhau
            // 
            this.menuDoiMatKhau.Name = "menuDoiMatKhau";
            this.menuDoiMatKhau.Size = new System.Drawing.Size(210, 26);
            this.menuDoiMatKhau.Text = "Đổi mật khẩu";
            this.menuDoiMatKhau.Click += new System.EventHandler(this.menuDoiMatKhau_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // menuDangXuat
            // 
            this.menuDangXuat.Name = "menuDangXuat";
            this.menuDangXuat.Size = new System.Drawing.Size(210, 26);
            this.menuDangXuat.Text = "Đăng xuất";
            this.menuDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(89, 24);
            this.toolStripMenuItem1.Text = "Trang Chủ";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // frmMainGiangVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 588);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMainGiangVien";
            this.Text = "frmMainGiangVien";
            this.Load += new System.EventHandler(this.frmMainGiangVien_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuHoSo;
        private System.Windows.Forms.ToolStripMenuItem menuThongTinCaNhan;
        private System.Windows.Forms.ToolStripMenuItem menuDoiMatKhau;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuKhoaGiangDay;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLyBaiTap;
        private System.Windows.Forms.ToolStripMenuItem thêmBàiTậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chấmBàiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}