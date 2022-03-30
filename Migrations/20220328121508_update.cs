using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "rollno",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "employee_id",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "id",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropSequence(
                name: "employee_id",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "id",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "rollno",
                schema: "dbo");
        }
    }
}
