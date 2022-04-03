using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RofoServer.Persistence.Migrations
{
    public partial class wipbaby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupId",
                table: "UserClaim");

            migrationBuilder.DropIndex(
                name: "IX_UserClaim_GroupId",
                table: "UserClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RofoGroup",
                table: "RofoGroup");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "UserClaim");

            migrationBuilder.RenameTable(
                name: "RofoGroup",
                newName: "Groups");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "UserClaim",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "UserClaim",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutExpiry",
                table: "UserAuthentication",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadedDate",
                table: "Rofos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Rofos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Rofos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Revoked",
                table: "RefreshToken",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "RefreshToken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshToken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GroupAccess",
                
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    GroupId = table.Column<int>(type: "integer", nullable: true),
                    Rights = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupAccess_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupAccess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rofos_GroupId",
                table: "Rofos",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAccess_GroupId",
                table: "GroupAccess",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAccess_UserId",
                table: "GroupAccess",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rofos_Groups_GroupId",
                table: "Rofos",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rofos_Groups_GroupId",
                table: "Rofos");

            migrationBuilder.DropTable(
                name: "GroupAccess");

            migrationBuilder.DropIndex(
                name: "IX_Rofos_GroupId",
                table: "Rofos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "UserClaim");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Rofos");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Rofos");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "RofoGroup");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "UserClaim",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "UserClaim",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockOutExpiry",
                table: "UserAuthentication",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadedDate",
                table: "Rofos",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Revoked",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RofoGroup",
                table: "RofoGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_GroupId",
                table: "UserClaim",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_RofoGroup_GroupId",
                table: "UserClaim",
                column: "GroupId",
                principalTable: "RofoGroup",
                principalColumn: "Id");
        }
    }
}
