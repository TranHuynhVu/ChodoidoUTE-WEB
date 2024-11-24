using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
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
    }
}
