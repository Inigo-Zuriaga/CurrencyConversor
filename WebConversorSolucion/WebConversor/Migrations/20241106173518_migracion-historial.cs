using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebConversor.Migrations
{
    /// <inheritdoc />
    public partial class migracionhistorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Users_UserId",
                table: "Histories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Histories",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Histories");

            migrationBuilder.RenameTable(
                name: "Histories",
                newName: "ExchangeHistory");

            migrationBuilder.RenameIndex(
                name: "IX_Histories_UserId",
                table: "ExchangeHistory",
                newName: "IX_ExchangeHistory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeHistory",
                table: "ExchangeHistory",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Coins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "D�lar Estadounidense");

            migrationBuilder.UpdateData(
                table: "Coins",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Yen Japon�s");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "D�lar Estadounidense");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeHistory_Users_UserId",
                table: "ExchangeHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeHistory_Users_UserId",
                table: "ExchangeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeHistory",
                table: "ExchangeHistory");

            migrationBuilder.RenameTable(
                name: "ExchangeHistory",
                newName: "Histories");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeHistory_UserId",
                table: "Histories",
                newName: "IX_Histories_UserId");

            migrationBuilder.AddColumn<double>(
                name: "Result",
                table: "Histories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Histories",
                table: "Histories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Coins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Dólar Estadounidense");

            migrationBuilder.UpdateData(
                table: "Coins",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Yen Japonés");

            migrationBuilder.UpdateData(
                table: "Histories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Result",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Dólar Estadounidense");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Users_UserId",
                table: "Histories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
