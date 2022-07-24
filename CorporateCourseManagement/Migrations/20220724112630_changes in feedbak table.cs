using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorporateCourseManagement.Migrations
{
    public partial class changesinfeedbaktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameOfTheStudentsEnrolled",
                table: "Courses",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfTheStudentsEnrolled",
                table: "Courses");
        }
    }
}
