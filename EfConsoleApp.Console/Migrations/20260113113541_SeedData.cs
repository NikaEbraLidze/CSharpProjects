using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfConsoleApp.Console.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "Id", "AgencyName", "Name" },
                values: new object[] { 1, null, "Jorge Mendes" });

            migrationBuilder.InsertData(
                table: "Club",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[] { 1, "Spain", "Real Madrid" });

            migrationBuilder.InsertData(
                table: "Footballers",
                columns: new[] { "Id", "BirthDate", "ClubId", "FirstName", "JerseyNumber", "LastName" },
                values: new object[] { 1, new DateTime(1985, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cristiano", (byte)7, "Ronaldo" });

            migrationBuilder.InsertData(
                table: "FootballersAgent",
                columns: new[] { "AgentId", "FootballerId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "TransferMarketData",
                columns: new[] { "Id", "ContractExpirationDate", "FootballerId", "Marketvalue" },
                values: new object[] { 1, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1000000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FootballersAgent",
                keyColumns: new[] { "AgentId", "FootballerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TransferMarketData",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Footballers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Club",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
