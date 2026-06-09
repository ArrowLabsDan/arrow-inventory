using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArrowInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordChangeDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordChangeDate",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordChangeDate",
                table: "AspNetUsers");
        }
    }
}
