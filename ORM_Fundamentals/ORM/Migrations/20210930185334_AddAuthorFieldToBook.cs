using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORM.Migrations
{
    public partial class AddAuthorFieldToBook : Migration
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
                columns: new[] { "Author", "CreatedDateTime" },
                values: new object[] { "Author1", new DateTime(2021, 9, 30, 21, 53, 33, 849, DateTimeKind.Local).AddTicks(9858) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "CreatedDateTime" },
                values: new object[] { "Author2", new DateTime(2021, 9, 30, 21, 53, 33, 852, DateTimeKind.Local).AddTicks(9821) });

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
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 21, 53, 33, 853, DateTimeKind.Local).AddTicks(1280));
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
                value: new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1918));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 49, 40, 303, DateTimeKind.Local).AddTicks(4055));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2021, 9, 30, 21, 49, 40, 306, DateTimeKind.Local).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(468));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1016));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDateTime",
                value: new DateTime(2021, 9, 30, 21, 49, 40, 307, DateTimeKind.Local).AddTicks(1028));
        }
    }
}
