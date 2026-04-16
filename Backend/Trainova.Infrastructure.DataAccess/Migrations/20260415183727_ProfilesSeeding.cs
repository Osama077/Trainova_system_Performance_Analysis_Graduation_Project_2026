using Microsoft.EntityFrameworkCore.Migrations;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Profiles.TeamsStaff;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProfilesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createdAt = new DateTime(2026, 1, 1);
            var enrollmentDate = new DateOnly(2026, 1, 1);

            // ===============================
            // Players (Matching User Ids ending in 'b')
            // ===============================
            migrationBuilder.InsertData(
                table: "Players",
                columns: new[]
                {
                "Id",
                "PlayerNumber",
                "TShirtName",
                "MedicalStatus",
                "CurrentMainPosition",
                "OtherAvailablePositions",
                "PerformanceLevel",
                "DateOfEnrolment",
                "CreatedAt",
                "CreatedBy",
                "LastUpdate"
                },
                values: new object[,]
                {
                    {
                        Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                        10,
                        "Ahmed Zain",
                        PlayerMedicalStatus.Fit.ToString(),
                        (int)Position.CAM,
                        (int)(Position.CDM | Position.CB),
                        0m,
                        enrollmentDate,
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                        7,
                        "Mousv",
                        PlayerMedicalStatus.Fit.ToString(),
                        (int)Position.ST,
                        (int)Position.LW,
                        0m,
                        enrollmentDate,
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb"),
                        8,
                        "Nasr",
                        PlayerMedicalStatus.Injured.ToString(),
                        (int)Position.CM,
                        (int)Position.CAM,
                        0m,
                        enrollmentDate,
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb"),
                        5,
                        "Fahmy",
                        PlayerMedicalStatus.Fit.ToString(),
                        (int)Position.CB,
                        (int)Position.RB,
                        0m,
                        enrollmentDate,
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb"),
                        6,
                        "Zezo",
                        PlayerMedicalStatus.Fit.ToString(),
                        (int)Position.RW,
                        (int)Position.LW,
                        0m,
                        enrollmentDate,
                        createdAt,
                        Guid.Empty,
                        null
                    }
                }
            );

            // ===============================
            // TeamStaffs (Matching System Owner Ids ending in 'a' or 'aaaa')
            // ===============================
            migrationBuilder.InsertData(
                table: "TeamStaffs",
                columns: new[]
                {
                "Id",
                "InsuranceFilesLink",
                "ContractFilesLink",
                "Role",
                "CreatedAt",
                "CreatedBy",
                "LastUpdate"
                },
                values: new object[,]
                {
                    {
                        Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                        null,
                        null,
                        TeamStaffRole.headCoach.ToString(),
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"),
                        null,
                        null,
                        TeamStaffRole.headCoach.ToString(),
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"),
                        null,
                        null,
                        TeamStaffRole.headCoach.ToString(),
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"),
                        null,
                        null,
                        TeamStaffRole.headCoach.ToString(),
                        createdAt,
                        Guid.Empty,
                        null
                    },
                    {
                        Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"),
                        null,
                        null,
                        TeamStaffRole.headCoach.ToString(),
                        createdAt,
                        Guid.Empty,
                        null
                    }
                }
            );
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValues: new object[]
                {
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb"),
                Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb"),
                Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb")
                }
            );

            migrationBuilder.DeleteData(
                table: "TeamStaffs",
                keyColumn: "Id",
                keyValues: new object[]
                {
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"),
                Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"),
                Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"),
                Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea")
                }
            );
        }


    }


}
