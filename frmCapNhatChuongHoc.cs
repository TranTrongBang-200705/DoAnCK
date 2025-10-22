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
    public partial class frmCapNhatChuongHoc : Form
    {
        private readonly Model1 _context;
        private Guid _selectedChuongHocId;

        public frmCapNhatChuongHoc(Model1 context)
        {
            InitializeComponent();
            _context = context;

            Load += async (s, e) => await TaiDuLieu();
        }
        private async Task TaiDuLieu()
        {
            await TaiComboBoxKhoaHoc();
            await TaiDanhSachChuongHoc();
        }
        private async Task TaiComboBoxKhoaHoc()
        {
            try
            {
                var khoaHocs = await _context.KhoaHocs
                    .Where(kh => kh.TrangThai == 1) // Chỉ lấy khóa học đã xuất bản
                    .OrderBy(kh => kh.TieuDe)
                    .ToListAsync();

                cmbKhoaHoc.DataSource = khoaHocs;
                cmbKhoaHoc.DisplayMember = "TieuDe";
                cmbKhoaHoc.ValueMember = "MaKhoaHoc";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task TaiDanhSachChuongHoc(string timKiem = "")
        {
            try
            {
                var query = _context.ChuongHocs
                    .Include(ch => ch.KhoaHoc)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(timKiem))
                {
                    query = query.Where(ch => ch.TieuDeChuong.Contains(timKiem) ||
                                              ch.KhoaHoc.TieuDe.Contains(timKiem));
                }

                var data = await query
                    .Select(ch => new
                    {
                        ch.MaChuong,
                        TenKhoaHoc = ch.KhoaHoc.TieuDe,
                        TenChuongHoc = ch.TieuDeChuong,
                        ch.NgayTao,
                        ch.ThuTu
                    })
                    .OrderBy(ch => ch.TenKhoaHoc)
                    .ThenBy(ch => ch.ThuTu)
                    .ToListAsync();

                dataGridViewChuongHoc.DataSource = data;

                // Ẩn cột mã chương và thứ tự
                if (dataGridViewChuongHoc.Columns["MaChuong"] != null)
                    dataGridViewChuongHoc.Columns["MaChuong"].Visible = false;
                if (dataGridViewChuongHoc.Columns["ThuTu"] != null)
                    dataGridViewChuongHoc.Columns["ThuTu"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách chương học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmCapNhatChuongHoc_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewChuongHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewChuongHoc.RowCount)
            {
                try
                {
                    var row = dataGridViewChuongHoc.Rows[e.RowIndex];

                    if (row.Cells["MaChuong"].Value != null)
                    {
                        _selectedChuongHocId = Guid.Parse(row.Cells["MaChuong"].Value.ToString());
                        HienThiChiTietChuongHoc(_selectedChuongHocId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn chương học: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void HienThiChiTietChuongHoc(Guid maChuong)
        {
            try
            {
                var chuongHoc = await _context.ChuongHocs
                    .Include(ch => ch.KhoaHoc)
                    .FirstOrDefaultAsync(ch => ch.MaChuong == maChuong);

                if (chuongHoc != null)
                {
                    txtTenChuongHoc.Text = chuongHoc.TieuDeChuong;
                    txtMoTa.Text = chuongHoc.MoTa;
                    cmbKhoaHoc.SelectedValue = chuongHoc.MaKhoaHoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết chương học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            var keyword = txtTimKiem.Text.Trim();
            await TaiDanhSachChuongHoc(keyword);
        }
        private bool KiemTraDuLieuDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtTenChuongHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chương học!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbKhoaHoc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearInput()
        {
            txtTenChuongHoc.Clear();
            txtMoTa.Clear();
            cmbKhoaHoc.SelectedIndex = -1;
            _selectedChuongHocId = Guid.Empty;
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn chương học chưa
                if (_selectedChuongHocId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn chương học cần xóa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy chương học từ database
                var chuongHoc = await _context.ChuongHocs
                    .FirstOrDefaultAsync(ch => ch.MaChuong == _selectedChuongHocId);

                if (chuongHoc == null)
                {
                    MessageBox.Show("Chương học không tồn tại!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem chương học có bài học không
                var coBaiHoc = await _context.BaiHocs
                    .AnyAsync(bh => bh.MaChuong == _selectedChuongHocId);

                if (coBaiHoc)
                {
                    MessageBox.Show("Không thể xóa chương học đã có bài học!\nHãy xóa hết bài học trong chương trước.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị xác nhận xóa
                var result = MessageBox.Show(
                    $"Bạn có CHẮC CHẮN muốn xóa chương học:\n\"{chuongHoc.TieuDeChuong}\"?\n\nDữ liệu sau khi xóa không thể khôi phục!",
                    "XÁC NHẬN XÓA",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _context.ChuongHocs.Remove(chuongHoc);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Xóa chương học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh dữ liệu và clear input
                    await TaiDanhSachChuongHoc();
                    ClearInput();
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: Không thể xóa chương học do có dữ liệu liên quan.\nChi tiết: {dbEx.InnerException?.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa chương học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedChuongHocId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn chương học cần sửa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraDuLieuDauVao()) return;

                var chuongHoc = await _context.ChuongHocs.FirstOrDefaultAsync(ch => ch.MaChuong == _selectedChuongHocId);
                if (chuongHoc != null)
                {
                    var maKhoaHoc = (Guid)cmbKhoaHoc.SelectedValue;

                    // Kiểm tra trùng tên chương học (trừ chính nó)
                    if (await _context.ChuongHocs.AnyAsync(ch =>
                            ch.TieuDeChuong == txtTenChuongHoc.Text.Trim() &&
                            ch.MaKhoaHoc == maKhoaHoc &&
                            ch.MaChuong != _selectedChuongHocId))
                    {
                        MessageBox.Show("Tên chương học đã tồn tại trong khóa học này!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin
                    chuongHoc.TieuDeChuong = txtTenChuongHoc.Text.Trim();
                    chuongHoc.MoTa = txtMoTa.Text.Trim();
                    chuongHoc.MaKhoaHoc = maKhoaHoc;

                    await _context.SaveChangesAsync();

                    MessageBox.Show("Cập nhật chương học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await TaiDanhSachChuongHoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật chương học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieuDauVao()) return;

                // Kiểm tra trùng tên chương học trong cùng khóa học
                var maKhoaHoc = (Guid)cmbKhoaHoc.SelectedValue;
                if (await _context.ChuongHocs.AnyAsync(ch =>
                        ch.TieuDeChuong == txtTenChuongHoc.Text.Trim() &&
                        ch.MaKhoaHoc == maKhoaHoc))
                {
                    MessageBox.Show("Tên chương học đã tồn tại trong khóa học này!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thứ tự tiếp theo
                var thuTuMoi = await _context.ChuongHocs
                    .Where(ch => ch.MaKhoaHoc == maKhoaHoc)
                    .CountAsync() + 1;

                // Tạo chương học mới
                var chuongHoc = new ChuongHoc
                {
                    MaChuong = Guid.NewGuid(),
                    TieuDeChuong = txtTenChuongHoc.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    MaKhoaHoc = maKhoaHoc,
                    ThuTu = thuTuMoi,
                    NgayTao = DateTime.Now
                };

                _context.ChuongHocs.Add(chuongHoc);
                await _context.SaveChangesAsync();

                MessageBox.Show("Thêm chương học thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await TaiDanhSachChuongHoc();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chương học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void cậpNhậtBàiHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmCapNhatBaiHoc = new frmCapNhatBaiHoc(_context);
            frmCapNhatBaiHoc.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}
