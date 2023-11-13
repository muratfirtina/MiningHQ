using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BrandMachineType",
                columns: table => new
                {
                    BrandsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MachineTypesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandMachineType", x => new { x.BrandsId, x.MachineTypesId });
                    table.ForeignKey(
                        name: "FK_BrandMachineType_Brands_BrandsId",
                        column: x => x.BrandsId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandMachineType_MachineTypes_MachineTypesId",
                        column: x => x.MachineTypesId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 236, 84, 198, 126, 159, 141, 145, 48, 204, 201, 248, 96, 145, 146, 76, 131, 169, 229, 20, 215, 1, 173, 101, 48, 59, 204, 112, 52, 65, 255, 203, 42, 105, 241, 94, 195, 85, 10, 241, 47, 177, 138, 95, 222, 212, 224, 61, 217, 194, 160, 160, 138, 181, 176, 77, 25, 163, 84, 232, 97, 145, 159, 153 }, new byte[] { 94, 239, 194, 22, 183, 222, 117, 70, 218, 34, 213, 103, 253, 207, 139, 201, 150, 125, 118, 89, 203, 252, 169, 123, 184, 162, 216, 30, 195, 5, 188, 31, 152, 238, 54, 17, 247, 127, 23, 9, 10, 249, 90, 119, 65, 62, 236, 116, 63, 205, 187, 218, 107, 167, 242, 204, 8, 3, 236, 144, 104, 37, 162, 215, 163, 42, 118, 157, 198, 47, 122, 212, 240, 73, 99, 102, 203, 64, 217, 23, 60, 240, 204, 124, 125, 232, 31, 143, 101, 184, 118, 168, 227, 151, 7, 95, 56, 27, 159, 51, 106, 65, 57, 73, 251, 20, 134, 230, 71, 52, 45, 238, 13, 166, 79, 85, 215, 160, 177, 66, 8, 148, 185, 40, 131, 198, 32, 18 } });

            migrationBuilder.CreateIndex(
                name: "IX_BrandMachineType_MachineTypesId",
                table: "BrandMachineType",
                column: "MachineTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandMachineType");

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
    }
}
