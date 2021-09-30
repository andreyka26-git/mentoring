using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Items_BookId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Citations",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Books");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

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

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Citations", "Content", "CreatedDateTime" },
                values: new object[,]
                {
                    { 1, 10, "Content1", new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1660) },
                    { 2, 10, "Content2", new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1918) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedDateTime", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 30, 21, 49, 40, 303, DateTimeKind.Local).AddTicks(4055), "Book1" },
                    { 2, new DateTime(2021, 9, 30, 21, 49, 40, 306, DateTimeKind.Local).AddTicks(8870), "Book1" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Mark", "ReviewDateTime", "ReviewerName" },
                values: new object[] { 1, 1, 4, new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(468), "Name1" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Mark", "ReviewDateTime", "ReviewerName" },
                values: new object[] { 2, 1, 2, new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1016), "Name2" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Mark", "ReviewDateTime", "ReviewerName" },
                values: new object[] { 3, 2, 5, new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1028), "Name3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Citations",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Items_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
