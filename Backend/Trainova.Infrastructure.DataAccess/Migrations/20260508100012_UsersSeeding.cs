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
                    "TFAEnabledAt",
                    "Role"
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
                null,
                "SystemAdmin"
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
                null,
                "SystemAdmin"
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
                null,
                "SystemAdmin"
            },
            // ===============================
            // TeamStaff (users only)
            // ===============================
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
                null,
                "SystemAdmin"
            },

            {
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"),
                "System Owner Amr 1",
                "Amr Mousv 1",
                "am7899@fayoum.edu.eg",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null,
                "Doctor"
            },

            {
                Guid.Parse("ffffffff-ffff-ffff-ffff-fffffffffff1"), // Id
                "System Owner Amr 2",
                "Amr Mousv 2",
                "amrdesigner378@gmail.com",
                teamId,
                "RESET_REQUIRED",
                true,
                true,
                createdAt,
                createdAt,
                Guid.Empty,
                null,
                false,
                null,
                "HeadCoach"
            },

            {
                Guid.Parse("ffffffff-ffff-ffff-ffff-fffffffffff2"), // Id
                "System Owner Amr 3",
                "Amr Mousv 3",
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
                null,
                "SystemAdmin"
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
                null,
                "Player"
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
                null,
                "Player"
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
                null,
                "Player"
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
                null,
                "Player"
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
                    // System Owners / Staff
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"),
                    Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"),
                    Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"),
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"),
                    Guid.Parse("ffffffff-ffff-ffff-ffff-fffffffffff1"), // Amr 2
                    Guid.Parse("ffffffff-ffff-ffff-ffff-fffffffffff2"), // Amr 3

                    // Players
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                    Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb"),
                    Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb"),
                    Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb")
                }
            );
        }

    }

}
