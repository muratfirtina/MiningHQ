using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentOperatorToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentOperatorId",
                table: "Machines",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 234, 2, 126, 93, 55, 70, 12, 129, 172, 205, 135, 144, 29, 173, 177, 245, 10, 81, 248, 49, 97, 197, 114, 182, 249, 179, 190, 224, 120, 3, 207, 212, 36, 120, 42, 75, 191, 55, 21, 16, 187, 0, 177, 157, 84, 233, 69, 66, 174, 162, 203, 74, 141, 221, 166, 183, 137, 99, 62, 178, 56, 173, 123, 75 }, new byte[] { 156, 192, 62, 54, 106, 247, 226, 182, 67, 167, 99, 165, 74, 94, 139, 58, 189, 126, 13, 9, 27, 48, 41, 218, 217, 69, 231, 150, 172, 209, 29, 75, 74, 209, 168, 76, 64, 56, 250, 105, 75, 62, 194, 58, 180, 48, 84, 182, 90, 60, 197, 154, 13, 223, 234, 231, 30, 21, 214, 211, 22, 44, 81, 155, 41, 9, 209, 40, 133, 123, 5, 161, 169, 10, 142, 120, 188, 236, 136, 82, 178, 6, 239, 237, 145, 26, 85, 46, 183, 239, 57, 132, 55, 196, 10, 20, 190, 107, 205, 91, 136, 148, 219, 53, 243, 186, 5, 209, 170, 191, 139, 230, 134, 245, 149, 235, 147, 237, 242, 58, 42, 200, 43, 169, 77, 111, 76, 148 } });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_CurrentOperatorId",
                table: "Machines",
                column: "CurrentOperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Employees_CurrentOperatorId",
                table: "Machines",
                column: "CurrentOperatorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Employees_CurrentOperatorId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_CurrentOperatorId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "CurrentOperatorId",
                table: "Machines");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 49, 6, 110, 201, 224, 16, 12, 134, 148, 65, 20, 109, 92, 70, 107, 20, 23, 198, 39, 30, 42, 104, 101, 38, 37, 236, 189, 197, 170, 129, 254, 48, 232, 180, 192, 0, 112, 123, 244, 27, 63, 50, 108, 238, 28, 117, 25, 158, 116, 240, 164, 60, 187, 44, 194, 103, 89, 234, 14, 29, 34, 245, 35, 44 }, new byte[] { 181, 239, 226, 58, 194, 127, 47, 178, 29, 13, 23, 165, 162, 221, 90, 222, 58, 153, 108, 148, 151, 222, 149, 181, 234, 190, 134, 194, 21, 6, 187, 58, 170, 177, 179, 132, 65, 138, 108, 132, 80, 213, 127, 102, 174, 223, 217, 231, 242, 12, 230, 24, 66, 125, 235, 185, 150, 176, 38, 81, 63, 253, 235, 179, 72, 84, 204, 70, 172, 154, 204, 39, 47, 14, 108, 34, 190, 43, 24, 107, 170, 112, 161, 174, 138, 2, 62, 156, 88, 53, 213, 242, 11, 167, 177, 170, 118, 81, 114, 196, 179, 37, 252, 155, 25, 138, 188, 240, 70, 168, 76, 107, 98, 208, 251, 203, 85, 42, 102, 55, 137, 150, 116, 171, 189, 27, 17, 64 } });
        }
    }
}
