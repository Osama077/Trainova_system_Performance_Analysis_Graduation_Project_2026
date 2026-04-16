using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { (byte)0, "SystemOwner", "systemowner" },
                    { (byte)8, "TestAccount", "testaccount" },
                    { (byte)1, "SystemAdmin", "systemadmin" },
                    { (byte)2, "Player", "player" },
                    { (byte)3, "TeamStaff", "teamstaff" },
                    { (byte)4, "HeadCoach", "headcoach" },
                    { (byte)5, "AssistantCoach", "assistantcoach" },
                    { (byte)6, "Doctor", "doctor" },
                    { (byte)7, "FitnessCoach", "fitnesscoach" }
                }
            );
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    (byte)0,
                    (byte)8,
                    (byte)1,
                    (byte)2,
                    (byte)3,
                    (byte)4,
                    (byte)5,
                    (byte)6,
                    (byte)7
                }
            );
        }


    }


}
