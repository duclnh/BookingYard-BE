using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fieldy.BookingYard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "067427517b4a436f8dc05a678bbf22b4");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "GoogleID",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreateBy", "CreateDate", "DeleteDate", "Email", "ExpirationResetToken", "Gender", "GoogleID", "ImageUrl", "Name", "PasswordHash", "Phone", "Point", "ResetToken", "Role", "UpdateBy", "UpdateDate", "VerificationToken" },
                values: new object[] { "75d87f410add49559e700ee61151d8d9", "123 Main St, City, Country", "75d87f410add49559e700ee61151d8d9", new DateTime(2024, 6, 10, 18, 19, 7, 561, DateTimeKind.Local).AddTicks(2758), null, "john@example.com", null, true, null, null, "John Doe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC", "1234567890", 100, null, (short)4, "75d87f410add49559e700ee61151d8d9", new DateTime(2024, 6, 10, 18, 19, 7, 561, DateTimeKind.Local).AddTicks(2772), "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "75d87f410add49559e700ee61151d8d9");

            migrationBuilder.DropColumn(
                name: "GoogleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreateBy", "CreateDate", "DeleteDate", "Email", "ExpirationResetToken", "Gender", "Name", "PasswordHash", "Phone", "Point", "ResetToken", "Role", "UpdateBy", "UpdateDate", "UserName", "VerificationToken" },
                values: new object[] { "067427517b4a436f8dc05a678bbf22b4", "123 Main St, City, Country", "067427517b4a436f8dc05a678bbf22b4", new DateTime(2024, 6, 4, 1, 40, 6, 68, DateTimeKind.Local).AddTicks(7644), null, "john@example.com", null, true, "John Doe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC", "1234567890", 100, null, (short)4, "067427517b4a436f8dc05a678bbf22b4", new DateTime(2024, 6, 4, 1, 40, 6, 68, DateTimeKind.Local).AddTicks(7657), "johndoe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC" });
        }
    }
}
