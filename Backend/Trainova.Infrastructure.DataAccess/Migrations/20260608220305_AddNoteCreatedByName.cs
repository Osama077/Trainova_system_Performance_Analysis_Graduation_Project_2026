using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteCreatedByName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "ScoutingCandidateNote",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "ScoutingCandidateNote");
        }
    }
}
