namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DangKyKhoaHocs = new HashSet<DangKyKhoaHoc>();
            KhoaHocs = new HashSet<KhoaHoc>();
            NopBaiTaps = new HashSet<NopBaiTap>();
            TienDoHocTaps = new HashSet<TienDoHocTap>();
        }

        [Key]
        public Guid MaNguoiDung { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhauHash { get; set; }

        [Required]
        [StringLength(100)]
        public string Ho { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        public int VaiTro { get; set; }

        [StringLength(500)]
        public string AnhDaiDien { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayTao { get; set; }

        public bool? DaKhoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyKhoaHoc> DangKyKhoaHocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHoc> KhoaHocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NopBaiTap> NopBaiTaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TienDoHocTap> TienDoHocTaps { get; set; }
    }
}
