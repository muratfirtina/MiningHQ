using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleBasedAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleOperationClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    OperationClaimId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleOperationClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 10, 15, 14, 18, 441, DateTimeKind.Utc).AddTicks(7790), null, "Full system access with all permissions", "Admin", null },
                    { 2, new DateTime(2025, 11, 10, 15, 14, 18, 441, DateTimeKind.Utc).AddTicks(7790), null, "Quarry moderator with limited access", "Moderator", null },
                    { 3, new DateTime(2025, 11, 10, 15, 14, 18, 441, DateTimeKind.Utc).AddTicks(7790), null, "HR assistant with employee management access", "HRAssistant", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 19, 242, 104, 254, 229, 139, 13, 179, 167, 81, 63, 22, 19, 90, 112, 219, 16, 204, 208, 165, 203, 19, 4, 64, 91, 153, 87, 101, 46, 209, 150, 83, 93, 62, 53, 86, 115, 175, 135, 245, 232, 35, 235, 240, 166, 217, 139, 217, 64, 35, 33, 112, 171, 66, 166, 195, 182, 225, 111, 52, 89, 202, 129, 155 }, new byte[] { 76, 227, 192, 159, 20, 100, 149, 77, 252, 224, 173, 208, 19, 126, 75, 50, 78, 158, 190, 85, 69, 72, 209, 19, 138, 68, 202, 29, 197, 150, 231, 187, 14, 197, 18, 236, 117, 74, 255, 23, 145, 21, 60, 209, 212, 100, 34, 115, 187, 4, 92, 122, 248, 119, 89, 238, 207, 86, 110, 31, 143, 87, 201, 6, 119, 12, 115, 200, 38, 186, 230, 231, 84, 247, 121, 244, 109, 162, 1, 107, 2, 139, 79, 93, 110, 240, 250, 158, 17, 132, 206, 8, 132, 142, 67, 75, 240, 86, 55, 222, 59, 242, 20, 150, 55, 82, 137, 171, 51, 130, 187, 115, 61, 59, 140, 142, 57, 193, 59, 139, 113, 40, 66, 3, 215, 132, 59, 241 } });

            migrationBuilder.InsertData(
                table: "RoleOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "RoleId", "UpdatedDate" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 10, 15, 14, 18, 441, DateTimeKind.Utc).AddTicks(9850), null, 1, 1, null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 10, 15, 14, 18, 442, DateTimeKind.Utc).AddTicks(7680), null, 1, null, new Guid("729c40f5-0859-48d7-a388-451520c1289c") });

            migrationBuilder.CreateIndex(
                name: "IX_RoleOperationClaims_OperationClaimId",
                table: "RoleOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOperationClaims_RoleId_OperationClaimId",
                table: "RoleOperationClaims",
                columns: new[] { "RoleId", "OperationClaimId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleOperationClaims");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 49, 146, 167, 138, 203, 127, 196, 155, 121, 28, 24, 56, 96, 151, 250, 17, 83, 157, 206, 34, 210, 70, 142, 21, 60, 40, 148, 245, 236, 243, 205, 234, 145, 111, 99, 53, 165, 127, 208, 20, 222, 73, 243, 166, 140, 168, 225, 58, 196, 71, 209, 65, 244, 149, 124, 130, 15, 34, 49, 88, 172, 215, 14, 53 }, new byte[] { 89, 27, 234, 223, 240, 136, 150, 48, 85, 86, 66, 89, 224, 51, 135, 253, 76, 250, 21, 12, 197, 104, 211, 114, 30, 238, 31, 101, 70, 158, 232, 255, 188, 73, 138, 179, 151, 94, 85, 93, 165, 91, 136, 141, 71, 45, 71, 176, 147, 239, 145, 159, 115, 81, 123, 233, 212, 108, 14, 233, 82, 48, 202, 12, 209, 97, 114, 142, 86, 39, 115, 34, 66, 254, 78, 29, 41, 246, 179, 148, 14, 242, 84, 200, 112, 47, 80, 94, 251, 116, 222, 152, 19, 245, 87, 117, 145, 232, 128, 234, 93, 224, 154, 140, 120, 160, 219, 140, 232, 133, 124, 194, 16, 240, 183, 38, 130, 42, 59, 10, 67, 68, 157, 176, 201, 94, 94, 97 } });
        }
    }
}
