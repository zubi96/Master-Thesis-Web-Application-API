using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterThesisWebApplication.Migrations
{
    public partial class QRCodeBase64removedfromLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeBase64",
                table: "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeBase64",
                table: "Locations",
                nullable: true);
        }
    }
}
