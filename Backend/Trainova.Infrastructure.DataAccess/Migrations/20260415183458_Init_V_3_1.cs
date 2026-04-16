using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init_V_3_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRecovered = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RecoveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecoveredByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutboxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmailType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Injuries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    InjuryType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    AverageRecoveryTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injuries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionBenchmarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionGroup = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    AvgPassAccuracy = table.Column<double>(type: "float", nullable: true),
                    AvgProgressivePasses = table.Column<double>(type: "float", nullable: true),
                    AvgPressures = table.Column<double>(type: "float", nullable: true),
                    AvgDistanceCovered = table.Column<double>(type: "float", nullable: true),
                    AvgXg = table.Column<double>(type: "float", nullable: true),
                    AvgVaep = table.Column<double>(type: "float", nullable: true),
                    WeightPassing = table.Column<double>(type: "float", nullable: true),
                    WeightShooting = table.Column<double>(type: "float", nullable: true),
                    WeightPositioning = table.Column<double>(type: "float", nullable: true),
                    WeightPressing = table.Column<double>(type: "float", nullable: true),
                    WeightMovement = table.Column<double>(type: "float", nullable: true),
                    WeightPhysical = table.Column<double>(type: "float", nullable: true),
                    WeightBehavioral = table.Column<double>(type: "float", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionBenchmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PlanGoul = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AccessPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_AccessPolicies_AccessPolicyId",
                        column: x => x.AccessPolicyId,
                        principalTable: "AccessPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoutingCandidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    PerformanceScore = table.Column<float>(type: "real", nullable: false),
                    InjuryRisk = table.Column<float>(type: "real", nullable: false),
                    MedecalStatus = table.Column<int>(type: "int", nullable: false),
                    CurrentMainPosition = table.Column<int>(type: "int", nullable: false),
                    OtherAvailablePositions = table.Column<int>(type: "int", nullable: false),
                    PerformanceLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutingCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoutingCandidates_Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputedFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPasses = table.Column<int>(type: "int", nullable: false),
                    PassAccuracy = table.Column<double>(type: "float", nullable: false),
                    ProgressivePasses = table.Column<int>(type: "int", nullable: false),
                    PassesUnderPressure = table.Column<int>(type: "int", nullable: false),
                    TotalShots = table.Column<int>(type: "int", nullable: false),
                    ShotsOnTarget = table.Column<int>(type: "int", nullable: false),
                    ShotAccuracy = table.Column<double>(type: "float", nullable: false),
                    TotalXg = table.Column<double>(type: "float", nullable: false),
                    XgPerShot = table.Column<double>(type: "float", nullable: false),
                    AvgPositionX = table.Column<double>(type: "float", nullable: false),
                    AvgPositionY = table.Column<double>(type: "float", nullable: false),
                    PositionDeviation = table.Column<double>(type: "float", nullable: false),
                    TotalPressures = table.Column<int>(type: "int", nullable: false),
                    PressureRegains = table.Column<int>(type: "int", nullable: false),
                    Ppda = table.Column<double>(type: "float", nullable: false),
                    DistanceCovered = table.Column<double>(type: "float", nullable: false),
                    ProgressiveCarries = table.Column<int>(type: "int", nullable: false),
                    CarrySuccessRate = table.Column<double>(type: "float", nullable: false),
                    DribbleSuccessRate = table.Column<double>(type: "float", nullable: false),
                    ActionsPer90 = table.Column<double>(type: "float", nullable: false),
                    ActivityDrop2ndHalf = table.Column<double>(type: "float", nullable: false),
                    AvgSpeed = table.Column<double>(type: "float", nullable: false),
                    SprintCount = table.Column<int>(type: "int", nullable: false),
                    HighIntensityActions = table.Column<int>(type: "int", nullable: false),
                    FoulsCommitted = table.Column<int>(type: "int", nullable: false),
                    FoulsWon = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false),
                    BallRetentionRate = table.Column<double>(type: "float", nullable: false),
                    VaepRating = table.Column<double>(type: "float", nullable: false),
                    OffensiveValue = table.Column<double>(type: "float", nullable: false),
                    DefensiveValue = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputedFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<TimeOnly>(type: "time", nullable: false),
                    Minute = table.Column<byte>(type: "tinyint", nullable: false),
                    Second = table.Column<byte>(type: "tinyint", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    LocationX = table.Column<double>(type: "float", nullable: true),
                    LocationY = table.Column<double>(type: "float", nullable: true),
                    PassLength = table.Column<double>(type: "float", nullable: true),
                    PassAngle = table.Column<double>(type: "float", nullable: true),
                    PassOutcome = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    PassEndX = table.Column<double>(type: "float", nullable: true),
                    PassEndY = table.Column<double>(type: "float", nullable: true),
                    IsProgressivePass = table.Column<bool>(type: "bit", nullable: true),
                    ShotOutcome = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ShotXg = table.Column<double>(type: "float", nullable: true),
                    ShotTechnique = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ShotEndX = table.Column<double>(type: "float", nullable: true),
                    ShotEndY = table.Column<double>(type: "float", nullable: true),
                    ShotAfterSetPiece = table.Column<bool>(type: "bit", nullable: true),
                    PredictedXg = table.Column<double>(type: "float", nullable: true),
                    BodyPart = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    EventIndex = table.Column<int>(type: "int", nullable: true),
                    CarryEndX = table.Column<double>(type: "float", nullable: true),
                    CarryEndY = table.Column<double>(type: "float", nullable: true),
                    DribbleOutcome = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    PressureDuration = table.Column<double>(type: "float", nullable: true),
                    UnderPressure = table.Column<bool>(type: "bit", nullable: true),
                    CounterPress = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartingPosition = table.Column<int>(type: "int", nullable: false),
                    IsStarter = table.Column<bool>(type: "bit", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lineups_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HomeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeScore = table.Column<byte>(type: "tinyint", nullable: false),
                    AwayScore = table.Column<byte>(type: "tinyint", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Referee = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    MatchWeek = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchVideos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ObjectStoregeProviderId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RelatedMatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchVideos_Matches_RelatedMatchId",
                        column: x => x.RelatedMatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassingScore = table.Column<double>(type: "float", nullable: false),
                    ShootingScore = table.Column<double>(type: "float", nullable: false),
                    PositioningScore = table.Column<double>(type: "float", nullable: false),
                    PressingScore = table.Column<double>(type: "float", nullable: false),
                    MovementScore = table.Column<double>(type: "float", nullable: false),
                    PhysicalScore = table.Column<double>(type: "float", nullable: false),
                    BehavioralScore = table.Column<double>(type: "float", nullable: false),
                    OverallScore = table.Column<double>(type: "float", nullable: false),
                    PositionFitScore = table.Column<double>(type: "float", nullable: false),
                    PercentileInTeam = table.Column<double>(type: "float", nullable: false),
                    PercentileInLeague = table.Column<double>(type: "float", nullable: false),
                    PercentileInPosition = table.Column<double>(type: "float", nullable: false),
                    PlayerCluster = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    PerformanceTrend = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelScores_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerInjuries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InjuryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cause = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SevertiyGrade = table.Column<int>(type: "int", nullable: false),
                    BodyPart = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HappendAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInjuries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerInjuries_Injuries_InjuryId",
                        column: x => x.InjuryId,
                        principalTable: "Injuries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    TShirtName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedicalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CurrentMainPosition = table.Column<int>(type: "int", nullable: false),
                    OtherAvailablePositions = table.Column<int>(type: "int", nullable: false),
                    PerformanceLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfEnrolment = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionMovement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintsCount = table.Column<int>(type: "int", nullable: true),
                    TotalDistance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    WalkDistance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    RunDistance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    HighSpeedRunDistance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    AverageSpeed = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    MaxSpeed = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    PeakAcceleration = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    PlayerLoad = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionMovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionMovement_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamStaffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsuranceFilesLink = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ContractFilesLink = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStaffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShowName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsTFAEnabled = table.Column<bool>(type: "bit", nullable: false),
                    TFAEnabledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(2400)", maxLength: 2400, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_TeamStaffs_TeamStaffId",
                        column: x => x.TeamStaffId,
                        principalTable: "TeamStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<byte>(type: "tinyint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId1 = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokeCause = table.Column<int>(type: "int", nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingSessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserAccessPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SessionState = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HappenedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSessions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAccessPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPoliciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceState = table.Column<int>(type: "int", nullable: false),
                    DoneScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrainingSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccessPolicies_AccessPolicies_AccessPoliciesId",
                        column: x => x.AccessPoliciesId,
                        principalTable: "AccessPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccessPolicies_TrainingSessions_TrainingSessionId",
                        column: x => x.TrainingSessionId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAccessPolicies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Action",
                table: "AuditLogs",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ChangedAt",
                table: "AuditLogs",
                column: "ChangedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityId",
                table: "AuditLogs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName",
                table: "AuditLogs",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_IsRecovered",
                table: "AuditLogs",
                column: "IsRecovered");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_SeasonId",
                table: "Competitions",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedFeatures_MatchId",
                table: "ComputedFeatures",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedFeatures_PlayerId",
                table: "ComputedFeatures",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_MatchId",
                table: "Events",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlayerId",
                table: "Events",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TeamId",
                table: "Events",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Injuries_InjuryType",
                table: "Injuries",
                column: "InjuryType");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_MatchId",
                table: "Lineups",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_PlayerId",
                table: "Lineups",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_StartingPosition",
                table: "Lineups",
                column: "StartingPosition");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_TeamId",
                table: "Lineups",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionId",
                table: "Matches",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDate",
                table: "Matches",
                column: "MatchDate");

            migrationBuilder.CreateIndex(
                name: "IX_MatchVideos_RelatedMatchId",
                table: "MatchVideos",
                column: "RelatedMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelScores_MatchId",
                table: "ModelScores",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelScores_PlayerId",
                table: "ModelScores",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_AccessPolicyId",
                table: "Plans",
                column: "AccessPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInjuries_InjuryId",
                table: "PlayerInjuries",
                column: "InjuryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInjuries_PlayerId",
                table: "PlayerInjuries",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInjuries_Status",
                table: "PlayerInjuries",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PositionBenchmarks_CompetitionId",
                table: "PositionBenchmarks",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionBenchmarks_SeasonId",
                table: "PositionBenchmarks",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutingCandidates_CurrentTeamId",
                table: "ScoutingCandidates",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionMovement_PlayerId",
                table: "SessionMovement",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionMovement_TrainingSessionId",
                table: "SessionMovement",
                column: "TrainingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_PlanId",
                table: "TrainingSessions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_UserAccessPolicyId",
                table: "TrainingSessions",
                column: "UserAccessPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicies_AccessPoliciesId",
                table: "UserAccessPolicies",
                column: "AccessPoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicies_TrainingSessionId",
                table: "UserAccessPolicies",
                column: "TrainingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicies_UserId",
                table: "UserAccessPolicies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId1",
                table: "UserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlayerId",
                table: "Users",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamStaffId",
                table: "Users",
                column: "TeamStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputedFeatures_Matches_MatchId",
                table: "ComputedFeatures",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputedFeatures_Players_PlayerId",
                table: "ComputedFeatures",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Matches_MatchId",
                table: "Events",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Matches_MatchId",
                table: "Lineups",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_PlayerId",
                table: "Lineups",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TrainingSessions_Id",
                table: "Matches",
                column: "Id",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelScores_Players_PlayerId",
                table: "ModelScores",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerInjuries_Players_PlayerId",
                table: "PlayerInjuries",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Users_Id",
                table: "Players",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionMovement_TrainingSessions_TrainingSessionId",
                table: "SessionMovement",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStaffs_Users_Id",
                table: "TeamStaffs",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_UserAccessPolicies_UserAccessPolicyId",
                table: "TrainingSessions",
                column: "UserAccessPolicyId",
                principalTable: "UserAccessPolicies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Players_PlayerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_TeamId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessPolicies_TrainingSessions_TrainingSessionId",
                table: "UserAccessPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamStaffs_Users_Id",
                table: "TeamStaffs");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ComputedFeatures");

            migrationBuilder.DropTable(
                name: "EmailOutboxes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Lineups");

            migrationBuilder.DropTable(
                name: "MatchVideos");

            migrationBuilder.DropTable(
                name: "ModelScores");

            migrationBuilder.DropTable(
                name: "PlayerInjuries");

            migrationBuilder.DropTable(
                name: "PositionBenchmarks");

            migrationBuilder.DropTable(
                name: "ScoutingCandidates");

            migrationBuilder.DropTable(
                name: "SessionMovement");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Injuries");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "UserAccessPolicies");

            migrationBuilder.DropTable(
                name: "AccessPolicies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TeamStaffs");
        }
    }
}
