using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterThesisWebApplication.Migrations
{
    public partial class QRCodeBase64addedtoLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeBase64",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeBase64",
                table: "Locations");
        }
    }
}
