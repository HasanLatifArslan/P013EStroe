﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013EStore.Core.Entities;

namespace P013EStroe.Data.Configurations
{
	internal class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Image).HasMaxLength(100);
			builder.Property(x => x.ProductCode).HasMaxLength(50);
			// FluentAPI ile class lar arası ilişki kurma
			builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandId);
			//brnad class ı ile products classı arasında 1 e çok ilişki vardır
			builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
			//category class ı ile products classı arasında 1 e çok ilişki vardır
		}
	}
}