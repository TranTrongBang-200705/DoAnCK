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
using DoAnCuoiKy.Models;

namespace DoAnCuoiKy
{
    public partial class frmDanhSachKhoaHoc : Form
    {
        private readonly NguoiDung _nguoiDungHienTai;
        private List<KhoaHoc> _danhSachKhoaHoc;
        private int _trangHienTai = 1;
        private const int _soKhoaHocMoiTrang = 3;

        public frmDanhSachKhoaHoc(NguoiDung nguoiDung)
        {
            InitializeComponent();
            _nguoiDungHienTai = nguoiDung;
            

            // Đặt font mặc định
            this.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        }

        private async void frmDanhSachKhoaHoc_Load(object sender, EventArgs e)
        {
            await TaiDanhMucFilter();
            TaiTrinhDoFilter();
            await TaiDanhSachKhoaHoc();
            await KiemTraTrangThaiDangKy();
        }

        private async Task TaiDanhMucFilter()
        {
            using (var context = new Model1())
            {
                try
                {
                    // Xóa items cũ trước khi thêm mới
                    cmbDanhMuc.Items.Clear();

                    // Lấy danh mục từ CSDL bằng Entity Framework - DISTINCT để loại bỏ trùng
                    var danhMucs = await context.DanhMucs
                        .Select(dm => dm.TenDanhMuc)
                        .Distinct()
                        .OrderBy(ten => ten)
                        .ToListAsync();

                    // Thêm item mặc định
                    cmbDanhMuc.Items.Add("Tất cả danh mục");

                    // Thêm danh mục từ CSDL (đã được DISTINCT)
                    foreach (var tenDanhMuc in danhMucs)
                    {
                        cmbDanhMuc.Items.Add(tenDanhMuc);
                    }

                    cmbDanhMuc.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tải danh mục: {ex.Message}");
                }
            }
        }

        private void TaiTrinhDoFilter()
        {
            cmbTrinhDo.Items.Clear();
            cmbTrinhDo.Items.Add("Tất cả trình độ");
            cmbTrinhDo.Items.Add("Cơ bản");
            cmbTrinhDo.Items.Add("Trung cấp");
            cmbTrinhDo.Items.Add("Nâng cao");
            cmbTrinhDo.SelectedIndex = 0;
        }

        private async Task TaiDanhSachKhoaHoc(string timKiem = "", string danhMuc = "", string trinhDo = "")
        {
            using (var context = new Model1())
            {
                try
                {
                    var query = context.KhoaHocs
                        .Include(k => k.DanhMuc)
                        .Include(k => k.NguoiDung)
                        .Where(k => k.TrangThai == 1);

                    if (!string.IsNullOrEmpty(timKiem))
                    {
                        query = query.Where(k => k.TieuDe.Contains(timKiem) || k.MoTa.Contains(timKiem));
                    }

                    if (!string.IsNullOrEmpty(danhMuc) && danhMuc != "Tất cả danh mục")
                    {
                        query = query.Where(k => k.DanhMuc.TenDanhMuc == danhMuc);
                    }

                    if (!string.IsNullOrEmpty(trinhDo) && trinhDo != "Tất cả trình độ")
                    {
                        int trinhDoValue = trinhDo switch
                        {
                            "Cơ bản" => 0,
                            "Trung cấp" => 1,
                            "Nâng cao" => 2,
                            _ => -1
                        };
                        if (trinhDoValue != -1)
                        {
                            query = query.Where(k => k.TrinhDo == trinhDoValue);
                        }
                    }

                    var khoaHocs = await query
                        .OrderByDescending(k => k.NgayTao)
                        .ToListAsync();

                    _danhSachKhoaHoc = khoaHocs;
                    _trangHienTai = 1;

                    HienThiDuLieuLenPanel(khoaHocs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}");
                }
            }
        }

        private void HienThiDuLieuLenPanel(List<KhoaHoc> khoaHocs)
        {
            // Ẩn tất cả panel trước
            panelKhoaHoc1.Visible = false;
            panelKhoaHoc2.Visible = false;
            panelKhoaHoc3.Visible = false;

            if (khoaHocs == null || !khoaHocs.Any())
            {
                lblThongBao.Text = "Không tìm thấy khóa học nào";
                CapNhatPhanTrang(0);
                return;
            }

            // TÍNH TOÁN PHÂN TRANG
            int viTriBatDau = (_trangHienTai - 1) * _soKhoaHocMoiTrang;
            var khoaHocTrangHienTai = khoaHocs
                .Skip(viTriBatDau)
                .Take(_soKhoaHocMoiTrang)
                .ToList();

            // Hiển thị dữ liệu lên từng panel có sẵn
            for (int i = 0; i < Math.Min(khoaHocTrangHienTai.Count, 3); i++)
            {
                var khoaHoc = khoaHocTrangHienTai[i];

                switch (i)
                {
                    case 0:
                        HienThiKhoaHocLenPanel(panelKhoaHoc1, khoaHoc);
                        break;
                    case 1:
                        HienThiKhoaHocLenPanel(panelKhoaHoc2, khoaHoc);
                        break;
                    case 2:
                        HienThiKhoaHocLenPanel(panelKhoaHoc3, khoaHoc);
                        break;
                }
            }

            lblThongBao.Text = $"Tìm thấy {khoaHocs.Count} khóa học - Trang {_trangHienTai}";
            CapNhatPhanTrang(khoaHocs.Count);
        }

        private void CapNhatPhanTrang(int tongSoKhoaHoc)
        {
            if (tongSoKhoaHoc == 0)
            {
                lblTrangHienTai.Text = "Trang 0/0";
                btnTruoc.Enabled = false;
                btnSau.Enabled = false;
                return;
            }

            int tongTrang = (int)Math.Ceiling(tongSoKhoaHoc / (double)_soKhoaHocMoiTrang);
            lblTrangHienTai.Text = $"Trang {_trangHienTai}/{tongTrang}";

            btnTruoc.Enabled = _trangHienTai > 1;
            btnSau.Enabled = _trangHienTai < tongTrang;
        }

        private void HienThiKhoaHocLenPanel(Panel panel, KhoaHoc khoaHoc)
        {
            panel.Visible = true;

            // Tìm các controls trong panel và gán dữ liệu
            foreach (Control control in panel.Controls)
            {
                // Đặt font cho tất cả controls
                control.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                switch (control.Name)
                {
                    case "lblTenKH1":
                    case "lblTenKH2":
                    case "lblTenKH3":
                        control.Text = khoaHoc.TieuDe;
                        control.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                        break;
                    case "lblMoTa1":
                    case "lblMoTa2":
                    case "lblMoTa3":
                        control.Text = khoaHoc.MoTa?.Length > 100 ?
                            khoaHoc.MoTa.Substring(0, 100) + "..." : khoaHoc.MoTa;
                        break;
                    case "lblGiangVien1":
                    case "lblGiangVien2":
                    case "lblGiangVien3":
                        control.Text = $"Giảng viên: {khoaHoc.NguoiDung?.Ho} {khoaHoc.NguoiDung?.Ten}";
                        break;
                    case "lblThongTin1":
                    case "lblThongTin2":
                    case "lblThongTin3":
                        control.Visible = false;
                        break;
                    case "lblGia1":
                    case "lblGia2":
                    case "lblGia3":
                        control.Visible = false;
                        break;
                    case "btnDangKy1":
                    case "btnDangKy2":
                    case "btnDangKy3":
                        var button = (Button)control;
                        button.Tag = khoaHoc.MaKhoaHoc;

                        // QUAN TRỌNG: Xóa tất cả sự kiện cũ trước khi thêm mới
                        button.Click -= BtnDangKy_Click;
                        button.Click -= btnDangKy1_Click;
                        button.Click -= btnDangKy2_Click;
                        button.Click -= btnDangKy3_Click;

                        // Chỉ thêm MỘT sự kiện duy nhất
                        button.Click += BtnDangKy_Click;

                        button.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
                        break;
                }
            }
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            // Xử lý đăng ký chung
            var button = (Button)sender;
            var maKhoaHoc = (Guid)button.Tag;
            XuLyDangKy(maKhoaHoc, button);
        }

        private async void XuLyDangKy(Guid maKhoaHoc, Button button)
        {
            using (var context = new Model1())
            {
                try
                {
                    // KIỂM TRA KỸ ĐĂNG KÝ TRÙNG TRƯỚC KHI THỰC HIỆN
                    var daDangKy = await context.DangKyKhoaHocs
                        .AnyAsync(dk => dk.MaHocVien == _nguoiDungHienTai.MaNguoiDung && dk.MaKhoaHoc == maKhoaHoc);

                    if (daDangKy)
                    {
                        MessageBox.Show("Bạn đã đăng ký khóa học này rồi!", "Thông báo");

                        // Cập nhật UI ngay lập tức
                        button.Enabled = true;
                        button.Text = "Đăng Ký";
                      
                        return;
                    }

                    // Kiểm tra khóa học có tồn tại không
                    var khoaHoc = await context.KhoaHocs
                        .FirstOrDefaultAsync(kh => kh.MaKhoaHoc == maKhoaHoc && kh.TrangThai == 1);

                    if (khoaHoc == null)
                    {
                        MessageBox.Show("Khóa học không tồn tại hoặc đã bị xóa!", "Lỗi");
                        return;
                    }

                    // Kiểm tra học viên có tồn tại không
                    var hocVien = await context.NguoiDungs
                        .FirstOrDefaultAsync(nd => nd.MaNguoiDung == _nguoiDungHienTai.MaNguoiDung);

                    if (hocVien == null)
                    {
                        MessageBox.Show("Thông tin học viên không hợp lệ!", "Lỗi");
                        return;
                    }

                    // Tạo đăng ký mới
                    var dangKy = new DangKyKhoaHoc
                    {
                        MaDangKy = Guid.NewGuid(),
                        MaHocVien = _nguoiDungHienTai.MaNguoiDung,
                        MaKhoaHoc = maKhoaHoc,
                        NgayDangKy = DateTime.Now,
                        PhanTramHoanThanh = 0,
                        DaHoanThanh = false
                    };

                    context.DangKyKhoaHocs.Add(dangKy);
                    await context.SaveChangesAsync();

                    MessageBox.Show($"Đăng ký khóa học '{khoaHoc.TieuDe}' thành công!", "Thành công");

                    // Cập nhật UI
                    button.Enabled = false;
                    button.Text = "Đã đăng ký";
                    button.BackColor = Color.Gray;
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
                {
                    // Xử lý lỗi UNIQUE CONSTRAINT cụ thể
                    var innerException = dbEx.InnerException?.InnerException ?? dbEx.InnerException ?? dbEx;

                    if (innerException.Message.Contains("UNIQUE KEY constraint") && innerException.Message.Contains("UQ_DangKyKh"))
                    {
                        MessageBox.Show("Bạn đã đăng ký khóa học này rồi!", "Thông báo");

                        // Cập nhật UI ngay lập tức
                        button.Enabled = false;
                        button.Text = "Đã đăng ký";
                        button.BackColor = Color.Gray;
                        return;
                    }

                    // Xử lý các lỗi khác
                    string errorMessage = "Lỗi đăng ký khóa học:\n";

                    if (innerException.Message.Contains("foreign key"))
                    {
                        if (innerException.Message.Contains("MaHocVien"))
                        {
                            errorMessage += "- Học viên không tồn tại\n";
                        }
                        else if (innerException.Message.Contains("MaKhoaHoc"))
                        {
                            errorMessage += "- Khóa học không tồn tại\n";
                        }
                        else
                        {
                            errorMessage += "- Lỗi khóa ngoại với database\n";
                        }
                    }
                    else if (innerException.Message.Contains("PRIMARY KEY"))
                    {
                        errorMessage += "- Lỗi trùng mã đăng ký\n";
                    }
                    else
                    {
                        errorMessage += $"- {innerException.Message}\n";
                    }

                    MessageBox.Show(errorMessage, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi đăng ký: {ex.Message}", "Lỗi");
                }
            }
        }
        private async Task KiemTraTrangThaiDangKy()
        {
            using (var context = new Model1())
            {
                try
                {
                    // Lấy tất cả các khóa học đã đăng ký bởi học viên hiện tại
                    var khoaHocDaDangKy = await context.DangKyKhoaHocs
                        .Where(dk => dk.MaHocVien == _nguoiDungHienTai.MaNguoiDung)
                        .Select(dk => dk.MaKhoaHoc)
                        .ToListAsync();

                    // Cập nhật UI cho tất cả các nút đăng ký
                    CapNhatTrangThaiNutDangKy(khoaHocDaDangKy);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi kiểm tra trạng thái đăng ký: {ex.Message}");
                }
            }
        }

        private void CapNhatTrangThaiNutDangKy(List<Guid> khoaHocDaDangKy)
        {
            // Duyệt qua tất cả các panel và cập nhật nút
            var panels = new[] { panelKhoaHoc1, panelKhoaHoc2, panelKhoaHoc3 };

            foreach (var panel in panels)
            {
                if (panel.Visible)
                {
                    var button = panel.Controls.OfType<Button>()
                        .FirstOrDefault(b => b.Name.StartsWith("btnDangKy"));

                    if (button != null && button.Tag != null && button.Tag is Guid maKhoaHoc)
                    {
                        if (khoaHocDaDangKy.Contains(maKhoaHoc))
                        {

                            button.Enabled = true;
                            button.Text = "Đăng ký";
                            button.BackColor = SystemColors.Control;

                        }
                        else
                        {
                            // ĐẢM BẢO NÚT ĐƯỢC BẬT LẠI NẾU CHƯA ĐĂNG KÝ
                            button.Enabled = true;
                            button.Text = "Đăng ký";
                            button.BackColor = SystemColors.Control; // Màu mặc định
                        }
                    }
                }
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string timKiem = txtTimKiem.Text.Trim();
            string danhMuc = cmbDanhMuc.SelectedItem?.ToString() ?? "";
            string trinhDo = cmbTrinhDo.SelectedItem?.ToString() ?? "";

            TaiDanhSachKhoaHoc(timKiem, danhMuc, trinhDo);
        }

        // Gộp 3 hàm đăng ký thành 1 hàm chung
        private void btnDangKy1_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var maKhoaHoc = (Guid)button.Tag;
            XuLyDangKy(maKhoaHoc, button);
        }

        private void btnDangKy2_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var maKhoaHoc = (Guid)button.Tag;
            XuLyDangKy(maKhoaHoc, button);
        }

        private void btnDangKy3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var maKhoaHoc = (Guid)button.Tag;
            XuLyDangKy(maKhoaHoc, button);
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            if (_trangHienTai > 1)
            {
                _trangHienTai--;
                HienThiDuLieuLenPanel(_danhSachKhoaHoc);
            }
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            int tongTrang = (int)Math.Ceiling(_danhSachKhoaHoc.Count / (double)_soKhoaHocMoiTrang);
            if (_trangHienTai < tongTrang)
            {
                _trangHienTai++;
                HienThiDuLieuLenPanel(_danhSachKhoaHoc);
            }
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnTimKiem_Click_1(sender, e);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}