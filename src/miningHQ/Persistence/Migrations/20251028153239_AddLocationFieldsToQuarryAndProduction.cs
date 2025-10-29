using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationFieldsToQuarryAndProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Altitude",
                table: "QuarryProductions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoordinateDescription",
                table: "QuarryProductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "QuarryProductions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "QuarryProductions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pafta",
                table: "QuarryProductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UtmEasting",
                table: "QuarryProductions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UtmNorthing",
                table: "QuarryProductions",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Altitude",
                table: "Quarries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pafta",
                table: "Quarries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UtmEasting",
                table: "Quarries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UtmNorthing",
                table: "Quarries",
                type: "double precision",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 227, 42, 17, 107, 183, 141, 86, 73, 89, 205, 214, 82, 2, 98, 112, 92, 224, 233, 143, 167, 236, 109, 250, 212, 66, 150, 234, 166, 74, 86, 55, 61, 40, 38, 32, 107, 196, 193, 211, 84, 75, 131, 151, 18, 55, 146, 92, 108, 106, 254, 206, 72, 199, 234, 145, 8, 63, 14, 167, 215, 230, 178, 243, 113 }, new byte[] { 178, 22, 232, 169, 191, 1, 129, 123, 194, 251, 237, 14, 90, 138, 29, 201, 99, 33, 89, 136, 5, 161, 250, 79, 11, 146, 25, 57, 15, 88, 73, 38, 131, 227, 248, 112, 91, 170, 70, 14, 97, 144, 144, 180, 35, 124, 23, 223, 22, 133, 26, 147, 165, 215, 185, 214, 28, 212, 101, 222, 254, 112, 171, 99, 211, 229, 33, 122, 7, 152, 212, 138, 139, 11, 74, 145, 241, 242, 195, 203, 138, 152, 153, 230, 229, 95, 146, 51, 234, 196, 119, 245, 222, 244, 163, 142, 194, 181, 60, 66, 200, 107, 115, 107, 133, 219, 2, 125, 216, 68, 82, 209, 148, 218, 9, 223, 68, 5, 149, 2, 163, 219, 54, 90, 89, 165, 116, 101 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altitude",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "CoordinateDescription",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "Pafta",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "UtmEasting",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "UtmNorthing",
                table: "QuarryProductions");

            migrationBuilder.DropColumn(
                name: "Altitude",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "Pafta",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "UtmEasting",
                table: "Quarries");

            migrationBuilder.DropColumn(
                name: "UtmNorthing",
                table: "Quarries");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 225, 1, 66, 152, 118, 10, 104, 172, 122, 58, 106, 24, 194, 42, 100, 252, 0, 34, 118, 98, 243, 71, 74, 34, 130, 123, 248, 138, 94, 216, 161, 197, 150, 23, 222, 9, 251, 127, 252, 136, 52, 59, 225, 88, 181, 212, 170, 171, 228, 147, 105, 202, 193, 11, 152, 74, 10, 152, 175, 81, 23, 70, 194, 215 }, new byte[] { 149, 33, 181, 248, 249, 54, 139, 99, 94, 78, 40, 240, 105, 39, 160, 148, 24, 130, 242, 158, 227, 149, 100, 73, 37, 216, 237, 166, 39, 121, 150, 226, 201, 96, 180, 209, 37, 99, 84, 210, 251, 250, 246, 134, 110, 1, 63, 73, 8, 240, 255, 190, 251, 67, 100, 24, 193, 230, 65, 83, 29, 64, 0, 136, 42, 193, 252, 40, 105, 73, 23, 176, 132, 162, 37, 122, 218, 2, 220, 50, 45, 91, 206, 157, 23, 51, 20, 104, 96, 241, 7, 214, 104, 237, 110, 56, 37, 146, 80, 26, 42, 186, 38, 168, 16, 80, 13, 25, 230, 171, 88, 177, 42, 200, 52, 46, 32, 51, 139, 3, 241, 145, 172, 168, 51, 33, 46, 157 } });
        }
    }
}
