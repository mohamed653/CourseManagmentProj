using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagmentSystem.Migrations
{
    public partial class instructor_Property_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicUrl",
                table: "Instructors");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePic",
                table: "Instructors",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Instructors");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicUrl",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
