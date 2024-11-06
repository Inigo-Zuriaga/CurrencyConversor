using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebConversor.Migrations
{
    /// <inheritdoc />
    public partial class migracionuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ExchangeHistory",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 1);

            migrationBuilder.InsertData(
                table: "ExchangeHistory",
                columns: new[] { "Id", "Date", "FromCoin", "ToCoin", "UserId" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 11, 6, 18, 55, 13, 961, DateTimeKind.Local).AddTicks(2005), "USD", "EUR", 2 },
                    { 3, new DateTime(2007, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "USD", "PLN", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastName", "Name" },
                values: new object[] { "Gomez", "Julian" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastName", "Name" },
                values: new object[] { "Garcia", "Manuel" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExchangeHistory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExchangeHistory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "ExchangeHistory",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastName", "Name" },
                values: new object[] { "aaa", "D�lar Estadounidense" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastName", "Name" },
                values: new object[] { "aaa", "Euro" });
        }
    }
}
