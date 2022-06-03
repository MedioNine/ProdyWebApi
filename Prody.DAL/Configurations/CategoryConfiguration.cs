using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prody.DAL.Entities;

namespace Prody.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(category => category.Id);

            builder
                .Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
