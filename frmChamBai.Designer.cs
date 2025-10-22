namespace DoAnCuoiKy
{
    partial class frmChamBai
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
            this.dgvBaiNop = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtxtNhanXet = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiem = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTrangThai = new System.Windows.Forms.ComboBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFileDinhKem = new System.Windows.Forms.Label();
            this.btnMoFile = new System.Windows.Forms.Button();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.rtxtNoiDungNop = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiNop)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBaiNop
            // 
            this.dgvBaiNop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaiNop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvBaiNop.Location = new System.Drawing.Point(462, 107);
            this.dgvBaiNop.Name = "dgvBaiNop";
            this.dgvBaiNop.RowHeadersWidth = 51;
            this.dgvBaiNop.RowTemplate.Height = 24;
            this.dgvBaiNop.Size = new System.Drawing.Size(924, 435);
            this.dgvBaiNop.TabIndex = 0;
            this.dgvBaiNop.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaiNop_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtNoiDungNop);
            this.groupBox1.Controls.Add(this.lblNoiDung);
            this.groupBox1.Controls.Add(this.btnMoFile);
            this.groupBox1.Controls.Add(this.lblFileDinhKem);
            this.groupBox1.Controls.Add(this.cmbTrangThai);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDiem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rtxtNhanXet);
            this.groupBox1.Location = new System.Drawing.Point(27, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 398);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chấm Bài";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 513);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "Lưu/Sửa";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtxtNhanXet
            // 
            this.rtxtNhanXet.Location = new System.Drawing.Point(41, 182);
            this.rtxtNhanXet.Name = "rtxtNhanXet";
            this.rtxtNhanXet.Size = new System.Drawing.Size(298, 39);
            this.rtxtNhanXet.TabIndex = 2;
            this.rtxtNhanXet.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nhận Xét ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Điểm";
            // 
            // txtDiem
            // 
            this.txtDiem.Location = new System.Drawing.Point(132, 102);
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.Size = new System.Drawing.Size(137, 22);
            this.txtDiem.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1303, 548);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 29);
            this.button3.TabIndex = 4;
            this.button3.Text = "Thoát";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Trạng Thái";
            // 
            // cmbTrangThai
            // 
            this.cmbTrangThai.FormattingEnabled = true;
            this.cmbTrangThai.Location = new System.Drawing.Point(132, 48);
            this.cmbTrangThai.Name = "cmbTrangThai";
            this.cmbTrangThai.Size = new System.Drawing.Size(121, 24);
            this.cmbTrangThai.TabIndex = 7;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã bài nộp";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tên học viên";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Tên bài tập";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Khóa học";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Ngày nộp";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Điểm";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Trạng thái";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // lblFileDinhKem
            // 
            this.lblFileDinhKem.AutoSize = true;
            this.lblFileDinhKem.Location = new System.Drawing.Point(38, 233);
            this.lblFileDinhKem.Name = "lblFileDinhKem";
            this.lblFileDinhKem.Size = new System.Drawing.Size(44, 16);
            this.lblFileDinhKem.TabIndex = 8;
            this.lblFileDinhKem.Text = "label4";
            // 
            // btnMoFile
            // 
            this.btnMoFile.Location = new System.Drawing.Point(41, 253);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(83, 29);
            this.btnMoFile.TabIndex = 5;
            this.btnMoFile.Text = "Mở File";
            this.btnMoFile.UseVisualStyleBackColor = true;
            this.btnMoFile.Click += new System.EventHandler(this.btnMoFile_Click);
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Location = new System.Drawing.Point(38, 294);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(115, 16);
            this.lblNoiDung.TabIndex = 9;
            this.lblNoiDung.Text = "Nội Dung Bài Nộp";
            // 
            // rtxtNoiDungNop
            // 
            this.rtxtNoiDungNop.Location = new System.Drawing.Point(41, 322);
            this.rtxtNoiDungNop.Name = "rtxtNoiDungNop";
            this.rtxtNoiDungNop.Size = new System.Drawing.Size(298, 41);
            this.rtxtNoiDungNop.TabIndex = 10;
            this.rtxtNoiDungNop.Text = "";
            // 
            // frmChamBai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1415, 584);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvBaiNop);
            this.Name = "frmChamBai";
            this.Text = "frmChamBai";
            this.Load += new System.EventHandler(this.frmChamBai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiNop)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBaiNop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxtNhanXet;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cmbTrangThai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.RichTextBox rtxtNoiDungNop;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.Button btnMoFile;
        private System.Windows.Forms.Label lblFileDinhKem;
    }
}