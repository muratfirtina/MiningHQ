using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStartWorkDateAndInitialHoursToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitialWorkingHoursOrKm",
                table: "Machines",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartWorkDate",
                table: "Machines",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 223, 212, 93, 62, 212, 200, 125, 34, 1, 161, 154, 182, 234, 54, 68, 23, 82, 201, 244, 34, 144, 12, 61, 179, 25, 114, 23, 105, 69, 236, 216, 141, 72, 6, 110, 177, 71, 62, 205, 37, 197, 222, 7, 106, 27, 37, 137, 35, 160, 9, 125, 142, 149, 101, 96, 209, 221, 204, 21, 195, 47, 29, 168, 142 }, new byte[] { 139, 122, 119, 31, 213, 200, 156, 51, 97, 98, 56, 213, 187, 183, 62, 195, 94, 195, 238, 153, 107, 25, 21, 35, 210, 28, 241, 97, 226, 71, 159, 180, 126, 63, 242, 25, 158, 68, 89, 138, 74, 95, 23, 106, 73, 246, 174, 181, 87, 47, 94, 241, 33, 176, 78, 91, 170, 220, 91, 39, 1, 84, 253, 253, 23, 15, 90, 196, 47, 74, 237, 50, 9, 210, 164, 70, 49, 135, 149, 171, 218, 52, 72, 238, 102, 226, 77, 232, 245, 41, 138, 32, 19, 76, 102, 224, 126, 72, 155, 79, 28, 208, 159, 247, 26, 180, 26, 104, 147, 38, 9, 216, 25, 141, 122, 19, 12, 10, 107, 180, 253, 105, 185, 179, 226, 210, 105, 67 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialWorkingHoursOrKm",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "StartWorkDate",
                table: "Machines");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 234, 2, 126, 93, 55, 70, 12, 129, 172, 205, 135, 144, 29, 173, 177, 245, 10, 81, 248, 49, 97, 197, 114, 182, 249, 179, 190, 224, 120, 3, 207, 212, 36, 120, 42, 75, 191, 55, 21, 16, 187, 0, 177, 157, 84, 233, 69, 66, 174, 162, 203, 74, 141, 221, 166, 183, 137, 99, 62, 178, 56, 173, 123, 75 }, new byte[] { 156, 192, 62, 54, 106, 247, 226, 182, 67, 167, 99, 165, 74, 94, 139, 58, 189, 126, 13, 9, 27, 48, 41, 218, 217, 69, 231, 150, 172, 209, 29, 75, 74, 209, 168, 76, 64, 56, 250, 105, 75, 62, 194, 58, 180, 48, 84, 182, 90, 60, 197, 154, 13, 223, 234, 231, 30, 21, 214, 211, 22, 44, 81, 155, 41, 9, 209, 40, 133, 123, 5, 161, 169, 10, 142, 120, 188, 236, 136, 82, 178, 6, 239, 237, 145, 26, 85, 46, 183, 239, 57, 132, 55, 196, 10, 20, 190, 107, 205, 91, 136, 148, 219, 53, 243, 186, 5, 209, 170, 191, 139, 230, 134, 245, 149, 235, 147, 237, 242, 58, 42, 200, 43, 169, 77, 111, 76, 148 } });
        }
    }
}
