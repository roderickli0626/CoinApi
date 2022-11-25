using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class Add_Duration_SubstanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "tblSubstance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "tblSubstance");
        }
    }
}
