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
    public partial class frmDangNhap : Form
    {
        private readonly Model1 _context;

        public frmDangNhap()
        {
            _context = new Model1();
            InitializeComponent();
            txtMatKhau.UseSystemPasswordChar = true;
            this.chkMK.CheckedChanged += chkMK_CheckedChanged;
            btnDN.MouseEnter += (s, e) => btnDN.BackColor = Color.FromArgb(35, 120, 160);
            btnDN.MouseLeave += (s, e) => btnDN.BackColor = Color.FromArgb(46, 134, 171);
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private async void btnDN_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDangNhap = txtTaiKhoan.Text.Trim();
                string matKhau = txtMatKhau.Text;
                int vaiTroChon = cboVaiTro.SelectedIndex;

                // Validate
                if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                    return;
                }

                if (vaiTroChon < 0)
                {
                    MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo");
                    return;
                }

                // Tìm người dùng trong database
                var nguoiDung = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);

                if (nguoiDung == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi đăng nhập");
                    txtTaiKhoan.Focus();
                    return;
                }

                // KIỂM TRA VAI TRÒ
                if (nguoiDung.VaiTro != vaiTroChon)
                {
                    string vaiTroThuc = LayTenVaiTro(nguoiDung.VaiTro);
                    string vaiTroChonText = LayTenVaiTro(vaiTroChon);

                    MessageBox.Show(
                        $"Tài khoản này là {vaiTroThuc}, không phải {vaiTroChonText}!\nVui lòng chọn đúng vai trò.",
                        "Sai vai trò", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra mật khẩu
                if (matKhau == nguoiDung.MatKhauHash)
                {
                    // ĐĂNG NHẬP THÀNH CÔNG - CHUYỂN HƯỚNG THEO VAI TRÒ
                    this.Hide();

                    switch (nguoiDung.VaiTro)
                    {
                        case 0: // HỌC VIÊN
                            var frmHocVien = new frmMainHocVien(nguoiDung, _context);
                            frmHocVien.Show();
                            break;

                        case 1: // GIẢNG VIÊN
                            var frmGiangVien = new frmMainGiangVien(nguoiDung, _context);
                            frmGiangVien.Show();
                            break;

                        case 2: // QUẢN TRỊ
                            var frmQuanTri = new frmMainQuanTri(nguoiDung, _context);
                            frmQuanTri.Show();
                            break;

                        default:
                            var frmDefault = new frmMainHocVien(nguoiDung, _context);
                            frmDefault.Show();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng!", "Lỗi đăng nhập");
                    txtMatKhau.Focus();
                    txtMatKhau.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi");
            }
        }
        private string LayTenVaiTro(int vaiTro)
        {
            return vaiTro switch
            {
                0 => "Học viên",
                1 => "Giảng viên",
                2 => "Quản trị viên",
                _ => "Không xác định"
            };
        }




        private void chkMK_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkMK.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
