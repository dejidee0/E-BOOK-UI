using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MODEL.Migrations
{
    public partial class seedReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f662384f-edec-4ddf-b6ea-ff4e00386a17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb28410d-7147-45f7-8703-8668957ca0b6");

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

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AppUserId", "BookId", "Comment", "DateCreated", "Rating", "Title" },
                values: new object[] { 1, "9408eccf-0b8b-4d88-b951-e10f83198e18", 3, "The book was lengthy but I loved it", new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2876), 4, "Golden boy" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AppUserId", "BookId", "Comment", "DateCreated", "Rating", "Title" },
                values: new object[] { 2, "9408eccf-0b8b-4d88-b951-e10f83198e18", 2, "The books were lengthy but I loved it", new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2881), 3, "Golden girl" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AppUserId", "BookId", "Comment", "DateCreated", "Rating", "Title" },
                values: new object[] { 3, "9408eccf-0b8b-4d88-b951-e10f83198e18", 2, "The book was lengthy but I loved it", new DateTime(2023, 4, 30, 16, 19, 28, 47, DateTimeKind.Local).AddTicks(2885), 4, "Golden woman" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ca71b24-145a-484a-b685-15fb183aeed7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e570095b-f80b-473e-bf2c-2b459334f0ef");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f662384f-edec-4ddf-b6ea-ff4e00386a17", "1", "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb28410d-7147-45f7-8703-8668957ca0b6", "2", "USER", "USER" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedToLib",
                value: new DateTime(2023, 4, 30, 16, 18, 51, 28, DateTimeKind.Local).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedToLib",
                value: new DateTime(2023, 4, 30, 16, 18, 51, 28, DateTimeKind.Local).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedToLib",
                value: new DateTime(2023, 4, 30, 16, 18, 51, 28, DateTimeKind.Local).AddTicks(5077));
        }
    }
}
