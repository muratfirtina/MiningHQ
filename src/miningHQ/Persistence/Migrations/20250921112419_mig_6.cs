using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eski LicenseType kolonunu sil
            migrationBuilder.DropColumn(
                name: "LicenseType",
                table: "Employees");

            // Yeni LicenseType kolonunu enum (int) olarak ekle
            migrationBuilder.AddColumn<int>(
                name: "LicenseType",
                table: "Employees",
                type: "integer",
                nullable: true);

            // Yeni OperatorLicense kolonunu ekle
            migrationBuilder.AddColumn<int>(
                name: "OperatorLicense",
                table: "Employees",
                type: "integer",
                nullable: true);

            // Seed data güncellemesi
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { 
                    new byte[] { 174, 179, 161, 2, 249, 198, 104, 25, 59, 76, 213, 89, 35, 38, 105, 60, 148, 230, 12, 103, 153, 79, 250, 255, 176, 217, 38, 11, 131, 235, 228, 166, 227, 133, 110, 37, 180, 241, 66, 160, 53, 192, 71, 42, 49, 34, 97, 194, 234, 142, 182, 234, 212, 10, 249, 192, 46, 114, 127, 222, 248, 194, 145, 228 }, 
                    new byte[] { 217, 35, 143, 235, 151, 219, 101, 33, 151, 115, 219, 112, 98, 166, 8, 102, 162, 88, 139, 43, 188, 174, 42, 143, 212, 126, 225, 8, 233, 140, 126, 35, 121, 170, 154, 110, 200, 99, 144, 9, 63, 21, 24, 75, 236, 157, 191, 199, 98, 2, 137, 254, 217, 7, 182, 122, 38, 120, 46, 90, 159, 3, 124, 46, 119, 162, 34, 62, 231, 41, 188, 37, 81, 102, 114, 81, 221, 75, 199, 103, 192, 114, 241, 82, 215, 160, 252, 118, 169, 28, 98, 221, 107, 149, 160, 40, 217, 152, 206, 48, 186, 39, 199, 103, 8, 211, 159, 209, 129, 251, 230, 231, 163, 194, 134, 213, 129, 49, 193, 190, 31, 144, 130, 32, 9, 86, 101, 195 } 
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // OperatorLicense kolonunu sil
            migrationBuilder.DropColumn(
                name: "OperatorLicense",
                table: "Employees");

            // LicenseType (enum/int) kolonunu sil
            migrationBuilder.DropColumn(
                name: "LicenseType",
                table: "Employees");

            // Eski string LicenseType kolonunu geri ekle
            migrationBuilder.AddColumn<string>(
                name: "LicenseType",
                table: "Employees",
                type: "text",
                nullable: true);

            // Seed data rollback
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { 
                    new byte[] { 233, 244, 95, 46, 31, 232, 123, 2, 36, 208, 247, 146, 152, 44, 106, 47, 231, 6, 202, 71, 156, 240, 31, 148, 63, 12, 73, 37, 137, 77, 212, 135, 157, 30, 100, 243, 208, 226, 197, 1, 213, 192, 76, 57, 60, 3, 234, 1, 115, 49, 27, 188, 231, 36, 109, 7, 21, 161, 195, 243, 104, 188, 160, 244 }, 
                    new byte[] { 24, 27, 147, 31, 47, 49, 13, 223, 197, 100, 242, 35, 242, 252, 250, 90, 64, 127, 103, 220, 103, 165, 164, 102, 3, 65, 19, 146, 228, 40, 7, 139, 240, 203, 248, 91, 187, 240, 12, 226, 42, 182, 29, 98, 229, 247, 228, 150, 184, 17, 250, 15, 47, 117, 25, 140, 127, 185, 55, 55, 50, 82, 19, 55, 82, 185, 149, 156, 211, 136, 236, 133, 46, 170, 67, 38, 119, 2, 61, 98, 136, 236, 57, 198, 238, 164, 168, 19, 171, 183, 125, 26, 25, 22, 98, 14, 23, 251, 240, 224, 220, 145, 101, 44, 167, 16, 132, 228, 186, 173, 72, 128, 102, 85, 208, 83, 136, 58, 36, 118, 220, 20, 75, 186, 228, 108, 248, 19 } 
                });
        }
    }
}