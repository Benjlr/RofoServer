using Microsoft.EntityFrameworkCore.Migrations;

namespace RofoServer.Persistence.Migrations
{
    public partial class wipwip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupIdId",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RofoGroup_RofoGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RofoGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RofoGroupId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GroupIdId",
                table: "UserClaim",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaim_GroupIdId",
                table: "UserClaim",
                newName: "IX_UserClaim_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupId",
                table: "UserClaim",
                column: "GroupId",
                principalTable: "RofoGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupId",
                table: "UserClaim");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "UserClaim",
                newName: "GroupIdId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaim_GroupId",
                table: "UserClaim",
                newName: "IX_UserClaim_GroupIdId");

            migrationBuilder.AddColumn<int>(
                name: "RofoGroupId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RofoGroupId",
                table: "Users",
                column: "RofoGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupIdId",
                table: "UserClaim",
                column: "GroupIdId",
                principalTable: "RofoGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RofoGroup_RofoGroupId",
                table: "Users",
                column: "RofoGroupId",
                principalTable: "RofoGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
