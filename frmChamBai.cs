using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmChamBai : Form
    {
        private readonly Model1 _context;
        private List<NopBaiTap> _danhSachBaiNop;
        private NopBaiTap _baiNopDangChon;

        public frmChamBai(Model1 context)
        {
            InitializeComponent();
            _context = context;
        }

        private async void frmChamBai_Load(object sender, EventArgs e)
        {
            TaiDuLieuComboBox();
            await TaiDanhSachBaiNop();

        }

        private void TaiDuLieuComboBox()
        {
            // Thêm các lựa chọn vào ComboBox
            cmbTrangThai.Items.Add("Tất cả bài nộp");
            cmbTrangThai.Items.Add("Bài chưa chấm");
            cmbTrangThai.Items.Add("Bài đã chấm");
            cmbTrangThai.SelectedIndex = 0; // Mặc định chọn "Tất cả bài nộp"

            // Gán sự kiện khi chọn item
            cmbTrangThai.SelectedIndexChanged += (s, e) => LocVaHienThiBaiNop();
        }

        private async Task TaiDanhSachBaiNop()
        {
            try
            {
                // Lấy tất cả bài nộp kèm thông tin liên quan
                _danhSachBaiNop = await _context.NopBaiTaps
                    .Include(n => n.BaiTap)
                    .Include(n => n.NguoiDung)
                    .Include(n => n.BaiTap.BaiHoc.ChuongHoc.KhoaHoc)
                    .OrderByDescending(n => n.NgayNop)
                    .ToListAsync();

                LocVaHienThiBaiNop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách bài nộp: {ex.Message}", "Lỗi");
            }
        }

        private void LocVaHienThiBaiNop()
        {
            var danhSachLoc = _danhSachBaiNop;

            // Lọc theo trạng thái từ ComboBox
            switch (cmbTrangThai.SelectedIndex)
            {
                case 1: // Bài chưa chấm
                    danhSachLoc = danhSachLoc.Where(n => n.TrangThai == 0).ToList();
                    break;
                case 2: // Bài đã chấm
                    danhSachLoc = danhSachLoc.Where(n => n.TrangThai == 1).ToList();
                    break;
                // case 0: Tất cả bài nộp - không lọc
            }

            HienThiDanhSachBaiNop(danhSachLoc);
        }

        private void HienThiDanhSachBaiNop(List<NopBaiTap> danhSach)
        {
            dgvBaiNop.Rows.Clear();

            foreach (var baiNop in danhSach)
            {
                dgvBaiNop.Rows.Add(
                    baiNop.MaNopBai,
                    baiNop.NguoiDung?.Ho + " " + baiNop.NguoiDung?.Ten,
                    baiNop.BaiTap?.TieuDe,
                    baiNop.BaiTap?.BaiHoc?.ChuongHoc?.KhoaHoc?.TieuDe,
                    baiNop.NgayNop?.ToString("dd/MM/yyyy HH:mm"),
                    baiNop.Diem.HasValue ? baiNop.Diem.Value.ToString("0.00") : "Chưa chấm",
                    baiNop.TrangThai == 1 ? "✅ Đã chấm" : "⏳ Chờ chấm"
                );
            }

            // Hiển thị số lượng bài nộp
            //lblThongKe.Text = $"Tổng số: {danhSach.Count} bài nộp";
        }

        private void dgvBaiNop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBaiNop.Rows.Count)
            {
                var maNopBaiCell = dgvBaiNop.Rows[e.RowIndex].Cells[0].Value;
                if (maNopBaiCell != null && Guid.TryParse(maNopBaiCell.ToString(), out Guid maNopBai))
                {
                    _baiNopDangChon = _danhSachBaiNop.FirstOrDefault(n => n.MaNopBai == maNopBai);
                    HienThiThongTinBaiNop(_baiNopDangChon);
                }
            }
        }

        private void HienThiThongTinBaiNop(NopBaiTap baiNop)
        {
            if (baiNop == null) return;

            // Hiển thị điểm nếu đã chấm
            if (baiNop.Diem.HasValue)
            {
                txtDiem.Text = baiNop.Diem.Value.ToString("0.00");
            }
            else
            {
                txtDiem.Text = "";
            }

            // Hiển thị nhận xét
            rtxtNhanXet.Text = baiNop.NhanXet ?? "";

            // HIỂN THỊ ĐƯỜNG DẪN FILE
            if (!string.IsNullOrEmpty(baiNop.DuongDanFile))
            {
                lblFileDinhKem.Text = $"File đính kèm: {Path.GetFileName(baiNop.DuongDanFile)}";
                lblFileDinhKem.Visible = true;
                btnMoFile.Visible = true;
                btnMoFile.Tag = baiNop.DuongDanFile; // Lưu đường dẫn để mở
            }
            else
            {
                lblFileDinhKem.Text = "Không có file đính kèm";
                lblFileDinhKem.Visible = true;
                btnMoFile.Visible = false;
            }

            // Hiển thị nội dung nộp (nếu có)
            if (!string.IsNullOrEmpty(baiNop.NoiDungNop))
            {
                rtxtNoiDungNop.Text = baiNop.NoiDungNop;
                rtxtNoiDungNop.Visible = true;
                lblNoiDung.Visible = true;
            }
            else
            {
                rtxtNoiDungNop.Text = "";
                rtxtNoiDungNop.Visible = false;
                lblNoiDung.Visible = false;
            }

            // CHO PHÉP SỬA CẢ KHI ĐÃ CHẤM
            bool daCham = baiNop.TrangThai == 1;

            // Luôn enable controls để có thể sửa
            txtDiem.Enabled = true;
            rtxtNhanXet.Enabled = true;

            // Hiển thị thông tin chi tiết
           // lblThongTinChiTiet.Text = $"Bài nộp của: {baiNop.NguoiDung?.Ho} {baiNop.NguoiDung?.Ten} - {baiNop.BaiTap?.TieuDe}";

            // Hiển thị trạng thái
            /*if (daCham)
            {
                lblTrangThai.Text = "📝 Đã chấm - Có thể sửa";
                lblTrangThai.ForeColor = Color.Blue;
            }
            else
            {
                lblTrangThai.Text = "⏳ Chưa chấm";
                lblTrangThai.ForeColor = Color.Orange;
            }*/
        }

        private async void LuuDiemVaNhanXet()
        {
            if (_baiNopDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn bài nộp cần chấm", "Thông báo");
                return;
            }

            if (!KiemTraDuLieuHopLe()) return;

            try
            {
                // Cập nhật điểm và nhận xét
                _baiNopDangChon.Diem = decimal.Parse(txtDiem.Text);
                _baiNopDangChon.NhanXet = rtxtNhanXet.Text;
                _baiNopDangChon.TrangThai = 1; // Đã chấm

                await _context.SaveChangesAsync();

                MessageBox.Show("Lưu điểm và nhận xét thành công!", "Thành công");
                await TaiDanhSachBaiNop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu điểm: {ex.Message}", "Lỗi");
            }
        }

        private bool KiemTraDuLieuHopLe()
        {
            // Kiểm tra điểm hợp lệ
            if (!decimal.TryParse(txtDiem.Text, out decimal diem) || diem < 0 || diem > 100)
            {
                MessageBox.Show("Vui lòng nhập điểm hợp lệ (0 - 100)", "Lỗi");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LuuDiemVaNhanXet();
        }

        private void MoFileBaiNop()
        {
            if (btnMoFile.Tag == null || string.IsNullOrEmpty(btnMoFile.Tag.ToString()))
            {
                MessageBox.Show("Không có file đính kèm", "Thông báo");
                return;
            }

            try
            {
                string filePath = btnMoFile.Tag.ToString();

                // Kiểm tra file có tồn tại không
                if (File.Exists(filePath))
                {
                    // Mở file với ứng dụng mặc định
                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show("File không tồn tại hoặc đã bị xóa", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở file: {ex.Message}", "Lỗi");
            }
        }

        // SỰ KIỆN XEM THƯ MỤC CHỨA FILE
        private void lblFileDinhKem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
            MoFileBaiNop();
        }
       

       
    }

}

