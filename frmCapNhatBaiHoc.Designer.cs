namespace DoAnCuoiKy
{
    partial class frmCapNhatBaiHoc
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
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblTenKhoa = new System.Windows.Forms.Label();
            this.txtTenBaiHoc = new System.Windows.Forms.TextBox();
            this.dgvBaiHoc = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbNoiDung = new System.Windows.Forms.RichTextBox();
            this.cmbChuongHoc = new System.Windows.Forms.ComboBox();
            this.lblGiangVien = new System.Windows.Forms.Label();
            this.rtbLink = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiHoc)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(1032, 109);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(99, 27);
            this.btnTimKiem.TabIndex = 20;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1057, 455);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 29);
            this.btnThoat.TabIndex = 18;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(858, 111);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(158, 22);
            this.txtTimKiem.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHuy);
            this.panel2.Controls.Add(this.btnSua);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Location = new System.Drawing.Point(17, 435);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 49);
            this.panel2.TabIndex = 23;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(298, 5);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(74, 29);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(159, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(72, 29);
            this.btnSua.TabIndex = 0;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(26, 5);
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
            this.lblTimKiem.Location = new System.Drawing.Point(789, 114);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(63, 16);
            this.lblTimKiem.TabIndex = 16;
            this.lblTimKiem.Text = "Tìm Kiếm";
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.label2);
            this.groupBoxThongTin.Controls.Add(this.rtbLink);
            this.groupBoxThongTin.Controls.Add(this.cmbChuongHoc);
            this.groupBoxThongTin.Controls.Add(this.lblGiangVien);
            this.groupBoxThongTin.Controls.Add(this.rtbNoiDung);
            this.groupBoxThongTin.Controls.Add(this.lblMoTa);
            this.groupBoxThongTin.Controls.Add(this.lblTenKhoa);
            this.groupBoxThongTin.Controls.Add(this.txtTenBaiHoc);
            this.groupBoxThongTin.Location = new System.Drawing.Point(14, 135);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Size = new System.Drawing.Size(397, 294);
            this.groupBoxThongTin.TabIndex = 22;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông Tin";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(38, 113);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(63, 16);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Nội Dung";
            // 
            // lblTenKhoa
            // 
            this.lblTenKhoa.AutoSize = true;
            this.lblTenKhoa.Location = new System.Drawing.Point(38, 30);
            this.lblTenKhoa.Name = "lblTenKhoa";
            this.lblTenKhoa.Size = new System.Drawing.Size(82, 16);
            this.lblTenKhoa.TabIndex = 4;
            this.lblTenKhoa.Text = "Tên Bài Học";
            // 
            // txtTenBaiHoc
            // 
            this.txtTenBaiHoc.Location = new System.Drawing.Point(172, 27);
            this.txtTenBaiHoc.Name = "txtTenBaiHoc";
            this.txtTenBaiHoc.Size = new System.Drawing.Size(178, 22);
            this.txtTenBaiHoc.TabIndex = 3;
            // 
            // dgvBaiHoc
            // 
            this.dgvBaiHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaiHoc.Location = new System.Drawing.Point(417, 142);
            this.dgvBaiHoc.Name = "dgvBaiHoc";
            this.dgvBaiHoc.RowHeadersWidth = 51;
            this.dgvBaiHoc.RowTemplate.Height = 24;
            this.dgvBaiHoc.Size = new System.Drawing.Size(714, 295);
            this.dgvBaiHoc.TabIndex = 21;
            this.dgvBaiHoc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaiHoc_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(411, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 36);
            this.label1.TabIndex = 17;
            this.label1.Text = "QUẢN LÝ KHÓA HỌC";
            // 
            // rtbNoiDung
            // 
            this.rtbNoiDung.Location = new System.Drawing.Point(41, 142);
            this.rtbNoiDung.Name = "rtbNoiDung";
            this.rtbNoiDung.Size = new System.Drawing.Size(309, 44);
            this.rtbNoiDung.TabIndex = 6;
            this.rtbNoiDung.Text = "";
            // 
            // cmbChuongHoc
            // 
            this.cmbChuongHoc.FormattingEnabled = true;
            this.cmbChuongHoc.Location = new System.Drawing.Point(172, 71);
            this.cmbChuongHoc.Name = "cmbChuongHoc";
            this.cmbChuongHoc.Size = new System.Drawing.Size(178, 24);
            this.cmbChuongHoc.TabIndex = 8;
            // 
            // lblGiangVien
            // 
            this.lblGiangVien.Location = new System.Drawing.Point(38, 74);
            this.lblGiangVien.Name = "lblGiangVien";
            this.lblGiangVien.Size = new System.Drawing.Size(128, 16);
            this.lblGiangVien.TabIndex = 7;
            this.lblGiangVien.Text = "Chọn Chương Học";
            // 
            // rtbLink
            // 
            this.rtbLink.Location = new System.Drawing.Point(41, 229);
            this.rtbLink.Name = "rtbLink";
            this.rtbLink.Size = new System.Drawing.Size(309, 44);
            this.rtbLink.TabIndex = 9;
            this.rtbLink.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Link Bài Học";
            // 
            // frmCapNhatBaiHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 496);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.groupBoxThongTin);
            this.Controls.Add(this.dgvBaiHoc);
            this.Controls.Add(this.label1);
            this.Name = "frmCapNhatBaiHoc";
            this.Text = "frmCapNhatBaiHoc";
            this.Load += new System.EventHandler(this.frmCapNhatBaiHoc_Load);
            this.panel2.ResumeLayout(false);
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiHoc)).EndInit();
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
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblTenKhoa;
        private System.Windows.Forms.TextBox txtTenBaiHoc;
        private System.Windows.Forms.DataGridView dgvBaiHoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbLink;
        private System.Windows.Forms.ComboBox cmbChuongHoc;
        private System.Windows.Forms.Label lblGiangVien;
        private System.Windows.Forms.RichTextBox rtbNoiDung;
    }
}