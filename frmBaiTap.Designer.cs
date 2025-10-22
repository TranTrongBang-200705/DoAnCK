namespace DoAnCuoiKy
{
    partial class frmBaiTap
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
            this.lblTieuDeBaiTap = new System.Windows.Forms.Label();
            this.treeViewBaiTap = new System.Windows.Forms.TreeView();
            this.lblNoiDungBaiTap = new System.Windows.Forms.Label();
            this.rtbNoiDungBaiTap = new System.Windows.Forms.RichTextBox();
            this.lblHanNop = new System.Windows.Forms.Label();
            this.dtpHanNop = new System.Windows.Forms.DateTimePicker();
            this.lblDiemToiDa = new System.Windows.Forms.Label();
            this.rtbNoiDungNopBai = new System.Windows.Forms.RichTextBox();
            this.numDiemToiDa = new System.Windows.Forms.NumericUpDown();
            this.lblNopBai = new System.Windows.Forms.Label();
            this.lblDinhKemFile = new System.Windows.Forms.Label();
            this.txtDuongDanFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnNopBai = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtTieuDeBaiTap = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemToiDa)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(262, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "📚 BÀI TẬP KHÓA HỌC";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblTieuDeBaiTap
            // 
            this.lblTieuDeBaiTap.AutoSize = true;
            this.lblTieuDeBaiTap.Location = new System.Drawing.Point(325, 116);
            this.lblTieuDeBaiTap.Name = "lblTieuDeBaiTap";
            this.lblTieuDeBaiTap.Size = new System.Drawing.Size(100, 16);
            this.lblTieuDeBaiTap.TabIndex = 1;
            this.lblTieuDeBaiTap.Text = "Tiêu đề bài tập:";
            // 
            // treeViewBaiTap
            // 
            this.treeViewBaiTap.Location = new System.Drawing.Point(51, 116);
            this.treeViewBaiTap.Name = "treeViewBaiTap";
            this.treeViewBaiTap.Size = new System.Drawing.Size(142, 582);
            this.treeViewBaiTap.TabIndex = 2;
            // 
            // lblNoiDungBaiTap
            // 
            this.lblNoiDungBaiTap.AutoSize = true;
            this.lblNoiDungBaiTap.Location = new System.Drawing.Point(325, 163);
            this.lblNoiDungBaiTap.Name = "lblNoiDungBaiTap";
            this.lblNoiDungBaiTap.Size = new System.Drawing.Size(108, 16);
            this.lblNoiDungBaiTap.TabIndex = 3;
            this.lblNoiDungBaiTap.Text = "Nội dung bài tập:";
            // 
            // rtbNoiDungBaiTap
            // 
            this.rtbNoiDungBaiTap.Location = new System.Drawing.Point(328, 199);
            this.rtbNoiDungBaiTap.Name = "rtbNoiDungBaiTap";
            this.rtbNoiDungBaiTap.Size = new System.Drawing.Size(428, 179);
            this.rtbNoiDungBaiTap.TabIndex = 4;
            this.rtbNoiDungBaiTap.Text = "";
            // 
            // lblHanNop
            // 
            this.lblHanNop.AutoSize = true;
            this.lblHanNop.Location = new System.Drawing.Point(325, 404);
            this.lblHanNop.Name = "lblHanNop";
            this.lblHanNop.Size = new System.Drawing.Size(61, 16);
            this.lblHanNop.TabIndex = 5;
            this.lblHanNop.Text = "Hạn nộp:";
            // 
            // dtpHanNop
            // 
            this.dtpHanNop.Location = new System.Drawing.Point(328, 434);
            this.dtpHanNop.Name = "dtpHanNop";
            this.dtpHanNop.Size = new System.Drawing.Size(200, 22);
            this.dtpHanNop.TabIndex = 6;
            // 
            // lblDiemToiDa
            // 
            this.lblDiemToiDa.AutoSize = true;
            this.lblDiemToiDa.Location = new System.Drawing.Point(333, 485);
            this.lblDiemToiDa.Name = "lblDiemToiDa";
            this.lblDiemToiDa.Size = new System.Drawing.Size(74, 16);
            this.lblDiemToiDa.TabIndex = 7;
            this.lblDiemToiDa.Text = "Điểm tối đa";
            // 
            // rtbNoiDungNopBai
            // 
            this.rtbNoiDungNopBai.Location = new System.Drawing.Point(328, 576);
            this.rtbNoiDungNopBai.Name = "rtbNoiDungNopBai";
            this.rtbNoiDungNopBai.Size = new System.Drawing.Size(416, 67);
            this.rtbNoiDungNopBai.TabIndex = 8;
            this.rtbNoiDungNopBai.Text = "";
            // 
            // numDiemToiDa
            // 
            this.numDiemToiDa.Location = new System.Drawing.Point(328, 504);
            this.numDiemToiDa.Name = "numDiemToiDa";
            this.numDiemToiDa.Size = new System.Drawing.Size(120, 22);
            this.numDiemToiDa.TabIndex = 9;
            // 
            // lblNopBai
            // 
            this.lblNopBai.AutoSize = true;
            this.lblNopBai.Location = new System.Drawing.Point(325, 557);
            this.lblNopBai.Name = "lblNopBai";
            this.lblNopBai.Size = new System.Drawing.Size(55, 16);
            this.lblNopBai.TabIndex = 10;
            this.lblNopBai.Text = "Nộp bài";
            // 
            // lblDinhKemFile
            // 
            this.lblDinhKemFile.AutoSize = true;
            this.lblDinhKemFile.Location = new System.Drawing.Point(325, 657);
            this.lblDinhKemFile.Name = "lblDinhKemFile";
            this.lblDinhKemFile.Size = new System.Drawing.Size(89, 16);
            this.lblDinhKemFile.TabIndex = 11;
            this.lblDinhKemFile.Text = "File đính kèm:";
            // 
            // txtDuongDanFile
            // 
            this.txtDuongDanFile.Location = new System.Drawing.Point(328, 676);
            this.txtDuongDanFile.Name = "txtDuongDanFile";
            this.txtDuongDanFile.Size = new System.Drawing.Size(200, 22);
            this.txtDuongDanFile.TabIndex = 12;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(328, 704);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(79, 31);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnNopBai
            // 
            this.btnNopBai.Location = new System.Drawing.Point(427, 772);
            this.btnNopBai.Name = "btnNopBai";
            this.btnNopBai.Size = new System.Drawing.Size(79, 31);
            this.btnNopBai.TabIndex = 14;
            this.btnNopBai.Text = "Nộp Bài";
            this.btnNopBai.UseVisualStyleBackColor = true;
            this.btnNopBai.Click += new System.EventHandler(this.btnNopBai_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(540, 772);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(79, 31);
            this.btnThoat.TabIndex = 15;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // txtTieuDeBaiTap
            // 
            this.txtTieuDeBaiTap.Location = new System.Drawing.Point(427, 113);
            this.txtTieuDeBaiTap.Name = "txtTieuDeBaiTap";
            this.txtTieuDeBaiTap.Size = new System.Drawing.Size(202, 22);
            this.txtTieuDeBaiTap.TabIndex = 16;
            // 
            // frmBaiTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 815);
            this.Controls.Add(this.txtTieuDeBaiTap);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNopBai);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDuongDanFile);
            this.Controls.Add(this.lblDinhKemFile);
            this.Controls.Add(this.lblNopBai);
            this.Controls.Add(this.numDiemToiDa);
            this.Controls.Add(this.rtbNoiDungNopBai);
            this.Controls.Add(this.lblDiemToiDa);
            this.Controls.Add(this.dtpHanNop);
            this.Controls.Add(this.lblHanNop);
            this.Controls.Add(this.rtbNoiDungBaiTap);
            this.Controls.Add(this.lblNoiDungBaiTap);
            this.Controls.Add(this.treeViewBaiTap);
            this.Controls.Add(this.lblTieuDeBaiTap);
            this.Controls.Add(this.label1);
            this.Name = "frmBaiTap";
            this.Text = "frmBaiTap";
            this.Load += new System.EventHandler(this.frmBaiTap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDiemToiDa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTieuDeBaiTap;
        private System.Windows.Forms.TreeView treeViewBaiTap;
        private System.Windows.Forms.Label lblNoiDungBaiTap;
        private System.Windows.Forms.RichTextBox rtbNoiDungBaiTap;
        private System.Windows.Forms.Label lblHanNop;
        private System.Windows.Forms.DateTimePicker dtpHanNop;
        private System.Windows.Forms.Label lblDiemToiDa;
        private System.Windows.Forms.RichTextBox rtbNoiDungNopBai;
        private System.Windows.Forms.NumericUpDown numDiemToiDa;
        private System.Windows.Forms.Label lblNopBai;
        private System.Windows.Forms.Label lblDinhKemFile;
        private System.Windows.Forms.TextBox txtDuongDanFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnNopBai;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox txtTieuDeBaiTap;
    }
}