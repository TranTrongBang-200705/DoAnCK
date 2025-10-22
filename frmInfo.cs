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
    public partial class frmInfo : Form
    {
        private readonly NguoiDung _nguoiDung;
        private readonly Model1 _context;
        public frmInfo(NguoiDung nguoiDung, Model1 context)
        {
            InitializeComponent();
            _nguoiDung = nguoiDung;
            _context = context;
            HienThiThongTin();
        }
        private void HienThiThongTin()
        {
            txtHo.Text = _nguoiDung.Ho;
            txtTen.Text = _nguoiDung.Ten;
            txtEmail.Text = _nguoiDung.Email;
            txtRole.Text = LayTenVaiTro(_nguoiDung.VaiTro);
        }
        public string LayTenVaiTro(int vaiTro)
        {
            return vaiTro switch
            {
                0 => "Học viên",
                1 => "Giảng viên",
                2 => "Quản trị viên",
                _ => "Không xác định"
            };
        }
        private void frmInfo_Load(object sender, EventArgs e)
        {

        }
        private async void SuaThongTin()
        {
            if (!KiemTraDuLieuHopLe()) return;

            try
            {
                // Tìm người dùng trong database
                var nguoiDung = await _context.NguoiDungs
                    .FirstOrDefaultAsync(nd => nd.MaNguoiDung == _nguoiDung.MaNguoiDung);

                if (nguoiDung == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng", "Lỗi");
                    return;
                }

                // Kiểm tra email có bị trùng không (trừ chính người dùng hiện tại)
                var emailTonTai = await _context.NguoiDungs
                    .AnyAsync(nd => nd.Email == txtEmail.Text.Trim() && nd.MaNguoiDung != _nguoiDung.MaNguoiDung);

                if (emailTonTai)
                {
                    MessageBox.Show("Email đã được sử dụng bởi người dùng khác", "Lỗi");
                    txtEmail.Focus();
                    return;
                }

                // Cập nhật thông tin
                nguoiDung.Ho = txtHo.Text.Trim();
                nguoiDung.Ten = txtTen.Text.Trim();
                nguoiDung.Email = txtEmail.Text.Trim();

                // Lưu thay đổi
                await _context.SaveChangesAsync();

                // Cập nhật thông tin trong object hiện tại
                _nguoiDung.Ho = nguoiDung.Ho;
                _nguoiDung.Ten = nguoiDung.Ten;
                _nguoiDung.Email = nguoiDung.Email;

                MessageBox.Show("Cập nhật thông tin thành công!", "Thành công");

                // Đóng form hoặc làm mới
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật thông tin: {ex.Message}", "Lỗi");
            }
        }

        private bool KiemTraDuLieuHopLe()
        {
            // Kiểm tra Họ
            if (string.IsNullOrWhiteSpace(txtHo.Text))
            {
                MessageBox.Show("Vui lòng nhập họ", "Lỗi");
                txtHo.Focus();
                return false;
            }

            // Kiểm tra Tên
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên", "Lỗi");
                txtTen.Focus();
                return false;
            }

            // Kiểm tra Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email", "Lỗi");
                txtEmail.Focus();
                return false;
            }

            // Kiểm tra định dạng email
            if (!KiemTraDinhDangEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ", "Lỗi");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return false;
            }

            return true;
        }

        private bool KiemTraDinhDangEmail(string email)
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

        private void button1_Click(object sender, EventArgs e)
        {
            SuaThongTin();
        }
    }
}
