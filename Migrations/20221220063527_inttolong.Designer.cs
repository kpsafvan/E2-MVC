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
    [Migration("20221220063527_inttolong")]
    partial class inttolong
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

                    b.Property<DateTime>("ExpDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Made")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<DateTime>("ManDate")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
