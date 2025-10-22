using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmMainQuanTri : Form
    {
        private readonly Model1 _context;
        private readonly NguoiDung _nguoiDunghientai;
        public frmMainQuanTri(NguoiDung nguoiDung, Model1 context)
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

        private void frmMainQuanTri_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýKhóaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmCapNhatKhoaHoc = new frmCapNhatKhoaHoc(_context);
            frmCapNhatKhoaHoc.MdiParent = this;
            frmCapNhatKhoaHoc.WindowState = FormWindowState.Normal;
            frmCapNhatKhoaHoc.Show();
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmQlNguoiDung = new frmQLNguoiDung(_context);
            frmQlNguoiDung.MdiParent = this;
            frmQlNguoiDung.WindowState = FormWindowState.Normal;
            frmQlNguoiDung.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

            // Mở form đăng nhập
            Application.Restart();
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
    }
}
