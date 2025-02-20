using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace centuras.org.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "300b643c-ed3e-405f-8e2c-358b7df75f9c", null, "Employee", "EMPLOYEE" },
                    { "88c66549-aabb-4e28-a8cb-c37b0303d932", null, "Administrator", "ADMINISTRATOR" },
                    { "98a350dd-ca1f-4339-bc59-6a61547f534a", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0", 0, "d4e385f3-da51-42f8-868e-574b34100c7d", "tpscott@centuras.org", true, false, null, "TPSCOTT@CENTURAS.ORG", "TPSCOTT", "AQAAAAIAAYagAAAAEDv9N0w+XRTlW7CWOqYa068AotRA5Frg0prIL3ko9Pqx/guxdAVpo7JkThfoRHY5ZA==", null, false, "eb40a90d-cb08-4472-9908-04a4fcac4eb1", false, "tpscott" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "88c66549-aabb-4e28-a8cb-c37b0303d932", "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "300b643c-ed3e-405f-8e2c-358b7df75f9c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98a350dd-ca1f-4339-bc59-6a61547f534a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "88c66549-aabb-4e28-a8cb-c37b0303d932", "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88c66549-aabb-4e28-a8cb-c37b0303d932");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0");
        }
    }
}
