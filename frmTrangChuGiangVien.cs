using DoAnCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class frmTrangChuGiangVien : Form
    {
        private readonly NguoiDung _nguoiDung;
        private readonly Model1 _context;
        public frmTrangChuGiangVien(NguoiDung nguoiDung, Model1 context)
        {
            InitializeComponent();
            _nguoiDung = nguoiDung;
            _context = context;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void frmTrangChuGiangVien_Load_1(object sender, EventArgs e)
        {
            label3.Text = $"{_nguoiDung.Ho} {_nguoiDung.Ten}";
            LoadThongBaoCaNhan();
            LoadThongBaoChung();
        }
        private Label TaoLabelThongBao(string text)
        {
            return new Label
            {
                Text = "• " + text,
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40),
                MaximumSize = new Size(350, 0), // tự xuống dòng nếu dài
                Margin = new Padding(5, 5, 5, 8)
            };
        }

        // 🧩 Hiển thị thông báo cá nhân
        private void LoadThongBaoCaNhan()
        {
            // Xóa tất cả label cũ trong panel
            flpThongBaoCaNhan.Controls.Clear();

            var thongBaoCaNhan = new List<string>
            {
                "Học viên Nguyễn Văn A vừa nộp bài 'SQL – Chương 3'.",
                "Lê Minh vừa đăng ký khóa học 'C# Căn Bản'.",
                "Có 2 bài tập mới đã được học viên hoàn thành."
            };

            foreach (var tb in thongBaoCaNhan)
            {
                var lbl = TaoLabelThongBao(tb);
                flpThongBaoCaNhan.Controls.Add(lbl);
            }
        }

        // 🧩 Hiển thị thông báo chung
        private void LoadThongBaoChung()
        {
            flpThongBaoChung.Controls.Clear();

            var thongBaoChung = new List<string>
            {
                "📢 Hệ thống sẽ bảo trì vào 0h ngày 30/10.",
                "🎉 Cuộc thi thiết kế khóa học sáng tạo bắt đầu ngày 1/11.",
                "⚙️ Tính năng chấm bài tự động sẽ được cập nhật trong tuần này."
            };

            foreach (var tb in thongBaoChung)
            {
                var lbl = TaoLabelThongBao(tb);
                flpThongBaoChung.Controls.Add(lbl);
            }
        }
    }
}
