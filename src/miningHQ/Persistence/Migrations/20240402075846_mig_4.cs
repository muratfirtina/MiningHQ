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
                values: new object[] { new byte[] { 38, 57, 49, 238, 124, 140, 155, 13, 198, 108, 160, 12, 28, 102, 130, 28, 196, 101, 249, 7, 88, 66, 108, 207, 149, 238, 237, 152, 197, 200, 156, 55, 140, 75, 112, 18, 111, 159, 50, 153, 124, 75, 221, 145, 104, 183, 197, 73, 237, 207, 118, 146, 53, 32, 222, 233, 144, 185, 123, 241, 12, 191, 70, 32 }, new byte[] { 211, 222, 15, 247, 232, 184, 115, 146, 141, 205, 237, 255, 191, 0, 117, 139, 240, 93, 109, 97, 146, 133, 42, 94, 243, 17, 48, 246, 246, 150, 177, 84, 76, 133, 50, 230, 153, 65, 123, 86, 200, 226, 44, 204, 48, 200, 239, 31, 92, 5, 123, 12, 96, 103, 160, 81, 28, 99, 74, 194, 62, 29, 35, 118, 135, 223, 180, 140, 114, 177, 57, 181, 236, 118, 172, 131, 110, 99, 96, 145, 216, 28, 245, 109, 203, 179, 181, 44, 116, 179, 186, 49, 149, 179, 33, 72, 239, 75, 112, 59, 136, 68, 230, 79, 223, 223, 171, 166, 227, 154, 190, 104, 58, 149, 253, 76, 77, 20, 148, 193, 153, 62, 80, 164, 70, 198, 187, 100 } });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
