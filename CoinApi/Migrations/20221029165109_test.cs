using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanguageNumber",
                table: "tblUser",
                newName: "languageNumber");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_tblLanguage_languageNumber",
                table: "tblUser");

            migrationBuilder.DropIndex(
                name: "IX_tblUser_languageNumber",
                table: "tblUser");

            migrationBuilder.RenameColumn(
                name: "languageNumber",
                table: "tblUser",
                newName: "LanguageNumber");
        }
    }
}
