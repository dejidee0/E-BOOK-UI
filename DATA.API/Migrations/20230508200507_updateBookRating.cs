using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MODEL.Migrations
{
    public partial class updateBookRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ca71b24-145a-484a-b685-15fb183aeed7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e570095b-f80b-473e-bf2c-2b459334f0ef");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Books",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "378a0ba9-46e8-4538-8afd-c9d6fa92bda0", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8114c7a7-c452-4e9a-952b-fd8e63b34a2d", "1", "ADMIN", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedToLib",
                value: new DateTime(2023, 5, 8, 21, 5, 5, 515, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedToLib",
                value: new DateTime(2023, 5, 8, 21, 5, 5, 515, DateTimeKind.Local).AddTicks(5334));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedToLib",
                value: new DateTime(2023, 5, 8, 21, 5, 5, 515, DateTimeKind.Local).AddTicks(5338));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 8, 21, 5, 5, 515, DateTimeKind.Local).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 5, 8, 21, 5, 5, 515, DateTimeKind.Local).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 5, 8, 21, 5, 5, 515, DateTimeKind.Local).AddTicks(5391));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "378a0ba9-46e8-4538-8afd-c9d6fa92bda0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8114c7a7-c452-4e9a-952b-fd8e63b34a2d");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8ca71b24-145a-484a-b685-15fb183aeed7", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e570095b-f80b-473e-bf2c-2b459334f0ef", "1", "ADMIN", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedToLib",
                value: new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2669));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedToLib",
                value: new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2691));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedToLib",
                value: new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2695));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2876));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2885));
        }
    }
}
