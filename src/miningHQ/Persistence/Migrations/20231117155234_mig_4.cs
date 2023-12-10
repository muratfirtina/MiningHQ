using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveUsages");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveUsages");

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeavesUsage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalEntitledLeaves = table.Column<int>(type: "integer", nullable: false),
                    TotalUsedLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeavesUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeavesUsage_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeavesUsage_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 134, 214, 200, 108, 11, 197, 181, 201, 203, 138, 57, 100, 54, 124, 174, 221, 46, 211, 208, 89, 88, 140, 189, 60, 235, 135, 11, 255, 52, 248, 220, 143, 61, 96, 239, 44, 200, 61, 42, 226, 219, 1, 26, 9, 187, 91, 176, 90, 251, 154, 118, 25, 101, 98, 165, 45, 255, 14, 176, 147, 61, 221, 97 }, new byte[] { 229, 110, 159, 3, 129, 116, 35, 83, 5, 60, 193, 99, 96, 249, 163, 245, 251, 64, 203, 229, 189, 70, 150, 81, 38, 200, 8, 216, 155, 157, 57, 92, 140, 25, 205, 10, 26, 179, 13, 247, 135, 125, 62, 212, 118, 104, 171, 162, 20, 146, 41, 61, 93, 170, 3, 88, 101, 95, 178, 204, 184, 95, 76, 102, 173, 93, 66, 146, 123, 162, 173, 172, 167, 173, 171, 222, 253, 113, 52, 194, 222, 185, 42, 114, 242, 31, 144, 163, 207, 193, 207, 188, 241, 160, 254, 99, 246, 85, 144, 48, 231, 92, 112, 31, 80, 198, 73, 19, 218, 68, 175, 77, 149, 90, 64, 58, 57, 231, 34, 203, 168, 3, 22, 29, 93, 94, 56, 8 } });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeavesUsage_EmployeeId",
                table: "EmployeeLeavesUsage",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeavesUsage_LeaveTypeId",
                table: "EmployeeLeavesUsage",
                column: "LeaveTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeavesUsage");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveUsages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalLeaveDays = table.Column<int>(type: "integer", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveUsages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeLeaveId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveUsages_EmployeeLeaves_EmployeeLeaveId",
                        column: x => x.EmployeeLeaveId,
                        principalTable: "EmployeeLeaveUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 236, 84, 198, 126, 159, 141, 145, 48, 204, 201, 248, 96, 145, 146, 76, 131, 169, 229, 20, 215, 1, 173, 101, 48, 59, 204, 112, 52, 65, 255, 203, 42, 105, 241, 94, 195, 85, 10, 241, 47, 177, 138, 95, 222, 212, 224, 61, 217, 194, 160, 160, 138, 181, 176, 77, 25, 163, 84, 232, 97, 145, 159, 153 }, new byte[] { 94, 239, 194, 22, 183, 222, 117, 70, 218, 34, 213, 103, 253, 207, 139, 201, 150, 125, 118, 89, 203, 252, 169, 123, 184, 162, 216, 30, 195, 5, 188, 31, 152, 238, 54, 17, 247, 127, 23, 9, 10, 249, 90, 119, 65, 62, 236, 116, 63, 205, 187, 218, 107, 167, 242, 204, 8, 3, 236, 144, 104, 37, 162, 215, 163, 42, 118, 157, 198, 47, 122, 212, 240, 73, 99, 102, 203, 64, 217, 23, 60, 240, 204, 124, 125, 232, 31, 143, 101, 184, 118, 168, 227, 151, 7, 95, 56, 27, 159, 51, 106, 65, 57, 73, 251, 20, 134, 230, 71, 52, 45, 238, 13, 166, 79, 85, 215, 160, 177, 66, 8, 148, 185, 40, 131, 198, 32, 18 } });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "EmployeeLeaveUsages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveUsages_EmployeeLeaveId",
                table: "LeaveUsages",
                column: "EmployeeLeaveId");
        }
    }
}
