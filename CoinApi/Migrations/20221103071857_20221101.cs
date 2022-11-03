using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class _20221101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_tblLanguage_languageNumber",
                table: "tblUser");

            migrationBuilder.DropIndex(
                name: "IX_tblUser_languageNumber",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "languageNumber",
                table: "tblUser");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "tblUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "tblUser");

            migrationBuilder.AddColumn<int>(
                name: "languageNumber",
                table: "tblUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_languageNumber",
                table: "tblUser",
                column: "languageNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUser_tblLanguage_languageNumber",
                table: "tblUser",
                column: "languageNumber",
                principalTable: "tblLanguage",
                principalColumn: "languageNumber");
        }
    }
}
