using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RofoServer.Persistence.Migrations
{
    public partial class latestWip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "UserClaim");

            migrationBuilder.AddColumn<int>(
                name: "RofoGroupId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupIdId",
                table: "UserClaim",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RofoGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RofoGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RofoGroupId",
                table: "Users",
                column: "RofoGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_GroupIdId",
                table: "UserClaim",
                column: "GroupIdId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupIdId",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RofoGroup_RofoGroupId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RofoGroup");

            migrationBuilder.DropIndex(
                name: "IX_Users_RofoGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserClaim_GroupIdId",
                table: "UserClaim");

            migrationBuilder.DropColumn(
                name: "RofoGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupIdId",
                table: "UserClaim");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "UserClaim",
                type: "text",
                nullable: true);
        }
    }
}
