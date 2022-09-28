using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddedNewsApiFunctionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastNewsRequest",
                table: "AppUserRequestsOverview",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NewsRequests",
                table: "AppUserRequestsOverview",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NewsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimesRequested = table.Column<int>(type: "int", nullable: false),
                    AverageResults = table.Column<int>(type: "int", nullable: false),
                    LastRequested = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsRequests");

            migrationBuilder.DropColumn(
                name: "LastNewsRequest",
                table: "AppUserRequestsOverview");

            migrationBuilder.DropColumn(
                name: "NewsRequests",
                table: "AppUserRequestsOverview");
        }
    }
}
