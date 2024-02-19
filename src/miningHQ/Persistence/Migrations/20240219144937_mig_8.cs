using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Timekeepings\" ALTER COLUMN \"Status\" TYPE integer USING \"Status\"::integer;");
            
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Timekeepings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 93, 246, 103, 81, 193, 71, 207, 66, 223, 132, 221, 178, 176, 225, 205, 20, 229, 242, 86, 74, 40, 215, 136, 219, 171, 184, 190, 16, 44, 88, 232, 107, 239, 227, 30, 147, 216, 117, 199, 60, 11, 189, 191, 3, 170, 152, 136, 87, 172, 49, 193, 74, 75, 126, 128, 255, 52, 112, 214, 21, 175, 136, 78, 13 }, new byte[] { 62, 104, 103, 20, 60, 170, 100, 88, 246, 51, 121, 85, 122, 69, 59, 91, 180, 20, 148, 4, 197, 13, 72, 168, 178, 153, 178, 15, 142, 100, 245, 176, 53, 252, 170, 158, 55, 48, 173, 211, 22, 49, 189, 130, 249, 42, 183, 190, 175, 231, 162, 252, 50, 196, 248, 96, 237, 119, 108, 16, 216, 155, 0, 80, 12, 9, 10, 148, 200, 113, 94, 179, 221, 184, 250, 165, 140, 36, 73, 216, 27, 27, 92, 50, 38, 147, 35, 248, 177, 41, 154, 181, 176, 67, 182, 222, 3, 223, 22, 221, 222, 237, 158, 112, 231, 149, 244, 9, 6, 67, 140, 216, 97, 184, 89, 12, 201, 231, 41, 38, 94, 227, 11, 224, 31, 245, 56, 196 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Timekeepings",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 75, 169, 52, 176, 84, 49, 188, 7, 0, 60, 87, 30, 69, 228, 196, 125, 15, 31, 224, 133, 216, 89, 162, 204, 85, 6, 191, 60, 42, 105, 241, 192, 38, 127, 105, 185, 75, 137, 5, 61, 2, 245, 62, 142, 43, 26, 10, 250, 223, 107, 140, 118, 84, 150, 159, 1, 37, 22, 17, 145, 183, 159, 239, 243 }, new byte[] { 211, 12, 227, 199, 32, 243, 143, 172, 54, 227, 85, 142, 107, 62, 0, 25, 4, 246, 246, 38, 14, 194, 85, 74, 205, 167, 49, 188, 38, 83, 84, 159, 199, 148, 173, 236, 161, 103, 233, 176, 195, 70, 72, 25, 198, 61, 126, 192, 199, 46, 57, 7, 242, 193, 37, 13, 105, 117, 188, 89, 10, 210, 23, 169, 14, 91, 226, 243, 127, 215, 250, 18, 205, 1, 207, 222, 229, 43, 195, 145, 153, 81, 182, 74, 133, 142, 227, 110, 50, 37, 8, 206, 249, 169, 130, 120, 105, 93, 86, 231, 6, 64, 24, 49, 69, 220, 195, 104, 219, 202, 242, 127, 153, 126, 175, 53, 121, 41, 69, 78, 63, 67, 16, 213, 51, 31, 64, 206 } });
        }
    }
}
