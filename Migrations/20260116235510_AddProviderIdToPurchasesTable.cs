using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerreteríaWeb_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderIdToPurchasesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Purchases");
        }
    }
}
