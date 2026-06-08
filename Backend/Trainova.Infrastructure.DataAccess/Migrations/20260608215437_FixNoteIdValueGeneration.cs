using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixNoteIdValueGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ScoutingCandidateNote",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "ScoutingCandidateNote",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200);
        }
    }
}
