using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArrowInventory.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSitesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiteCountry",
                table: "Sites",
                newName: "siteCountry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "siteCountry",
                table: "Sites",
                newName: "SiteCountry");
        }
    }
}
