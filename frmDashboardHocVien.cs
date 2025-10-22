using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmDashboardHocVien : Form
    {
        private readonly NguoiDung _nguoiDungHienTai;
        private readonly Model1 _context;

        public frmDashboardHocVien(NguoiDung nguoiDung, Model1 context)
        {
            InitializeComponent();
            _nguoiDungHienTai = nguoiDung;
            _context = context;

            // Đặt font mặc định hỗ trợ tiếng Việt
            this.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        }

        private async void frmDashboard_Load(object sender, EventArgs e)
        {
            await TaiDuLieuDashboard();
        }

        private async Task TaiDuLieuDashboard()
        {
            try
            {
                // ĐẢM BẢO TÍNH LẠI TIẾN ĐỘ CHÍNH XÁC TRƯỚC KHI HIỂN THỊ
                await TinhLaiTienDoTatCaKhoaHoc();

                // Load dữ liệu
                await TaiKhoaHoc1();
                await TaiTienDoHocTap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task TinhLaiTienDoTatCaKhoaHoc()
        {
            try
            {
                // LẤY TẤT CẢ KHÓA HỌC HỌC VIÊN ĐÃ ĐĂNG KÝ
                var khoaHocDaDangKy = await _context.DangKyKhoaHocs
                    .Where(d => d.MaHocVien == _nguoiDungHienTai.MaNguoiDung)
                    .ToListAsync();

                // TÍNH LẠI TIẾN ĐỘ CHO TỪNG KHÓA HỌC
                foreach (var dangKy in khoaHocDaDangKy)
                {
                    try
                    {
                        await _context.Database.ExecuteSqlCommandAsync(
                            "EXEC sp_TinhPhanTramHoanThanhKhoaHoc @p0, @p1",
                            _nguoiDungHienTai.MaNguoiDung.ToString(),
                            dangKy.MaKhoaHoc.ToString()
                        );
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi tính tiến độ cho khóa học {dangKy.MaKhoaHoc}: {ex.Message}");
                    }
                }

                // LƯU THAY ĐỔI VÀO CSDL
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tính lại tiến độ: {ex.Message}");
            }
        }

        private async Task TaiKhoaHoc1()
        {
            try
            {
                // 1. Số khóa học đã đăng ký
                var soKhoaHoc = await _context.DangKyKhoaHocs
                    .CountAsync(d => d.MaHocVien == _nguoiDungHienTai.MaNguoiDung);
                lblSoKhoaHoc.Text = soKhoaHoc.ToString();

                // 2. Số khóa học đang học (chưa hoàn thành)
                var dangHoc = await _context.DangKyKhoaHocs
                    .CountAsync(d => d.MaHocVien == _nguoiDungHienTai.MaNguoiDung &&
                                     d.DaHoanThanh == false);
                lblDangHoc.Text = dangHoc.ToString();

                // 3. Số khóa học đã hoàn thành
                var hoanThanh = await _context.DangKyKhoaHocs
                    .CountAsync(d => d.MaHocVien == _nguoiDungHienTai.MaNguoiDung &&
                                     d.DaHoanThanh == true);
                lblHoanThanh.Text = hoanThanh.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thống kê khóa học: {ex.Message}");
            }
        }

        private async Task TaiTienDoHocTap()
        {
            try
            {
                // Lấy 3 khóa học có tiến độ cao nhất - DỮ LIỆU THẬT TỪ CSDL
                var khoaHocDangHoc = await _context.DangKyKhoaHocs
                    .Include(d => d.KhoaHoc)
                    .Where(d => d.MaHocVien == _nguoiDungHienTai.MaNguoiDung)
                    .OrderByDescending(d => d.PhanTramHoanThanh)
                    .Take(3)
                    .ToListAsync();

                // DEBUG: Kiểm tra dữ liệu thật
                Console.WriteLine("=== DỮ LIỆU THẬT TỪ CSDL ===");
                foreach (var dangKy in khoaHocDangHoc)
                {
                    Console.WriteLine($"Khóa: {dangKy.KhoaHoc.TieuDe}, %: {dangKy.PhanTramHoanThanh}%");
                }

                // Cập nhật UI cho từng khóa học
                for (int i = 0; i < khoaHocDangHoc.Count; i++)
                {
                    var dangKy = khoaHocDangHoc[i];

                    switch (i)
                    {
                        case 0:
                            CapNhatKhoaHocUI(lblTenKH1, progressBar1, lblPhanTram1, lblTrangThai1, dangKy);
                            panel1.Visible = true;
                            break;
                        case 1:
                            CapNhatKhoaHocUI(lblTenKH2, progressBar2, lblPhanTram2, lblTrangThai2, dangKy);
                            panel2.Visible = true;
                            break;
                        case 2:
                            CapNhatKhoaHocUI(lblTenKH3, progressBar3, lblPhanTram3, lblTrangThai3, dangKy);
                            panel3.Visible = true;
                            break;
                    }
                }

                // Ẩn các panel không có dữ liệu
                if (khoaHocDangHoc.Count < 1) panel1.Visible = false;
                if (khoaHocDangHoc.Count < 2) panel2.Visible = false;
                if (khoaHocDangHoc.Count < 3) panel3.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải tiến độ học tập: {ex.Message}");
            }
        }

        private void CapNhatKhoaHocUI(Label lblTen, System.Windows.Forms.ProgressBar progress, Label lblPhanTram, Label lblTrangThai, DangKyKhoaHoc dangKy)
        {
            try
            {
                // Đặt font cho các control
                lblTen.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
                lblPhanTram.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);
                lblTrangThai.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);

                // Tên khóa học
                lblTen.Text = dangKy.KhoaHoc?.TieuDe ?? "Khóa học không xác định";

                // Tiến độ THẬT từ CSDL
                int phanTram = (int)(dangKy.PhanTramHoanThanh ?? 0);
                progress.Value = phanTram;
                lblPhanTram.Text = $"{phanTram}%";

                // Trạng thái CHÍNH XÁC - SỬ DỤNG KÝ TỰ ĐƠN GIẢN
                if (dangKy.DaHoanThanh == true)
                {
                    lblTrangThai.Text = "Hoàn thành (100%)";
                    lblTrangThai.ForeColor = Color.Green;
                }
                else if (phanTram > 0)
                {
                    lblTrangThai.Text = $"Đang học ({phanTram}%)";
                    lblTrangThai.ForeColor = Color.Blue;
                }
                else
                {
                    lblTrangThai.Text = "Chưa bắt đầu";
                    lblTrangThai.ForeColor = Color.Gray;
                }

                Console.WriteLine($"Hiển thị: {dangKy.KhoaHoc?.TieuDe} - {phanTram}% - {lblTrangThai.Text}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật UI: {ex.Message}");
            }
        }

        // THÊM NÚT REFRESH ĐỂ CẬP NHẬT DỮ LIỆU THẬT
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await TaiDuLieuDashboard();
            MessageBox.Show("Đã cập nhật dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Các nút chức năng
        private void btnKhoaHocCuaToi_Click(object sender, EventArgs e)
        {
            var frmKhoaHocCuaToi = new frmKhoaHocCuaToi(_nguoiDungHienTai);
            frmKhoaHocCuaToi.Show();
            this.Hide();
        }

        private void btnDanhSachKhoaHoc_Click(object sender, EventArgs e)
        {
            var frmDanhSachKhoaHoc = new frmDanhSachKhoaHoc(_nguoiDungHienTai);
            frmDanhSachKhoaHoc.Show();
            this.Hide();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}