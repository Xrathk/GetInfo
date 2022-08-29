using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddedWeatherAndUserRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserRequestsOverview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherRequests = table.Column<int>(type: "int", nullable: false),
                    LastWeatherRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRequestsOverview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserRequestsOverview_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRecordedTemperature = table.Column<double>(type: "float", nullable: false),
                    MaxRecordedTemperature = table.Column<double>(type: "float", nullable: false),
                    MinRecordedTemperature = table.Column<double>(type: "float", nullable: false),
                    TimesRequested = table.Column<int>(type: "int", nullable: false),
                    LastRequested = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRequestsOverview_AppUserId",
                table: "AppUserRequestsOverview",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRequestsOverview");

            migrationBuilder.DropTable(
                name: "WeatherRequests");
        }
    }
}
