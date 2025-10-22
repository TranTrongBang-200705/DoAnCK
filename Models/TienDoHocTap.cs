namespace DoAnCuoiKy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TienDoHocTap")]
    public partial class TienDoHocTap
    {
        [Key]
        public Guid MaTienDo { get; set; }

        public Guid MaHocVien { get; set; }

        public Guid MaBaiHoc { get; set; }

        public bool? DaXem { get; set; }

        public int? ThoiGianXem { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? NgayXem { get; set; }

        public decimal? TiLeHoanThanh { get; set; }

        public virtual BaiHoc BaiHoc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
