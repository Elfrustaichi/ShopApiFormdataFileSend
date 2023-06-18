using Microsoft.EntityFrameworkCore;
using ShopNT.Core.Entities;
using ShopNT.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Data
{
    public class ShopNTDbContext:DbContext
    {
        public ShopNTDbContext(DbContextOptions<ShopNTDbContext> options):base(options)
        {
            
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
