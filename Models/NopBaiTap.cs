namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NopBaiTap")]
    public partial class NopBaiTap
    {
        [Key]
        public Guid MaNopBai { get; set; }

        public Guid MaBaiTap { get; set; }

        public Guid MaHocVien { get; set; }

        public string NoiDungNop { get; set; }

        [StringLength(500)]
        public string DuongDanFile { get; set; }

        public decimal? Diem { get; set; }

        [StringLength(1000)]
        public string NhanXet { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayNop { get; set; }

        public int? TrangThai { get; set; }

        public virtual BaiTap BaiTap { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
