using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class AddFieldForTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(5083));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(5520));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 340, DateTimeKind.Local).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(3133));

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CreatedDateTime", "Name" },
                values: new object[] { 3, "Author2", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), "Book1" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(4038));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(4534));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "ReviewDateTime" },
                values: new object[] { 3, new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(4544) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 853, DateTimeKind.Local).AddTicks(1852));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 853, DateTimeKind.Local).AddTicks(2104));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 849, DateTimeKind.Local).AddTicks(9858));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 852, DateTimeKind.Local).AddTicks(9821));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 853, DateTimeKind.Local).AddTicks(767));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 853, DateTimeKind.Local).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "ReviewDateTime" },
                values: new object[] { 2, new DateTime(2021, 9, 30, 21, 53, 33, 853, DateTimeKind.Local).AddTicks(1280) });
        }
    }
}
