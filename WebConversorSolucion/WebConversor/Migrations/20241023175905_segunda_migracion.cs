using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebConversor.Migrations
{
    /// <inheritdoc />
    public partial class segunda_migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Img", "LastName", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "asda@gmail.com", "dd", "aaa", "Dólar Estadounidense", "ddd" },
                    { 2, "ggrg2@gmail.com", "ff", "aaa", "Euro", "fff" }
                });

            migrationBuilder.InsertData(
                table: "Histories",
                columns: new[] { "Id", "Date", "FromCoin", "Result", "ToCoin", "UserId" },
                values: new object[] { 1, new DateTime(2004, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "EUR", 20.0, "USD", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
