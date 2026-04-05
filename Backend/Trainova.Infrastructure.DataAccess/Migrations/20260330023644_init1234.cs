using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "EmailOutboxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmailType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
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
                    InjuryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HomeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeScore = table.Column<byte>(type: "tinyint", nullable: false),
                    AwayScore = table.Column<byte>(type: "tinyint", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchWeek = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanGoul = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_Plans_AccessPolicy_AccessPolicyId",
                        column: x => x.AccessPolicyId,
                        principalTable: "AccessPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsuranceFilesLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractFilesLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coaches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    TShirtName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedecalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMainPosition = table.Column<int>(type: "int", nullable: false),
                    OtherAvailablePositions = table.Column<int>(type: "int", nullable: false),
                    PerformanceLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfEnrolment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Team_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    TokenType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "ComputedFeature",
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
                    table.PrimaryKey("PK_ComputedFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputedFeature_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputedFeature_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
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
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationX = table.Column<double>(type: "float", nullable: true),
                    LocationY = table.Column<double>(type: "float", nullable: true),
                    PassLength = table.Column<double>(type: "float", nullable: true),
                    PassAngle = table.Column<double>(type: "float", nullable: true),
                    PassOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassEndX = table.Column<double>(type: "float", nullable: true),
                    PassEndY = table.Column<double>(type: "float", nullable: true),
                    IsProgressivePass = table.Column<bool>(type: "bit", nullable: true),
                    ShotOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShotXg = table.Column<double>(type: "float", nullable: true),
                    ShotTechnique = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShotEndX = table.Column<double>(type: "float", nullable: true),
                    ShotEndY = table.Column<double>(type: "float", nullable: true),
                    ShotAfterSetPiece = table.Column<bool>(type: "bit", nullable: true),
                    PredictedXg = table.Column<double>(type: "float", nullable: true),
                    BodyPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventIndex = table.Column<int>(type: "int", nullable: true),
                    CarryEndX = table.Column<double>(type: "float", nullable: true),
                    CarryEndY = table.Column<double>(type: "float", nullable: true),
                    DribbleOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PressureDuration = table.Column<double>(type: "float", nullable: true),
                    UnderPressure = table.Column<bool>(type: "bit", nullable: true),
                    CounterPress = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lineup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartingPosition = table.Column<int>(type: "int", nullable: true),
                    IsStarter = table.Column<bool>(type: "bit", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lineup_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineup_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lineup_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelScore",
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
                    PlayerCluster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformanceTrend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelScore_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelScore_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
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
                    Status = table.Column<int>(type: "int", nullable: false),
                    Cause = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_PlayerInjuries_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPoliciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_UserAccessPolicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccessPolicy_AccessPolicy_AccessPolicyId",
                        column: x => x.AccessPolicyId,
                        principalTable: "AccessPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccessPolicy_TrainingSessions_TrainingSessionId",
                        column: x => x.TrainingSessionId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAccessPolicy_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_TeamId",
                table: "Coaches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_UserId",
                table: "Coaches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedFeature_MatchId",
                table: "ComputedFeature",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputedFeature_PlayerId",
                table: "ComputedFeature",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_MatchId",
                table: "Event",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PlayerId",
                table: "Event",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TeamId",
                table: "Event",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineup_MatchId",
                table: "Lineup",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineup_PlayerId",
                table: "Lineup",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineup_TeamId",
                table: "Lineup",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelScore_MatchId",
                table: "ModelScore",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelScore_PlayerId",
                table: "ModelScore",
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
                name: "IX_Players_CurrentTeamId",
                table: "Players",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_PlanId",
                table: "TrainingSessions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicy_AccessPolicyId",
                table: "UserAccessPolicy",
                column: "AccessPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicy_TrainingSessionId",
                table: "UserAccessPolicy",
                column: "TrainingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessPolicy_UserId",
                table: "UserAccessPolicy",
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
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "ComputedFeature");

            migrationBuilder.DropTable(
                name: "EmailOutboxes");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Lineup");

            migrationBuilder.DropTable(
                name: "ModelScore");

            migrationBuilder.DropTable(
                name: "PlayerInjuries");

            migrationBuilder.DropTable(
                name: "UserAccessPolicy");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Injuries");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "AccessPolicy");
        }
    }
}
