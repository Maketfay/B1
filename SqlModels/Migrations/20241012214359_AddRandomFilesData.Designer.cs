﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SqlModels;

#nullable disable

namespace SqlModels.Migrations
{
    [DbContext(typeof(WebContext))]
    [Migration("20241012214359_AddRandomFilesData")]
    partial class AddRandomFilesData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SqlModels.Entities.RandomFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CyrillicWord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LatinWord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("RandomDouble")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("RandomInteger")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RandomFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
