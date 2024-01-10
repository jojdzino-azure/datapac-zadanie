using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Migrations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id);
            builder.HasIndex(e => e.Guid);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.ToTable("User");
        }
    }
}