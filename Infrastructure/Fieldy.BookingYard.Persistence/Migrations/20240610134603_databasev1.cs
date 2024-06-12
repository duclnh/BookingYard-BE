using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fieldy.BookingYard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class databasev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "75d87f410add49559e700ee61151d8d9");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreateBy", "CreateDate", "DeleteDate", "Email", "ExpirationResetToken", "Gender", "GoogleID", "ImageUrl", "Name", "PasswordHash", "Phone", "Point", "ResetToken", "Role", "UpdateBy", "UpdateDate", "VerificationToken" },
                values: new object[] { "593a161453f34aa79ad2edb2aed4fce4", "123 Main St, City, Country", "593a161453f34aa79ad2edb2aed4fce4", new DateTime(2024, 6, 10, 20, 46, 3, 105, DateTimeKind.Local).AddTicks(2536), null, "john@example.com", null, true, null, null, "John Doe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC", "1234567890", 100, null, (short)4, "593a161453f34aa79ad2edb2aed4fce4", new DateTime(2024, 6, 10, 20, 46, 3, 105, DateTimeKind.Local).AddTicks(2547), "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "593a161453f34aa79ad2edb2aed4fce4");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreateBy", "CreateDate", "DeleteDate", "Email", "ExpirationResetToken", "Gender", "GoogleID", "ImageUrl", "Name", "PasswordHash", "Phone", "Point", "ResetToken", "Role", "UpdateBy", "UpdateDate", "VerificationToken" },
                values: new object[] { "75d87f410add49559e700ee61151d8d9", "123 Main St, City, Country", "75d87f410add49559e700ee61151d8d9", new DateTime(2024, 6, 10, 18, 19, 7, 561, DateTimeKind.Local).AddTicks(2758), null, "john@example.com", null, true, null, null, "John Doe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC", "1234567890", 100, null, (short)4, "75d87f410add49559e700ee61151d8d9", new DateTime(2024, 6, 10, 18, 19, 7, 561, DateTimeKind.Local).AddTicks(2772), "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC" });
        }
    }
}
