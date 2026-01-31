using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentsAreAllowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CommentsAreAllowed", "CreatedAt", "Description", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { new Guid("4b363c0a-6917-46fa-aac0-815f2fd9351b"), true, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hello World!", null, "Second Topic" },
                    { new Guid("9ec8a152-a054-4de9-b30a-c9286e03ed72"), true, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hello World!", null, "First Topic" },
                    { new Guid("f9b01bd8-c9db-4da9-a1cd-46606eb0dadf"), true, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hello World!", null, "Third Topic" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "TopicId" },
                values: new object[,]
                {
                    { new Guid("72775f5b-0878-483e-97ea-2bc255aa8b74"), "Hello Comment!", new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4b363c0a-6917-46fa-aac0-815f2fd9351b") },
                    { new Guid("a59e8cef-d838-4e7c-8396-86b1b3a96a98"), "Hello Comment!", new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9ec8a152-a054-4de9-b30a-c9286e03ed72") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TopicId",
                table: "Comments",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
