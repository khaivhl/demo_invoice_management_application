using API_them_sua_xoa_du_lieu.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_them_sua_xoa_du_lieu.Context
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<Loaisanpham> Loaisanpham { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = DESKTOP-GAKP3K6; Database = APIthemsuaxoadulieu   ; Trusted_Connection = True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
    }
}
