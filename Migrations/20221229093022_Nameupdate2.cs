using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E2.Migrations
{
    /// <inheritdoc />
    public partial class Nameupdate2 : Migration
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
        }
    }
}
