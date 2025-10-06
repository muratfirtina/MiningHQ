using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseDateAndDescriptionToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Machines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Machines",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 49, 6, 110, 201, 224, 16, 12, 134, 148, 65, 20, 109, 92, 70, 107, 20, 23, 198, 39, 30, 42, 104, 101, 38, 37, 236, 189, 197, 170, 129, 254, 48, 232, 180, 192, 0, 112, 123, 244, 27, 63, 50, 108, 238, 28, 117, 25, 158, 116, 240, 164, 60, 187, 44, 194, 103, 89, 234, 14, 29, 34, 245, 35, 44 }, new byte[] { 181, 239, 226, 58, 194, 127, 47, 178, 29, 13, 23, 165, 162, 221, 90, 222, 58, 153, 108, 148, 151, 222, 149, 181, 234, 190, 134, 194, 21, 6, 187, 58, 170, 177, 179, 132, 65, 138, 108, 132, 80, 213, 127, 102, 174, 223, 217, 231, 242, 12, 230, 24, 66, 125, 235, 185, 150, 176, 38, 81, 63, 253, 235, 179, 72, 84, 204, 70, 172, 154, 204, 39, 47, 14, 108, 34, 190, 43, 24, 107, 170, 112, 161, 174, 138, 2, 62, 156, 88, 53, 213, 242, 11, 167, 177, 170, 118, 81, 114, 196, 179, 37, 252, 155, 25, 138, 188, 240, 70, 168, 76, 107, 98, 208, 251, 203, 85, 42, 102, 55, 137, 150, 116, 171, 189, 27, 17, 64 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Machines");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 206, 75, 18, 55, 196, 17, 27, 162, 249, 190, 240, 217, 115, 107, 9, 108, 142, 38, 188, 137, 13, 0, 139, 190, 104, 154, 139, 190, 59, 69, 51, 103, 183, 16, 122, 185, 34, 225, 123, 61, 194, 136, 147, 134, 4, 205, 219, 152, 150, 81, 55, 235, 6, 108, 107, 172, 1, 209, 173, 23, 52, 87, 43, 211 }, new byte[] { 39, 221, 109, 228, 178, 145, 133, 227, 46, 220, 102, 81, 34, 157, 179, 244, 199, 22, 217, 255, 163, 252, 63, 159, 22, 75, 206, 106, 166, 249, 198, 135, 17, 254, 38, 86, 61, 33, 180, 233, 152, 159, 151, 132, 149, 133, 245, 100, 156, 252, 152, 230, 24, 138, 214, 213, 248, 200, 199, 67, 18, 19, 73, 63, 19, 183, 212, 75, 51, 158, 75, 28, 155, 192, 162, 10, 250, 78, 174, 231, 221, 121, 169, 201, 71, 141, 33, 192, 152, 223, 10, 242, 148, 67, 215, 87, 119, 138, 246, 60, 111, 111, 211, 120, 48, 57, 16, 34, 210, 219, 218, 218, 79, 75, 184, 186, 206, 89, 250, 27, 87, 6, 25, 90, 166, 247, 127, 207 } });
        }
    }
}
