using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Artour.Domain.Migrations
{
    public partial class ModelsFill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "end_date",
                table: "visit",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "start_date",
                table: "visit",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "profile_type",
                table: "user",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "tour",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "tour",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "date_seen",
                table: "sight_seen",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "sight_image",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "file_size",
                table: "sight_image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "full_file_name",
                table: "sight_image",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "sight_image",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "uploaded_on",
                table: "sight_image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "sight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "sight",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date",
                table: "visit");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "visit");

            migrationBuilder.DropColumn(
                name: "profile_type",
                table: "user");

            migrationBuilder.DropColumn(
                name: "description",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "title",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "date_seen",
                table: "sight_seen");

            migrationBuilder.DropColumn(
                name: "description",
                table: "sight_image");

            migrationBuilder.DropColumn(
                name: "file_size",
                table: "sight_image");

            migrationBuilder.DropColumn(
                name: "full_file_name",
                table: "sight_image");

            migrationBuilder.DropColumn(
                name: "order",
                table: "sight_image");

            migrationBuilder.DropColumn(
                name: "uploaded_on",
                table: "sight_image");

            migrationBuilder.DropColumn(
                name: "description",
                table: "sight");

            migrationBuilder.DropColumn(
                name: "title",
                table: "sight");
        }
    }
}
