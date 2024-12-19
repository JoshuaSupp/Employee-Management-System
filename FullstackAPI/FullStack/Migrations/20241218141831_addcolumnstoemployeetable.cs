using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullStack.API.Migrations
{
    public partial class addcolumnstoemployeetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallingName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DOB",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalEmail",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalPhone",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CallingName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonalEmail",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonalPhone",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Employees");
        }
    }
}
