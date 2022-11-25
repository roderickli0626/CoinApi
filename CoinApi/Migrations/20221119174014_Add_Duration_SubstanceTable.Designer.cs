﻿// <auto-generated />
using System;
using CoinApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinApi.Migrations
{
    [DbContext(typeof(CoinApiContext))]
    [Migration("20221119174014_Add_Duration_SubstanceTable")]
    partial class Add_Duration_SubstanceTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

            modelBuilder.Entity("CoinApi.DB_Models.tblSubstance", b =>
                {
                    b.Property<int>("SubstanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubstanceID"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

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

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LanguageNumber")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("tblUser");
                });
#pragma warning restore 612, 618
        }
    }
}
