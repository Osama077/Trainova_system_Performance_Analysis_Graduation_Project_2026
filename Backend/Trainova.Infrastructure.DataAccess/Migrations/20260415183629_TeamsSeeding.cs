using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    /// <inheritdoc />
    public partial class TeamsSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var teamId =
                Guid.Parse("11111111-1111-1111-1111-111111111111");

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[]
                {
                    "Id",
                    "TeamName",
                    "Country",
                    "CreatedAt",
                    "CreatedBy",
                    "LastUpdate"
                },
                values: new object[]
                {
                    teamId,
                    "the Stupids",
                    "El fayoum dawla",
                    new DateTime(2026, 1, 1),
                    Guid.Empty,
                    null
                }
            );
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue:
                    Guid.Parse("11111111-1111-1111-1111-111111111111")
            );
        }

    }


}
