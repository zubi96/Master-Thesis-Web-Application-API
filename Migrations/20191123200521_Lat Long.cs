using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterThesisWebApplication.Migrations
{
    public partial class LatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LatLong",
                table: "Locations",
                newName: "Lng");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Locations",
                newName: "LatLong");
        }
    }
}
