using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<BorrowingEntity> Borrowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    Id = 1,
                    Guid = new Guid("7fdeb6bd-8fdc-4618-8284-a5e2e1e44919"),
                    BirthDate = new DateTime(1996, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc),
                    Email = "jozef.schneider.95@gmail.com",
                    FirstName = "Jozef",
                    LastName = "Schneider",
                    RegisteredAt = new DateTime(2022, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc)
                },
                new UserEntity()
                {
                    Id = 2,
                    Guid = new Guid("b2572760-2335-4ab4-a3b9-173a1ab3f85e"),
                    BirthDate = new DateTime(1998, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc),
                    Email = "somerandommail@gmail.com",
                    FirstName = "Daniel",
                    LastName = "Vidlicka",
                    RegisteredAt = new DateTime(2023, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc)
                }
            );
        }
    }
}