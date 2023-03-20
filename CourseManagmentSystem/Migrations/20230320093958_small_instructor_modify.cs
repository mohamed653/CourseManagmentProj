using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagmentSystem.Migrations
{
    public partial class small_instructor_modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequested",
                table: "Instructors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequested",
                table: "Instructors");
        }
    }
}
