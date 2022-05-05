using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RofoServer.Persistence.Migrations
{
    public partial class initazure1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RofoComment",
                columns: table => new
                {
                    RofoCommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UploadedById = table.Column<int>(type: "integer", nullable: true),
                    ParentPhotoRofoId = table.Column<int>(type: "integer", nullable: true),
                    UploadedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Visible = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RofoComment", x => x.RofoCommentId);
                    table.ForeignKey(
                        name: "FK_RofoComment_Rofos_ParentPhotoRofoId",
                        column: x => x.ParentPhotoRofoId,
                        principalTable: "Rofos",
                        principalColumn: "RofoId");
                    table.ForeignKey(
                        name: "FK_RofoComment_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RofoComment_ParentPhotoRofoId",
                table: "RofoComment",
                column: "ParentPhotoRofoId");

            migrationBuilder.CreateIndex(
                name: "IX_RofoComment_UploadedById",
                table: "RofoComment",
                column: "UploadedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RofoComment");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Groups");
        }
    }
}
