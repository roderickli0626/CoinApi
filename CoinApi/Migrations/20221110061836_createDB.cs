using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblLanguage",
                columns: table => new
                {
                    languageNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLanguage", x => x.languageNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblSubstance",
                columns: table => new
                {
                    SubstanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hidde = table.Column<bool>(type: "bit", nullable: true),
                    WavFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StandardYesNo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubstance", x => x.SubstanceID);
                });

            migrationBuilder.CreateTable(
                name: "tblSubstanceForGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubstanceID = table.Column<int>(type: "int", nullable: true),
                    GroupNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubstanceForGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSubstanceGroup",
                columns: table => new
                {
                    GroupNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    ViewYesNo = table.Column<bool>(type: "bit", nullable: true),
                    StandardYesNo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubstanceGroup", x => x.GroupNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblSubstanceGroupText",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNumber = table.Column<int>(type: "int", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubstanceGroupText", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSubstanceText",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubstanceID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubstanceText", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageNumber = table.Column<int>(type: "int", nullable: true),
                    ActiveAcount = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblLanguage");

            migrationBuilder.DropTable(
                name: "tblSubstance");

            migrationBuilder.DropTable(
                name: "tblSubstanceForGroup");

            migrationBuilder.DropTable(
                name: "tblSubstanceGroup");

            migrationBuilder.DropTable(
                name: "tblSubstanceGroupText");

            migrationBuilder.DropTable(
                name: "tblSubstanceText");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
