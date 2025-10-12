using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextMaintenanceHour",
                table: "Maintenances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OilsChanged",
                table: "Maintenances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartsChanged",
                table: "Maintenances",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 115, 212, 210, 157, 216, 204, 186, 252, 236, 160, 132, 165, 172, 166, 36, 154, 230, 209, 208, 230, 48, 153, 242, 166, 34, 61, 113, 33, 11, 178, 252, 85, 115, 220, 208, 80, 173, 145, 49, 7, 143, 216, 186, 241, 37, 155, 143, 143, 208, 184, 65, 207, 118, 106, 136, 200, 10, 54, 248, 106, 25, 242, 92, 54 }, new byte[] { 148, 173, 66, 25, 212, 212, 22, 4, 13, 157, 193, 124, 153, 14, 93, 239, 24, 144, 136, 217, 250, 9, 145, 0, 193, 212, 191, 140, 20, 96, 252, 64, 6, 163, 68, 161, 171, 222, 245, 94, 158, 90, 184, 165, 14, 32, 41, 131, 163, 81, 174, 214, 251, 212, 87, 254, 210, 36, 178, 11, 83, 191, 118, 189, 236, 21, 212, 58, 50, 187, 192, 116, 108, 76, 34, 128, 90, 42, 11, 212, 75, 214, 149, 250, 188, 221, 37, 90, 80, 65, 118, 101, 97, 233, 101, 236, 250, 100, 20, 129, 25, 211, 251, 28, 28, 197, 152, 56, 207, 69, 50, 233, 103, 16, 158, 163, 66, 175, 29, 83, 117, 130, 115, 139, 62, 165, 127, 25 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextMaintenanceHour",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "OilsChanged",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "PartsChanged",
                table: "Maintenances");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 138, 172, 143, 25, 210, 142, 159, 104, 19, 198, 125, 132, 172, 144, 5, 124, 248, 138, 240, 131, 63, 22, 123, 89, 158, 72, 207, 156, 236, 97, 94, 79, 136, 97, 107, 201, 215, 196, 162, 132, 124, 110, 184, 197, 46, 3, 250, 28, 209, 204, 89, 237, 33, 7, 244, 215, 128, 113, 98, 195, 181, 112, 84, 111 }, new byte[] { 165, 220, 214, 11, 128, 29, 147, 238, 62, 199, 194, 210, 226, 213, 238, 160, 13, 68, 110, 108, 133, 75, 222, 228, 1, 153, 170, 116, 87, 230, 66, 137, 82, 198, 21, 126, 77, 131, 13, 0, 211, 6, 4, 216, 92, 24, 127, 107, 76, 8, 69, 80, 1, 205, 148, 138, 196, 222, 155, 253, 232, 168, 189, 246, 59, 102, 115, 40, 251, 172, 2, 138, 232, 208, 169, 70, 41, 141, 144, 189, 178, 105, 79, 207, 159, 113, 80, 208, 107, 53, 200, 49, 243, 230, 179, 142, 103, 77, 249, 37, 35, 106, 75, 60, 43, 233, 96, 108, 8, 25, 159, 28, 139, 7, 246, 111, 63, 106, 67, 137, 61, 111, 183, 89, 24, 85, 234, 56 } });
        }
    }
}
