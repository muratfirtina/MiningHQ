using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 86, 214, 127, 82, 200, 93, 53, 129, 147, 100, 41, 49, 45, 109, 197, 67, 241, 210, 156, 151, 76, 76, 159, 179, 238, 106, 246, 104, 150, 23, 61, 23, 137, 116, 18, 88, 147, 248, 66, 1, 49, 98, 121, 163, 208, 128, 83, 171, 8, 249, 254, 8, 175, 1, 220, 79, 25, 39, 95, 155, 254, 237, 213, 136 }, new byte[] { 206, 22, 4, 140, 74, 12, 237, 115, 1, 60, 165, 140, 49, 183, 255, 213, 224, 57, 8, 66, 84, 123, 118, 149, 10, 198, 102, 146, 23, 72, 102, 184, 85, 53, 237, 81, 141, 168, 190, 207, 65, 105, 220, 82, 180, 246, 201, 21, 203, 29, 29, 178, 155, 90, 154, 21, 74, 99, 249, 231, 0, 140, 165, 135, 69, 61, 174, 153, 32, 170, 164, 138, 195, 42, 89, 178, 199, 14, 48, 224, 171, 250, 107, 150, 160, 135, 205, 176, 172, 41, 175, 36, 154, 241, 24, 174, 125, 203, 125, 105, 123, 247, 201, 130, 151, 10, 157, 94, 125, 198, 244, 178, 24, 200, 54, 167, 233, 17, 69, 247, 250, 204, 49, 28, 199, 138, 98, 39 } });
        }
    }
}
