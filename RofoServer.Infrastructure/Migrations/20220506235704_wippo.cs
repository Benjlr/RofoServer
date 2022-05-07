using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RofoServer.Persistence.Migrations
{
    public partial class wippo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileMetaData",
                table: "Rofos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecurityStamp",
                table: "Rofos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileMetaData",
                table: "Rofos");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Rofos");
        }
    }
}
