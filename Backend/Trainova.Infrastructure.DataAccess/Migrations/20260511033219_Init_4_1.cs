using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init_4_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Players_PlayerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_TeamStaffs_TeamStaffId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlayerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TeamStaffId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamStaffId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamStaffId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlayerId",
                table: "Users",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamStaffId",
                table: "Users",
                column: "TeamStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Players_PlayerId",
                table: "Users",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TeamStaffs_TeamStaffId",
                table: "Users",
                column: "TeamStaffId",
                principalTable: "TeamStaffs",
                principalColumn: "Id");
        }
    }
}
