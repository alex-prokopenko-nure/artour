using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Artour.Domain.Migrations
{
    public partial class Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tour",
                columns: table => new
                {
                    tour_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    owner_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tour", x => x.tour_id);
                });

            migrationBuilder.CreateTable(
                name: "sight",
                columns: table => new
                {
                    sight_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tour_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sight", x => x.sight_id);
                    table.ForeignKey(
                        name: "FK_sight_tour_tour_id",
                        column: x => x.tour_id,
                        principalTable: "tour",
                        principalColumn: "tour_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "visit",
                columns: table => new
                {
                    visit_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    tour_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visit", x => x.visit_id);
                    table.ForeignKey(
                        name: "FK_visit_tour_tour_id",
                        column: x => x.tour_id,
                        principalTable: "tour",
                        principalColumn: "tour_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_visit_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sight_image",
                columns: table => new
                {
                    sight_image_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sight_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sight_image", x => x.sight_image_id);
                    table.ForeignKey(
                        name: "FK_sight_image_sight_sight_id",
                        column: x => x.sight_id,
                        principalTable: "sight",
                        principalColumn: "sight_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sight_seen",
                columns: table => new
                {
                    sight_seen_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sight_id = table.Column<int>(nullable: false),
                    visit_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sight_seen", x => x.sight_seen_id);
                    table.ForeignKey(
                        name: "FK_sight_seen_sight_sight_id",
                        column: x => x.sight_id,
                        principalTable: "sight",
                        principalColumn: "sight_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sight_seen_visit_visit_id",
                        column: x => x.visit_id,
                        principalTable: "visit",
                        principalColumn: "visit_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sight_tour_id",
                table: "sight",
                column: "tour_id");

            migrationBuilder.CreateIndex(
                name: "IX_sight_image_sight_id",
                table: "sight_image",
                column: "sight_id");

            migrationBuilder.CreateIndex(
                name: "IX_sight_seen_sight_id",
                table: "sight_seen",
                column: "sight_id");

            migrationBuilder.CreateIndex(
                name: "IX_sight_seen_visit_id",
                table: "sight_seen",
                column: "visit_id");

            migrationBuilder.CreateIndex(
                name: "IX_visit_tour_id",
                table: "visit",
                column: "tour_id");

            migrationBuilder.CreateIndex(
                name: "IX_visit_user_id",
                table: "visit",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sight_image");

            migrationBuilder.DropTable(
                name: "sight_seen");

            migrationBuilder.DropTable(
                name: "sight");

            migrationBuilder.DropTable(
                name: "visit");

            migrationBuilder.DropTable(
                name: "tour");
        }
    }
}
