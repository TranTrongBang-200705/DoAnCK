using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnCuoiKy.Models;

namespace DoAnCuoiKy
{
    public partial class frmMainHocVien : Form
    
    {
        private readonly Model1 _context;
        private readonly NguoiDung _nguoiDunghientai;
        public frmMainHocVien(NguoiDung nguoiDung, Model1 context)
        {
            InitializeComponent();
            _nguoiDunghientai = nguoiDung;
            _context = context;

            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Hệ Thống E-Learning - {_nguoiDunghientai.Ho} {_nguoiDunghientai.Ten}";

            // Hiển thị thông tin user
            lblUserInfo.Text = $"👤 {_nguoiDunghientai.Ho} {_nguoiDunghientai.Ten}";
            lblVaiTro.Text = $"🎯 {LayTenVaiTro(_nguoiDunghientai.VaiTro)}";

            // Timer cập nhật thời gian
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) => lblThoiGian.Text = $"🕐 {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            timer.Start();
        }

        public string LayTenVaiTro(int vaiTro)
        {
            return vaiTro switch
            {
                0 => "Học viên",
                1 => "Giảng viên",
                2 => "Quản trị viên",
                _ => "Không xác định"
            };
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

            // Mở form đăng nhập
            Application.Restart();
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmDashboard = new frmDashboardHocVien(_nguoiDunghientai, _context);
            frmDashboard.MdiParent = this;
            frmDashboard.WindowState = FormWindowState.Normal;
            frmDashboard.Show();
        }

        private void tấtCảKhóaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void khóaHọcCủaTôiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Gọi form thông tin cá nhân
            var frmThongTin = new frmInfo(_nguoiDunghientai, _context);
            frmThongTin.MdiParent = this; // Đặt làm form con của Main
            frmThongTin.WindowState = FormWindowState.Normal;
            frmThongTin.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmDoiMK = new frmDoiMK(_nguoiDunghientai, _context);
            frmDoiMK.MdiParent = this;
            frmDoiMK.WindowState = FormWindowState.Normal;
            frmDoiMK.Show();
        }

        private void doToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tấtCảKhóaHọcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var frmDanhSachKH = new frmDanhSachKhoaHoc(_nguoiDunghientai);
            frmDanhSachKH.MdiParent = this;
            frmDanhSachKH.WindowState = FormWindowState.Maximized;
            frmDanhSachKH.Show();
        }

        private void khóaHọcCủaTôiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            

            var frmKhoaHocCuaToi = new frmKhoaHocCuaToi(_nguoiDunghientai);
            frmKhoaHocCuaToi.MdiParent = this;
            frmKhoaHocCuaToi.WindowState = FormWindowState.Maximized;
            frmKhoaHocCuaToi.Show();
        }

        private void điểmSốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmDiem = new frmDiem(_nguoiDunghientai.MaNguoiDung.ToString());
            frmDiem.MdiParent = this;
            frmDiem.WindowState = FormWindowState.Maximized;
            frmDiem.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
