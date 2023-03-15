using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagmentSystem.Migrations
{
    public partial class InitialMigration_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Students",
                newName: "Email");
        }
    }
}
