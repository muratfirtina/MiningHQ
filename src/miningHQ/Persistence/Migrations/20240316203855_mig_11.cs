using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Jobs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 104, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departments.Admin", null },
                    { 105, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departments.Read", null },
                    { 106, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departments.Write", null },
                    { 107, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departments.Add", null },
                    { 108, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departments.Update", null },
                    { 109, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Departments.Delete", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 59, 106, 156, 176, 12, 2, 202, 227, 79, 66, 22, 143, 144, 85, 235, 102, 185, 127, 222, 72, 117, 131, 38, 1, 17, 123, 58, 106, 130, 155, 134, 188, 84, 149, 151, 199, 253, 103, 188, 191, 172, 6, 12, 188, 29, 220, 16, 50, 189, 20, 239, 55, 112, 113, 95, 2, 88, 208, 224, 214, 46, 45, 214, 55 }, new byte[] { 148, 232, 5, 45, 143, 137, 16, 59, 138, 231, 80, 81, 250, 116, 37, 208, 196, 74, 27, 115, 21, 128, 139, 248, 224, 100, 192, 218, 201, 97, 145, 95, 48, 67, 71, 42, 101, 147, 179, 158, 161, 153, 166, 246, 100, 99, 79, 6, 36, 46, 103, 246, 162, 105, 49, 230, 177, 112, 31, 218, 171, 65, 195, 4, 255, 110, 185, 170, 80, 246, 79, 2, 127, 26, 2, 127, 108, 255, 215, 7, 247, 131, 68, 11, 16, 230, 51, 92, 200, 225, 188, 184, 190, 81, 53, 67, 181, 86, 124, 101, 114, 73, 221, 104, 86, 46, 172, 108, 28, 172, 247, 130, 188, 44, 9, 167, 248, 212, 88, 89, 103, 250, 154, 240, 8, 153, 167, 1 } });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Departments_DepartmentId",
                table: "Jobs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Departments_DepartmentId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 194, 83, 138, 165, 130, 96, 118, 41, 161, 184, 32, 176, 71, 151, 3, 240, 68, 150, 118, 129, 45, 126, 97, 29, 169, 99, 142, 97, 81, 150, 219, 23, 20, 78, 29, 128, 245, 9, 191, 78, 16, 191, 177, 217, 126, 143, 77, 225, 101, 160, 26, 211, 119, 94, 222, 155, 209, 107, 87, 18, 67, 38, 213, 76 }, new byte[] { 251, 111, 203, 8, 147, 143, 136, 89, 175, 162, 44, 236, 83, 101, 27, 197, 124, 107, 168, 182, 130, 243, 100, 51, 32, 31, 186, 68, 55, 62, 143, 70, 72, 97, 201, 110, 146, 43, 143, 241, 25, 135, 186, 114, 65, 247, 226, 53, 46, 2, 6, 99, 198, 59, 218, 71, 40, 87, 180, 47, 115, 195, 60, 234, 232, 18, 37, 199, 225, 100, 93, 76, 50, 137, 31, 37, 104, 214, 117, 240, 22, 214, 249, 92, 214, 192, 199, 104, 81, 179, 135, 34, 41, 231, 175, 125, 54, 208, 176, 12, 252, 0, 74, 106, 8, 50, 12, 127, 88, 217, 8, 174, 164, 148, 54, 153, 3, 240, 250, 11, 180, 198, 238, 66, 0, 48, 13, 220 } });
        }
    }
}
