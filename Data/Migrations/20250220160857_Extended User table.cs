using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace centuras.org.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e5bb062-ff26-4392-abf4-9baedd3f40ba", new DateOnly(1988, 8, 19), "Trenton", "Scott", "AQAAAAIAAYagAAAAEMTHmn02paUsgjR1p+E91/fJvKGfi9G84FYl9Fqutu2uagYcRYfGqkdhJvUVzgKlKg==", "aab19587-7330-462d-9e71-a6dab4a876d2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "092e8775-aaa5-4ea6-9ccc-a9fa1e39d1a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4e385f3-da51-42f8-868e-574b34100c7d", "AQAAAAIAAYagAAAAEDv9N0w+XRTlW7CWOqYa068AotRA5Frg0prIL3ko9Pqx/guxdAVpo7JkThfoRHY5ZA==", "eb40a90d-cb08-4472-9908-04a4fcac4eb1" });
        }
    }
}
