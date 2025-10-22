using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAnCuoiKy.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ELearningPlatform")
        {
        }

        public virtual DbSet<BaiHoc> BaiHocs { get; set; }
        public virtual DbSet<BaiTap> BaiTaps { get; set; }
        public virtual DbSet<ChuongHoc> ChuongHocs { get; set; }
        public virtual DbSet<DangKyKhoaHoc> DangKyKhoaHocs { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NopBaiTap> NopBaiTaps { get; set; }
        public virtual DbSet<TienDoHocTap> TienDoHocTaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiHoc>()
                .HasMany(e => e.BaiTaps)
                .WithRequired(e => e.BaiHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaiHoc>()
                .HasMany(e => e.TienDoHocTaps)
                .WithRequired(e => e.BaiHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaiTap>()
                .Property(e => e.DiemToiDa)
                .HasPrecision(5, 2);

            modelBuilder.Entity<BaiTap>()
                .HasMany(e => e.NopBaiTaps)
                .WithRequired(e => e.BaiTap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChuongHoc>()
                .HasMany(e => e.BaiHocs)
                .WithRequired(e => e.ChuongHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DangKyKhoaHoc>()
                .Property(e => e.PhanTramHoanThanh)
                .HasPrecision(5, 2);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.DanhMuc1)
                .WithOptional(e => e.DanhMuc2)
                .HasForeignKey(e => e.MaDanhMucCha);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.KhoaHocs)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.GiaTien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.DiemDanhGiaTB)
                .HasPrecision(3, 2);

            modelBuilder.Entity<KhoaHoc>()
                .HasMany(e => e.ChuongHocs)
                .WithRequired(e => e.KhoaHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhoaHoc>()
                .HasMany(e => e.DangKyKhoaHocs)
                .WithRequired(e => e.KhoaHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.DangKyKhoaHocs)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.MaHocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.KhoaHocs)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.MaGiangVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.NopBaiTaps)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.MaHocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.TienDoHocTaps)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.MaHocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NopBaiTap>()
                .Property(e => e.Diem)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TienDoHocTap>()
                .Property(e => e.TiLeHoanThanh)
                .HasPrecision(5, 2);
        }
    }
}
