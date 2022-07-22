using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorporateCourseManagement.Migrations
{
    public partial class RoleChagesinDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                defaultValueSql: "'Trainee'",
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(sbyte),
                oldType: "tinyint",
                oldNullable: true,
                oldDefaultValueSql: "1")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "role",
                table: "users",
                type: "tinyint",
                nullable: true,
                defaultValueSql: "1",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValueSql: "'Trainee'")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }
    }
}
