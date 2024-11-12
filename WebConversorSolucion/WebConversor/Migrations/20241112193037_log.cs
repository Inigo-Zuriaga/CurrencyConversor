using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebConversor.Migrations
{
    /// <inheritdoc />
    public partial class log : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FromCoin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCoin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "Name", "ShortName", "Symbol" },
                values: new object[,]
                {
                    { 1, "D�lar Estadounidense", "USD", "USD" },
                    { 2, "Euro", "EUR", "EUR" },
                    { 3, "Yen Japon�s", "YEN", "JPY" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FechaNacimiento", "Img", "LastName", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "asda@gmail.com", null, "dd", "Gomez", "Julian", "ddd" },
                    { 2, "ggrg2@gmail.com", null, "ff", "Garcia", "Manuel", "fff" }
                });

            migrationBuilder.InsertData(
                table: "ExchangeHistory",
                columns: new[] { "Id", "Date", "FromCoin", "ToCoin", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2004, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "EUR", "USD", 1 },
                    { 2, new DateTime(2024, 11, 12, 20, 30, 36, 71, DateTimeKind.Local).AddTicks(2136), "USD", "EUR", 2 },
                    { 3, new DateTime(2007, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "USD", "PLN", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistory_UserId",
                table: "ExchangeHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "ExchangeHistory");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
