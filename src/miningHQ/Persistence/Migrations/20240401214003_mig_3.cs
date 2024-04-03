using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Employees_EmployeeId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_EmployeeId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "EmployeePhotoId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeEmployeePhoto",
                columns: table => new
                {
                    EmployeePhotosId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEmployeePhoto", x => new { x.EmployeePhotosId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_EmployeeEmployeePhoto_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEmployeePhoto_Files_EmployeePhotosId",
                        column: x => x.EmployeePhotosId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 255, 210, 15, 0, 156, 27, 70, 175, 187, 198, 220, 70, 254, 197, 82, 246, 200, 9, 47, 154, 164, 50, 224, 15, 158, 78, 183, 134, 207, 69, 53, 126, 250, 131, 225, 1, 0, 126, 138, 213, 198, 71, 1, 108, 74, 165, 142, 157, 40, 80, 68, 216, 39, 148, 167, 27, 250, 109, 30, 4, 105, 154, 120, 205 }, new byte[] { 82, 91, 127, 149, 22, 245, 175, 238, 127, 53, 200, 121, 57, 121, 156, 147, 202, 117, 19, 115, 8, 89, 96, 179, 223, 196, 224, 173, 246, 163, 176, 243, 187, 70, 34, 143, 144, 95, 168, 88, 104, 158, 63, 139, 37, 220, 194, 123, 121, 127, 207, 195, 135, 126, 168, 225, 29, 28, 252, 86, 116, 228, 177, 55, 246, 59, 191, 212, 42, 137, 36, 88, 232, 168, 141, 83, 16, 214, 116, 119, 16, 254, 164, 163, 41, 214, 33, 156, 158, 12, 178, 122, 172, 77, 117, 112, 58, 77, 88, 172, 26, 59, 107, 131, 110, 132, 5, 193, 182, 242, 50, 199, 66, 177, 178, 21, 134, 20, 173, 165, 107, 187, 178, 93, 34, 153, 188, 253 } });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEmployeePhoto_EmployeesId",
                table: "EmployeeEmployeePhoto",
                column: "EmployeesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEmployeePhoto");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeePhotoId",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 186, 215, 116, 228, 71, 137, 251, 98, 216, 78, 121, 203, 59, 151, 255, 225, 18, 2, 64, 6, 170, 5, 169, 217, 239, 71, 58, 241, 198, 182, 55, 112, 179, 75, 166, 145, 55, 200, 30, 199, 181, 105, 9, 236, 109, 217, 162, 228, 145, 72, 151, 182, 111, 160, 66, 49, 159, 177, 46, 215, 108, 84, 170, 66 }, new byte[] { 255, 156, 255, 22, 109, 171, 124, 84, 145, 52, 63, 85, 157, 2, 43, 186, 14, 231, 120, 57, 143, 117, 120, 77, 157, 50, 140, 37, 65, 16, 29, 162, 212, 12, 3, 152, 75, 195, 214, 115, 127, 147, 156, 222, 24, 28, 12, 226, 43, 144, 179, 166, 169, 176, 51, 103, 178, 207, 107, 71, 91, 145, 43, 81, 102, 178, 185, 6, 38, 166, 7, 248, 216, 183, 104, 244, 153, 76, 77, 181, 128, 13, 69, 55, 205, 187, 172, 1, 6, 72, 224, 166, 92, 158, 16, 81, 83, 76, 167, 183, 8, 215, 109, 162, 219, 188, 92, 254, 108, 157, 190, 228, 91, 223, 24, 13, 208, 208, 216, 227, 211, 32, 197, 246, 243, 60, 50, 218 } });

            migrationBuilder.CreateIndex(
                name: "IX_Files_EmployeeId",
                table: "Files",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Employees_EmployeeId",
                table: "Files",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
