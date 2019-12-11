using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterThesisWebApplication.Migrations
{
    public partial class AddedgendertomobileUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "MobileUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "MobileUsers");
        }
    }
}
