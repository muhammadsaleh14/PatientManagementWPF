using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class RememberdAddedUniqueToPriorityCorrectlyDiagnosis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiagnosisHeadings_Heading_Priority",
                table: "DiagnosisHeadings");

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

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisHeadings_Heading_Priority",
                table: "DiagnosisHeadings",
                columns: new[] { "Heading", "Priority" },
                unique: true);
        }
    }
}
