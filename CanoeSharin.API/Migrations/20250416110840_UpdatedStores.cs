using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanoeSharin.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "RentalStores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "RentalStores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
