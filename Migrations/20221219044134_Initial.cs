using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    Brand = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    Made = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    ManDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Category = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "Date", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
