using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class AddAuhtorField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 12, 21, 33, 192, DateTimeKind.Local).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 12, 21, 33, 193, DateTimeKind.Local).AddTicks(43));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "CreatedDateTime" },
                values: new object[] { "Author1", new DateTime(2021, 10, 1, 12, 21, 33, 189, DateTimeKind.Local).AddTicks(9486) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "CreatedDateTime" },
                values: new object[] { "Author2", new DateTime(2021, 10, 1, 12, 21, 33, 192, DateTimeKind.Local).AddTicks(7965) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Author",
                value: "Author2");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 10, 1, 12, 21, 33, 192, DateTimeKind.Local).AddTicks(8837));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 12, 20, 59, 236, DateTimeKind.Local).AddTicks(655));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 12, 20, 59, 236, DateTimeKind.Local).AddTicks(896));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 12, 20, 59, 232, DateTimeKind.Local).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 12, 20, 59, 235, DateTimeKind.Local).AddTicks(8551));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 10, 1, 12, 20, 59, 235, DateTimeKind.Local).AddTicks(9601));
        }
    }
}
