using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Migrations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Book");
            builder.Property(e => e.Name).HasMaxLength(256);
            builder.Property(e => e.Description).HasMaxLength(4000);
            builder.Property(e => e.InternalComment).IsRequired(false);
        }
    }
}