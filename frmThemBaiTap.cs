using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmThemBaiTap : Form
    {
        private readonly Model1 _context;
        private List<BaiTap> _danhSachBaiTap;
        private List<KhoaHoc> _danhSachKhoaHoc;
        private BaiTap _baiTapDangChon;

        public frmThemBaiTap(Model1 context)
        {

            InitializeComponent();
            _context = context;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void frmThemBaiTap_Load(object sender, EventArgs e)
        {
            await TaiDuLieuKhoiDong();
            await TaiDanhSachBaiTap();
        }

        private async Task TaiDuLieuKhoiDong()
        {
            try
            {
                // Tải danh sách khóa học vào ComboBox
                _danhSachKhoaHoc = await _context.KhoaHocs
                    .OrderBy(kh => kh.TieuDe)
                    .ToListAsync();

                cmbKhoaHoc.DataSource = _danhSachKhoaHoc;
                cmbKhoaHoc.DisplayMember = "TieuDe";
                cmbKhoaHoc.ValueMember = "MaKhoaHoc";

                // Load chương học khi chọn khóa học
                cmbKhoaHoc.SelectedIndexChanged += async (s, e) => { await TaiChuongHocTheoKhoaHoc(); };

                // Load chương học cho khóa học đầu tiên
                if (_danhSachKhoaHoc.Any())
                {
                    await TaiChuongHocTheoKhoaHoc();
                }

                // ... phần còn lại giữ nguyên
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        private async Task TaiChuongHocTheoKhoaHoc()
        {
            if (cmbKhoaHoc.SelectedValue == null) return;

            try
            {
                var maKhoaHoc = (Guid)cmbKhoaHoc.SelectedValue;

                var danhSachChuongHoc = await _context.ChuongHocs
                    .Where(ch => ch.MaKhoaHoc == maKhoaHoc)
                    .OrderBy(ch => ch.ThuTu)
                    .ToListAsync();

                cmbChuongHoc.DataSource = danhSachChuongHoc;
                cmbChuongHoc.DisplayMember = "TieuDeChuong";
                cmbChuongHoc.ValueMember = "MaChuong";

                // Nếu không có chương học, clear combobox
                if (!danhSachChuongHoc.Any())
                {
                    cmbChuongHoc.DataSource = null;
                    cmbChuongHoc.Items.Clear();
                    cmbChuongHoc.Text = "Không có chương học";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chương học: {ex.Message}", "Lỗi");
                cmbChuongHoc.DataSource = null;
                cmbChuongHoc.Items.Clear();
                cmbChuongHoc.Text = "Lỗi tải dữ liệu";
            }
        }

        private async Task TaiDanhSachBaiTap()
        {
            try
            {
                _danhSachBaiTap = await _context.BaiTaps
                    .Include(bt => bt.BaiHoc.ChuongHoc.KhoaHoc)
                    .OrderByDescending(bt => bt.NgayTao)
                    .ToListAsync();

                HienThiDanhSachBaiTap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách bài tập: {ex.Message}", "Lỗi");
            }
        }

        private void HienThiDanhSachBaiTap()
        {
            dgvBaiTap.Rows.Clear();

            foreach (var baiTap in _danhSachBaiTap)
            {
                dgvBaiTap.Rows.Add(
                    baiTap.MaBaiTap.ToString(),
                    baiTap.BaiHoc?.ChuongHoc?.KhoaHoc?.TieuDe,
                    baiTap.BaiHoc?.ChuongHoc?.TieuDeChuong,
                    baiTap.TieuDe,
                    baiTap.HanNop?.ToString("dd/MM/yyyy")
                );
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemBaiTapMoi();
        }

        private async void ThemBaiTapMoi()
        {
            if (!KiemTraDuLieuHopLe()) return;

            try
            {
                // Kiểm tra mã bài tập đã tồn tại chưa
                Guid maBaiTap;
                if (!string.IsNullOrWhiteSpace(txtMaBaiTap.Text) && Guid.TryParse(txtMaBaiTap.Text, out maBaiTap))
                {
                    var baiTapTonTai = await _context.BaiTaps
                        .FirstOrDefaultAsync(bt => bt.MaBaiTap == maBaiTap);

                    if (baiTapTonTai != null)
                    {
                        MessageBox.Show(
                            "Mã bài tập đã tồn tại! Vui lòng sử dụng mã khác hoặc để trống để tự động tạo mã mới.",
                            "Lỗi");
                        return;
                    }
                }

                // Lấy bài học đầu tiên của chương học được chọn
                var maChuong = (Guid)cmbChuongHoc.SelectedValue;
                var baiHocDauTien = await _context.BaiHocs
                    .Where(bh => bh.MaChuong == maChuong)
                    .OrderBy(bh => bh.ThuTu)
                    .FirstOrDefaultAsync();

                if (baiHocDauTien == null)
                {
                    MessageBox.Show("Chương học này không có bài học nào!", "Lỗi");
                    return;
                }

                // Tạo mã bài tập mới nếu người dùng không nhập
                Guid maBaiTapMoi;
                if (string.IsNullOrWhiteSpace(txtMaBaiTap.Text))
                {
                    maBaiTapMoi = Guid.NewGuid();
                }
                else
                {
                    maBaiTapMoi = Guid.Parse(txtMaBaiTap.Text);
                }

                var baiTap = new BaiTap
                {
                    MaBaiTap = maBaiTapMoi,
                    MaBaiHoc = baiHocDauTien.MaBaiHoc, // Sử dụng bài học đầu tiên của chương
                    TieuDe = txtTieuDe.Text.Trim(),
                    NoiDung = txtNoiDung.Text.Trim(),
                    HanNop = dtpHanNop.Value,
                    DiemToiDa = numDiemToiDa.Value,
                    NgayTao = DateTime.Now
                };

                _context.BaiTaps.Add(baiTap);
                await _context.SaveChangesAsync();

                // Hiển thị mã bài tập vừa tạo
                txtMaBaiTap.Text = baiTap.MaBaiTap.ToString();

                MessageBox.Show("Thêm bài tập thành công!", "Thành công");
                await TaiDanhSachBaiTap();
                XoaForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm bài tập: {ex.Message}", "Lỗi");
            }
        }

        private bool KiemTraDuLieuHopLe()
        {
            if (cmbKhoaHoc.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học", "Lỗi");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTieuDe.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề bài tập", "Lỗi");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung bài tập", "Lỗi");
                return false;
            }

            if (dtpHanNop.Value < DateTime.Now)
            {
                MessageBox.Show("Hạn nộp phải lớn hơn thời gian hiện tại", "Lỗi");
                return false;
            }

            return true;
        }

        private void XoaForm()
        {
            txtMaBaiTap.Clear();
            txtTieuDe.Clear();
            txtNoiDung.Clear();
            numDiemToiDa.Value = 10;
            dtpHanNop.Value = DateTime.Now.AddDays(7);
            _baiTapDangChon = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThemBaiTapMoi();
        }

        private async void SuaBaiTap()
        {
            if (string.IsNullOrWhiteSpace(txtMaBaiTap.Text))
            {
                MessageBox.Show("Vui lòng nhập mã bài tập cần sửa", "Thông báo");
                return;
            }

            if (!KiemTraDuLieuHopLe()) return;

            try
            {
                // Tìm bài tập theo mã
                Guid maBaiTap;
                if (!Guid.TryParse(txtMaBaiTap.Text, out maBaiTap))
                {
                    MessageBox.Show("Mã bài tập không hợp lệ", "Lỗi");
                    return;
                }

                var baiTap = await _context.BaiTaps
                    .FirstOrDefaultAsync(bt => bt.MaBaiTap == maBaiTap);

                if (baiTap == null)
                {
                    MessageBox.Show("Không tìm thấy bài tập với mã này", "Lỗi");
                    return;
                }

                // Lấy bài học đầu tiên của chương học được chọn
                var maChuong = (Guid)cmbChuongHoc.SelectedValue;
                var baiHocDauTien = await _context.BaiHocs
                    .Where(bh => bh.MaChuong == maChuong)
                    .OrderBy(bh => bh.ThuTu)
                    .FirstOrDefaultAsync();

                if (baiHocDauTien == null)
                {
                    MessageBox.Show("Chương học này không có bài học nào!", "Lỗi");
                    return;
                }

                // Cập nhật thông tin bài tập
                baiTap.MaBaiHoc = baiHocDauTien.MaBaiHoc;
                baiTap.TieuDe = txtTieuDe.Text.Trim();
                baiTap.NoiDung = txtNoiDung.Text.Trim();
                baiTap.HanNop = dtpHanNop.Value;
                baiTap.DiemToiDa = numDiemToiDa.Value;

                await _context.SaveChangesAsync();

                MessageBox.Show("Sửa bài tập thành công!", "Thành công");
                await TaiDanhSachBaiTap();
                XoaForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa bài tập: {ex.Message}", "Lỗi");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SuaBaiTap();
        }

        private void dgvBaiTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBaiTap.Rows.Count)
            {
                // Lấy mã bài tập từ dòng được chọn
                var maBaiTapCell = dgvBaiTap.Rows[e.RowIndex].Cells[0].Value;
                if (maBaiTapCell != null)
                {
                    // Hiển thị mã bài tập lên txtMaBaiTap
                    txtMaBaiTap.Text = maBaiTapCell.ToString();

                    // Tự động tải thông tin bài tập lên form
                    TaiThongTinBaiTapTheoMa(txtMaBaiTap.Text);
                }
            }
        }

        private async void XoaBaiTap()
        {
            if (string.IsNullOrWhiteSpace(txtMaBaiTap.Text))
            {
                MessageBox.Show("Vui lòng nhập mã bài tập cần xóa", "Thông báo");
                return;
            }

            try
            {
                // Tìm bài tập theo mã
                Guid maBaiTap;
                if (!Guid.TryParse(txtMaBaiTap.Text, out maBaiTap))
                {
                    MessageBox.Show("Mã bài tập không hợp lệ", "Lỗi");
                    return;
                }

                var baiTap = await _context.BaiTaps
                    .FirstOrDefaultAsync(bt => bt.MaBaiTap == maBaiTap);

                if (baiTap == null)
                {
                    MessageBox.Show("Không tìm thấy bài tập với mã này", "Lỗi");
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa bài tập '{baiTap.TieuDe}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    _context.BaiTaps.Remove(baiTap);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Xóa bài tập thành công!", "Thành công");
                    await TaiDanhSachBaiTap();
                    XoaForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa bài tập: {ex.Message}", "Lỗi");
            }
        }

        private async void TaiThongTinBaiTapTheoMa(string maBaiTap)
        {
            try
            {
                Guid maBaiTapGuid;
                if (!Guid.TryParse(maBaiTap, out maBaiTapGuid)) return;

                var baiTap = await _context.BaiTaps
                    .Include(bt => bt.BaiHoc.ChuongHoc.KhoaHoc)
                    .FirstOrDefaultAsync(bt => bt.MaBaiTap == maBaiTapGuid);

                if (baiTap != null)
                {
                    // Hiển thị thông tin lên form
                    txtTieuDe.Text = baiTap.TieuDe;
                    txtNoiDung.Text = baiTap.NoiDung;
                    numDiemToiDa.Value = (decimal)baiTap.DiemToiDa;
                    dtpHanNop.Value = baiTap.HanNop ?? DateTime.Now.AddDays(7);

                    // Chọn khóa học tương ứng
                    var khoaHoc = _danhSachKhoaHoc.FirstOrDefault(kh =>
                        kh.MaKhoaHoc == baiTap.BaiHoc.ChuongHoc.MaKhoaHoc);
                    if (khoaHoc != null)
                    {
                        cmbKhoaHoc.SelectedValue = khoaHoc.MaKhoaHoc;

                        // Đợi một chút để ComboBox chương học được load xong
                        await Task.Delay(100);

                        // Chọn chương học tương ứng
                        var chuongHoc = await _context.ChuongHocs
                            .FirstOrDefaultAsync(ch => ch.MaChuong == baiTap.BaiHoc.MaChuong);
                        if (chuongHoc != null)
                        {
                            cmbChuongHoc.SelectedValue = chuongHoc.MaChuong;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin bài tập: {ex.Message}", "Lỗi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XoaBaiTap();
        }
        private async void TimKiemBaiTapTheoKhoaHoc()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                // Nếu ô tìm kiếm trống, tải lại toàn bộ danh sách
                await TaiDanhSachBaiTap();
                return;
            }

            try
            {
                _danhSachBaiTap = await _context.BaiTaps
                    .Include(bt => bt.BaiHoc.ChuongHoc.KhoaHoc)
                    .Where(bt => bt.BaiHoc.ChuongHoc.KhoaHoc.TieuDe.Contains(tuKhoa))
                    .OrderByDescending(bt => bt.NgayTao)
                    .ToListAsync();

                HienThiDanhSachBaiTap();

                // Hiển thị thông báo số kết quả tìm được
                MessageBox.Show($"Tìm thấy {_danhSachBaiTap.Count} bài tập trong khóa học có tên chứa '{tuKhoa}'", "Kết quả tìm kiếm");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TimKiemBaiTapTheoKhoaHoc();
        }
    }
}
