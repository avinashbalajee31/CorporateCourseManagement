using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorporateCourseManagement.Migrations
{
    public partial class _3rddbchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "role",
                keyValue: null,
                column: "role",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "users",
                type: "longtext",
                nullable: false,
                defaultValueSql: "user",
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldDefaultValueSql: "user")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "users",
                type: "longtext",
                nullable: true,
                defaultValueSql: "user",
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValueSql: "user")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }
    }
}
