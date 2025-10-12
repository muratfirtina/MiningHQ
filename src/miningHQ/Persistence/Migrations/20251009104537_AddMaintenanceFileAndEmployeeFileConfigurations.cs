using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceFileAndEmployeeFileConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Önce TPT için alt tabloları oluştur
            
            // EmployeeFiles tablosunu oluştur (eğer yoksa)
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeeFiles') THEN
                        CREATE TABLE ""EmployeeFiles"" (
                            ""Id"" uuid NOT NULL,
                            ""Showcase"" boolean NOT NULL DEFAULT false,
                            CONSTRAINT ""PK_EmployeeFiles"" PRIMARY KEY (""Id""),
                            CONSTRAINT ""FK_EmployeeFiles_Files_Id"" FOREIGN KEY (""Id"") 
                                REFERENCES ""Files""(""Id"") ON DELETE CASCADE
                        );
                    END IF;
                END $$;
            ");

            // MaintenanceFiles tablosunu oluştur (eğer yoksa)
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'MaintenanceFiles') THEN
                        CREATE TABLE ""MaintenanceFiles"" (
                            ""Id"" uuid NOT NULL,
                            CONSTRAINT ""PK_MaintenanceFiles"" PRIMARY KEY (""Id""),
                            CONSTRAINT ""FK_MaintenanceFiles_Files_Id"" FOREIGN KEY (""Id"") 
                                REFERENCES ""Files""(""Id"") ON DELETE CASCADE
                        );
                    END IF;
                END $$;
            ");

            // EmployeePhoto FK ekle (eğer tablo varsa ve FK yoksa)
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeePhoto') THEN
                        IF NOT EXISTS (
                            SELECT 1 FROM information_schema.table_constraints 
                            WHERE constraint_name = 'FK_EmployeePhoto_Files_Id'
                        ) THEN
                            ALTER TABLE ""EmployeePhoto""
                            ADD CONSTRAINT ""FK_EmployeePhoto_Files_Id""
                            FOREIGN KEY (""Id"") REFERENCES ""Files""(""Id"")
                            ON DELETE CASCADE;
                        END IF;
                    END IF;
                END $$;
            ");

            // MachineFiles FK ekle (eğer tablo varsa ve FK yoksa)
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'MachineFiles') THEN
                        IF NOT EXISTS (
                            SELECT 1 FROM information_schema.table_constraints 
                            WHERE constraint_name = 'FK_MachineFiles_Files_Id'
                        ) THEN
                            ALTER TABLE ""MachineFiles""
                            ADD CONSTRAINT ""FK_MachineFiles_Files_Id""
                            FOREIGN KEY (""Id"") REFERENCES ""Files""(""Id"")
                            ON DELETE CASCADE;
                        END IF;
                    END IF;
                END $$;
            ");

            // EmployeeEmployeeFile tablosu varsa yeniden adlandır, yoksa oluştur
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeeEmployeeFile') THEN
                        ALTER TABLE ""EmployeeEmployeeFile"" RENAME TO ""EmployeeFileEmployees"";
                    ELSIF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeeFileEmployees') THEN
                        CREATE TABLE ""EmployeeFileEmployees"" (
                            ""EmployeeFilesId"" uuid NOT NULL,
                            ""EmployeesId"" uuid NOT NULL,
                            CONSTRAINT ""PK_EmployeeFileEmployees"" PRIMARY KEY (""EmployeeFilesId"", ""EmployeesId"")
                        );
                        
                        CREATE INDEX ""IX_EmployeeFileEmployees_EmployeesId"" ON ""EmployeeFileEmployees"" (""EmployeesId"");
                    END IF;
                END $$;
            ");

            // MaintenanceMaintenanceFile tablosu varsa yeniden adlandır, yoksa oluştur
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'MaintenanceMaintenanceFile') THEN
                        ALTER TABLE ""MaintenanceMaintenanceFile"" RENAME TO ""MaintenanceFileMaintenance"";
                    ELSIF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'MaintenanceFileMaintenance') THEN
                        CREATE TABLE ""MaintenanceFileMaintenance"" (
                            ""MaintenanceFilesId"" uuid NOT NULL,
                            ""MaintenancesId"" uuid NOT NULL,
                            CONSTRAINT ""PK_MaintenanceFileMaintenance"" PRIMARY KEY (""MaintenanceFilesId"", ""MaintenancesId"")
                        );
                        
                        CREATE INDEX ""IX_MaintenanceFileMaintenance_MaintenancesId"" ON ""MaintenanceFileMaintenance"" (""MaintenancesId"");
                    END IF;
                END $$;
            ");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 49, 4, 22, 119, 49, 163, 154, 10, 150, 154, 137, 131, 186, 230, 202, 70, 14, 233, 91, 37, 112, 142, 140, 169, 82, 230, 118, 132, 72, 179, 85, 90, 114, 104, 124, 120, 67, 251, 211, 135, 15, 58, 157, 200, 159, 103, 135, 21, 43, 237, 77, 220, 71, 253, 200, 114, 162, 163, 108, 21, 155, 105, 244, 55 }, new byte[] { 241, 29, 89, 102, 113, 34, 255, 150, 98, 155, 137, 56, 94, 153, 71, 11, 214, 206, 182, 113, 2, 223, 215, 133, 236, 87, 158, 6, 117, 55, 50, 145, 205, 97, 67, 44, 8, 126, 192, 196, 137, 246, 150, 207, 6, 120, 12, 191, 3, 8, 154, 196, 46, 34, 66, 219, 248, 79, 61, 108, 161, 229, 86, 44, 237, 155, 18, 22, 126, 249, 178, 77, 39, 60, 149, 161, 70, 50, 13, 239, 244, 67, 61, 137, 103, 184, 136, 196, 129, 187, 161, 83, 200, 96, 160, 127, 128, 79, 214, 230, 64, 239, 173, 33, 159, 18, 112, 93, 96, 223, 135, 204, 194, 120, 237, 96, 69, 84, 102, 103, 73, 146, 158, 231, 169, 55, 52, 230 } });

            // Foreign key'leri ekle (eğer yoksa)
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.table_constraints 
                        WHERE constraint_name = 'FK_EmployeeFileEmployees_EmployeeFiles_EmployeeFilesId'
                    ) THEN
                        ALTER TABLE ""EmployeeFileEmployees""
                        ADD CONSTRAINT ""FK_EmployeeFileEmployees_EmployeeFiles_EmployeeFilesId""
                        FOREIGN KEY (""EmployeeFilesId"") REFERENCES ""EmployeeFiles""(""Id"")
                        ON DELETE CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.table_constraints 
                        WHERE constraint_name = 'FK_EmployeeFileEmployees_Employees_EmployeesId'
                    ) THEN
                        ALTER TABLE ""EmployeeFileEmployees""
                        ADD CONSTRAINT ""FK_EmployeeFileEmployees_Employees_EmployeesId""
                        FOREIGN KEY (""EmployeesId"") REFERENCES ""Employees""(""Id"")
                        ON DELETE CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.table_constraints 
                        WHERE constraint_name = 'FK_MaintenanceFileMaintenance_MaintenanceFiles_MaintenanceFile~'
                    ) THEN
                        ALTER TABLE ""MaintenanceFileMaintenance""
                        ADD CONSTRAINT ""FK_MaintenanceFileMaintenance_MaintenanceFiles_MaintenanceFile~""
                        FOREIGN KEY (""MaintenanceFilesId"") REFERENCES ""MaintenanceFiles""(""Id"")
                        ON DELETE CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.table_constraints 
                        WHERE constraint_name = 'FK_MaintenanceFileMaintenance_Maintenances_MaintenancesId'
                    ) THEN
                        ALTER TABLE ""MaintenanceFileMaintenance""
                        ADD CONSTRAINT ""FK_MaintenanceFileMaintenance_Maintenances_MaintenancesId""
                        FOREIGN KEY (""MaintenancesId"") REFERENCES ""Maintenances""(""Id"")
                        ON DELETE CASCADE;
                    END IF;
                END $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rollback için tabloları eski haline döndür
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeeFileEmployees') THEN
                        DROP TABLE IF EXISTS ""EmployeeFileEmployees"" CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'MaintenanceFileMaintenance') THEN
                        DROP TABLE IF EXISTS ""MaintenanceFileMaintenance"" CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'MaintenanceFiles') THEN
                        DROP TABLE IF EXISTS ""MaintenanceFiles"" CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeeFiles') THEN
                        DROP TABLE IF EXISTS ""EmployeeFiles"" CASCADE;
                    END IF;
                END $$;
            ");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("729c40f5-0859-48d7-a388-451520c1289c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 115, 212, 210, 157, 216, 204, 186, 252, 236, 160, 132, 165, 172, 166, 36, 154, 230, 209, 208, 230, 48, 153, 242, 166, 34, 61, 113, 33, 11, 178, 252, 85, 115, 220, 208, 80, 173, 145, 49, 7, 143, 216, 186, 241, 37, 155, 143, 143, 208, 184, 65, 207, 118, 106, 136, 200, 10, 54, 248, 106, 25, 242, 92, 54 }, new byte[] { 148, 173, 66, 25, 212, 212, 22, 4, 13, 157, 193, 124, 153, 14, 93, 239, 24, 144, 136, 217, 250, 9, 145, 0, 193, 212, 191, 140, 20, 96, 252, 64, 6, 163, 68, 161, 171, 222, 245, 94, 158, 90, 184, 165, 14, 32, 41, 131, 163, 81, 174, 214, 251, 212, 87, 254, 210, 36, 178, 11, 83, 191, 118, 189, 236, 21, 212, 58, 50, 187, 192, 116, 108, 76, 34, 128, 90, 42, 11, 212, 75, 214, 149, 250, 188, 221, 37, 90, 80, 65, 118, 101, 97, 233, 101, 236, 250, 100, 20, 129, 25, 211, 251, 28, 28, 197, 152, 56, 207, 69, 50, 233, 103, 16, 158, 163, 66, 175, 29, 83, 117, 130, 115, 139, 62, 165, 127, 25 } });
        }
    }
}
