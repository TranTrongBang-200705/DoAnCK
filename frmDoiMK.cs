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
    public partial class frmDoiMK : Form
    {
        private readonly NguoiDung _nguoiDunghientai;
        private readonly Model1 _context;
        public frmDoiMK(NguoiDung nguoiDung, Model1 context)
        {
            InitializeComponent();
            _context = context;
            _nguoiDunghientai = nguoiDung;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoiMatKhau();
        }

        private void frmDoiMK_Load(object sender, EventArgs e)
        {
            txtMatKhauHienTai.PasswordChar = '•';
            txtMatKhauMoi.PasswordChar = '•';
            txtXacNhanMatKhauMoi.PasswordChar = '•';
        }
        private async void DoiMatKhau()
        {
            if (!KiemTraDuLieuHopLe()) return;

            try
            {
                // Tìm người dùng trong database
                var nguoiDung = await _context.NguoiDungs
                    .FirstOrDefaultAsync(nd => nd.MaNguoiDung == _nguoiDunghientai.MaNguoiDung);

                if (nguoiDung == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng", "Lỗi");
                    return;
                }

                // Kiểm tra mật khẩu hiện tại
                if (nguoiDung.MatKhauHash != txtMatKhauHienTai.Text)
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng", "Lỗi");
                    txtMatKhauHienTai.Focus();
                    txtMatKhauHienTai.SelectAll();
                    return;
                }

                // Cập nhật mật khẩu mới
                nguoiDung.MatKhauHash = txtMatKhauMoi.Text;

                // Lưu thay đổi
                await _context.SaveChangesAsync();

                // Cập nhật thông tin trong object hiện tại
                _nguoiDunghientai.MatKhauHash = txtMatKhauMoi.Text;

                MessageBox.Show("Đổi mật khẩu thành công!", "Thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đổi mật khẩu: {ex.Message}", "Lỗi");
            }
        }

        private bool KiemTraDuLieuHopLe()
        {
            // Kiểm tra mật khẩu hiện tại
            if (string.IsNullOrWhiteSpace(txtMatKhauHienTai.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại", "Lỗi");
                txtMatKhauHienTai.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mới
            if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới", "Lỗi");
                txtMatKhauMoi.Focus();
                return false;
            }

            // Kiểm tra xác nhận mật khẩu mới
            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu mới", "Lỗi");
                txtXacNhanMatKhauMoi.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mới và xác nhận khớp nhau
            if (txtMatKhauMoi.Text != txtXacNhanMatKhauMoi.Text)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp", "Lỗi");
                txtXacNhanMatKhauMoi.Focus();
                txtXacNhanMatKhauMoi.SelectAll();
                return false;
            }

            // Kiểm tra mật khẩu mới không trùng mật khẩu cũ
            if (txtMatKhauHienTai.Text == txtMatKhauMoi.Text)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại", "Lỗi");
                txtMatKhauMoi.Focus();
                txtMatKhauMoi.SelectAll();
                return false;
            }

            // Kiểm tra độ dài mật khẩu mới
            if (txtMatKhauMoi.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự", "Lỗi");
                txtMatKhauMoi.Focus();
                txtMatKhauMoi.SelectAll();
                return false;
            }

            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked)
            {
                txtMatKhauHienTai.PasswordChar = '\0';
                txtMatKhauMoi.PasswordChar = '\0';
                txtXacNhanMatKhauMoi.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauHienTai.PasswordChar = '•';
                txtMatKhauMoi.PasswordChar = '•';
                txtXacNhanMatKhauMoi.PasswordChar = '•';
            }
        }
    }
}
