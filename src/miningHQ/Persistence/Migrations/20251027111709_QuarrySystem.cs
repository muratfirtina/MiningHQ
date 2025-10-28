using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class QuarrySystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Quarries_QuarryId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Quarries_QuarryId",
                table: "Machines");

            migrationBuilder.AddColumn<string>(
                name: "CoordinateDescription",
                table: "Quarries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Quarries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Quarries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Quarries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Quarries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MiningEngineerId",
                table: "Quarries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuarryFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Showcase = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuarryFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuarryFiles_Files_Id",
                        column: x => x.Id,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuarryProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuarryId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WeekEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProductionAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductionUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    StockAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    StockUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SalesAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SalesUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuarryProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuarryProductions_Quarries_QuarryId",
                        column: x => x.QuarryId,
                        principalTable: "Quarries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuarryFileQuarries",
                columns: table => new
                {
                    QuarriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuarryFilesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuarryFileQuarries", x => new { x.QuarriesId, x.QuarryFilesId });
                    table.ForeignKey(
                        name: "FK_QuarryFileQuarries_Quarries_QuarriesId",
                        column: x => x.QuarriesId,
                        principalTable: "Quarries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuarryFileQuarries_QuarryFiles_QuarryFilesId",
                        column: x => x.QuarryFilesId,
                        principalTable: "QuarryFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 225, 1, 66, 152, 118, 10, 104, 172, 122, 58, 106, 24, 194, 42, 100, 252, 0, 34, 118, 98, 243, 71, 74, 34, 130, 123, 248, 138, 94, 216, 161, 197, 150, 23, 222, 9, 251, 127, 252, 136, 52, 59, 225, 88, 181, 212, 170, 171, 228, 147, 105, 202, 193, 11, 152, 74, 10, 152, 175, 81, 23, 70, 194, 215 }, new byte[] { 149, 33, 181, 248, 249, 54, 139, 99, 94, 78, 40, 240, 105, 39, 160, 148, 24, 130, 242, 158, 227, 149, 100, 73, 37, 216, 237, 166, 39, 121, 150, 226, 201, 96, 180, 209, 37, 99, 84, 210, 251, 250, 246, 134, 110, 1, 63, 73, 8, 240, 255, 190, 251, 67, 100, 24, 193, 230, 65, 83, 29, 64, 0, 136, 42, 193, 252, 40, 105, 73, 23, 176, 132, 162, 37, 122, 218, 2, 220, 50, 45, 91, 206, 157, 23, 51, 20, 104, 96, 241, 7, 214, 104, 237, 110, 56, 37, 146, 80, 26, 42, 186, 38, 168, 16, 80, 13, 25, 230, 171, 88, 177, 42, 200, 52, 46, 32, 51, 139, 3, 241, 145, 172, 168, 51, 33, 46, 157 } });

            migrationBuilder.CreateIndex(
                name: "IX_Quarries_MiningEngineerId",
                table: "Quarries",
                column: "MiningEngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuarryFileQuarries_QuarryFilesId",
                table: "QuarryFileQuarries",
                column: "QuarryFilesId");

            migrationBuilder.CreateIndex(
                name: "IX_QuarryProductions_QuarryId",
                table: "QuarryProductions",
                column: "QuarryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Quarries_QuarryId",
                table: "Employees",
                column: "QuarryId",
                principalTable: "Quarries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Quarries_QuarryId",
                table: "Machines",
                column: "QuarryId",
                principalTable: "Quarries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quarries_Employees_MiningEngineerId",
                table: "Quarries",
                column: "MiningEngineerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Quarries_QuarryId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Quarries_QuarryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Quarries_Employees_MiningEngineerId",
                table: "Quarries");

            migrationBuilder.DropTable(
                name: "QuarryFileQuarries");

            migrationBuilder.DropTable(
                name: "QuarryProductions");

            migrationBuilder.DropTable(
                name: "QuarryFiles");

            migrationBuilder.DropIndex(
                name: "IX_Quarries_MiningEngineerId",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "CoordinateDescription",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "MiningEngineerId",
                table: "Quarries");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 185, 180, 82, 246, 131, 148, 215, 21, 76, 47, 13, 4, 44, 15, 204, 154, 151, 98, 173, 93, 155, 179, 13, 111, 100, 184, 82, 41, 89, 184, 100, 160, 72, 225, 52, 126, 10, 88, 182, 19, 186, 32, 18, 131, 86, 117, 9, 95, 51, 207, 30, 224, 223, 248, 6, 152, 12, 161, 69, 118, 27, 91, 248, 23 }, new byte[] { 153, 29, 84, 251, 151, 46, 93, 203, 29, 171, 221, 63, 136, 222, 190, 105, 185, 152, 79, 110, 156, 77, 140, 200, 0, 67, 214, 205, 125, 193, 69, 216, 238, 192, 160, 192, 73, 110, 72, 132, 103, 167, 136, 222, 54, 44, 46, 165, 74, 156, 153, 114, 152, 227, 9, 130, 244, 95, 207, 76, 241, 181, 20, 92, 237, 127, 42, 143, 238, 13, 242, 15, 61, 197, 236, 171, 215, 241, 63, 136, 220, 36, 204, 191, 203, 148, 98, 231, 162, 236, 42, 171, 24, 18, 75, 106, 190, 247, 225, 3, 158, 29, 239, 230, 199, 243, 45, 141, 113, 225, 101, 222, 112, 122, 36, 29, 62, 212, 14, 29, 179, 83, 192, 144, 137, 103, 53, 248 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Quarries_QuarryId",
                table: "Employees",
                column: "QuarryId",
                principalTable: "Quarries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Quarries_QuarryId",
                table: "Machines",
                column: "QuarryId",
                principalTable: "Quarries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
