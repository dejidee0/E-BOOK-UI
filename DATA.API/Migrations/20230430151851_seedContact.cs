using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MODEL.Migrations
{
    public partial class seedContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ae0c91b-34b5-41e4-b23c-bd8837715efd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecfa29f7-fcec-4efb-9e2a-3061e1dc07fc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f662384f-edec-4ddf-b6ea-ff4e00386a17", "1", "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb28410d-7147-45f7-8703-8668957ca0b6", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AddedToLib", "AppUserId", "Author", "BookImg", "Description", "ISBN", "NoPage", "Publisher", "PublisherDate", "Title", "ViewBook" },
                values: new object[] { 1, new DateTime(2023, 4, 30, 16, 18, 51, 28, DateTimeKind.Local).AddTicks(5052), "9408eccf-0b8b-4d88-b951-e10f83198e18", "Paulo Coelho", "", "The story of Santiago, an Andalusian shepherd boy...", "978-0062315007", 208, "HarperOne", new DateTime(1988, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Alchemist", 0 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AddedToLib", "AppUserId", "Author", "BookImg", "Description", "ISBN", "NoPage", "Publisher", "PublisherDate", "Title", "ViewBook" },
                values: new object[] { 2, new DateTime(2023, 4, 30, 16, 18, 51, 28, DateTimeKind.Local).AddTicks(5074), "9408eccf-0b8b-4d88-b951-e10f83198e18", "Harper Lee", "", "The story of a young girl and her lawyer father...", "978-0446310789", 336, "Grand Central Publishing", new DateTime(1960, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "To Kill a Mockingbird", 0 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AddedToLib", "AppUserId", "Author", "BookImg", "Description", "ISBN", "NoPage", "Publisher", "PublisherDate", "Title", "ViewBook" },
                values: new object[] { 3, new DateTime(2023, 4, 30, 16, 18, 51, 28, DateTimeKind.Local).AddTicks(5077), "9408eccf-0b8b-4d88-b951-e10f83198e18", "George Orwell", "", "A dystopian novel set in a totalitarian society...", "978-0451524935", 328, "Signet Classics", new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f662384f-edec-4ddf-b6ea-ff4e00386a17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb28410d-7147-45f7-8703-8668957ca0b6");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ae0c91b-34b5-41e4-b23c-bd8837715efd", "2", "USER", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecfa29f7-fcec-4efb-9e2a-3061e1dc07fc", "1", "ADMIN", "ADMIN" });
        }
    }
}
