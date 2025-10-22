using DoAnCuoiKy.Models;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmCapNhatKhoaHoc : Form
    {
        private readonly Model1 _context;
        private Guid _selectedKhoaHocId;

        public frmCapNhatKhoaHoc(Model1 context)
        {
            InitializeComponent();
            _context = context;

            Load += async (s, e) => await TaiDuLieuKhoaHoc();
            LoadComboBox();
        }

        // Constructor bổ sung để gọi từ frmMainQuanTri

        private async Task TaiDuLieuKhoaHoc()
        {
            try
            {
                var data = await _context.KhoaHocs
                    .Include(kh => kh.DanhMuc)
                    .Include(kh => kh.NguoiDung)
                    .Select(kh => new
                    {
                        kh.MaKhoaHoc,
                        kh.TieuDe,
                        kh.MoTa,
                        DanhMuc = kh.DanhMuc.TenDanhMuc,
                        GiangVien = kh.NguoiDung.Ho + " " + kh.NguoiDung.Ten,
                        TrinhDo = kh.TrinhDo == 0 ? "Cơ bản" : kh.TrinhDo == 1 ? "Trung cấp" : "Nâng cao",
                        kh.GiaTien,
                        kh.NgayTao
                    })
                    .ToListAsync();

                // Không bind vào DataGridView nếu không hiển thị
                 dataGridViewKhoaHoc.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBox()
        {
            try
            {
                // Load danh mục
                var danhMuc = _context.DanhMucs.ToList();
                comboDanhMuc.DataSource = danhMuc;
                comboDanhMuc.DisplayMember = "TenDanhMuc";
                comboDanhMuc.ValueMember = "MaDanhMuc";

                // Load giảng viên
                var giangVien = _context.NguoiDungs
                    .Where(nd => nd.VaiTro == 1) // Vai trò giảng viên
                    .ToList();
                comboGiangVien.DataSource = giangVien;
                comboGiangVien.DisplayMember = "Ho";
                comboGiangVien.ValueMember = "MaNguoiDung";

                // Load trình độ
                comboTrinhDo.Items.Clear();
                comboTrinhDo.Items.AddRange(new object[]
                {
                    "Cơ bản",
                    "Trung cấp",
                    "Nâng cao"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải combobox: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HienThiChiTietKhoaHoc(Guid maKhoaHoc)
        {
            try
            {
                var khoaHoc = _context.KhoaHocs
                    .Include(kh => kh.DanhMuc)
                    .Include(kh => kh.NguoiDung)
                    .FirstOrDefault(kh => kh.MaKhoaHoc == maKhoaHoc);

                if (khoaHoc != null)
                {
                    txtMaKhoa.Text = khoaHoc.MaKhoaHoc.ToString();
                    txtTenKhoaHoc.Text = khoaHoc.TieuDe;
                    txtMoTa.Text = khoaHoc.MoTa;
                    //txtLinkBaiGiang.Text = khoaHoc.AnhBia;

                    comboDanhMuc.SelectedValue = khoaHoc.MaDanhMuc;
                    comboGiangVien.SelectedValue = khoaHoc.MaGiangVien;
                    comboTrinhDo.SelectedIndex = khoaHoc.TrinhDo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết khóa học: {ex.Message}", "Lỗi");
            }
        }
        private async void frmCapNhatKhoaHoc_Load(object sender, EventArgs e)
        {
            
        }

        

      


        private void btnQLNguoiDung_Click(object sender, EventArgs e)
        {
            
            
        }

        private async void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            try
            {
                var keyword = txtTimKiem.Text.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    await TaiDuLieuKhoaHoc();
                    return;
                }

                var data = await _context.KhoaHocs
                    .Include(kh => kh.DanhMuc)
                    .Include(kh => kh.NguoiDung)
                    .Where(kh => kh.TieuDe.ToLower().Contains(keyword) ||
                                 kh.MoTa.ToLower().Contains(keyword) ||
                                 kh.DanhMuc.TenDanhMuc.ToLower().Contains(keyword) ||
                                 (kh.NguoiDung.Ho + " " + kh.NguoiDung.Ten).ToLower().Contains(keyword))
                    .Select(kh => new
                    {
                        kh.MaKhoaHoc,
                        kh.TieuDe,
                        kh.MoTa,
                        DanhMuc = kh.DanhMuc.TenDanhMuc,
                        GiangVien = kh.NguoiDung.Ho + " " + kh.NguoiDung.Ten,
                        TrinhDo = kh.TrinhDo == 0 ? "Cơ bản" : kh.TrinhDo == 1 ? "Trung cấp" : "Nâng cao",
                        kh.GiaTien,
                        kh.NgayTao
                    })
                    .ToListAsync();

                dataGridViewKhoaHoc.DataSource = data;

                if (dataGridViewKhoaHoc.Columns["MaKhoaHoc"] != null)
                    dataGridViewKhoaHoc.Columns["MaKhoaHoc"].Visible = false;

                if (data.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khóa học nào phù hợp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedKhoaHocId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn khóa học cần sửa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraDuLieuDauVao()) return;

                var khoaHoc = await _context.KhoaHocs.FirstOrDefaultAsync(kh => kh.MaKhoaHoc == _selectedKhoaHocId);
                if (khoaHoc != null)
                {
                    // Kiểm tra trùng tên khóa học (trừ chính nó)
                    if (await _context.KhoaHocs.AnyAsync(kh =>
                        kh.TieuDe == txtTenKhoaHoc.Text.Trim() && kh.MaKhoaHoc != _selectedKhoaHocId))
                    {
                        MessageBox.Show("Tên khóa học đã tồn tại!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin
                    khoaHoc.TieuDe = txtTenKhoaHoc.Text.Trim();
                    khoaHoc.MoTa = txtMoTa.Text.Trim();
                    //khoaHoc.AnhBia = txtLinkBaiGiang.Text.Trim();
                    khoaHoc.MaDanhMuc = (Guid)comboDanhMuc.SelectedValue;
                    khoaHoc.MaGiangVien = (Guid)comboGiangVien.SelectedValue;
                    khoaHoc.TrinhDo = comboTrinhDo.SelectedIndex;
                    khoaHoc.NgayCapNhat = DateTime.Now;

                    await _context.SaveChangesAsync();

                    MessageBox.Show("Cập nhật khóa học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await TaiDuLieuKhoaHoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private async void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieuDauVao()) return;

                // Kiểm tra trùng tên khóa học
                if (await _context.KhoaHocs.AnyAsync(kh => kh.TieuDe == txtTenKhoaHoc.Text.Trim()))
                {
                    MessageBox.Show("Tên khóa học đã tồn tại!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo khóa học mới
                var khoaHoc = new KhoaHoc
                {
                    MaKhoaHoc = Guid.NewGuid(),
                    TieuDe = txtTenKhoaHoc.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    //AnhBia = txtLinkBaiGiang.Text.Trim(),
                    MaDanhMuc = (Guid)comboDanhMuc.SelectedValue,
                    MaGiangVien = (Guid)comboGiangVien.SelectedValue,
                    TrinhDo = comboTrinhDo.SelectedIndex,
                    GiaTien = 0,
                    TrangThai = 1, // Đã xuất bản
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                };

                _context.KhoaHocs.Add(khoaHoc);
                await _context.SaveChangesAsync();

                MessageBox.Show("Thêm khóa học thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await TaiDuLieuKhoaHoc();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInput()
        {
            txtMaKhoa.Clear();
            txtTenKhoaHoc.Clear();
            txtMoTa.Clear();
            //txtLinkBaiGiang.Clear();
            comboDanhMuc.SelectedIndex = -1;
            comboGiangVien.SelectedIndex = -1;
            comboTrinhDo.SelectedIndex = -1;
            _selectedKhoaHocId = Guid.Empty;
        }
        private bool KiemTraDuLieuDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtTenKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khóa học!", "Cảnh báo");
                return false;
            }

            if (comboDanhMuc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Cảnh báo");
                return false;
            }

            if (comboGiangVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giảng viên!", "Cảnh báo");
                return false;
            }

            if (comboTrinhDo.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trình độ!", "Cảnh báo");
                return false;
            }

            return true;
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedKhoaHocId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn khóa học cần xóa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var khoaHoc = await _context.KhoaHocs.FirstOrDefaultAsync(kh => kh.MaKhoaHoc == _selectedKhoaHocId);
                if (khoaHoc != null)
                {
                    // Kiểm tra xem khóa học có chương học không
                    var coChuongHoc = await _context.ChuongHocs.AnyAsync(ch => ch.MaKhoaHoc == _selectedKhoaHocId);
                    if (coChuongHoc)
                    {
                        MessageBox.Show("Không thể xóa khóa học đã có chương học!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khóa học này không?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        _context.KhoaHocs.Remove(khoaHoc);
                        await _context.SaveChangesAsync();

                        MessageBox.Show("Xóa khóa học thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await TaiDuLieuKhoaHoc();
                        ClearInput();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GridViewKhoaHoc_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void quảnLýChươngHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmChuongHoc = new frmCapNhatChuongHoc(_context);
            frmChuongHoc.Show();
        }

        private void dataGridViewKhoaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewKhoaHoc.RowCount)
            {
                try
                {
                    var row = dataGridViewKhoaHoc.Rows[e.RowIndex];

                    if (row.Cells["MaKhoaHoc"].Value != null)
                    {
                        _selectedKhoaHocId = Guid.Parse(row.Cells["MaKhoaHoc"].Value.ToString());
                        HienThiChiTietKhoaHoc(_selectedKhoaHocId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn khóa học: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}