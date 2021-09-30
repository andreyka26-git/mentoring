using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class UpdateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 20, 2, 200, DateTimeKind.Local).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 20, 2, 200, DateTimeKind.Local).AddTicks(3863));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 20, 2, 196, DateTimeKind.Local).AddTicks(9835));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 22, 20, 2, 200, DateTimeKind.Local).AddTicks(1512));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 22, 20, 2, 200, DateTimeKind.Local).AddTicks(2496));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 22, 20, 2, 200, DateTimeKind.Local).AddTicks(3027));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDateTime",
                value: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 22, 17, 48, 343, DateTimeKind.Local).AddTicks(4544));
        }
    }
}
