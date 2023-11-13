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
                name: "BrandId",
                table: "MachineTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 159, 222, 86, 237, 164, 28, 96, 158, 233, 169, 202, 139, 39, 255, 232, 192, 33, 174, 76, 150, 171, 31, 182, 250, 212, 162, 248, 141, 23, 31, 140, 4, 194, 234, 222, 32, 160, 168, 201, 9, 177, 14, 226, 64, 65, 125, 240, 200, 238, 68, 170, 229, 184, 71, 26, 133, 134, 158, 117, 211, 251, 210, 122, 219 }, new byte[] { 218, 42, 11, 127, 245, 161, 112, 189, 215, 146, 153, 76, 219, 21, 74, 132, 200, 141, 129, 58, 15, 223, 32, 25, 3, 132, 12, 126, 34, 145, 233, 190, 200, 102, 73, 218, 154, 119, 80, 58, 62, 219, 2, 164, 231, 242, 194, 247, 16, 156, 99, 207, 221, 149, 224, 154, 245, 62, 185, 26, 221, 208, 78, 176, 73, 154, 143, 238, 124, 129, 93, 145, 252, 77, 215, 136, 239, 204, 213, 165, 17, 252, 217, 57, 172, 252, 145, 59, 253, 25, 143, 64, 163, 236, 106, 55, 120, 76, 58, 231, 114, 123, 217, 44, 177, 202, 96, 108, 216, 198, 135, 50, 31, 60, 207, 112, 94, 25, 224, 92, 60, 61, 239, 145, 75, 24, 46, 43 } });

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypes_BrandId",
                table: "MachineTypes",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineTypes_Brands_BrandId",
                table: "MachineTypes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineTypes_Brands_BrandId",
                table: "MachineTypes");

            migrationBuilder.DropIndex(
                name: "IX_MachineTypes_BrandId",
                table: "MachineTypes");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "MachineTypes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 158, 93, 61, 198, 178, 164, 132, 64, 114, 204, 227, 82, 233, 244, 180, 122, 81, 158, 63, 121, 206, 93, 35, 126, 124, 111, 178, 164, 243, 26, 120, 35, 96, 72, 126, 48, 55, 165, 39, 171, 106, 232, 68, 3, 237, 18, 152, 209, 162, 97, 56, 96, 24, 62, 244, 183, 47, 5, 104, 36, 57, 135, 88, 205 }, new byte[] { 64, 55, 125, 232, 76, 251, 156, 194, 138, 236, 77, 208, 61, 197, 160, 12, 91, 96, 183, 40, 170, 150, 69, 129, 98, 122, 20, 54, 236, 64, 102, 225, 219, 253, 20, 173, 83, 159, 218, 165, 143, 199, 175, 96, 60, 1, 25, 140, 47, 61, 32, 11, 242, 204, 7, 77, 151, 190, 143, 81, 166, 246, 63, 102, 178, 135, 76, 64, 24, 85, 67, 147, 75, 15, 206, 68, 60, 162, 12, 214, 73, 217, 170, 28, 113, 136, 161, 18, 118, 243, 132, 63, 14, 23, 118, 47, 56, 69, 63, 107, 84, 29, 116, 231, 246, 79, 104, 193, 238, 144, 139, 214, 215, 67, 158, 12, 48, 231, 62, 208, 25, 46, 238, 132, 50, 70, 85, 147 } });
        }
    }
}
