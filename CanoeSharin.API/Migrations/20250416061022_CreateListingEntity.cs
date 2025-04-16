using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanoeSharin.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateListingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalStores",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalStores", x => x.StoreID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_StoreID",
                table: "Listings",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_RentalStores_StoreID",
                table: "Listings",
                column: "StoreID",
                principalTable: "RentalStores",
                principalColumn: "StoreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_RentalStores_StoreID",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "RentalStores");

            migrationBuilder.DropIndex(
                name: "IX_Listings_StoreID",
                table: "Listings");
        }
    }
}
