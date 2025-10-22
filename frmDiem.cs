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
    public partial class frmDiem : Form
    {
        private readonly string _maHocVien;
        private List<DangKyKhoaHoc> _danhSachKhoaHoc;

        public frmDiem(string maHocVien)
        {
            InitializeComponent();
            _maHocVien = maHocVien;

            // 🔥 SỬA FONT: Đặt font mặc định cho toàn bộ form
            this.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            // Kết nối sự kiện AfterSelect
            treeViewKhoaHoc.AfterSelect += treeViewKhoaHoc_AfterSelect;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _ = TaiDuLieuKhoaHoc(); // Sử dụng discard operator để tránh warning
        }

        private async Task TaiDuLieuKhoaHoc()
        {
            try
            {
                if (string.IsNullOrEmpty(_maHocVien))
                {
                    MessageBox.Show("Lỗi: Không có thông tin học viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var context = new Model1())
                {
                    var maHocVienGuid = Guid.Parse(_maHocVien);

                    // Load danh sách khóa học đã đăng ký
                    _danhSachKhoaHoc = await context.DangKyKhoaHocs
                        .Include(dk => dk.KhoaHoc)
                        .Include(dk => dk.KhoaHoc.NguoiDung)
                        .Include(dk => dk.KhoaHoc.ChuongHocs.Select(ch => ch.BaiHocs.Select(bh => bh.BaiTaps)))
                        .Where(dk => dk.MaHocVien == maHocVienGuid)
                        .OrderByDescending(dk => dk.NgayDangKy)
                        .ToListAsync();

                    HienThiDanhSachKhoaHoc();

                    if (_danhSachKhoaHoc.Any())
                    {
                        var khoaHocDauTien = _danhSachKhoaHoc.First();
                        await HienThiChiTietKhoaHoc(khoaHocDauTien);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiDanhSachKhoaHoc()
        {
            treeViewKhoaHoc.Nodes.Clear();

            foreach (var dangKy in _danhSachKhoaHoc)
            {
                var khoaHoc = dangKy.KhoaHoc;
                // 🔥 SỬA FONT: Dùng ký tự đơn giản thay vì emoji
                var nodeKhoaHoc = new TreeNode($"{khoaHoc.TieuDe} ({(dangKy.DaHoanThanh == true ? "Hoan thanh" : "Dang hoc")})");
                nodeKhoaHoc.Tag = dangKy;
                treeViewKhoaHoc.Nodes.Add(nodeKhoaHoc);
            }

            if (treeViewKhoaHoc.Nodes.Count > 0)
            {
                treeViewKhoaHoc.SelectedNode = treeViewKhoaHoc.Nodes[0];
            }
        }

        private async Task HienThiChiTietKhoaHoc(DangKyKhoaHoc dangKy)
        {
            try
            {
                var khoaHoc = dangKy.KhoaHoc;
                var giangVien = khoaHoc.NguoiDung;

                lblTenKhoaHoc.Text = khoaHoc.TieuDe;
                lblGiangVien.Text = $"{giangVien.Ho} {giangVien.Ten}";
                progressTienDo.Value = (int)(dangKy.PhanTramHoanThanh ?? 0);
                lblPhanTram.Text = $"{progressTienDo.Value}%";

                // Tính điểm số
                await TinhVaHienThiDiemSo(dangKy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết khóa học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task TinhVaHienThiDiemSo(DangKyKhoaHoc dangKy)
        {
            try
            {
                using (var context = new Model1())
                {
                    var maHocVienGuid = Guid.Parse(_maHocVien);
                    var maKhoaHoc = dangKy.MaKhoaHoc;

                    // Lấy danh sách mã bài tập trong khóa học
                    var danhSachMaBaiTap = await context.BaiTaps
                        .Where(bt => bt.BaiHoc.ChuongHoc.MaKhoaHoc == maKhoaHoc)
                        .Select(bt => bt.MaBaiTap)
                        .ToListAsync();

                    // Lấy bài đã nộp của học viên
                    var baiDaNop = await context.NopBaiTaps
                        .Where(nbt => nbt.MaHocVien == maHocVienGuid &&
                                     danhSachMaBaiTap.Contains(nbt.MaBaiTap))
                        .ToListAsync();

                    // Lấy toàn bộ danh sách bài tập để hiển thị
                    var danhSachBaiTap = await context.BaiTaps
                        .Where(bt => bt.BaiHoc.ChuongHoc.MaKhoaHoc == maKhoaHoc)
                        .ToListAsync();

                    // Tính điểm tiến trình (50%)
                    double diemTienTrinh = (double)(dangKy.PhanTramHoanThanh ?? 0) / 10.0;

                    // Tính điểm bài tập (50%)
                    double diemBaiTap = await TinhDiemBaiTapAsync(context, baiDaNop, danhSachBaiTap);

                    // Tính điểm tổng kết
                    double diemTongKet = (diemTienTrinh * 0.5) + (diemBaiTap * 0.5);

                    // Hiển thị điểm số trên UI thread
                    this.Invoke(new Action(() =>
                    {
                        lblDiemTienTrinh.Text = $"{diemTienTrinh:0.0}/10";
                        lblDiemBaiTap.Text = $"{diemBaiTap:0.0}/10";
                        lblDiemTongKet.Text = $"{diemTongKet:0.0}/10";

                        // 🔥 BỎ XẾP LOẠI: Xóa dòng hiển thị xếp loại
                        // lblXepLoai.Text = XepLoaiHocLuc(diemTongKet);

                        HienThiChiTietBaiTap(baiDaNop, danhSachBaiTap);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính điểm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<double> TinhDiemBaiTapAsync(Model1 context, List<NopBaiTap> baiDaNop, List<BaiTap> danhSachBaiTap)
        {
            if (!danhSachBaiTap.Any()) return 0;

            double tongDiem = 0;
            int soBaiDuocCham = 0;

            foreach (var baiNop in baiDaNop)
            {
                if (baiNop.TrangThai == 1 && baiNop.Diem.HasValue)
                {
                    // Lấy điểm tối đa từ database
                    var diemToiDa = await context.BaiTaps
                        .Where(bt => bt.MaBaiTap == baiNop.MaBaiTap)
                        .Select(bt => bt.DiemToiDa)
                        .FirstOrDefaultAsync();

                    if (diemToiDa > 0)
                    {
                        double diemChuan = (double)((baiNop.Diem.Value / diemToiDa) * 10);
                        tongDiem += diemChuan;
                        soBaiDuocCham++;
                    }
                }
            }

            if (soBaiDuocCham > 0)
            {
                return tongDiem / soBaiDuocCham;
            }

            return 0;
        }

        // 🔥 BỎ XẾP LOẠI: Comment hoặc xóa hàm xếp loại
        /*
        private string XepLoaiHocLuc(double diem)
        {
            if (diem >= 9.0) return "Xuất sắc";
            if (diem >= 8.0) return "Giỏi";
            if (diem >= 7.0) return "Khá";
            if (diem >= 5.5) return "Trung bình";
            return "Yếu";
        }
        */

        private void HienThiChiTietBaiTap(List<NopBaiTap> baiDaNop, List<BaiTap> danhSachBaiTap)
        {
            listViewBaiTap.Items.Clear();

            // 🔥 SỬA FONT: Đặt font cho ListView
            listViewBaiTap.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            foreach (var baiTap in danhSachBaiTap)
            {
                var baiNop = baiDaNop.FirstOrDefault(nbt => nbt.MaBaiTap == baiTap.MaBaiTap);

                var item = new ListViewItem(baiTap.TieuDe);

                if (baiNop != null)
                {
                    if (baiNop.TrangThai == 1 && baiNop.Diem.HasValue)
                    {
                        item.SubItems.Add($"{baiNop.Diem.Value:0.0}/{baiTap.DiemToiDa:0.0}");
                        item.SubItems.Add("Da cham");
                        item.ForeColor = Color.Green;
                    }
                    else
                    {
                        item.SubItems.Add("-");
                        item.SubItems.Add("Cho cham");
                        item.ForeColor = Color.Orange;
                    }
                }
                else
                {
                    item.SubItems.Add("-");
                    item.SubItems.Add("Chua nop");
                    item.ForeColor = Color.Red;
                }

                listViewBaiTap.Items.Add(item);
            }
        }

        private async void treeViewKhoaHoc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is DangKyKhoaHoc dangKy)
            {
                await HienThiChiTietKhoaHoc(dangKy);
            }
        }

        private void frmDiem_Load(object sender, EventArgs e)
        {
            // Có thể để trống
        }

        // 🔥 HÀM MỚI: Đặt font cho tất cả controls
        private void SetFontForAllControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                ctrl.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                // Đặt font đặc biệt cho các label tiêu đề
                if (ctrl.Name.StartsWith("lblTen") || ctrl.Name.StartsWith("lblDiem"))
                {
                    ctrl.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                }

                // Đệ quy cho các controls con
                if (ctrl.HasChildren)
                {
                    SetFontForAllControls(ctrl);
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // 🔥 Áp dụng font cho tất cả controls khi form hiển thị
            SetFontForAllControls(this);
        }
    }
}