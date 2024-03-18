using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class RemovedUniqueFromPriorityDiagnosisHeading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiagnosisHeadings_Priority",
                table: "DiagnosisHeadings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisHeadings_Priority",
                table: "DiagnosisHeadings",
                column: "Priority",
                unique: true);
        }
    }
}
