using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UsersSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var teamId =
                Guid.Parse("11111111-1111-1111-1111-111111111111");

            var createdAt =
                new DateTime(2026, 1, 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id",
                    "ShowName",
                    "FullName",
                    "Email",
                    "TeamId",
                    "PasswordHash",
                    "IsActive",
                    "IsEmailConfirmed",
                    "ConfirmedAt",
                    "CreatedAt",
                    "CreatedBy",
                    "LastUpdate",
                    "IsTFAEnabled",
                    "TFAEnabledAt"
                },
                values: new object[,]
                {
            // ===============================
            // System Owners
            // ===============================

            {
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                "System Owner Ahmed",
                "Ahmed kh Zain",
                "ahmed.kh.zain2156@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },

            {
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"),
                "System Owner Amr",
                "Amr Mousv",
                "am7899@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },

            {
                Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"),
                "System Owner Osama",
                "Osama Nasr",
                "Osamanasserm524@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },
            {
                Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"),
                "System Owner Fahmy",
                "Fahmy",
                "abode1029fahmy38@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },
            {
                Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"),
                "System Owner Zezo",
                "Zezo",
                "zeyadahmed20042020@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },

            // ===============================
            // Players (users only)
            // ===============================

            {
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                "Player Ahmed Zain",
                "Ahmed Zain",
                "eltwo3m@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },

            {
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                "Player Amr Mousv",
                "Mousv",
                "amrworkfront@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },
            {
                Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb"),
                "Player Osama",
                "Nasr",
                "Osamanasserm125@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },
            {
                Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb"),
                "Player Fahmy",
                "Fahmy",
                "abdalrahmanmohamedf@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            },

            {
                Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb"),
                "Player Zezo",
                "Zezo",
                "zezoahmed20042022@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null
            }
                }
            );
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"),
                    Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"),
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"),
                    Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"),


                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb"),
                    Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb"),
                    Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb")
                }
            );
        }

    }

}
