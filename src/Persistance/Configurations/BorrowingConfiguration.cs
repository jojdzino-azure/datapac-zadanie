using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.Migrations
{
    public class BorrowingConfiguration : IEntityTypeConfiguration<BorrowingEntity>
    {
        public void Configure(EntityTypeBuilder<BorrowingEntity> builder)
        {
            builder.HasOne(e => e.Book);
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id);

            builder.ToTable("Borrowing");
        }
    }
}