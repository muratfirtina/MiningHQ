using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddQuarryModeratorRoleSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuarryModerators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuarryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuarryModerators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuarryModerators_Quarries_QuarryId",
                        column: x => x.QuarryId,
                        principalTable: "Quarries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuarryModerators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 110, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QuarryModerators.Admin", null },
                    { 111, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QuarryModerators.Read", null },
                    { 112, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QuarryModerators.Write", null },
                    { 113, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QuarryModerators.Add", null },
                    { 114, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QuarryModerators.Update", null },
                    { 115, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QuarryModerators.Delete", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 49, 146, 167, 138, 203, 127, 196, 155, 121, 28, 24, 56, 96, 151, 250, 17, 83, 157, 206, 34, 210, 70, 142, 21, 60, 40, 148, 245, 236, 243, 205, 234, 145, 111, 99, 53, 165, 127, 208, 20, 222, 73, 243, 166, 140, 168, 225, 58, 196, 71, 209, 65, 244, 149, 124, 130, 15, 34, 49, 88, 172, 215, 14, 53 }, new byte[] { 89, 27, 234, 223, 240, 136, 150, 48, 85, 86, 66, 89, 224, 51, 135, 253, 76, 250, 21, 12, 197, 104, 211, 114, 30, 238, 31, 101, 70, 158, 232, 255, 188, 73, 138, 179, 151, 94, 85, 93, 165, 91, 136, 141, 71, 45, 71, 176, 147, 239, 145, 159, 115, 81, 123, 233, 212, 108, 14, 233, 82, 48, 202, 12, 209, 97, 114, 142, 86, 39, 115, 34, 66, 254, 78, 29, 41, 246, 179, 148, 14, 242, 84, 200, 112, 47, 80, 94, 251, 116, 222, 152, 19, 245, 87, 117, 145, 232, 128, 234, 93, 224, 154, 140, 120, 160, 219, 140, 232, 133, 124, 194, 16, 240, 183, 38, 130, 42, 59, 10, 67, 68, 157, 176, 201, 94, 94, 97 } });

            migrationBuilder.CreateIndex(
                name: "IX_QuarryModerators_QuarryId",
                table: "QuarryModerators",
                column: "QuarryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuarryModerators_UserId_QuarryId",
                table: "QuarryModerators",
                columns: new[] { "UserId", "QuarryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuarryModerators");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 227, 42, 17, 107, 183, 141, 86, 73, 89, 205, 214, 82, 2, 98, 112, 92, 224, 233, 143, 167, 236, 109, 250, 212, 66, 150, 234, 166, 74, 86, 55, 61, 40, 38, 32, 107, 196, 193, 211, 84, 75, 131, 151, 18, 55, 146, 92, 108, 106, 254, 206, 72, 199, 234, 145, 8, 63, 14, 167, 215, 230, 178, 243, 113 }, new byte[] { 178, 22, 232, 169, 191, 1, 129, 123, 194, 251, 237, 14, 90, 138, 29, 201, 99, 33, 89, 136, 5, 161, 250, 79, 11, 146, 25, 57, 15, 88, 73, 38, 131, 227, 248, 112, 91, 170, 70, 14, 97, 144, 144, 180, 35, 124, 23, 223, 22, 133, 26, 147, 165, 215, 185, 214, 28, 212, 101, 222, 254, 112, 171, 99, 211, 229, 33, 122, 7, 152, 212, 138, 139, 11, 74, 145, 241, 242, 195, 203, 138, 152, 153, 230, 229, 95, 146, 51, 234, 196, 119, 245, 222, 244, 163, 142, 194, 181, 60, 66, 200, 107, 115, 107, 133, 219, 2, 125, 216, 68, 82, 209, 148, 218, 9, 223, 68, 5, 149, 2, 163, 219, 54, 90, 89, 165, 116, 101 } });
        }
    }
}
