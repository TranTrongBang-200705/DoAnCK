namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiTap")]
    public partial class BaiTap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiTap()
        {
            NopBaiTaps = new HashSet<NopBaiTap>();
        }

        [Key]
        public Guid MaBaiTap { get; set; }

        public Guid MaBaiHoc { get; set; }

        [Required]
        [StringLength(255)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? HanNop { get; set; }

        public decimal? DiemToiDa { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayTao { get; set; }

        public virtual BaiHoc BaiHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NopBaiTap> NopBaiTaps { get; set; }
    }
}
