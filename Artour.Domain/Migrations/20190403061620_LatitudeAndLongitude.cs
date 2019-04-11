using Microsoft.EntityFrameworkCore.Migrations;

namespace Artour.Domain.Migrations
{
    public partial class LatitudeAndLongitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "sight",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "sight",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "sight");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "sight");
        }
    }
}
