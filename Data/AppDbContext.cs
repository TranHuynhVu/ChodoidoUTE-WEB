using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Follower> Follower { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionDetail> MissionDetail { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVideo> ProductsVideo { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ServiceDetail> ServiceDetail { get; set; }
        public DbSet<ServicePackage> ServicePackage { get; set; }

        public DbSet<YeuThich> YeuThichs { get; set; }  
        public DbSet<TinNhan> TinNhans { get; set; }

        public DbSet<ChiTietTinNhan> ChiTietTinNhans { get; set; }
        public DbSet<RateStar> RateStars { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
