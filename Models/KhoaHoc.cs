namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhoaHoc")]
    public partial class KhoaHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoaHoc()
        {
            ChuongHocs = new HashSet<ChuongHoc>();
            DangKyKhoaHocs = new HashSet<DangKyKhoaHoc>();
        }

        [Key]
        public Guid MaKhoaHoc { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDe { get; set; }

        public string MoTa { get; set; }

        public Guid MaGiangVien { get; set; }

        public Guid MaDanhMuc { get; set; }

        public decimal? GiaTien { get; set; }

        [StringLength(500)]
        public string AnhBia { get; set; }

        public int TrinhDo { get; set; }

        public int? TrangThai { get; set; }

        public decimal? DiemDanhGiaTB { get; set; }

        public int? TongHocVien { get; set; }

        public int? TongBaiHoc { get; set; }

        public int? TongThoiGian { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayCapNhat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuongHoc> ChuongHocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyKhoaHoc> DangKyKhoaHocs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
