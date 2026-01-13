using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfConsoleApp.Console.Migrations
{
    /// <inheritdoc />
    public partial class InitRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footballers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JerseyNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footballers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Footballers_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballersAgent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootballerId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballersAgent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballersAgent_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FootballersAgent_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferMarketData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marketvalue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FootballerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferMarketData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferMarketData_Footballers_FootballerId",
                        column: x => x.FootballerId,
                        principalTable: "Footballers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Footballers_ClubId",
                table: "Footballers",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballersAgent_AgentId",
                table: "FootballersAgent",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballersAgent_FootballerId",
                table: "FootballersAgent",
                column: "FootballerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferMarketData_FootballerId",
                table: "TransferMarketData",
                column: "FootballerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballersAgent");

            migrationBuilder.DropTable(
                name: "TransferMarketData");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Footballers");

            migrationBuilder.DropTable(
                name: "Club");
        }
    }
}
