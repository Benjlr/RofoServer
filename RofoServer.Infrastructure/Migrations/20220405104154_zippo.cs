using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RofoServer.Persistence.Migrations
{
    public partial class zippo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_UserId",
                table: "UserClaim");

            migrationBuilder.DropIndex(
                name: "IX_GroupAccess_User_Group",
                table: "GroupAccess");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "GroupAccess");

            migrationBuilder.DropColumn(
                name: "User",
                table: "GroupAccess");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserClaim",
                newName: "RofoUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                newName: "IX_UserClaim_RofoUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RefreshToken",
                newName: "RofoUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_RofoUserId");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupAccess",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GroupAccess",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupAccess_GroupId",
                table: "GroupAccess",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAccess_UserId",
                table: "GroupAccess",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAccess_Groups_GroupId",
                table: "GroupAccess",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAccess_Users_UserId",
                table: "GroupAccess",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_RofoUserId",
                table: "RefreshToken",
                column: "RofoUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_RofoUserId",
                table: "UserClaim",
                column: "RofoUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupAccess_Groups_GroupId",
                table: "GroupAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAccess_Users_UserId",
                table: "GroupAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_RofoUserId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_RofoUserId",
                table: "UserClaim");

            migrationBuilder.DropIndex(
                name: "IX_GroupAccess_GroupId",
                table: "GroupAccess");

            migrationBuilder.DropIndex(
                name: "IX_GroupAccess_UserId",
                table: "GroupAccess");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupAccess");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GroupAccess");

            migrationBuilder.RenameColumn(
                name: "RofoUserId",
                table: "UserClaim",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaim_RofoUserId",
                table: "UserClaim",
                newName: "IX_UserClaim_UserId");

            migrationBuilder.RenameColumn(
                name: "RofoUserId",
                table: "RefreshToken",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_RofoUserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "GroupAccess",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "GroupAccess",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupAccess_User_Group",
                table: "GroupAccess",
                columns: new[] { "User", "Group" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
