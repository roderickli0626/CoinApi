using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "tblCountry",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountry", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "tblCoupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoupenCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsAmount = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCoupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGroupQuestionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGroupQuestionInfo", x => x.Id);
                });

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
                name: "tblLanguageGUI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLanguageGUI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSubstance",
                columns: table => new
                {
                    SubstanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hidde = table.Column<bool>(type: "bit", nullable: true),
                    WavFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardYesNo = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ActiveAcount = table.Column<bool>(type: "bit", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressSalutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsEnableLogin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_tblUser_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_tblUser_tblCountry_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountry",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "tblQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Questions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupQueInfoId = table.Column<int>(type: "int", nullable: true),
                    GroupNumber = table.Column<int>(type: "int", nullable: true),
                    languageNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblQuestions_tblGroupQuestionInfo_GroupQueInfoId",
                        column: x => x.GroupQueInfoId,
                        principalTable: "tblGroupQuestionInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblQuestions_tblLanguage_languageNumber",
                        column: x => x.languageNumber,
                        principalTable: "tblLanguage",
                        principalColumn: "languageNumber");
                    table.ForeignKey(
                        name: "FK_tblQuestions_tblSubstanceGroup_GroupNumber",
                        column: x => x.GroupNumber,
                        principalTable: "tblSubstanceGroup",
                        principalColumn: "GroupNumber");
                });

            migrationBuilder.CreateTable(
                name: "tblModules",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNumberID = table.Column<int>(type: "int", nullable: true),
                    NameModule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSubscription = table.Column<bool>(type: "bit", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblModules", x => x.ModuleID);
                    table.ForeignKey(
                        name: "FK_tblModules_tblSubstanceGroupText_GroupNumberID",
                        column: x => x.GroupNumberID,
                        principalTable: "tblSubstanceGroupText",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblGroupQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    GroupNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGroupQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGroupQuestions_tblQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "tblQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblGroupQuestions_tblSubstanceGroup_GroupNumber",
                        column: x => x.GroupNumber,
                        principalTable: "tblSubstanceGroup",
                        principalColumn: "GroupNumber");
                });

            migrationBuilder.CreateTable(
                name: "tblModulePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblModulePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblModulePoints_tblModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "tblModules",
                        principalColumn: "ModuleID");
                });

            migrationBuilder.CreateTable(
                name: "tblModuleSubScriptionPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblModuleSubScriptionPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblModuleSubScriptionPoint_tblModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "tblModules",
                        principalColumn: "ModuleID");
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "UserID", "ActiveAcount", "AddressFirstName", "AddressLastName", "AddressSalutation", "AddressTitle", "Adress", "CategoryId", "City", "Company", "Country", "CountryId", "Department", "DeviceNumber", "Email", "FirstName", "IsAdmin", "IsEnableLogin", "LanguageNumber", "LastName", "Location", "Password", "Phone", "PostCode", "Salutation", "SurName", "Title" },
                values: new object[] { -1, null, null, null, null, null, null, null, null, null, null, null, null, null, "superadmin@gmail.com", null, true, null, null, null, "", "zdb7S539SnztgO8VIhz2OlEie4OQRS/eDZzxFmktU0g=", "", null, null, "", "" });

            migrationBuilder.CreateIndex(
                name: "IX_tblGroupQuestions_GroupNumber",
                table: "tblGroupQuestions",
                column: "GroupNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblGroupQuestions_QuestionId",
                table: "tblGroupQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblModulePoints_ModuleId",
                table: "tblModulePoints",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblModules_GroupNumberID",
                table: "tblModules",
                column: "GroupNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_tblModuleSubScriptionPoint_ModuleId",
                table: "tblModuleSubScriptionPoint",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuestions_GroupNumber",
                table: "tblQuestions",
                column: "GroupNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuestions_GroupQueInfoId",
                table: "tblQuestions",
                column: "GroupQueInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblQuestions_languageNumber",
                table: "tblQuestions",
                column: "languageNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_CategoryId",
                table: "tblUser",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_CountryId",
                table: "tblUser",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCoupons");

            migrationBuilder.DropTable(
                name: "tblGroupQuestions");

            migrationBuilder.DropTable(
                name: "tblLanguageGUI");

            migrationBuilder.DropTable(
                name: "tblModulePoints");

            migrationBuilder.DropTable(
                name: "tblModuleSubScriptionPoint");

            migrationBuilder.DropTable(
                name: "tblOrders");

            migrationBuilder.DropTable(
                name: "tblSubstance");

            migrationBuilder.DropTable(
                name: "tblSubstanceForGroup");

            migrationBuilder.DropTable(
                name: "tblSubstanceText");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "tblQuestions");

            migrationBuilder.DropTable(
                name: "tblModules");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblCountry");

            migrationBuilder.DropTable(
                name: "tblGroupQuestionInfo");

            migrationBuilder.DropTable(
                name: "tblLanguage");

            migrationBuilder.DropTable(
                name: "tblSubstanceGroup");

            migrationBuilder.DropTable(
                name: "tblSubstanceGroupText");
        }
    }
}
