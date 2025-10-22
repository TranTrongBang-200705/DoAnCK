namespace DoAnCuoiKy
{
    partial class frmMainHocVien
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.trangChủToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tấtCảKhóaHọcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.khóaHọcCủaTôiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tHỐNGKÊToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.điểmSốToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hồSơToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinCáNhânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đổiMậtKhẩuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVaiTro = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblThoiGian = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trangChủToolStripMenuItem,
            this.doToolStripMenuItem,
            this.tHỐNGKÊToolStripMenuItem,
            this.hồSơToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 28);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // trangChủToolStripMenuItem
            // 
            this.trangChủToolStripMenuItem.Name = "trangChủToolStripMenuItem";
            this.trangChủToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.trangChủToolStripMenuItem.Text = "🏠 Trang Chủ";
            this.trangChủToolStripMenuItem.Click += new System.EventHandler(this.trangChủToolStripMenuItem_Click);
            // 
            // doToolStripMenuItem
            // 
            this.doToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tấtCảKhóaHọcToolStripMenuItem,
            this.khóaHọcCủaTôiToolStripMenuItem});
            this.doToolStripMenuItem.Name = "doToolStripMenuItem";
            this.doToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.doToolStripMenuItem.Text = "📚 KHÓA HỌC";
            this.doToolStripMenuItem.Click += new System.EventHandler(this.doToolStripMenuItem_Click);
            // 
            // tấtCảKhóaHọcToolStripMenuItem
            // 
            this.tấtCảKhóaHọcToolStripMenuItem.Name = "tấtCảKhóaHọcToolStripMenuItem";
            this.tấtCảKhóaHọcToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.tấtCảKhóaHọcToolStripMenuItem.Text = "Tất cả khóa học";
            this.tấtCảKhóaHọcToolStripMenuItem.Click += new System.EventHandler(this.tấtCảKhóaHọcToolStripMenuItem_Click_1);
            // 
            // khóaHọcCủaTôiToolStripMenuItem
            // 
            this.khóaHọcCủaTôiToolStripMenuItem.Name = "khóaHọcCủaTôiToolStripMenuItem";
            this.khóaHọcCủaTôiToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.khóaHọcCủaTôiToolStripMenuItem.Text = "Khóa học của tôi";
            this.khóaHọcCủaTôiToolStripMenuItem.Click += new System.EventHandler(this.khóaHọcCủaTôiToolStripMenuItem_Click_1);
            // 
            // tHỐNGKÊToolStripMenuItem
            // 
            this.tHỐNGKÊToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.điểmSốToolStripMenuItem});
            this.tHỐNGKÊToolStripMenuItem.Name = "tHỐNGKÊToolStripMenuItem";
            this.tHỐNGKÊToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.tHỐNGKÊToolStripMenuItem.Text = "📊 THỐNG KÊ ";
            // 
            // điểmSốToolStripMenuItem
            // 
            this.điểmSốToolStripMenuItem.Name = "điểmSốToolStripMenuItem";
            this.điểmSốToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.điểmSốToolStripMenuItem.Text = "Điểm số";
            this.điểmSốToolStripMenuItem.Click += new System.EventHandler(this.điểmSốToolStripMenuItem_Click);
            // 
            // hồSơToolStripMenuItem
            // 
            this.hồSơToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinCáNhânToolStripMenuItem,
            this.đổiMậtKhẩuToolStripMenuItem,
            this.toolStripSeparator2,
            this.đăngXuấtToolStripMenuItem});
            this.hồSơToolStripMenuItem.Name = "hồSơToolStripMenuItem";
            this.hồSơToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.hồSơToolStripMenuItem.Text = "👤 Hồ Sơ";
            // 
            // thôngTinCáNhânToolStripMenuItem
            // 
            this.thôngTinCáNhânToolStripMenuItem.Name = "thôngTinCáNhânToolStripMenuItem";
            this.thôngTinCáNhânToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.thôngTinCáNhânToolStripMenuItem.Text = "Thông Tin Cá Nhân";
            this.thôngTinCáNhânToolStripMenuItem.Click += new System.EventHandler(this.thôngTinCáNhânToolStripMenuItem_Click);
            // 
            // đổiMậtKhẩuToolStripMenuItem
            // 
            this.đổiMậtKhẩuToolStripMenuItem.Name = "đổiMậtKhẩuToolStripMenuItem";
            this.đổiMậtKhẩuToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.đổiMậtKhẩuToolStripMenuItem.Text = "Đổi Mật Khẩu";
            this.đổiMậtKhẩuToolStripMenuItem.Click += new System.EventHandler(this.đổiMậtKhẩuToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng Xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserInfo,
            this.lblVaiTro,
            this.lblThoiGian});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(151, 20);
            this.lblUserInfo.Text = "toolStripStatusLabel1";
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(151, 20);
            this.lblVaiTro.Text = "toolStripStatusLabel2";
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(151, 20);
            this.lblThoiGian.Text = "toolStripStatusLabel3";
            // 
            // frmMainHocVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMainHocVien";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem trangChủToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hồSơToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinCáNhânToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đổiMậtKhẩuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblUserInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblVaiTro;
        private System.Windows.Forms.ToolStripStatusLabel lblThoiGian;
        private System.Windows.Forms.ToolStripMenuItem doToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tấtCảKhóaHọcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem khóaHọcCủaTôiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tHỐNGKÊToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem điểmSốToolStripMenuItem;
    }
}

