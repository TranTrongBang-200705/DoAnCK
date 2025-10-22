using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DoAnCuoiKy.Models;

namespace DoAnCuoiKy
{
    public partial class frmMainGiangVien : Form
    {
        // 🔹 Biến toàn cục
        private readonly Model1 _context;
        private readonly NguoiDung _nguoiDunghientai;

        // 🔹 Hàm khởi tạo có tham số (nhận thông tin người đăng nhập)
        public frmMainGiangVien(NguoiDung nguoiDung, Model1 context)
        {
            InitializeComponent();
            _nguoiDunghientai = nguoiDung;
            _context = context;
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Hệ Thống E-Learning - Giảng Viên: {_nguoiDunghientai.Ho} {_nguoiDunghientai.Ten}";
        }

        // 🔹 Sự kiện khi form load
        private void frmMainGiangVien_Load(object sender, EventArgs e)
        {
           /* LoadThongKe();
            LoadCoSo();
           */
        }

        private void mnuKhoaGiangDay_Click(object sender, EventArgs e)
        {
            var frmKhoaGiangDay = new frmKhoaGiangDay(_nguoiDunghientai.MaNguoiDung.ToString());
            frmKhoaGiangDay.MdiParent = this;
            frmKhoaGiangDay.WindowState = FormWindowState.Maximized;
            frmKhoaGiangDay.Show();
        }

        private void menuQuanLyBaiTap_Click(object sender, EventArgs e)
        {
            
        }

        private void chấmBàiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmChamBai = new frmChamBai(_context);
            frmChamBai.MdiParent = this;
            frmChamBai.WindowState = FormWindowState.Maximized;
            frmChamBai.Show();
        }

        private void thêmBàiTậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmThemBaiTap = new frmThemBaiTap(_context);
            frmThemBaiTap.MdiParent = this;
            frmThemBaiTap.WindowState = FormWindowState.Maximized;
            frmThemBaiTap.Show();
        }

        private void menuTrangChu_Click(object sender, EventArgs e)
        {
           
        }

        private void menuThongTinCaNhan_Click(object sender, EventArgs e)
        {
            var frmInfo = new frmInfo(_nguoiDunghientai, _context);
            frmInfo.MdiParent = this;
            frmInfo.WindowState = FormWindowState.Maximized;
            frmInfo.Show();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();

            // Mở form đăng nhập
            Application.Restart();
        }

        private void menuDoiMatKhau_Click(object sender, EventArgs e)
        {
            var frmDoiMk = new frmDoiMK(_nguoiDunghientai, _context);
            frmDoiMk.MdiParent = this;
            frmDoiMk.WindowState = FormWindowState.Maximized;
            frmDoiMk.Show();
        }
    }

        // 🔹 Hàm hiển thị danh sách cơ sở (nếu có dgvPhongHoc)
        /* private void LoadCoSo()
         {
             var listCoSo = _context.CoSoes
                 .Select(c => new { c.TenCoSo, c.KyHieuPhong, c.DiaChi })
                 .ToList();
             dgvPhongHoc.DataSource = listCoSo;
         }

         // 🔹 Hàm thống kê nhanh
         private void LoadThongKe()
         {
             Guid maGiangVien = _nguoiDung.MaNguoiDung;

             int soKhoaHoc = _context.KhoaHocs.Count(k => k.MaGiangVien == maGiangVien);
             lblKhoaHoc.Text = $"Khóa học đang dạy: {soKhoaHoc}";

             // 2️⃣ Bài tập đã giao
             int soBaiTap = (from k in _context.KhoaHocs
                             join c in _context.ChuongHocs on k.MaKhoaHoc equals c.MaKhoaHoc
                             join b in _context.BaiHocs on c.MaChuong equals b.MaChuong
                             join bt in _context.BaiTaps on b.MaBaiHoc equals bt.MaBaiHoc
                             where k.MaGiangVien == maGiangVien
                             select bt).Count();
             lblBaiTapDaGiao.Text = $"Bài tập đã giao: {soBaiTap}";

             // 3️⃣ Bài tập đã chấm
             int soBaiCham = (from bt in _context.BaiTaps
                              join nb in _context.NopBaiTaps on bt.MaBaiTap equals nb.MaBaiTap
                              join bh in _context.BaiHocs on bt.MaBaiHoc equals bh.MaBaiHoc
                              join ch in _context.ChuongHocs on bh.MaChuong equals ch.MaChuong
                              join kh in _context.KhoaHocs on ch.MaKhoaHoc equals kh.MaKhoaHoc
                              where kh.MaGiangVien == maGiangVien && nb.TrangThai == 1
                              select nb).Count();
             lblBaiTapDaCham.Text = $"Bài tập đã chấm: {soBaiCham}";
         }



         private void mnuKhoaGiangDay_Click(object sender, EventArgs e)
         {
             var maGV = _nguoiDung.MaNguoiDung; // kiểu Guid
             frmKhoaGiangDay frm = new frmKhoaGiangDay(maGV);
             frm.ShowDialog();

         }

         private void menuQuanLyBaiTap_Click(object sender, EventArgs e)
         {
             frmQLBT f = new frmQLBT();
             f.StartPosition = FormStartPosition.CenterScreen; // căn giữa màn hình
             f.ShowDialog();
         }*/
    }

