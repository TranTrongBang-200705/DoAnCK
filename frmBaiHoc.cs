using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmBaiHoc : Form
    {
        private readonly Model1 _context;
        private readonly string _maKhoaHoc;
        private readonly string _maHocVien;
        private List<ChuongHoc> _danhSachChuong;
        private List<BaiHoc> _danhSachBaiHoc;
        private List<TienDoHocTap> _tienDoHocVien;
        private BaiHoc _baiHocHienTai;
        private int _baiHocIndex = 0;

        // Dictionary để lưu tiến độ cho từng bài học
        private Dictionary<Guid, (double progress, int seconds)> _tienDoTheoBaiHoc;
        private Timer _progressTimer;
        private bool _isVideoPlaying = false;

        public frmBaiHoc(string maKhoaHoc, string maHocVien)
        {
            InitializeComponent();

            _maKhoaHoc = maKhoaHoc;
            _maHocVien = maHocVien;
            _context = new Model1();
            _tienDoTheoBaiHoc = new Dictionary<Guid, (double, int)>();

            SetupProgressTracking();
            Load += async (s, e) => await TaiDuLieuKhoaHoc();
        }

        private void SetupProgressTracking()
        {
            _progressTimer = new Timer();
            _progressTimer.Interval = 2000; // ⚡ GIẢM: 2 giây cập nhật 1 lần (thay vì 5 giây)
            _progressTimer.Tick += async (s, e) => await CapNhatTienDoVideo();
        }

        private async Task TaiDuLieuKhoaHoc()
        {
            try
            {
                // 1. Load thông tin khóa học
                var khoaHoc = await _context.KhoaHocs
                    .FirstOrDefaultAsync(kh => kh.MaKhoaHoc.ToString() == _maKhoaHoc);

                lblTenKhoaHoc.Text = khoaHoc?.TieuDe ?? "Khóa học";

                // 2. Load danh sách chương học
                _danhSachChuong = await _context.ChuongHocs
                    .Where(ch => ch.MaKhoaHoc.ToString() == _maKhoaHoc)
                    .OrderBy(ch => ch.ThuTu)
                    .ToListAsync();

                // 3. Load danh sách bài học
                var maChuongs = _danhSachChuong.Select(ch => ch.MaChuong).ToList();
                _danhSachBaiHoc = await _context.BaiHocs
                    .Where(bh => maChuongs.Contains(bh.MaChuong) && bh.TrangThai == 1)
                    .OrderBy(bh => bh.ThuTu)
                    .ToListAsync();

                // 4. 🔥 THAY ĐỔI: Gọi phương thức async mới
                await KhoiTaoTienDoTuCSDL();

                // 5. Hiển thị danh sách bài học
                HienThiDanhSachBaiHoc();

                // 6. Hiển thị bài học đầu tiên
                if (_danhSachBaiHoc.Any())
                {
                    _baiHocHienTai = _danhSachBaiHoc.First();
                    _baiHocIndex = 0;
                    HienThiBaiHoc(_baiHocHienTai);
                }

                // 7. Cập nhật thống kê
                await CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        // KHỞI TẠO TIẾN ĐỘ TỪ CSDL CHO TẤT CẢ BÀI HỌC
        // KHỞI TẠO TIẾN ĐỘ TỪ CSDL CHO TẤT CẢ BÀI HỌC
        private async Task KhoiTaoTienDoTuCSDL()
        {
            try
            {
                _tienDoTheoBaiHoc.Clear();

                // 🔥 QUAN TRỌNG: Tạo context MỚI và force refresh
                using (var freshContext = new Model1())
                {
                    // 🔥 QUAN TRỌNG: Tắt auto-detect changes để load dữ liệu mới
                    freshContext.Configuration.AutoDetectChangesEnabled = false;

                    var maBaiHocs = _danhSachBaiHoc.Select(bh => bh.MaBaiHoc).ToList();

                    // 🔥 QUAN TRỌNG: Load với AsNoTracking để tránh cache
                    var tienDoMoi = await freshContext.TienDoHocTaps
                        .AsNoTracking() // QUAN TRỌNG: Không cache dữ liệu
                        .Where(td => td.MaHocVien.ToString() == _maHocVien &&
                                   maBaiHocs.Contains(td.MaBaiHoc))
                        .ToListAsync();

                    _tienDoHocVien = tienDoMoi;

                    foreach (var baiHoc in _danhSachBaiHoc)
                    {
                        var tienDo = tienDoMoi.FirstOrDefault(td => td.MaBaiHoc == baiHoc.MaBaiHoc);

                        if (tienDo != null)
                        {
                            _tienDoTheoBaiHoc[baiHoc.MaBaiHoc] = (
                                (double)tienDo.TiLeHoanThanh,
                                tienDo.ThoiGianXem ?? 0
                            );
                            Console.WriteLine($"🔥 Load từ CSDL - Bài {baiHoc.TieuDeBaiHoc}: {tienDo.TiLeHoanThanh}%");
                        }
                        else
                        {
                            // 🔥 QUAN TRỌNG: Khởi tạo mới với context chính
                            await KhoiTaoTienDoMoi(baiHoc.MaBaiHoc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khởi tạo tiến độ: {ex.Message}");
            }
        }

        // 🔥 HÀM MỚI: Khởi tạo tiến độ mới
        private async Task KhoiTaoTienDoMoi(Guid maBaiHoc)
        {
            try
            {
                // Tạo bản ghi tiến độ mới trong CSDL
                var tienDoMoi = new TienDoHocTap
                {
                    MaTienDo = Guid.NewGuid(),
                    MaHocVien = Guid.Parse(_maHocVien),
                    MaBaiHoc = maBaiHoc,
                    DaXem = false,
                    ThoiGianXem = 0,
                    TiLeHoanThanh = 0,
                    NgayXem = null
                };

                _context.TienDoHocTaps.Add(tienDoMoi);
                await _context.SaveChangesAsync();

                // Cập nhật dictionary
                _tienDoTheoBaiHoc[maBaiHoc] = (0, 0);

                Console.WriteLine($"✅ Đã tạo mới tiến độ cho bài học: {maBaiHoc}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi tạo tiến độ mới: {ex.Message}");
            }
        }

        private void HienThiDanhSachBaiHoc()
        {
            treeViewBaiHoc.Nodes.Clear();

            foreach (var chuong in _danhSachChuong)
            {
                var nodeChuong = new TreeNode($"Chương {chuong.ThuTu}: {chuong.TieuDeChuong}");

                var baiHocTrongChuong = _danhSachBaiHoc
                    .Where(bh => bh.MaChuong == chuong.MaChuong)
                    .OrderBy(bh => bh.ThuTu)
                    .ToList();

                foreach (var baiHoc in baiHocTrongChuong)
                {
                    var tienDo = _tienDoTheoBaiHoc.ContainsKey(baiHoc.MaBaiHoc)
                        ? _tienDoTheoBaiHoc[baiHoc.MaBaiHoc].progress
                        : 0;

                    var icon = GetIconForBaiHoc(tienDo);
                    var nodeBai = new TreeNode($"{icon} Bài {baiHoc.ThuTu}: {baiHoc.TieuDeBaiHoc} ({tienDo:0}%)");
                    nodeBai.Tag = baiHoc;
                    nodeChuong.Nodes.Add(nodeBai);
                }

                treeViewBaiHoc.Nodes.Add(nodeChuong);
                nodeChuong.Expand();
            }

            treeViewBaiHoc.AfterSelect += TreeViewBaiHoc_AfterSelect;
        }

        private string GetIconForBaiHoc(double tienDo)
        {
            if (tienDo >= 90) return "✓";
            if (tienDo > 0) return "►";
            return "○";
        }

        private void TreeViewBaiHoc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is BaiHoc baiHocDuocChon)
            {
                // 🔥 QUAN TRỌNG: Tìm bài học bằng MaBaiHoc thay vì object reference
                var baiHocTrongDanhSach = _danhSachBaiHoc.FirstOrDefault(bh => bh.MaBaiHoc == baiHocDuocChon.MaBaiHoc);

                if (baiHocTrongDanhSach != null)
                {
                    _baiHocHienTai = baiHocTrongDanhSach;
                    _baiHocIndex = _danhSachBaiHoc.IndexOf(baiHocTrongDanhSach);

                    Console.WriteLine($"🎯 Đã chọn: Bài {_baiHocIndex + 1} - {_baiHocHienTai.TieuDeBaiHoc}");
                    HienThiBaiHoc(_baiHocHienTai);
                }
                else
                {
                    Console.WriteLine($"❌ Không tìm thấy bài học trong danh sách: {baiHocDuocChon.TieuDeBaiHoc}");
                }
            }
        }

        private void HienThiBaiHoc(BaiHoc baiHoc)
        {
            Console.WriteLine($"🎯 Hiển thị bài học: {baiHoc.TieuDeBaiHoc}");

            // Dừng timer hiện tại
            _progressTimer.Stop();
            _isVideoPlaying = false;
            Console.WriteLine("⏹️ Đã STOP timer");

            // Hiển thị tiêu đề
            lblTieuDeBaiHoc.Text = baiHoc.TieuDeBaiHoc;

            // Hiển thị tiến độ
            if (_tienDoTheoBaiHoc.ContainsKey(baiHoc.MaBaiHoc))
            {
                var (progress, seconds) = _tienDoTheoBaiHoc[baiHoc.MaBaiHoc];
                progressBaiHoc.Value = (int)progress;
                lblTienDoBaiHoc.Text = $"{progress:0}% hoàn thành";
                Console.WriteLine($"📊 Tiến độ hiện tại: {progress}% ({seconds}s)");
            }
            else
            {
                progressBaiHoc.Value = 0;
                lblTienDoBaiHoc.Text = "0% hoàn thành";
                Console.WriteLine("📊 Tiến độ hiện tại: 0% (chưa có dữ liệu)");
            }

            // Hiển thị video (nếu có)
            XuLyHienThiVideo(baiHoc);
            // Dừng timer hiện tại
            _progressTimer.Stop();
            _isVideoPlaying = false;

            // Hiển thị tiêu đề
            lblTieuDeBaiHoc.Text = baiHoc.TieuDeBaiHoc;

            // 🔥 HIỂN THỊ TIẾN ĐỘ CỦA BÀI HỌC NÀY
            if (_tienDoTheoBaiHoc.ContainsKey(baiHoc.MaBaiHoc))
            {
                var (progress, seconds) = _tienDoTheoBaiHoc[baiHoc.MaBaiHoc];
                progressBaiHoc.Value = (int)progress;
                lblTienDoBaiHoc.Text = $"{progress:0}% hoàn thành";
                Console.WriteLine($"🎯 Hiển thị bài {baiHoc.TieuDeBaiHoc}: {progress}%");
            }
            else
            {
                progressBaiHoc.Value = 0;
                lblTienDoBaiHoc.Text = "0% hoàn thành";
            }

            // Hiển thị video (nếu có)
            XuLyHienThiVideo(baiHoc);

            // Hiển thị nội dung bài học
            rtbNoiDung.Text = baiHoc.NoiDung;

            // Hiển thị thông tin bài học
            lblThongTinBaiHoc.Text = $"🕒 Thời lượng: {baiHoc.ThoiLuong} phút\n" +
                                   $"🎯 Loại bài: {GetKieuBaiHocText(baiHoc.KieuBaiHoc)}";

            // Cập nhật nút điều hướng
            btnTruoc.Enabled = _baiHocIndex > 0;
            btnSau.Enabled = _baiHocIndex < _danhSachBaiHoc.Count - 1;
        }

        private async void XuLyHienThiVideo(BaiHoc baiHoc)
        {
            if (string.IsNullOrEmpty(baiHoc.DuongDanVideo))
            {
                webView2Video.Visible = false;
                return;
            }

            webView2Video.Visible = true;

            try
            {
                if (webView2Video.CoreWebView2 == null)
                {
                    await webView2Video.EnsureCoreWebView2Async(null);
                }

                // Load video
                webView2Video.CoreWebView2.Navigate(baiHoc.DuongDanVideo);

                // Bắt đầu theo dõi sau 3 giây
                await Task.Delay(3000);

                _isVideoPlaying = true;
                _progressTimer.Start();

                Console.WriteLine($"▶️ Bắt đầu theo dõi video: {baiHoc.TieuDeBaiHoc}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load video: {ex.Message}");
                _isVideoPlaying = false;
            }
        }

        private string GetKieuBaiHocText(int kieuBaiHoc)
        {
            return kieuBaiHoc switch
            {
                0 => "Video",
                1 => "Văn bản",
                2 => "Quiz",
                _ => "Không xác định"
            };
        }

        private async Task CapNhatTienDoVideo()
        {
            Console.WriteLine($"🔴 TIMER CHẠY - Bài hiện tại: {_baiHocHienTai?.TieuDeBaiHoc}, IsPlaying: {_isVideoPlaying}");

            if (_baiHocHienTai == null)
            {
                Console.WriteLine("❌ _baiHocHienTai là NULL");
                return;
            }

            if (!_isVideoPlaying)
            {
                Console.WriteLine("❌ _isVideoPlaying là FALSE");
                return;
            }

            try
            {
                // 🔥 KIỂM TRA XEM BÀI HỌC CÓ TRONG DICTIONARY KHÔNG
                if (!_tienDoTheoBaiHoc.ContainsKey(_baiHocHienTai.MaBaiHoc))
                {
                    Console.WriteLine($"⚠️ Bài học chưa có trong dictionary, đang khởi tạo...");
                    await KhoiTaoTienDoMoi(_baiHocHienTai.MaBaiHoc);

                    // Kiểm tra lại sau khi khởi tạo
                    if (!_tienDoTheoBaiHoc.ContainsKey(_baiHocHienTai.MaBaiHoc))
                    {
                        Console.WriteLine($"❌ Vẫn không có bài học trong dictionary sau khi khởi tạo");
                        return;
                    }
                }

                var (currentProgress, currentSeconds) = _tienDoTheoBaiHoc[_baiHocHienTai.MaBaiHoc];

                Console.WriteLine($"📊 Trước khi cập nhật - Thời gian: {currentSeconds}s, Tiến độ: {currentProgress}%");

                // TĂNG THỜI GIAN
                int newSeconds = currentSeconds + 10;
                double progressIncrement = TinhPhanTramTangThem(10, (int)_baiHocHienTai.ThoiLuong);
                double newProgress = Math.Min(currentProgress + progressIncrement, 100);

                Console.WriteLine($"⏱️ Sau khi cập nhật - Thời gian: {newSeconds}s, Tiến độ: {newProgress}%");

                // Cập nhật dictionary
                _tienDoTheoBaiHoc[_baiHocHienTai.MaBaiHoc] = (newProgress, newSeconds);

                // Cập nhật UI
                progressBaiHoc.Value = (int)newProgress;
                lblTienDoBaiHoc.Text = $"{newProgress:0}% hoàn thành";

                // Cập nhật CSDL
                await CapNhatTienDoDatabase(_baiHocHienTai.MaBaiHoc, newProgress, newSeconds);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi cập nhật tiến độ: {ex.Message}");
            }
        }


        // 🔥 TÍNH PHẦN TRĂM TĂNG THÊM cho mỗi khoảng thời gian
        private double TinhPhanTramTangThem(int secondsToAdd, int thoiLuongBaiHoc)
        {
            if (thoiLuongBaiHoc <= 0)
                return 0;

            // Chuyển thời lượng bài học từ phút sang giây
            int totalSeconds = thoiLuongBaiHoc * 60;

            // ⚡ TĂNG: Tính % tăng thêm nhanh hơn
            double increment = (secondsToAdd / (double)totalSeconds) * 100;

            // ⚡ TĂNG: Giới hạn tối đa 10% mỗi lần cập nhật (thay vì 5%)
            return Math.Min(increment, 10.0);
        }

        private async Task CapNhatTienDoDatabase(Guid maBaiHoc, double progress, int seconds)
        {
            try
            {
                // 1. Cập nhật tiến độ bài học
                await _context.Database.ExecuteSqlCommandAsync(
                    "EXEC sp_CapNhatTienDoHocTap @p0, @p1, @p2, @p3",
                    _maHocVien, maBaiHoc.ToString(), seconds, (decimal)progress
                );

                // 2. Cập nhật tiến độ khóa học
                try
                {
                    await _context.Database.ExecuteSqlCommandAsync(
                        "EXEC sp_TinhPhanTramHoanThanhKhoaHoc @p0, @p1",
                        _maHocVien, _maKhoaHoc
                    );
                    Console.WriteLine("✅ Đã gọi stored procedure");
                }
                catch
                {
                    Console.WriteLine("🔄 Stored procedure lỗi, dùng phương thức trực tiếp");
                    await CapNhatTrucTiepDangKyKhoaHoc();
                }

                // 3. 🔥 QUAN TRỌNG: Refresh lại dữ liệu từ CSDL
                await KhoiTaoTienDoTuCSDL();

                // 4. Cập nhật UI
                await KiemTraVaCapNhatDangKyKhoaHoc();

                Console.WriteLine($"🎉 Đã cập nhật hoàn tất: {progress}%");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi cập nhật CSDL: {ex.Message}");
            }
        }

        // 🔥 PHƯƠNG THỨC MỚI: Kiểm tra và cập nhật trực tiếp DangKyKhoaHoc
        private async Task KiemTraVaCapNhatDangKyKhoaHoc()
        {
            try
            {
                // Đọc dữ liệu mới nhất từ DangKyKhoaHoc
                var dangKy = await _context.DangKyKhoaHocs
                    .FirstOrDefaultAsync(dk => dk.MaHocVien.ToString() == _maHocVien &&
                                               dk.MaKhoaHoc.ToString() == _maKhoaHoc);

                if (dangKy != null)
                {
                    Console.WriteLine($"📊 DangKyKhoaHoc - PhanTramHoanThanh: {dangKy.PhanTramHoanThanh}%, DaHoanThanh: {dangKy.DaHoanThanh}");

                    // Cập nhật UI ngay lập tức
                    this.Invoke(new Action(() => {
                        progressTongQuat.Value = (int)(dangKy.PhanTramHoanThanh ?? 0);
                        lblThongKe.Text = $"📊 THỐNG KÊ:\n" +
                                        $"• % hoàn thành khóa: {dangKy.PhanTramHoanThanh:0}%\n" +
                                        $"• Trạng thái: {(dangKy.DaHoanThanh == true ? "✅ Đã hoàn thành" : "📚 Đang học")}";
                    }));
                }
                else
                {
                    Console.WriteLine("❌ Không tìm thấy bản ghi DangKyKhoaHoc");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi kiểm tra DangKyKhoaHoc: {ex.Message}");
            }
        }
        // 🔥 DỰ PHÒNG: Cập nhật trực tiếp nếu stored procedure không hoạt động
        private async Task CapNhatTrucTiepDangKyKhoaHoc()
        {
            try
            {
                // Tính toán phần trăm hoàn thành khóa học
                var tongBaiHoc = _danhSachBaiHoc.Count;
                var soBaiHocDaHoanThanh = _tienDoHocVien.Count(td => td.TiLeHoanThanh >= 90);

                var phanTramHoanThanh = tongBaiHoc > 0 ? (soBaiHocDaHoanThanh * 100.0) / tongBaiHoc : 0;
                var daHoanThanh = phanTramHoanThanh >= 100;

                // Cập nhật trực tiếp vào DangKyKhoaHoc
                var dangKy = await _context.DangKyKhoaHocs
                    .FirstOrDefaultAsync(dk => dk.MaHocVien.ToString() == _maHocVien &&
                                               dk.MaKhoaHoc.ToString() == _maKhoaHoc);

                if (dangKy != null)
                {
                    dangKy.PhanTramHoanThanh = (decimal)phanTramHoanThanh;
                    dangKy.DaHoanThanh = daHoanThanh;
                    dangKy.NgayTruyCapCuoi = DateTime.Now;

                    if (daHoanThanh && dangKy.NgayHoanThanh == null)
                    {
                        dangKy.NgayHoanThanh = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();
                    Console.WriteLine($"🔥 Đã cập nhật TRỰC TIẾP DangKyKhoaHoc: {phanTramHoanThanh:0}%");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật trực tiếp DangKyKhoaHoc: {ex.Message}");
            }
        }
        private async Task CapNhatThongKe()
        {
            try
            {
                var dangKy = await _context.DangKyKhoaHocs
                    .FirstOrDefaultAsync(dk => dk.MaHocVien.ToString() == _maHocVien &&
                                               dk.MaKhoaHoc.ToString() == _maKhoaHoc);

                var phanTramThat = dangKy?.PhanTramHoanThanh ?? 0;

                lblThongKe.Text = $"📊 THỐNG KÊ:\n" +
                                $"• % hoàn thành khóa: {phanTramThat:0}%\n" +
                                $"• (Dữ liệu thật từ CSDL)";

                progressTongQuat.Value = (int)phanTramThat;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật thống kê: {ex.Message}");
            }
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sử dụng controls trên video player để điều khiển");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _progressTimer?.Stop();
            this.Close();
        }

        private void frmBaiHoc_Load(object sender, EventArgs e)
        {
            // Khởi tạo thêm nếu cần
        }

        private void btnTruoc_Click_1(object sender, EventArgs e)
        {
            if (_baiHocIndex > 0)
            {
                _baiHocIndex--;
                _baiHocHienTai = _danhSachBaiHoc[_baiHocIndex];
                HienThiBaiHoc(_baiHocHienTai);
            }
        }

        private void btnSau_Click_1(object sender, EventArgs e)
        {
            if (_baiHocIndex < _danhSachBaiHoc.Count - 1)
            {
                _baiHocIndex++;
                _baiHocHienTai = _danhSachBaiHoc[_baiHocIndex];
                HienThiBaiHoc(_baiHocHienTai);
            }
        }
    }
}