using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalEntitledLeaves",
                table: "EmployeeLeavesUsage");

            migrationBuilder.DropColumn(
                name: "TotalUsedLeaveDays",
                table: "EmployeeLeavesUsage");

            migrationBuilder.AddColumn<int>(
                name: "UsedDays",
                table: "EmployeeLeavesUsage",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EntitledLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntitledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitledLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntitledLeaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntitledLeaves_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 86, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntitledLeaves.Admin", null },
                    { 87, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntitledLeaves.Read", null },
                    { 88, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntitledLeaves.Write", null },
                    { 89, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntitledLeaves.Add", null },
                    { 90, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntitledLeaves.Update", null },
                    { 91, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EntitledLeaves.Delete", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 206, 30, 93, 185, 255, 27, 41, 230, 100, 124, 129, 71, 87, 120, 179, 92, 23, 181, 182, 185, 203, 104, 98, 134, 173, 3, 184, 106, 199, 28, 21, 202, 187, 160, 44, 154, 90, 239, 84, 137, 24, 231, 43, 138, 233, 240, 187, 218, 110, 77, 207, 36, 165, 24, 171, 146, 2, 178, 235, 241, 96, 8, 80, 89 }, new byte[] { 151, 165, 6, 136, 242, 22, 98, 109, 235, 43, 126, 18, 136, 184, 214, 22, 128, 236, 41, 209, 180, 38, 139, 240, 224, 194, 30, 204, 4, 245, 6, 65, 159, 237, 176, 178, 145, 14, 221, 253, 34, 179, 186, 129, 107, 221, 240, 233, 219, 88, 105, 147, 34, 163, 110, 254, 150, 123, 69, 45, 112, 93, 9, 159, 71, 20, 184, 215, 32, 42, 196, 53, 226, 141, 67, 72, 219, 72, 194, 218, 32, 49, 180, 232, 122, 108, 197, 238, 143, 187, 132, 58, 76, 146, 149, 177, 53, 116, 135, 189, 196, 129, 36, 2, 69, 40, 56, 163, 90, 207, 242, 173, 253, 254, 64, 106, 57, 126, 69, 6, 56, 70, 214, 251, 249, 173, 172, 81 } });

            migrationBuilder.CreateIndex(
                name: "IX_EntitledLeaves_EmployeeId",
                table: "EntitledLeaves",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitledLeaves_LeaveTypeId",
                table: "EntitledLeaves",
                column: "LeaveTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntitledLeaves");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DropColumn(
                name: "UsedDays",
                table: "EmployeeLeavesUsage");

            migrationBuilder.AddColumn<int>(
                name: "TotalEntitledLeaves",
                table: "EmployeeLeavesUsage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalUsedLeaveDays",
                table: "EmployeeLeavesUsage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 210, 134, 214, 200, 108, 11, 197, 181, 201, 203, 138, 57, 100, 54, 124, 174, 221, 46, 211, 208, 89, 88, 140, 189, 60, 235, 135, 11, 255, 52, 248, 220, 143, 61, 96, 239, 44, 200, 61, 42, 226, 219, 1, 26, 9, 187, 91, 176, 90, 251, 154, 118, 25, 101, 98, 165, 45, 255, 14, 176, 147, 61, 221, 97 }, new byte[] { 229, 110, 159, 3, 129, 116, 35, 83, 5, 60, 193, 99, 96, 249, 163, 245, 251, 64, 203, 229, 189, 70, 150, 81, 38, 200, 8, 216, 155, 157, 57, 92, 140, 25, 205, 10, 26, 179, 13, 247, 135, 125, 62, 212, 118, 104, 171, 162, 20, 146, 41, 61, 93, 170, 3, 88, 101, 95, 178, 204, 184, 95, 76, 102, 173, 93, 66, 146, 123, 162, 173, 172, 167, 173, 171, 222, 253, 113, 52, 194, 222, 185, 42, 114, 242, 31, 144, 163, 207, 193, 207, 188, 241, 160, 254, 99, 246, 85, 144, 48, 231, 92, 112, 31, 80, 198, 73, 19, 218, 68, 175, 77, 149, 90, 64, 58, 57, 231, 34, 203, 168, 3, 22, 29, 93, 94, 56, 8 } });
        }
    }
}
