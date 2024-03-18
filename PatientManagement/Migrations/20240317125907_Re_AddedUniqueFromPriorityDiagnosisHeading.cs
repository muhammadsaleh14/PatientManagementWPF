using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class Re_AddedUniqueFromPriorityDiagnosisHeading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisHeadings_Priority",
                table: "DiagnosisHeadings",
                column: "Priority",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiagnosisHeadings_Priority",
                table: "DiagnosisHeadings");
        }
    }
}
