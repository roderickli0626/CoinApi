﻿// <auto-generated />
using System;
using CoinApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinApi.Migrations
{
    [DbContext(typeof(CoinApiContext))]
    partial class CoinApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoinApi.DB_Models.tblCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderNo")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("tblCategory");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblCountry", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("tblCountry");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblCoupons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CoupenCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDatetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsAmount")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MinAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDatetime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("tblCoupons");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblGroupQuestionInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblGroupQuestionInfo");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblGroupQuestions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupNumber");

                    b.HasIndex("QuestionId");

                    b.ToTable("tblGroupQuestions");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblLanguage", b =>
                {
                    b.Property<int>("languageNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("languageNumber"), 1L, 1);

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("languageNumber");

                    b.ToTable("tblLanguage");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblLanguageGUI", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LanguageNumber")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("key")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblLanguageGUI");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblModulePoints", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Point")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("tblModulePoints");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblModules", b =>
                {
                    b.Property<int>("ModuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleID"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupNumberID")
                        .HasColumnType("int");

                    b.Property<bool?>("IsSubscription")
                        .HasColumnType("bit");

                    b.Property<string>("NameModule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDatetime")
                        .HasColumnType("datetime2");

                    b.HasKey("ModuleID");

                    b.HasIndex("GroupNumberID");

                    b.ToTable("tblModules");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblModuleSubScriptionPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Point")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("tblModuleSubScriptionPoint");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblOrderItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Qty")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("tblOrderItems");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblOrders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ModuleID")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblOrders");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblPayPalConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDatetime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("tblPayPalConfiguration");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblQuestions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<int?>("GroupQueInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("languageNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupNumber");

                    b.HasIndex("GroupQueInfoId");

                    b.HasIndex("languageNumber");

                    b.ToTable("tblQuestions");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblSubstance", b =>
                {
                    b.Property<int>("SubstanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubstanceID"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Hidde")
                        .HasColumnType("bit");

                    b.Property<bool?>("StandardYesNo")
                        .HasColumnType("bit");

                    b.Property<byte[]>("WavFile")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("SubstanceID");

                    b.ToTable("tblSubstance");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblSubstanceForGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SubstanceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblSubstanceForGroup");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblSubstanceGroup", b =>
                {
                    b.Property<int>("GroupNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupNumber"), 1L, 1);

                    b.Property<bool?>("StandardYesNo")
                        .HasColumnType("bit");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.Property<bool?>("ViewYesNo")
                        .HasColumnType("bit");

                    b.HasKey("GroupNumber");

                    b.ToTable("tblSubstanceGroup");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblSubstanceGroupText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblSubstanceGroupText");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblSubstanceText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<int?>("SubstanceID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblSubstanceText");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblUser", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<bool?>("ActiveAcount")
                        .HasColumnType("bit");

                    b.Property<string>("AddressFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressSalutation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsEnableLogin")
                        .HasColumnType("bit");

                    b.Property<int?>("LanguageNumber")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostCode")
                        .HasColumnType("int");

                    b.Property<string>("Salutation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CountryId");

                    b.ToTable("tblUser");

                    b.HasData(
                        new
                        {
                            UserID = -1,
                            Email = "superadmin@gmail.com",
                            IsAdmin = true,
                            Location = "",
                            Password = "zdb7S539SnztgO8VIhz2OlEie4OQRS/eDZzxFmktU0g=",
                            Phone = "",
                            SurName = "",
                            Title = ""
                        });
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblGroupQuestions", b =>
                {
                    b.HasOne("CoinApi.DB_Models.tblSubstanceGroup", "tblSubstanceGroup")
                        .WithMany()
                        .HasForeignKey("GroupNumber");

                    b.HasOne("CoinApi.DB_Models.tblQuestions", "tblQuestions")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.Navigation("tblQuestions");

                    b.Navigation("tblSubstanceGroup");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblModulePoints", b =>
                {
                    b.HasOne("CoinApi.DB_Models.tblModules", "tblModules")
                        .WithMany()
                        .HasForeignKey("ModuleId");

                    b.Navigation("tblModules");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblModules", b =>
                {
                    b.HasOne("CoinApi.DB_Models.tblSubstanceGroupText", "tblSubstanceGroupText")
                        .WithMany()
                        .HasForeignKey("GroupNumberID");

                    b.Navigation("tblSubstanceGroupText");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblModuleSubScriptionPoint", b =>
                {
                    b.HasOne("CoinApi.DB_Models.tblModules", "tblModules")
                        .WithMany()
                        .HasForeignKey("ModuleId");

                    b.Navigation("tblModules");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblQuestions", b =>
                {
                    b.HasOne("CoinApi.DB_Models.tblSubstanceGroup", "tblSubstanceGroup")
                        .WithMany()
                        .HasForeignKey("GroupNumber");

                    b.HasOne("CoinApi.DB_Models.tblGroupQuestionInfo", "tblGroupQuestionInfo")
                        .WithMany()
                        .HasForeignKey("GroupQueInfoId");

                    b.HasOne("CoinApi.DB_Models.tblLanguage", "tblLanguage")
                        .WithMany()
                        .HasForeignKey("languageNumber");

                    b.Navigation("tblGroupQuestionInfo");

                    b.Navigation("tblLanguage");

                    b.Navigation("tblSubstanceGroup");
                });

            modelBuilder.Entity("CoinApi.DB_Models.tblUser", b =>
                {
                    b.HasOne("CoinApi.DB_Models.tblCategory", "tblCategory")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("CoinApi.DB_Models.tblCountry", "tblCountry")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("tblCategory");

                    b.Navigation("tblCountry");
                });
#pragma warning restore 612, 618
        }
    }
}
