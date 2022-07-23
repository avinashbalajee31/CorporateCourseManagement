using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorporateCourseManagement.Migrations
{
    public partial class courseModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfStudentsEnrolled",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfStudentsEnrolled",
                table: "Courses");
        }
    }
}
