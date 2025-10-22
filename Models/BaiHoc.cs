namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiHoc")]
    public partial class BaiHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiHoc()
        {
            BaiTaps = new HashSet<BaiTap>();
            TienDoHocTaps = new HashSet<TienDoHocTap>();
        }

        [Key]
        public Guid MaBaiHoc { get; set; }

        public Guid MaChuong { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDeBaiHoc { get; set; }

        public string NoiDung { get; set; }

        [StringLength(500)]
        public string DuongDanVideo { get; set; }

        public int? ThoiLuong { get; set; }

        public int ThuTu { get; set; }

        public int KieuBaiHoc { get; set; }

        public int? TrangThai { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayTao { get; set; }

        public virtual ChuongHoc ChuongHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiTap> BaiTaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TienDoHocTap> TienDoHocTaps { get; set; }
    }
}
