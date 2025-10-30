using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmBaiTap : Form
    {
        private readonly Model1 _context;
        private readonly string _maKhoaHoc;
        private readonly string _maHocVien;
        private List<BaiTap> _danhSachBaiTap;
        private BaiTap _baiTapHienTai;
        private NopBaiTap _baiDaNop;

        public frmBaiTap(string maKhoaHoc, string maHocVien)
        {
            InitializeComponent();
            _maKhoaHoc = maKhoaHoc;
            _maHocVien = maHocVien;
            _context = new Model1();

            Load += async (s, e) => await TaiDuLieuBaiTap();
        }

        private async Task TaiDuLieuBaiTap()
        {
            try
            {
                Console.WriteLine($"🔍 Bắt đầu tải bài tập - MaKhoaHoc: {_maKhoaHoc}, MaHocVien: {_maHocVien}");

                // Kiểm tra tham số
                if (string.IsNullOrEmpty(_maKhoaHoc) || string.IsNullOrEmpty(_maHocVien))
                {
                    MessageBox.Show("Lỗi: Không có thông tin khóa học hoặc học viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuyển đổi GUID
                var maKhoaHocGuid = Guid.Parse(_maKhoaHoc);
                var maHocVienGuid = Guid.Parse(_maHocVien);

                // Load danh sách bài tập từ khóa học
                _danhSachBaiTap = await _context.BaiTaps
                    .Include(bt => bt.BaiHoc.ChuongHoc)
                    .Where(bt => bt.BaiHoc.ChuongHoc.MaKhoaHoc == maKhoaHocGuid)
                    .OrderBy(bt => bt.BaiHoc.ChuongHoc.ThuTu)
                    .ThenBy(bt => bt.BaiHoc.ThuTu)
                    .ToListAsync();

                Console.WriteLine($"📚 Tìm thấy {_danhSachBaiTap.Count} bài tập");

                if (_danhSachBaiTap.Any())
                {
                    // Load bài đã nộp của học viên
                    var maBaiTapIds = _danhSachBaiTap.Select(bt => bt.MaBaiTap).ToList();
                    var baiDaNop = await _context.NopBaiTaps
                        .Where(nbt => nbt.MaHocVien == maHocVienGuid && maBaiTapIds.Contains(nbt.MaBaiTap))
                        .ToListAsync();

                    HienThiDanhSachBaiTap(baiDaNop);

                    // Hiển thị bài tập đầu tiên
                    _baiTapHienTai = _danhSachBaiTap.First();
                    HienThiChiTietBaiTap(_baiTapHienTai);
                }
                else
                {
                    MessageBox.Show("Không có bài tập nào trong khóa học này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi tải bài tập: {ex.Message}");
                Console.WriteLine($"❌ StackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi tải dữ liệu bài tập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiDanhSachBaiTap(List<NopBaiTap> baiDaNop)
        {
            treeViewBaiTap.Nodes.Clear();

            var nhomTheoChuong = _danhSachBaiTap.GroupBy(bt => bt.BaiHoc.ChuongHoc);

            foreach (var nhom in nhomTheoChuong)
            {
                var chuong = nhom.Key;
                var nodeChuong = new TreeNode($"Chương {chuong.ThuTu}: {chuong.TieuDeChuong}");

                foreach (var baiTap in nhom)
                {
                    var baiNop = baiDaNop.FirstOrDefault(nbt => nbt.MaBaiTap == baiTap.MaBaiTap);
                    var trangThai = GetTrangThaiBaiTap(baiNop);
                    var icon = GetIconForBaiTap(trangThai);

                    var nodeBai = new TreeNode($"{icon} {baiTap.TieuDe} ({trangThai})");
                    nodeBai.Tag = baiTap;
                    nodeChuong.Nodes.Add(nodeBai);
                }

                treeViewBaiTap.Nodes.Add(nodeChuong);
                nodeChuong.Expand();
            }
        }

        private string GetTrangThaiBaiTap(NopBaiTap baiNop)
        {
            if (baiNop == null) return "Chưa nộp";
            if (baiNop.TrangThai == 1)
            {
                var diemToiDa = _danhSachBaiTap.First(bt => bt.MaBaiTap == baiNop.MaBaiTap).DiemToiDa;
                return $"Đã chấm: {baiNop.Diem}/{diemToiDa}";
            }
            return "Đã nộp - Chờ chấm";
        }

        private string GetIconForBaiTap(string trangThai)
        {
            return trangThai switch
            {
                "Chưa nộp" => "○",
                "Đã nộp - Chờ chấm" => "↻",
                string s when s.Contains("Đã chấm") => "✓",
                _ => "○"
            };
        }

        private void HienThiChiTietBaiTap(BaiTap baiTap)
        {
            try
            {
                _baiTapHienTai = baiTap;

                // Hiển thị thông tin bài tập
                txtTieuDeBaiTap.Text = baiTap.TieuDe;
                rtbNoiDungBaiTap.Text = baiTap.NoiDung;

                if (baiTap.HanNop.HasValue)
                {
                    dtpHanNop.Value = baiTap.HanNop.Value;
                }
                else
                {
                    dtpHanNop.Value = DateTime.Now.AddDays(7);
                }

                numDiemToiDa.Value = (decimal)baiTap.DiemToiDa;

                // Chuyển đổi GUID
                var maHocVienGuid = Guid.Parse(_maHocVien);

                // Load bài đã nộp (nếu có)
                _baiDaNop = _context.NopBaiTaps
                    .FirstOrDefault(nbt => nbt.MaBaiTap == baiTap.MaBaiTap && nbt.MaHocVien == maHocVienGuid);

                // Hiển thị thông tin bài nộp
                if (_baiDaNop != null)
                {
                    rtbNoiDungNopBai.Text = _baiDaNop.NoiDungNop;
                    txtDuongDanFile.Text = _baiDaNop.DuongDanFile;

                    // Disable nếu đã nộp
                    rtbNoiDungNopBai.ReadOnly = true;
                    btnBrowse.Enabled = false;
                    btnNopBai.Enabled = false;
                    btnNopBai.Text = "Đã nộp";
                    btnNopBai.BackColor = Color.Gray;
                }
                else
                {
                    rtbNoiDungNopBai.Clear();
                    txtDuongDanFile.Clear();
                    rtbNoiDungNopBai.ReadOnly = false;
                    btnBrowse.Enabled = true;
                    btnNopBai.Enabled = true;
                    btnNopBai.Text = "📤 Nộp Bài";
                    btnNopBai.BackColor = Color.FromArgb(0, 123, 255);
                }

                // Kiểm tra hạn nộp
                KiemTraHanNop(baiTap);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi hiển thị bài tập: {ex.Message}");
            }
        }

        private void KiemTraHanNop(BaiTap baiTap)
        {
            if (baiTap.HanNop.HasValue && baiTap.HanNop < DateTime.Now)
            {
                rtbNoiDungNopBai.ReadOnly = true;
                btnBrowse.Enabled = false;
                btnNopBai.Enabled = false;
                btnNopBai.Text = "Đã hết hạn";
                btnNopBai.BackColor = Color.Red;

                MessageBox.Show("Bài tập đã hết hạn nộp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void treeViewBaiTap_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BaiTap baiTap)
            {
                HienThiChiTietBaiTap(baiTap);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tất cả files (*.*)|*.*|PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx";
                openFileDialog.FilterIndex = 2;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDanFile.Text = openFileDialog.FileName;
                }
            }
        }

        private async void btnNopBai_Click(object sender, EventArgs e)
        {
            if (_baiTapHienTai == null) return;

            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(rtbNoiDungNopBai.Text))
                {
                    MessageBox.Show("Vui lòng nhập nội dung nộp bài!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra hạn nộp
                if (_baiTapHienTai.HanNop.HasValue && _baiTapHienTai.HanNop < DateTime.Now)
                {
                    MessageBox.Show("Bài tập đã hết hạn nộp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo bài nộp mới
                var baiNop = new NopBaiTap
                {
                    MaNopBai = Guid.NewGuid(),
                    MaBaiTap = _baiTapHienTai.MaBaiTap,
                    MaHocVien = Guid.Parse(_maHocVien),
                    NoiDungNop = rtbNoiDungNopBai.Text.Trim(),
                    DuongDanFile = txtDuongDanFile.Text,
                    NgayNop = DateTime.Now,
                    TrangThai = 0 // 0: Đã nộp, 1: Đã chấm
                };

                _context.NopBaiTaps.Add(baiNop);
                await _context.SaveChangesAsync();

                MessageBox.Show("Nộp bài thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh lại giao diện
                await TaiDuLieuBaiTap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nộp bài: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = TaiDuLieuBaiTap();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBaiTap_Load(object sender, EventArgs e)
        {
            treeViewBaiTap.AfterSelect += treeViewBaiTap_AfterSelect;
        }

        // Các sự kiện click cho label (nếu cần)
        private void label1_Click(object sender, EventArgs e)
        {
            // Code xử lý nếu cần
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
           Close();
        }
    }
}