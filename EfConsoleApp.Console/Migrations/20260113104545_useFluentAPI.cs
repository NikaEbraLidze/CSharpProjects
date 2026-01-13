using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfConsoleApp.Console.Migrations
{
    /// <inheritdoc />
    public partial class useFluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FootballersAgent",
                table: "FootballersAgent");

            migrationBuilder.DropIndex(
                name: "IX_FootballersAgent_FootballerId",
                table: "FootballersAgent");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FootballersAgent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FootballersAgent",
                table: "FootballersAgent",
                columns: new[] { "FootballerId", "AgentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FootballersAgent",
                table: "FootballersAgent");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FootballersAgent",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FootballersAgent",
                table: "FootballersAgent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FootballersAgent_FootballerId",
                table: "FootballersAgent",
                column: "FootballerId");
        }
    }
}
