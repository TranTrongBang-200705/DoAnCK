namespace DoAnCuoiKy
{
    partial class frmTrangChuGiangVien
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxCaNhan = new System.Windows.Forms.GroupBox();
            this.flpThongBaoCaNhan = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxChung = new System.Windows.Forms.GroupBox();
            this.flpThongBaoChung = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxCaNhan.SuspendLayout();
            this.groupBoxChung.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Xin Chào,";
            // 
            // groupBoxCaNhan
            // 
            this.groupBoxCaNhan.Controls.Add(this.flpThongBaoCaNhan);
            this.groupBoxCaNhan.Location = new System.Drawing.Point(70, 194);
            this.groupBoxCaNhan.Name = "groupBoxCaNhan";
            this.groupBoxCaNhan.Size = new System.Drawing.Size(406, 244);
            this.groupBoxCaNhan.TabIndex = 11;
            this.groupBoxCaNhan.TabStop = false;
            this.groupBoxCaNhan.Text = "Thông báo cá nhân ";
            // 
            // flpThongBaoCaNhan
            // 
            this.flpThongBaoCaNhan.AutoScroll = true;
            this.flpThongBaoCaNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThongBaoCaNhan.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpThongBaoCaNhan.Location = new System.Drawing.Point(3, 18);
            this.flpThongBaoCaNhan.Name = "flpThongBaoCaNhan";
            this.flpThongBaoCaNhan.Size = new System.Drawing.Size(400, 223);
            this.flpThongBaoCaNhan.TabIndex = 12;
            this.flpThongBaoCaNhan.WrapContents = false;
            // 
            // groupBoxChung
            // 
            this.groupBoxChung.Controls.Add(this.flpThongBaoChung);
            this.groupBoxChung.Location = new System.Drawing.Point(678, 194);
            this.groupBoxChung.Name = "groupBoxChung";
            this.groupBoxChung.Size = new System.Drawing.Size(406, 244);
            this.groupBoxChung.TabIndex = 12;
            this.groupBoxChung.TabStop = false;
            this.groupBoxChung.Text = "Thông báo chung";
            // 
            // flpThongBaoChung
            // 
            this.flpThongBaoChung.AutoScroll = true;
            this.flpThongBaoChung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThongBaoChung.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpThongBaoChung.Location = new System.Drawing.Point(3, 18);
            this.flpThongBaoChung.Name = "flpThongBaoChung";
            this.flpThongBaoChung.Size = new System.Drawing.Size(400, 223);
            this.flpThongBaoChung.TabIndex = 12;
            this.flpThongBaoChung.WrapContents = false;
            // 
            // frmTrangChuGiangVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 622);
            this.Controls.Add(this.groupBoxChung);
            this.Controls.Add(this.groupBoxCaNhan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "frmTrangChuGiangVien";
            this.Text = "frmTrangChuGiangVien";
            this.Load += new System.EventHandler(this.frmTrangChuGiangVien_Load_1);
            this.groupBoxCaNhan.ResumeLayout(false);
            this.groupBoxChung.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxCaNhan;
        private System.Windows.Forms.FlowLayoutPanel flpThongBaoCaNhan;
        private System.Windows.Forms.GroupBox groupBoxChung;
        private System.Windows.Forms.FlowLayoutPanel flpThongBaoChung;
    }
}