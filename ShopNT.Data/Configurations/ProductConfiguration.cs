using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNT.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x=>x.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.SalePrice).HasColumnType("Money");
            builder.Property(x => x.CostPrice).HasColumnType("Money");
            builder.Property(x => x.DiscountPercent).HasColumnType("Money");
            builder.Property(x=>x.ImageName).HasMaxLength(100);
        }
    }
}
