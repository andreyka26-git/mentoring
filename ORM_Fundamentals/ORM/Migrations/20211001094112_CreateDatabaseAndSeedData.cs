using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class CreateDatabaseAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Citations = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Citations", "Content", "CreatedDateTime" },
                values: new object[,]
                {
                    { 1, 10, "Content1", new DateTime(2011, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 10, "Content2", new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedDateTime", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2001, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book1" },
                    { 2, new DateTime(1999, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book2" },
                    { 3, new DateTime(2006, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book3" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Mark", "ReviewDateTime", "ReviewerName" },
                values: new object[] { 1, 1, 4, new DateTime(2005, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name1" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Mark", "ReviewDateTime", "ReviewerName" },
                values: new object[] { 2, 1, 2, new DateTime(2012, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name2" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Mark", "ReviewDateTime", "ReviewerName" },
                values: new object[] { 3, 3, 5, new DateTime(2012, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name3" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
