using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class AddDefaultCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tblCategory",
                columns: new[] { "CategoryId", "Name", "OrderNo" },
                values: new object[] { 1, "Therapeuten", null });

            migrationBuilder.InsertData(
                table: "tblCategory",
                columns: new[] { "CategoryId", "Name", "OrderNo" },
                values: new object[] { 2, "Händler", null });

            migrationBuilder.InsertData(
                table: "tblCategory",
                columns: new[] { "CategoryId", "Name", "OrderNo" },
                values: new object[] { 3, "Privatpersonen", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblCategory",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblCategory",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tblCategory",
                keyColumn: "CategoryId",
                keyValue: 3);
        }
    }
}
