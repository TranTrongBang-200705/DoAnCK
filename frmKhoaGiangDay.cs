using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmKhoaGiangDay : Form
    {
        private readonly Model1 _context;
        private readonly string _maGiangVien;
        private List<KhoaHoc> _danhSachKhoaHoc;
        private int _trangHienTai = 1;
        private const int _soKhoaHocMoiTrang = 2; // Hiển thị 2 khóa học mỗi trang

        public frmKhoaGiangDay(string maGiangVien)
        {
            _maGiangVien = maGiangVien;
            _context = new Model1();
            InitializeComponent();
        }

        private async void frmKhoaGiangDay_Load(object sender, EventArgs e)
        {
            await TaiDanhSachKhoaHocGiangDay();
        }
        private async Task TaiDanhSachKhoaHocGiangDay(string timKiem = "", string locTheo = "", string trinhDo = "")
        {
            try
            {
                var maGiangVienGuid = Guid.Parse(_maGiangVien);

                // Entity Framework: Lấy khóa học của giảng viên
                var query = _context.KhoaHocs
                    .Include(kh => kh.DanhMuc)
                    .Include(kh => kh.ChuongHocs.Select(ch => ch.BaiHocs))
                    .Include(kh => kh.DangKyKhoaHocs)
                    .Where(kh => kh.MaGiangVien == maGiangVienGuid);

                // Filter theo tìm kiếm
                if (!string.IsNullOrEmpty(timKiem))
                {
                    query = query.Where(kh => kh.TieuDe.Contains(timKiem));
                }

                _danhSachKhoaHoc = await query
                    .OrderByDescending(kh => kh.NgayTao)
                    .ToListAsync();

                _trangHienTai = 1;
                HienThiDanhSachKhoaHoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}");
            }
        }

        private void HienThiDanhSachKhoaHoc()
        {
            // Ẩn tất cả panel trước
            panelKH1.Visible = false;
            panelKH2.Visible = false;

            if (_danhSachKhoaHoc == null || !_danhSachKhoaHoc.Any())
            {
                // Hiển thị thông báo nếu không có khóa học
                label2.Text = "Bạn chưa có khóa học nào";
                panelKH1.Visible = true;
                return;
            }

            int batDau = (_trangHienTai - 1) * _soKhoaHocMoiTrang;
            int ketThuc = Math.Min(batDau + _soKhoaHocMoiTrang, _danhSachKhoaHoc.Count);

            // Hiển thị tối đa 2 khóa học mỗi trang
            for (int i = 0; i < ketThuc - batDau; i++)
            {
                var khoaHoc = _danhSachKhoaHoc[batDau + i];
                HienThiKhoaHoc(i, khoaHoc);
            }

            CapNhatPhanTrang();
        }

        private void CapNhatPhanTrang()
        {
            if (_danhSachKhoaHoc == null || !_danhSachKhoaHoc.Any())
            {
                label23.Text = "Trang 0/0";
                return;
            }

            int tongTrang = (int)Math.Ceiling(_danhSachKhoaHoc.Count / (double)_soKhoaHocMoiTrang);
            label23.Text = $"Trang {_trangHienTai}/{tongTrang}";
        }

        private void HienThiKhoaHoc(int index, KhoaHoc khoaHoc)
        {
            // Tính số liệu thống kê
            int soHocVien = khoaHoc.DangKyKhoaHocs?.Count ?? 0;
            int soBaiHoc = khoaHoc.ChuongHocs?.Sum(ch => ch.BaiHocs?.Count ?? 0) ?? 0;
            int soBaiTap = khoaHoc.ChuongHocs?.Sum(ch => ch.BaiHocs?.Sum(bh => bh.BaiTaps?.Count ?? 0) ?? 0) ?? 0;

            string trinhDoText = GetTrinhDoText(khoaHoc.TrinhDo);

            switch (index)
            {
                case 0:
                    // Panel 1 - Sử dụng các label có sẵn
                    panelKH1.Visible = true;
                    label2.Text = $"🎯 {khoaHoc.TieuDe}";                           // Tên khóa học
                    label3.Text = $"📁 {khoaHoc.DanhMuc?.TenDanhMuc}";              // Danh mục
                    label4.Text = $"🎓 {trinhDoText}";                              // Trình độ
                    label5.Text = $"👥 {soHocVien} học viên";                       // Số học viên
                    label6.Text = $"📚 {soBaiHoc} bài học";                         // Số bài học
                    label7.Text = $"📝 {soBaiTap} bài tập";                         // Số bài tập
                    label8.Text = $"📅 {khoaHoc.NgayTao:dd/MM/yyyy}";               // Ngày tạo
                    break;

                case 1:
                    // Panel 2 - Sử dụng các label có sẵn
                    panelKH2.Visible = true;
                    label15.Text = $"🎯 {khoaHoc.TieuDe}";                          // Tên khóa học
                    label14.Text = $"📁 {khoaHoc.DanhMuc?.TenDanhMuc}";             // Danh mục
                    label13.Text = $"🎓 {trinhDoText}";                             // Trình độ
                    label12.Text = $"👥 {soHocVien} học viên";                      // Số học viên
                    label11.Text = $"📚 {soBaiHoc} bài học";                        // Số bài học
                    label10.Text = $"📝 {soBaiTap} bài tập";                        // Số bài tập
                    label9.Text = $"📅 {khoaHoc.NgayTao:dd/MM/yyyy}";               // Ngày tạo
                    break;
            }
        }

        private string GetTrinhDoText(int trinhDo)
        {
            return trinhDo switch
            {
                0 => "Cơ bản",
                1 => "Trung cấp",
                2 => "Nâng cao",
                _ => "Không xác định"
            };
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            if (_trangHienTai > 1)
            {
                _trangHienTai--;
                HienThiDanhSachKhoaHoc();
            }
        }

        private void btnTiep_Click(object sender, EventArgs e)
        {
            int tongTrang = (int)Math.Ceiling(_danhSachKhoaHoc.Count / (double)_soKhoaHocMoiTrang);
            if (_trangHienTai < tongTrang)
            {
                _trangHienTai++;
                HienThiDanhSachKhoaHoc();
            }
        }
    }
}
