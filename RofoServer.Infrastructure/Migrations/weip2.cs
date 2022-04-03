using Microsoft.EntityFrameworkCore.Migrations;

namespace RofoServer.Persistence.Migrations
{
    public partial class weip2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAuthentication_UserAuthDetailsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserAuthDetailsId",
                table: "Users",
                newName: "UserAuthentication");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserAuthDetailsId",
                table: "Users",
                newName: "IX_Users_UserAuthentication");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAuthentication_UserAuthentication",
                table: "Users",
                column: "UserAuthentication",
                principalTable: "UserAuthentication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAuthentication_UserAuthentication",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserAuthentication",
                table: "Users",
                newName: "UserAuthDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserAuthentication",
                table: "Users",
                newName: "IX_Users_UserAuthDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAuthentication_UserAuthDetailsId",
                table: "Users",
                column: "UserAuthDetailsId",
                principalTable: "UserAuthentication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
