using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeEnumsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Enum kolonlarını nullable yap
            migrationBuilder.AlterColumn<int>(
                name: "LicenseType",
                table: "Employees",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "TypeOfBlood", 
                table: "Employees",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Nullable değerleri 0 ile güncelle
            migrationBuilder.Sql(@"
                UPDATE ""Employees"" 
                SET ""LicenseType"" = 0 
                WHERE ""LicenseType"" IS NULL;
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Employees"" 
                SET ""TypeOfBlood"" = 0 
                WHERE ""TypeOfBlood"" IS NULL;
            ");

            migrationBuilder.AlterColumn<int>(
                name: "LicenseType",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeOfBlood",
                table: "Employees",
                type: "integer", 
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}