using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class AddedPriorityToDiagnosisHeading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Visits_DiagnosisHeadingId",
                table: "Diagnoses");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "DiagnosisHeadings",
                type: "TEXT COLLATE NOCASE",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "DiagnosisHeadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisHeadings_Heading",
                table: "DiagnosisHeadings",
                column: "Heading",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisHeadings_Heading_Priority",
                table: "DiagnosisHeadings",
                columns: new[] { "Heading", "Priority" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_DiagnosisHeadings_DiagnosisHeadingId",
                table: "Diagnoses",
                column: "DiagnosisHeadingId",
                principalTable: "DiagnosisHeadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_DiagnosisHeadings_DiagnosisHeadingId",
                table: "Diagnoses");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisHeadings_Heading",
                table: "DiagnosisHeadings");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisHeadings_Heading_Priority",
                table: "DiagnosisHeadings");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "DiagnosisHeadings");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "DiagnosisHeadings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT COLLATE NOCASE");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Visits_DiagnosisHeadingId",
                table: "Diagnoses",
                column: "DiagnosisHeadingId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
