using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RofoServer.Persistence.Migrations
{
    public partial class wipbaby1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupAccess_Groups_GroupId",
                table: "GroupAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAccess_Users_UserId",
                table: "GroupAccess");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupAccess_User_Group",
                table: "GroupAccess");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "GroupAccess");

            migrationBuilder.DropColumn(
                name: "User",
                table: "GroupAccess");

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
        }
    }
}
