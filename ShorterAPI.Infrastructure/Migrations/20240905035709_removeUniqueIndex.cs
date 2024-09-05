﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShorterAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shorty_ShortUrl",
                table: "Shorty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shorty_ShortUrl",
                table: "Shorty",
                column: "ShortUrl",
                unique: true);
        }
    }
}
