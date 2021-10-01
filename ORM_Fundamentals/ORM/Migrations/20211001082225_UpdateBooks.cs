using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class UpdateBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 11, 22, 25, 0, DateTimeKind.Local).AddTicks(3455));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 11, 22, 25, 0, DateTimeKind.Local).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 11, 22, 24, 997, DateTimeKind.Local).AddTicks(3971));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "Name" },
                values: new object[] { new DateTime(2021, 10, 1, 11, 22, 25, 0, DateTimeKind.Local).AddTicks(1508), "Book2" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Book3");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 10, 1, 11, 22, 25, 0, DateTimeKind.Local).AddTicks(2376));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 11, 17, 44, 523, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 11, 17, 44, 524, DateTimeKind.Local).AddTicks(287));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 10, 1, 11, 17, 44, 519, DateTimeKind.Local).AddTicks(5037));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "Name" },
                values: new object[] { new DateTime(2021, 10, 1, 11, 17, 44, 523, DateTimeKind.Local).AddTicks(6917), "Book1" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Book1");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 10, 1, 11, 17, 44, 523, DateTimeKind.Local).AddTicks(8341));
        }
    }
}
