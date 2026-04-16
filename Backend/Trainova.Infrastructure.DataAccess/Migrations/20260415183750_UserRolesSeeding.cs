using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainova.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createdAt = new DateTime(2026, 1, 1);

            // ===============================
            // System Owners (All Roles: 0, 1, 2, 3)
            // ===============================
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedAt" },
                values: new object[,]
                {
                    // Owner Ahmed
                    { (byte)0, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), createdAt },
                    { (byte)1, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), createdAt },
                    { (byte)2, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), createdAt },
                    { (byte)3, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), createdAt },

                    // Owner Amr
                    { (byte)0, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"), createdAt },
                    { (byte)1, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"), createdAt },
                    { (byte)2, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"), createdAt },
                    { (byte)3, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"), createdAt },

                    // Owner Osama
                    { (byte)0, Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"), createdAt },
                    { (byte)1, Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"), createdAt },
                    { (byte)2, Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"), createdAt },
                    { (byte)3, Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"), createdAt },

                    // Owner Fahmy
                    { (byte)0, Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"), createdAt },
                    { (byte)1, Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"), createdAt },
                    { (byte)2, Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"), createdAt },
                    { (byte)3, Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"), createdAt },

                    // Owner Zezo
                    { (byte)0, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"), createdAt },
                    { (byte)1, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"), createdAt },
                    { (byte)2, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"), createdAt },
                    { (byte)3, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"), createdAt }
                });

            // ===============================
            // Players (Role: Player Only - 2)
            // ===============================
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedAt" },
                values: new object[,]
                {
                    { (byte)2, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), createdAt },
                    { (byte)2, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), createdAt },
                    { (byte)2, Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb"), createdAt },
                    { (byte)2, Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb"), createdAt },
                    { (byte)2, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb"), createdAt }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // System Owners
            DeleteRolesForUser(migrationBuilder, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));
            DeleteRolesForUser(migrationBuilder, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbba"));
            DeleteRolesForUser(migrationBuilder, Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccca"));
            DeleteRolesForUser(migrationBuilder, Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddda"));
            DeleteRolesForUser(migrationBuilder, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeea"));

            // Players
            migrationBuilder.DeleteData(table: "UserRoles", keyColumns: new[] { "RoleId", "UserId" }, keyValues: new object[] { (byte)2, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab") });
            migrationBuilder.DeleteData(table: "UserRoles", keyColumns: new[] { "RoleId", "UserId" }, keyValues: new object[] { (byte)2, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") });
            migrationBuilder.DeleteData(table: "UserRoles", keyColumns: new[] { "RoleId", "UserId" }, keyValues: new object[] { (byte)2, Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccb") });
            migrationBuilder.DeleteData(table: "UserRoles", keyColumns: new[] { "RoleId", "UserId" }, keyValues: new object[] { (byte)2, Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddb") });
            migrationBuilder.DeleteData(table: "UserRoles", keyColumns: new[] { "RoleId", "UserId" }, keyValues: new object[] { (byte)2, Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeeb") });
        }

        private static void DeleteRolesForUser(MigrationBuilder migrationBuilder, Guid userId)
        {
            byte[] roles = { 0, 1, 2, 3 };
            foreach (var role in roles)
            {
                migrationBuilder.DeleteData(
                    table: "UserRoles",
                    keyColumns: new[] { "RoleId", "UserId" },
                    keyValues: new object[] { role, userId });
            }
        }

    }


}
