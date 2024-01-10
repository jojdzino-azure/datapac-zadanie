using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Migrations
{
    public class BorrowingConfiguration : IEntityTypeConfiguration<BorrowingEntity>
    {
        public void Configure(EntityTypeBuilder<BorrowingEntity> builder)
        {
            builder.HasOne(e => e.Book);
            builder.ToTable("Borrowing");
        }
    }
}