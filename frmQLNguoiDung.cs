using DoAnCuoiKy.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmQLNguoiDung : Form
    {
        private readonly Model1 _context;
        private Guid _selectedUserId;

        public frmQLNguoiDung(Model1 context)
        {
            InitializeComponent();
            _context = context;
             Load += async (s, e) => await TaiDuLieuNguoiDung();
            LoadComboVaiTro();
        }

        // ====== LOAD DỮ LIỆU BAN ĐẦU ======
        private void LoadComboVaiTro()
        {
            comboVaiTro.Items.Clear();
            comboVaiTro.Items.AddRange(new object[]
            {
                "0 - Học viên",
                "1 - Giảng viên",
                "2 - Admin"
            });
        }
        private async Task TaiDuLieuNguoiDung()
        {
            try
            {
                var data = await _context.NguoiDungs
                    .Select(nd => new
                    {
                        nd.MaNguoiDung,
                        nd.Ho,
                        nd.Ten,
                        nd.Email,
                        nd.TenDangNhap,
                        VaiTro = nd.VaiTro == 0 ? "Học viên"
                            : nd.VaiTro == 1 ? "Giảng viên" : "Admin",
                        nd.NgayTao,
                        
                    })
                    .ToListAsync();

                dataGridViewUsers.DataSource = data;
                if (dataGridViewUsers.Columns["MaNguoiDung"] != null)
                    dataGridViewUsers.Columns["MaNguoiDung"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      

        // ====== CLICK TRÊN DÒNG DỮ LIỆU ======
        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewUsers.RowCount)
            {
                try
                {
                    var row = dataGridViewUsers.Rows[e.RowIndex];

                    if (row.Cells["MaNguoiDung"].Value != null)
                    {
                        _selectedUserId = Guid.Parse(row.Cells["MaNguoiDung"].Value.ToString());
                        HienThiChiTietNguoiDung(_selectedUserId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn người dùng: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void HienThiChiTietNguoiDung(Guid maNguoiDung)
        {
            try
            {
                var user = _context.NguoiDungs.FirstOrDefault(u => u.MaNguoiDung == maNguoiDung);

                if (user != null)
                {
                    //txtMaUser.Text = user.MaNguoiDung.ToString();
                    txtHo.Text = user.Ho;
                    txtTen.Text = user.Ten;
                    txtEmail.Text = user.Email;
                    txtTenDangNhap.Text = user.TenDangNhap;
                    txtMatKhau.Text = user.MatKhauHash;

                    if (user.VaiTro >= 0 && user.VaiTro <= 2)
                        comboVaiTro.SelectedIndex = user.VaiTro;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====== NÚT THÊM ======
        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieuDauVao()) return;

                // Kiểm tra trùng tên đăng nhập
                if (await _context.NguoiDungs.AnyAsync(u => u.TenDangNhap == txtTenDangNhap.Text.Trim()))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trùng email
                if (await _context.NguoiDungs.AnyAsync(u => u.Email == txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Email đã tồn tại!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo user mới
                var user = new NguoiDung
                {
                    MaNguoiDung = Guid.NewGuid(),
                    Ho = txtHo.Text.Trim(),
                    Ten = txtTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    MatKhauHash = txtMatKhau.Text.Trim(),
                    VaiTro = comboVaiTro.SelectedIndex,
                    NgayTao = DateTime.Now,
                    
                };

                _context.NguoiDungs.Add(user);
                await _context.SaveChangesAsync();

                MessageBox.Show("Thêm người dùng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await TaiDuLieuNguoiDung();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====== NÚT SỬA ======
        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUserId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraDuLieuDauVao()) return;

                var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == _selectedUserId);
                if (user != null)
                {
                    // Kiểm tra trùng tên đăng nhập (trừ chính nó)
                    if (await _context.NguoiDungs.AnyAsync(u =>
                        u.TenDangNhap == txtTenDangNhap.Text.Trim() && u.MaNguoiDung != _selectedUserId))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra trùng email (trừ chính nó)
                    if (await _context.NguoiDungs.AnyAsync(u =>
                        u.Email == txtEmail.Text.Trim() && u.MaNguoiDung != _selectedUserId))
                    {
                        MessageBox.Show("Email đã tồn tại!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin
                    user.Ho = txtHo.Text.Trim();
                    user.Ten = txtTen.Text.Trim();
                    user.Email = txtEmail.Text.Trim();
                    user.TenDangNhap = txtTenDangNhap.Text.Trim();
                    user.MatKhauHash = txtMatKhau.Text.Trim();
                    user.VaiTro = comboVaiTro.SelectedIndex;

                    await _context.SaveChangesAsync();

                    MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await TaiDuLieuNguoiDung();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====== NÚT XÓA ======
        private async void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUserId == Guid.Empty)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == _selectedUserId);
                if (user != null)
                {
                    // Kiểm tra nếu là admin cuối cùng
                    if (user.VaiTro == 2 && await _context.NguoiDungs.CountAsync(u => u.VaiTro == 2) <= 1)
                    {
                        MessageBox.Show("Không thể xóa admin cuối cùng!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        _context.NguoiDungs.Remove(user);
                        await _context.SaveChangesAsync();

                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await TaiDuLieuNguoiDung();
                        ClearInput();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====== NÚT TÌM KIẾM ======
        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                var keyword = txtTimKiem.Text.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    await TaiDuLieuNguoiDung();
                    return;
                }

                var data = await _context.NguoiDungs
                    .Where(u => u.TenDangNhap.ToLower().Contains(keyword) ||
                                u.Email.ToLower().Contains(keyword) ||
                                u.Ho.ToLower().Contains(keyword) ||
                                u.Ten.ToLower().Contains(keyword) ||
                                (u.Ho + " " + u.Ten).ToLower().Contains(keyword))
                    .Select(nd => new
                    {
                        nd.MaNguoiDung,
                        nd.Ho,
                        nd.Ten,
                        nd.Email,
                        nd.TenDangNhap,
                        VaiTro = nd.VaiTro == 0 ? "Học viên"
                            : nd.VaiTro == 1 ? "Giảng viên" : "Admin",
                        nd.NgayTao,
                        
                    })
                    .ToListAsync();

                dataGridViewUsers.DataSource = data;

                if (data.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy người dùng nào phù hợp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====== NÚT LƯU / HỦY / THOÁT ======
        private void btnLuu_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            MessageBox.Show("Đã hủy thao tác!", "Thông báo");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private bool KiemTraDuLieuDauVao()
        {
            if (string.IsNullOrWhiteSpace(txtHo.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!KiemTraEmailHopLe(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email không hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private bool KiemTraEmailHopLe(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // ====== HÀM TIỆN ÍCH ======
        private void ClearInput()
        {
            //txtMaUser.Clear();
            txtHo.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            comboVaiTro.SelectedIndex = -1;
        }

        // ====== Chuyển Form ======
        private void quảnLýKhóaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  frmCapNhatKhoaHoc f = new frmCapNhatKhoaHoc(); // Qua Form Cập Nhật Khóa Học
         //   f.Show();       // mở form mới
         //   this.Close();   // đóng form hiện tại
        }

        private void frmQLNguoiDung_Load(object sender, EventArgs e)
        {

        }
    }
}
