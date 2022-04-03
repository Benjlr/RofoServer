using Microsoft.EntityFrameworkCore.Migrations;

namespace RofoServer.Persistence.Migrations
{
    public partial class newwip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_UserId",
                table: "UserClaim");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserClaim",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAuthentication_UserAuthDetailsId",
                table: "Users",
                column: "UserAuthDetailsId",
                principalTable: "UserAuthentication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_UserId",
                table: "UserClaim");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserClaim",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAuthentication_UserAuthentication",
                table: "Users",
                column: "UserAuthentication",
                principalTable: "UserAuthentication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
