﻿// <auto-generated />
using System;
using E2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E2.Migrations
{
    [DbContext(typeof(E2DbContext))]
    [Migration("20221229053335_Nameupdate1")]
    partial class Nameupdate1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E2.Models.ProductModel", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<DateTime?>("ExpDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpiryDate");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Made")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Make");

                    b.Property<DateTime?>("ManDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ManufactureDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("E2.Models.StockModel", b =>
                {
                    b.Property<long>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("StockId"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<long>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("Date");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("StockId");

                    b.HasIndex("ProductId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("E2.Models.UserModel", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("E2.Models.StockModel", b =>
                {
                    b.HasOne("E2.Models.ProductModel", "Product")
                        .WithMany("Stock")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("E2.Models.ProductModel", b =>
                {
                    b.Navigation("Stock");
                });
#pragma warning restore 612, 618
        }
    }
}
