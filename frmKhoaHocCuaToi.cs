using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmKhoaHocCuaToi : Form
    {
        private readonly NguoiDung _nguoiDungHienTai;
        private List<DangKyKhoaHoc> _danhSachKhoaHoc;
        private int _trangHienTai = 1;
        private const int _soKhoaHocMoiTrang = 3;

        public frmKhoaHocCuaToi(NguoiDung nguoiDung)
        {
            InitializeComponent();
            _nguoiDungHienTai = nguoiDung;

            // 🔥 SỬA FONT: Đặt font mặc định cho toàn bộ form
            this.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        }

        private async void frmKhoaHocCuaToi_Load(object sender, EventArgs e)
        {
            await TaiDuLieuLoc();
            await TaiDanhSachKhoaHocCuaToi();
        }

        private Task TaiDuLieuLoc()
        {
            // Thêm các tùy chọn lọc
            cmbLocTheo.Items.Add("Tất cả");
            cmbLocTheo.Items.Add("Đang học");
            cmbLocTheo.Items.Add("Đã hoàn thành");
            cmbLocTheo.SelectedIndex = 0;

            return Task.CompletedTask;
        }

        private async Task TaiDanhSachKhoaHocCuaToi(string timKiem = "", string locTheo = "")
        {
            using (var context = new Model1())
            {
                try
                {
                    // Entity Framework: Lấy khóa học của học viên
                    var query = context.DangKyKhoaHocs
                        .Include(dk => dk.KhoaHoc)
                        .Include(dk => dk.KhoaHoc.NguoiDung) // Giảng viên
                        .Where(dk => dk.MaHocVien == _nguoiDungHienTai.MaNguoiDung);

                    // Filter theo tìm kiếm
                    if (!string.IsNullOrEmpty(timKiem))
                    {
                        query = query.Where(dk => dk.KhoaHoc.TieuDe.Contains(timKiem));
                    }

                    // Filter theo trạng thái
                    if (!string.IsNullOrEmpty(locTheo) && locTheo != "Tất cả")
                    {
                        if (locTheo == "Đang học")
                        {
                            query = query.Where(dk => dk.DaHoanThanh == false);
                        }
                        else if (locTheo == "Đã hoàn thành")
                        {
                            query = query.Where(dk => dk.DaHoanThanh == true);
                        }
                    }

                    _danhSachKhoaHoc = await query
                        .OrderByDescending(dk => dk.NgayTruyCapCuoi)
                        .ThenByDescending(dk => dk.NgayDangKy)
                        .ToListAsync();

                    _trangHienTai = 1;
                    HienThiDanhSachKhoaHoc();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}");
                }
            }
        }

        private void HienThiDanhSachKhoaHoc()
        {
            // Ẩn tất cả panel trước
            panelKH1.Visible = false;
            panelKH2.Visible = false;
            panelKH3.Visible = false;

            if (_danhSachKhoaHoc == null || !_danhSachKhoaHoc.Any())
            {
                // Hiển thị thông báo nếu không có khóa học
                lblTenKH1.Text = "Bạn chưa đăng ký khóa học nào";
                panelKH1.Visible = true;
                CapNhatPhanTrang();
                return;
            }

            // TÍNH TOÁN PHÂN TRANG - QUAN TRỌNG
            int viTriBatDau = (_trangHienTai - 1) * _soKhoaHocMoiTrang;
            var khoaHocTrangHienTai = _danhSachKhoaHoc
                .Skip(viTriBatDau)
                .Take(_soKhoaHocMoiTrang)
                .ToList();

            // Hiển thị tối đa 3 khóa học của TRANG HIỆN TẠI
            for (int i = 0; i < khoaHocTrangHienTai.Count; i++)
            {
                var dangKy = khoaHocTrangHienTai[i];
                HienThiKhoaHoc(i, dangKy);
            }

            CapNhatPhanTrang();
        }

        private void CapNhatPhanTrang()
        {
            if (_danhSachKhoaHoc == null || !_danhSachKhoaHoc.Any())
            {
                lblTrangHienTai.Text = "Trang 0/0";
                btnTruoc.Enabled = false;
                btnSau.Enabled = false;
                return;
            }

            int tongTrang = (int)Math.Ceiling(_danhSachKhoaHoc.Count / (double)_soKhoaHocMoiTrang);
            lblTrangHienTai.Text = $"Trang {_trangHienTai}/{tongTrang}";

            btnTruoc.Enabled = _trangHienTai > 1;
            btnSau.Enabled = _trangHienTai < tongTrang;
        }

        private void HienThiKhoaHoc(int index, DangKyKhoaHoc dangKy)
        {
            var khoaHoc = dangKy.KhoaHoc;
            var giangVien = khoaHoc.NguoiDung;

            // Xử lý ngày cập nhật
            string hienThiNgay;
            if (dangKy.DaHoanThanh == true && dangKy.NgayHoanThanh.HasValue)
            {
                hienThiNgay = $"Hoàn thành: {dangKy.NgayHoanThanh.Value:dd/MM/yyyy}";
            }
            else if (dangKy.NgayTruyCapCuoi.HasValue)
            {
                hienThiNgay = $"Cập nhật: {dangKy.NgayTruyCapCuoi.Value:dd/MM/yyyy}";
            }
            else if (dangKy.NgayDangKy.HasValue)
            {
                hienThiNgay = $"Đăng ký: {dangKy.NgayDangKy.Value:dd/MM/yyyy}";
            }
            else
            {
                hienThiNgay = "Chưa học";
            }

            // Xác định trạng thái button
            string textVaoHoc, textBaiTap;
            if (dangKy.DaHoanThanh == true)
            {
                textVaoHoc = "Ôn tập";
                textBaiTap = "Bài Tập";
            }
            else
            {
                textVaoHoc = "Vào học";
                textBaiTap = "Bài tập";
            }

            switch (index)
            {
                case 0:
                    // Panel 1
                    panelKH1.Visible = true;
                    lblTenKH1.Text = khoaHoc.TieuDe?.ToUpper();
                    lblTrangThai1.Text = dangKy.DaHoanThanh == true ?
                       "Trạng thái: Đã hoàn thành • 100% hoàn thành" :
                       $"Trạng thái: Đang học • {dangKy.PhanTramHoanThanh:0}% hoàn thành";

                    progressBar1.Value = (int)(dangKy.PhanTramHoanThanh ?? 0);
                    lblPhanTram1.Text = $"{progressBar1.Value}%";

                    lblGiangVien1.Text = $"Giảng viên: {giangVien?.Ho} {giangVien?.Ten}";
                    lblNgayCapNhat1.Text = hienThiNgay;

                    // Cập nhật button
                    btnVaoHoc1.Tag = dangKy.MaKhoaHoc;
                    btnBaiTap1.Tag = dangKy.MaKhoaHoc;
                    btnVaoHoc1.Text = textVaoHoc;
                    btnBaiTap1.Text = textBaiTap;

                    // 🔥 SỬA FONT: Đặt font cho các controls trong panel 1
                    SetFontForPanelControls(panelKH1);
                    break;

                case 1:
                    // Panel 2
                    panelKH2.Visible = true;
                    lblTenKH2.Text = khoaHoc.TieuDe?.ToUpper();
                    lblTrangThai2.Text = dangKy.DaHoanThanh == true ?
                        "Trạng thái: Đã hoàn thành • 100% hoàn thành" :
                        $"Trạng thái: Đang học • {dangKy.PhanTramHoanThanh:0}% hoàn thành";

                    progressBar2.Value = (int)(dangKy.PhanTramHoanThanh ?? 0);
                    lblPhanTram2.Text = $"{progressBar2.Value}%";

                    lblGiangVien2.Text = $"Giảng viên: {giangVien?.Ho} {giangVien?.Ten}";
                    lblNgayCapNhat2.Text = hienThiNgay;

                    // Cập nhật button
                    btnVaoHoc2.Tag = dangKy.MaKhoaHoc;
                    btnBaiTap2.Tag = dangKy.MaKhoaHoc;
                    btnVaoHoc2.Text = textVaoHoc;
                    btnBaiTap2.Text = textBaiTap;

                    // 🔥 SỬA FONT: Đặt font cho các controls trong panel 2
                    SetFontForPanelControls(panelKH2);
                    break;

                case 2:
                    // Panel 3
                    panelKH3.Visible = true;
                    lblTenKH3.Text = khoaHoc.TieuDe?.ToUpper();
                    lblTrangThai3.Text = dangKy.DaHoanThanh == true ?
                        "Trạng thái: Đã hoàn thành • 100% hoàn thành" :
                        $"Trạng thái: Đang học • {dangKy.PhanTramHoanThanh:0}% hoàn thành";

                    progressBar3.Value = (int)(dangKy.PhanTramHoanThanh ?? 0);
                    lblPhanTram3.Text = $"{progressBar3.Value}%";

                    lblGiangVien3.Text = $"Giảng viên: {giangVien?.Ho} {giangVien?.Ten}";
                    lblNgayCapNhat3.Text = hienThiNgay;

                    // Cập nhật button
                    btnVaoHoc3.Tag = dangKy.MaKhoaHoc;
                    btnBaiTap3.Tag = dangKy.MaKhoaHoc;
                    btnVaoHoc3.Text = textVaoHoc;
                    btnBaiTap3.Text = textBaiTap;

                    // 🔥 SỬA FONT: Đặt font cho các controls trong panel 3
                    SetFontForPanelControls(panelKH3);
                    break;
            }
        }

        // 🔥 HÀM MỚI: Đặt font cho tất cả controls trong panel
        private void SetFontForPanelControls(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                control.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                // Đặt font đặc biệt cho các label tiêu đề
                if (control.Name.StartsWith("lblTenKH"))
                {
                    control.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                }

                // Đặt font cho các button
                if (control is Button button)
                {
                    button.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnTimKiem_Click(sender, e);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = txtTimKiem.Text.Trim();
            string locTheo = cmbLocTheo.SelectedItem?.ToString() ?? "";

            TaiDanhSachKhoaHocCuaToi(timKiem, locTheo);
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            if (_trangHienTai > 1)
            {
                _trangHienTai--;
                HienThiDanhSachKhoaHoc();
            }
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            int tongTrang = (int)Math.Ceiling(_danhSachKhoaHoc.Count / (double)_soKhoaHocMoiTrang);
            if (_trangHienTai < tongTrang)
            {
                _trangHienTai++;
                HienThiDanhSachKhoaHoc();
            }
        }

        private void btnVaoHoc1_Click(object sender, EventArgs e)
        {
            if (btnVaoHoc1.Tag != null)
            {
                var maKhoaHoc = btnVaoHoc1.Tag.ToString();
                var frmBaiHoc = new frmBaiHoc(maKhoaHoc, _nguoiDungHienTai.MaNguoiDung.ToString());
                frmBaiHoc.Show();
            }
        }

        private void btnVaoHoc2_Click(object sender, EventArgs e)
        {
            if (btnVaoHoc2.Tag != null)
            {
                var maKhoaHoc = btnVaoHoc2.Tag.ToString();
                var frmBaiHoc = new frmBaiHoc(maKhoaHoc, _nguoiDungHienTai.MaNguoiDung.ToString());
                frmBaiHoc.Show();
            }
        }

        private void btnVaoHoc3_Click(object sender, EventArgs e)
        {
            if (btnVaoHoc3.Tag != null)
            {
                var maKhoaHoc = btnVaoHoc3.Tag.ToString();
                var frmBaiHoc = new frmBaiHoc(maKhoaHoc, _nguoiDungHienTai.MaNguoiDung.ToString());
                frmBaiHoc.Show();
            }
        }

        private void btnBaiTap1_Click(object sender, EventArgs e)
        {
            if (btnBaiTap1.Tag != null)
            {
                var maKhoaHoc = btnBaiTap1.Tag.ToString();
                var frmBaiTap = new frmBaiTap(maKhoaHoc, _nguoiDungHienTai.MaNguoiDung.ToString());
                frmBaiTap.Show();
            }
        }

        private void btnBaiTap2_Click(object sender, EventArgs e)
        {
            if (btnBaiTap2.Tag != null)
            {
                var maKhoaHoc = btnBaiTap2.Tag.ToString();
                var frmBaiTap = new frmBaiTap(maKhoaHoc, _nguoiDungHienTai.MaNguoiDung.ToString());
                frmBaiTap.Show();
            }
        }

        private void btnBaiTap3_Click(object sender, EventArgs e)
        {
            if (btnBaiTap3.Tag != null)
            {
                var maKhoaHoc = btnBaiTap3.Tag.ToString();
                var frmBaiTap = new frmBaiTap(maKhoaHoc, _nguoiDungHienTai.MaNguoiDung.ToString());
                frmBaiTap.Show();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Không cần xử lý
        }
    }
}