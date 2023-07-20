using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E2.Migrations
{
    /// <inheritdoc />
    public partial class Nameupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManDate",
                table: "Products",
                newName: "ManufactureDate");

            migrationBuilder.RenameColumn(
                name: "Made",
                table: "Products",
                newName: "Make");

            migrationBuilder.RenameColumn(
                name: "ExpDate",
                table: "Products",
                newName: "ExpiryDate");

           /* migrationBuilder.AlterColumn<DateTime>(
                name: "ManufactureDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Make",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(25)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManufactureDate",
                table: "Products",
                newName: "ManDate");

            migrationBuilder.RenameColumn(
                name: "Make",
                table: "Products",
                newName: "Made");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Products",
                newName: "ExpDate");

         /*   migrationBuilder.AlterColumn<DateTime>(
                name: "ManDate",
                table: "Products",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Made",
                table: "Products",
                type: "VARCHAR(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpDate",
                table: "Products",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);*/
        }
    }
}
