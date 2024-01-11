using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistance.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorName = table.Column<string>(type: "text", nullable: false),
                    InternalComment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Borrowing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BorrowedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BorrowedForDays = table.Column<int>(type: "integer", nullable: false),
                    ReturnedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notified = table.Column<bool>(type: "boolean", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    BorrowedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrowing_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrowing_User_BorrowedById",
                        column: x => x.BorrowedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "Guid", "LastName", "RegisteredAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1996, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc), "jozef.schneider.95@gmail.com", "Jozef", new Guid("7fdeb6bd-8fdc-4618-8284-a5e2e1e44919"), "Schneider", new DateTime(2022, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc) },
                    { 2, new DateTime(1998, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc), "somerandommail@gmail.com", "Daniel", new Guid("b2572760-2335-4ab4-a3b9-173a1ab3f85e"), "Vidlicka", new DateTime(2023, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BookId",
                table: "Borrowing",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BorrowedById",
                table: "Borrowing",
                column: "BorrowedById");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_Id",
                table: "Borrowing",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Guid",
                table: "User",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrowing");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
