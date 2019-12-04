using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterThesisWebApplication.Migrations
{
    public partial class MobileUserLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobileUserLocation_Locations_LocationId",
                table: "MobileUserLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileUserLocation_MobileUsers_MobileUserId",
                table: "MobileUserLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobileUserLocation",
                table: "MobileUserLocation");

            migrationBuilder.RenameTable(
                name: "MobileUserLocation",
                newName: "MobileUserLocations");

            migrationBuilder.RenameIndex(
                name: "IX_MobileUserLocation_MobileUserId",
                table: "MobileUserLocations",
                newName: "IX_MobileUserLocations_MobileUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobileUserLocations",
                table: "MobileUserLocations",
                columns: new[] { "LocationId", "MobileUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MobileUserLocations_Locations_LocationId",
                table: "MobileUserLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileUserLocations_MobileUsers_MobileUserId",
                table: "MobileUserLocations",
                column: "MobileUserId",
                principalTable: "MobileUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobileUserLocations_Locations_LocationId",
                table: "MobileUserLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_MobileUserLocations_MobileUsers_MobileUserId",
                table: "MobileUserLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobileUserLocations",
                table: "MobileUserLocations");

            migrationBuilder.RenameTable(
                name: "MobileUserLocations",
                newName: "MobileUserLocation");

            migrationBuilder.RenameIndex(
                name: "IX_MobileUserLocations_MobileUserId",
                table: "MobileUserLocation",
                newName: "IX_MobileUserLocation_MobileUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobileUserLocation",
                table: "MobileUserLocation",
                columns: new[] { "LocationId", "MobileUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MobileUserLocation_Locations_LocationId",
                table: "MobileUserLocation",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobileUserLocation_MobileUsers_MobileUserId",
                table: "MobileUserLocation",
                column: "MobileUserId",
                principalTable: "MobileUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
