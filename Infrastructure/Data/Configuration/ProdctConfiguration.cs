using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProdctConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.Name).IsRequired();
            builder.Property(product => product.Description).IsRequired();
            builder.Property(product => product.Price).IsRequired();
            builder.Property(product => product.PictureUrl).IsRequired();
            builder.HasOne(product => product.ProductBrand).WithMany()
                   .HasForeignKey(product =>product.ProductBrandId);
            builder.HasOne(product => product.ProductType).WithMany()
                   .HasForeignKey(product =>product.ProductTypeId);
        }
    }
}