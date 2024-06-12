using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fieldy.BookingYard.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResetToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExpirationResetToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<short>(type: "smallint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserID", x => x.UserID);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreateBy", "CreateDate", "DeleteDate", "Email", "ExpirationResetToken", "Gender", "Name", "PasswordHash", "Phone", "Point", "ResetToken", "Role", "UpdateBy", "UpdateDate", "UserName", "VerificationToken" },
                values: new object[] { "067427517b4a436f8dc05a678bbf22b4", "123 Main St, City, Country", "067427517b4a436f8dc05a678bbf22b4", new DateTime(2024, 6, 4, 1, 40, 6, 68, DateTimeKind.Local).AddTicks(7644), null, "john@example.com", null, true, "John Doe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC", "1234567890", 100, null, (short)4, "067427517b4a436f8dc05a678bbf22b4", new DateTime(2024, 6, 4, 1, 40, 6, 68, DateTimeKind.Local).AddTicks(7657), "johndoe", "$2a$10$OtTTdcHCfgYbdouDOccIr.AnkZHsZif3064o/vd1PQg1iQH5kZqJC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
