using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Artour.Domain.Migrations
{
    public partial class Extension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "city_id",
                table: "tour",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    comment_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    tour_id = table.Column<int>(nullable: false),
                    mark = table.Column<int>(nullable: false),
                    text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_comment_tour_tour_id",
                        column: x => x.tour_id,
                        principalTable: "tour",
                        principalColumn: "tour_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    region_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.region_id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    region_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.country_id);
                    table.ForeignKey(
                        name: "FK_country_region_region_id",
                        column: x => x.region_id,
                        principalTable: "region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    city_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    country_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.city_id);
                    table.ForeignKey(
                        name: "FK_city_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tour_city_id",
                table: "tour",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_city_country_id",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_tour_id",
                table: "comment",
                column: "tour_id");

            migrationBuilder.CreateIndex(
                name: "IX_country_region_id",
                table: "country",
                column: "region_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tour_city_city_id",
                table: "tour",
                column: "city_id",
                principalTable: "city",
                principalColumn: "city_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tour_city_city_id",
                table: "tour");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "region");

            migrationBuilder.DropIndex(
                name: "IX_tour_city_id",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "tour");
        }
    }
}
