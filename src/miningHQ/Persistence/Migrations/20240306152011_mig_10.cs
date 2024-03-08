using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Overtimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    OvertimeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OvertimeHours = table.Column<float>(type: "real", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Overtimes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 98, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Overtimes.Admin", null },
                    { 99, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Overtimes.Read", null },
                    { 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Overtimes.Write", null },
                    { 101, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Overtimes.Add", null },
                    { 102, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Overtimes.Update", null },
                    { 103, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Overtimes.Delete", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 194, 83, 138, 165, 130, 96, 118, 41, 161, 184, 32, 176, 71, 151, 3, 240, 68, 150, 118, 129, 45, 126, 97, 29, 169, 99, 142, 97, 81, 150, 219, 23, 20, 78, 29, 128, 245, 9, 191, 78, 16, 191, 177, 217, 126, 143, 77, 225, 101, 160, 26, 211, 119, 94, 222, 155, 209, 107, 87, 18, 67, 38, 213, 76 }, new byte[] { 251, 111, 203, 8, 147, 143, 136, 89, 175, 162, 44, 236, 83, 101, 27, 197, 124, 107, 168, 182, 130, 243, 100, 51, 32, 31, 186, 68, 55, 62, 143, 70, 72, 97, 201, 110, 146, 43, 143, 241, 25, 135, 186, 114, 65, 247, 226, 53, 46, 2, 6, 99, 198, 59, 218, 71, 40, 87, 180, 47, 115, 195, 60, 234, 232, 18, 37, 199, 225, 100, 93, 76, 50, 137, 31, 37, 104, 214, 117, 240, 22, 214, 249, 92, 214, 192, 199, 104, 81, 179, 135, 34, 41, 231, 175, 125, 54, 208, 176, 12, 252, 0, 74, 106, 8, 50, 12, 127, 88, 217, 8, 174, 164, 148, 54, 153, 3, 240, 250, 11, 180, 198, 238, 66, 0, 48, 13, 220 } });

            migrationBuilder.CreateIndex(
                name: "IX_Overtimes_EmployeeId",
                table: "Overtimes",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Overtimes");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 233, 7, 207, 189, 125, 107, 26, 126, 202, 76, 55, 243, 200, 178, 230, 162, 213, 125, 235, 123, 255, 158, 157, 80, 21, 253, 195, 193, 8, 125, 22, 181, 220, 245, 252, 224, 66, 250, 201, 36, 249, 139, 221, 30, 199, 64, 184, 47, 27, 211, 240, 99, 201, 230, 120, 187, 182, 211, 30, 162, 184, 241, 24 }, new byte[] { 27, 229, 120, 166, 182, 108, 199, 49, 234, 207, 174, 177, 17, 172, 93, 174, 19, 253, 82, 124, 198, 36, 21, 82, 30, 193, 111, 140, 69, 10, 123, 17, 147, 19, 197, 104, 142, 63, 13, 54, 4, 62, 191, 254, 62, 45, 203, 169, 41, 181, 97, 77, 221, 11, 100, 163, 27, 119, 92, 46, 64, 188, 253, 191, 41, 165, 159, 60, 130, 164, 235, 130, 136, 168, 69, 183, 39, 254, 8, 132, 183, 136, 20, 123, 154, 247, 102, 177, 224, 103, 5, 107, 8, 90, 110, 92, 206, 7, 159, 47, 220, 111, 251, 136, 74, 123, 1, 0, 176, 154, 248, 121, 174, 156, 157, 203, 41, 7, 144, 217, 135, 108, 254, 120, 8, 8, 178, 166 } });
        }
    }
}
