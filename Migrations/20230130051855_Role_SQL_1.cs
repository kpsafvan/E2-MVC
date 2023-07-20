using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E2.Migrations
{
    /// <inheritdoc />
    public partial class RoleSQL1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Alter table [Ecommerce].[dbo].[Users] add constraint Roles check (Role in ('Admin','User'));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Alter table [Ecommerce].[dbo].[Users] Drop constraint Roles;");
        }
    }
}
