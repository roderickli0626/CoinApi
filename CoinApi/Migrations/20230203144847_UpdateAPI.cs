using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class UpdateAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "tblUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblModuleSubScriptionPoint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "File",
                table: "tblModules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<int>(
                name: "GroupNumber",
                table: "tblModulePoints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                table: "tblCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPayPalConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayPalConfiguration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOrderItems");

            migrationBuilder.DropTable(
                name: "tblPayPalConfiguration");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblModuleSubScriptionPoint");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "tblModulePoints");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "tblCategory");

            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "tblModules",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
