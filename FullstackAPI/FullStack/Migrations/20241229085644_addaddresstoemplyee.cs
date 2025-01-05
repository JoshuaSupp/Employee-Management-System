using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullStack.API.Migrations
{
    public partial class addaddresstoemplyee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermanentAddress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalAddress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermanentAddress",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonalAddress",
                table: "Employees");
        }
    }
}
