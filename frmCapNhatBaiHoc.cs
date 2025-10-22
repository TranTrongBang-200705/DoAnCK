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
    public partial class frmCapNhatBaiHoc : Form
    {
        private readonly Model1 _context;
        private Guid _selectedBaiHocId;
        public frmCapNhatBaiHoc(Model1 context)
        {
            InitializeComponent();
            _context = context;

            Load += async (s, e) => await TaiDuLieu();
        }
        private async Task TaiDuLieu()
        {
            await TaiComboBoxChuongHoc();
            await TaiDanhSachBaiHoc();
        }

        private async Task TaiComboBoxChuongHoc()
        {
            try
            {
                var chuongHocs = await _context.ChuongHocs
                    .Include(ch => ch.KhoaHoc)
                    .OrderBy(ch => ch.KhoaHoc.TieuDe)
                    .ThenBy(ch => ch.ThuTu)
                    .ToListAsync();

                cmbChuongHoc.DataSource = chuongHocs;
                cmbChuongHoc.DisplayMember = "TieuDeChuong";
                cmbChuongHoc.ValueMember = "MaChuong";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách chương học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task TaiDanhSachBaiHoc(string timKiem = "")
        {
            try
            {
                var query = _context.BaiHocs
                    .Include(bh => bh.ChuongHoc)
                    .Include(bh => bh.ChuongHoc.KhoaHoc)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(timKiem))
                {
                    query = query.Where(bh => bh.TieuDeBaiHoc.Contains(timKiem) ||
                                             bh.ChuongHoc.TieuDeChuong.Contains(timKiem));
                }

                var data = await query
                    .Select(bh => new
                    {
                        bh.MaBaiHoc,
                        TenBaiHoc = bh.TieuDeBaiHoc,
                        TenChuongHoc = bh.ChuongHoc.TieuDeChuong,
                        bh.NgayTao,
                        bh.ThuTu
                    })
                    .OrderBy(bh => bh.TenChuongHoc)
                    .ThenBy(bh => bh.ThuTu)
                    .ToListAsync();

                dgvBaiHoc.DataSource = data;

                // Ẩn cột mã bài học và thứ tự
                if (dgvBaiHoc.Columns["MaBaiHoc"] != null)
                    dgvBaiHoc.Columns["MaBaiHoc"].Visible = false;
                if (dgvBaiHoc.Columns["ThuTu"] != null)
                    dgvBaiHoc.Columns["ThuTu"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCapNhatBaiHoc_Load(object sender, EventArgs e)
        {

        }

        private void dgvBaiHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBaiHoc.RowCount)
            {
                try
                {
                    var row = dgvBaiHoc.Rows[e.RowIndex];

                    if (row.Cells["MaBaiHoc"].Value != null)
                    {
                        _selectedBaiHocId = Guid.Parse(row.Cells["MaBaiHoc"].Value.ToString());
                        HienThiChiTietBaiHoc(_selectedBaiHocId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn bài học: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void HienThiChiTietBaiHoc(Guid maBaiHoc)
        {
            try
            {
                var baiHoc = await _context.BaiHocs
                    .Include(bh => bh.ChuongHoc)
                    .FirstOrDefaultAsync(bh => bh.MaBaiHoc == maBaiHoc);

                if (baiHoc != null)
                {
                    txtTenBaiHoc.Text = baiHoc.TieuDeBaiHoc;
                    rtbNoiDung.Text = baiHoc.NoiDung;
                    rtbLink.Text = baiHoc.DuongDanVideo;
                    cmbChuongHoc.SelectedValue = baiHoc.MaChuong;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieuDauVao()) return;

                var maChuong = (Guid)cmbChuongHoc.SelectedValue;

                // Kiểm tra trùng tên bài học trong cùng chương học
                if (await _context.BaiHocs.AnyAsync(bh =>
                    bh.TieuDeBaiHoc == txtTenBaiHoc.Text.Trim() &&
                    bh.MaChuong == maChuong))
                {
                    MessageBox.Show("Tên bài học đã tồn tại trong chương học này!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thứ tự tiếp theo
                var thuTuMoi = await _context.BaiHocs
                    .Where(bh => bh.MaChuong == maChuong)
                    .CountAsync() + 1;

                // Tạo bài học mới
                var baiHoc = new BaiHoc
                {
                    MaBaiHoc = Guid.NewGuid(),
                    TieuDeBaiHoc = txtTenBaiHoc.Text.Trim(),
                    NoiDung = rtbNoiDung.Text.Trim(),
                    DuongDanVideo = rtbLink.Text.Trim(),
                    MaChuong = maChuong,
                    ThuTu = thuTuMoi,
                    KieuBaiHoc = 0, // Mặc định là Video
                    TrangThai = 1, // Hiện
                    ThoiLuong = 0, // Có thể thêm tính năng nhập thời lượng sau
                    NgayTao = DateTime.Now
                };

                _context.BaiHocs.Add(baiHoc);
                await _context.SaveChangesAsync();

                MessageBox.Show("Thêm bài học thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await TaiDanhSachBaiHoc();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedBaiHocId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn bài học cần sửa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraDuLieuDauVao()) return;

                var baiHoc = await _context.BaiHocs.FirstOrDefaultAsync(bh => bh.MaBaiHoc == _selectedBaiHocId);
                if (baiHoc != null)
                {
                    var maChuong = (Guid)cmbChuongHoc.SelectedValue;

                    // Kiểm tra trùng tên bài học (trừ chính nó)
                    if (await _context.BaiHocs.AnyAsync(bh =>
                        bh.TieuDeBaiHoc == txtTenBaiHoc.Text.Trim() &&
                        bh.MaChuong == maChuong &&
                        bh.MaBaiHoc != _selectedBaiHocId))
                    {
                        MessageBox.Show("Tên bài học đã tồn tại trong chương học này!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin
                    baiHoc.TieuDeBaiHoc = txtTenBaiHoc.Text.Trim();
                    baiHoc.NoiDung = rtbNoiDung.Text.Trim();
                    baiHoc.DuongDanVideo = rtbLink.Text.Trim();
                    baiHoc.MaChuong = maChuong;

                    await _context.SaveChangesAsync();

                    MessageBox.Show("Cập nhật bài học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await TaiDanhSachBaiHoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedBaiHocId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn bài học cần xóa!", "Cảnh báo");
                    return;
                }

                var baiHoc = await _context.BaiHocs
                    .FirstOrDefaultAsync(bh => bh.MaBaiHoc == _selectedBaiHocId);

                if (baiHoc == null)
                {
                    MessageBox.Show("Bài học không tồn tại!", "Cảnh báo");
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa bài học:\n\"{baiHoc.TieuDeBaiHoc}\"?\n\nTất cả dữ liệu liên quan (tiến độ, bài tập) cũng sẽ bị xóa!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Xóa dữ liệu liên quan trước
                    var tienDoLienQuan = await _context.TienDoHocTaps
                        .Where(td => td.MaBaiHoc == _selectedBaiHocId)
                        .ToListAsync();
                    _context.TienDoHocTaps.RemoveRange(tienDoLienQuan);

                    var baiTapLienQuan = await _context.BaiTaps
                        .Where(bt => bt.MaBaiHoc == _selectedBaiHocId)
                        .ToListAsync();
                    _context.BaiTaps.RemoveRange(baiTapLienQuan);

                    // Xóa bài học
                    _context.BaiHocs.Remove(baiHoc);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Xóa bài học thành công!", "Thông báo");
                    await TaiDanhSachBaiHoc();
                    ClearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa bài học: {ex.Message}", "Lỗi");
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            var keyword = txtTimKiem.Text.Trim();
            await TaiDanhSachBaiHoc(keyword);
        }
        private bool KiemTraDuLieuDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtTenBaiHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bài học!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbChuongHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chương học!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearInput()
        {
            txtTenBaiHoc.Clear();
            rtbNoiDung.Clear();
            rtbLink.Clear();
            cmbChuongHoc.SelectedIndex = -1;
            _selectedBaiHocId = Guid.Empty;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
