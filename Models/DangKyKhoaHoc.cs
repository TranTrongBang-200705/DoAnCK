namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKyKhoaHoc")]
    public partial class DangKyKhoaHoc
    {
        [Key]
        public Guid MaDangKy { get; set; }

        public Guid MaHocVien { get; set; }

        public Guid MaKhoaHoc { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayDangKy { get; set; }

        public decimal? PhanTramHoanThanh { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayTruyCapCuoi { get; set; }

        public bool? DaHoanThanh { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayHoanThanh { get; set; }

        public int? DiemDanhGia { get; set; }

        [StringLength(1000)]
        public string BinhLuan { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
