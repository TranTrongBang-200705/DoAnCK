namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongHoc")]
    public partial class ChuongHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongHoc()
        {
            BaiHocs = new HashSet<BaiHoc>();
        }

        [Key]
        public Guid MaChuong { get; set; }

        public Guid MaKhoaHoc { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDeChuong { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        public int ThuTu { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayTao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiHoc> BaiHocs { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
