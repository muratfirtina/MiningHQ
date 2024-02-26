using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Employees\" ALTER COLUMN \"TypeOfBlood\" TYPE integer USING \"TypeOfBlood\"::integer;");
            migrationBuilder.AlterColumn<int>(
                name: "TypeOfBlood",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 156, 233, 7, 207, 189, 125, 107, 26, 126, 202, 76, 55, 243, 200, 178, 230, 162, 213, 125, 235, 123, 255, 158, 157, 80, 21, 253, 195, 193, 8, 125, 22, 181, 220, 245, 252, 224, 66, 250, 201, 36, 249, 139, 221, 30, 199, 64, 184, 47, 27, 211, 240, 99, 201, 230, 120, 187, 182, 211, 30, 162, 184, 241, 24 }, new byte[] { 27, 229, 120, 166, 182, 108, 199, 49, 234, 207, 174, 177, 17, 172, 93, 174, 19, 253, 82, 124, 198, 36, 21, 82, 30, 193, 111, 140, 69, 10, 123, 17, 147, 19, 197, 104, 142, 63, 13, 54, 4, 62, 191, 254, 62, 45, 203, 169, 41, 181, 97, 77, 221, 11, 100, 163, 27, 119, 92, 46, 64, 188, 253, 191, 41, 165, 159, 60, 130, 164, 235, 130, 136, 168, 69, 183, 39, 254, 8, 132, 183, 136, 20, 123, 154, 247, 102, 177, 224, 103, 5, 107, 8, 90, 110, 92, 206, 7, 159, 47, 220, 111, 251, 136, 74, 123, 1, 0, 176, 154, 248, 121, 174, 156, 157, 203, 41, 7, 144, 217, 135, 108, 254, 120, 8, 8, 178, 166 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeOfBlood",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 93, 246, 103, 81, 193, 71, 207, 66, 223, 132, 221, 178, 176, 225, 205, 20, 229, 242, 86, 74, 40, 215, 136, 219, 171, 184, 190, 16, 44, 88, 232, 107, 239, 227, 30, 147, 216, 117, 199, 60, 11, 189, 191, 3, 170, 152, 136, 87, 172, 49, 193, 74, 75, 126, 128, 255, 52, 112, 214, 21, 175, 136, 78, 13 }, new byte[] { 62, 104, 103, 20, 60, 170, 100, 88, 246, 51, 121, 85, 122, 69, 59, 91, 180, 20, 148, 4, 197, 13, 72, 168, 178, 153, 178, 15, 142, 100, 245, 176, 53, 252, 170, 158, 55, 48, 173, 211, 22, 49, 189, 130, 249, 42, 183, 190, 175, 231, 162, 252, 50, 196, 248, 96, 237, 119, 108, 16, 216, 155, 0, 80, 12, 9, 10, 148, 200, 113, 94, 179, 221, 184, 250, 165, 140, 36, 73, 216, 27, 27, 92, 50, 38, 147, 35, 248, 177, 41, 154, 181, 176, 67, 182, 222, 3, 223, 22, 221, 222, 237, 158, 112, 231, 149, 244, 9, 6, 67, 140, 216, 97, 184, 89, 12, 201, 231, 41, 38, 94, 227, 11, 224, 31, 245, 56, 196 } });
        }
    }
}
