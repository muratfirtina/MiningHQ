using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInitialWorkingHoursToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InitialWorkingHoursOrKm",
                table: "Machines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 138, 172, 143, 25, 210, 142, 159, 104, 19, 198, 125, 132, 172, 144, 5, 124, 248, 138, 240, 131, 63, 22, 123, 89, 158, 72, 207, 156, 236, 97, 94, 79, 136, 97, 107, 201, 215, 196, 162, 132, 124, 110, 184, 197, 46, 3, 250, 28, 209, 204, 89, 237, 33, 7, 244, 215, 128, 113, 98, 195, 181, 112, 84, 111 }, new byte[] { 165, 220, 214, 11, 128, 29, 147, 238, 62, 199, 194, 210, 226, 213, 238, 160, 13, 68, 110, 108, 133, 75, 222, 228, 1, 153, 170, 116, 87, 230, 66, 137, 82, 198, 21, 126, 77, 131, 13, 0, 211, 6, 4, 216, 92, 24, 127, 107, 76, 8, 69, 80, 1, 205, 148, 138, 196, 222, 155, 253, 232, 168, 189, 246, 59, 102, 115, 40, 251, 172, 2, 138, 232, 208, 169, 70, 41, 141, 144, 189, 178, 105, 79, 207, 159, 113, 80, 208, 107, 53, 200, 49, 243, 230, 179, 142, 103, 77, 249, 37, 35, 106, 75, 60, 43, 233, 96, 108, 8, 25, 159, 28, 139, 7, 246, 111, 63, 106, 67, 137, 61, 111, 183, 89, 24, 85, 234, 56 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "InitialWorkingHoursOrKm",
                table: "Machines",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 223, 212, 93, 62, 212, 200, 125, 34, 1, 161, 154, 182, 234, 54, 68, 23, 82, 201, 244, 34, 144, 12, 61, 179, 25, 114, 23, 105, 69, 236, 216, 141, 72, 6, 110, 177, 71, 62, 205, 37, 197, 222, 7, 106, 27, 37, 137, 35, 160, 9, 125, 142, 149, 101, 96, 209, 221, 204, 21, 195, 47, 29, 168, 142 }, new byte[] { 139, 122, 119, 31, 213, 200, 156, 51, 97, 98, 56, 213, 187, 183, 62, 195, 94, 195, 238, 153, 107, 25, 21, 35, 210, 28, 241, 97, 226, 71, 159, 180, 126, 63, 242, 25, 158, 68, 89, 138, 74, 95, 23, 106, 73, 246, 174, 181, 87, 47, 94, 241, 33, 176, 78, 91, 170, 220, 91, 39, 1, 84, 253, 253, 23, 15, 90, 196, 47, 74, 237, 50, 9, 210, 164, 70, 49, 135, 149, 171, 218, 52, 72, 238, 102, 226, 77, 232, 245, 41, 138, 32, 19, 76, 102, 224, 126, 72, 155, 79, 28, 208, 159, 247, 26, 180, 26, 104, 147, 38, 9, 216, 25, 141, 122, 19, 12, 10, 107, 180, 253, 105, 185, 179, 226, 210, 105, 67 } });
        }
    }
}
