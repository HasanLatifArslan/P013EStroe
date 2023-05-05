using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013EStore.Core.Entities;

namespace P013EStroe.Data.Configurations
{
	internal class SliderConfigurations : IEntityTypeConfiguration<Slider>
	{
		public void Configure(EntityTypeBuilder<Slider> builder)
		{
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Image).HasMaxLength(100);
			builder.Property(x => x.Link).HasMaxLength(100);
			builder.Property(x => x.Description).HasMaxLength(500);
		}
	}
}
