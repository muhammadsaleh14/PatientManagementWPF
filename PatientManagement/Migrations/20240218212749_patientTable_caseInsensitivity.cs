using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class patientTable_caseInsensitivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patients",
                type: "TEXT COLLATE NOCASE",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT COLLATE NOCASE");
        }
    }
}
