using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class SessionIdUserIdInterconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "AppUserSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSessions_AppUserID",
                table: "AppUserSessions",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserSessions_AppUsers_AppUserID",
                table: "AppUserSessions",
                column: "AppUserID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserSessions_AppUsers_AppUserID",
                table: "AppUserSessions");

            migrationBuilder.DropIndex(
                name: "IX_AppUserSessions_AppUserID",
                table: "AppUserSessions");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "AppUserSessions");
        }
    }
}
