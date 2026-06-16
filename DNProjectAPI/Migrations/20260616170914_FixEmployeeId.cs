using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employees",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "id");
        }
    }
}
