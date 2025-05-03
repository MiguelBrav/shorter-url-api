using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShorterAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class favoriteshorty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteShorty",
                columns: table => new
                {
                    ShortyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteShorty", x => new { x.ShortyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoriteShorty_Shorty_ShortyId",
                        column: x => x.ShortyId,
                        principalTable: "Shorty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteShorty");
        }
    }
}
