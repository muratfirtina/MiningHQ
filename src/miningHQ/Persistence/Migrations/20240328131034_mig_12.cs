using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Employees_EmployeeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Maintenances_MaintenanceId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Files");

            migrationBuilder.AddColumn<bool>(
                name: "Showcase",
                table: "Files",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 20, 95, 205, 115, 202, 108, 72, 86, 105, 81, 152, 252, 4, 128, 73, 123, 55, 51, 15, 12, 143, 33, 198, 0, 185, 221, 17, 221, 28, 77, 91, 254, 116, 0, 16, 250, 253, 191, 157, 88, 148, 126, 49, 203, 104, 151, 208, 180, 210, 235, 114, 161, 15, 215, 195, 124, 47, 45, 155, 167, 113, 206, 170, 244 }, new byte[] { 50, 220, 123, 127, 58, 154, 100, 199, 36, 160, 248, 203, 237, 211, 82, 59, 187, 87, 174, 80, 20, 65, 30, 254, 1, 134, 103, 25, 44, 5, 78, 74, 104, 70, 117, 135, 77, 209, 97, 33, 185, 208, 240, 204, 70, 18, 89, 206, 79, 36, 26, 122, 26, 141, 96, 17, 16, 105, 81, 78, 151, 142, 136, 177, 221, 114, 81, 173, 181, 181, 105, 174, 191, 14, 239, 83, 57, 117, 113, 35, 220, 38, 108, 41, 36, 240, 231, 108, 109, 235, 180, 18, 93, 218, 211, 45, 240, 149, 235, 253, 223, 123, 200, 87, 93, 250, 244, 163, 191, 181, 90, 59, 26, 139, 126, 22, 211, 134, 39, 117, 18, 225, 165, 221, 228, 222, 61, 120 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Employees_EmployeeId",
                table: "Files",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Maintenances_MaintenanceId",
                table: "Files",
                column: "MaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Employees_EmployeeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Maintenances_MaintenanceId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Showcase",
                table: "Files");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Files",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 59, 106, 156, 176, 12, 2, 202, 227, 79, 66, 22, 143, 144, 85, 235, 102, 185, 127, 222, 72, 117, 131, 38, 1, 17, 123, 58, 106, 130, 155, 134, 188, 84, 149, 151, 199, 253, 103, 188, 191, 172, 6, 12, 188, 29, 220, 16, 50, 189, 20, 239, 55, 112, 113, 95, 2, 88, 208, 224, 214, 46, 45, 214, 55 }, new byte[] { 148, 232, 5, 45, 143, 137, 16, 59, 138, 231, 80, 81, 250, 116, 37, 208, 196, 74, 27, 115, 21, 128, 139, 248, 224, 100, 192, 218, 201, 97, 145, 95, 48, 67, 71, 42, 101, 147, 179, 158, 161, 153, 166, 246, 100, 99, 79, 6, 36, 46, 103, 246, 162, 105, 49, 230, 177, 112, 31, 218, 171, 65, 195, 4, 255, 110, 185, 170, 80, 246, 79, 2, 127, 26, 2, 127, 108, 255, 215, 7, 247, 131, 68, 11, 16, 230, 51, 92, 200, 225, 188, 184, 190, 81, 53, 67, 181, 86, 124, 101, 114, 73, 221, 104, 86, 46, 172, 108, 28, 172, 247, 130, 188, 44, 9, 167, 248, 212, 88, 89, 103, 250, 154, 240, 8, 153, 167, 1 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Employees_EmployeeId",
                table: "Files",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Maintenances_MaintenanceId",
                table: "Files",
                column: "MaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
