using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace centuras.org.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d7c89bf4-484d-409e-be06-7ce48e2f7059", "TPSCOTT@CENTURAS.ORG", "AQAAAAIAAYagAAAAEEnUtnXfVd7wQtsVQlzug3TU3v5MtfYYVL1J+QWWICjiYS8uafcbPraUoJayWk9LMA==", "9745ceef-c53a-43b6-9793-a63777ee909e", "tpscott@centuras.org" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2e5bb062-ff26-4392-abf4-9baedd3f40ba", "TPSCOTT", "AQAAAAIAAYagAAAAEMTHmn02paUsgjR1p+E91/fJvKGfi9G84FYl9Fqutu2uagYcRYfGqkdhJvUVzgKlKg==", "aab19587-7330-462d-9e71-a6dab4a876d2", "tpscott" });
        }
    }
}
