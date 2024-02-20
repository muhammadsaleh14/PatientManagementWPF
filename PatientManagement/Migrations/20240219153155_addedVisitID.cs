using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class addedVisitID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecord_Visit_VisitId",
                table: "PatientRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Visit_VisitId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Patients_PatientId",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visit",
                table: "Visit");

            migrationBuilder.RenameTable(
                name: "Visit",
                newName: "Visits");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_PatientId",
                table: "Visits",
                newName: "IX_Visits_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visits",
                table: "Visits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecord_Visits_VisitId",
                table: "PatientRecord",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Visits_VisitId",
                table: "Prescription",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Patients_PatientId",
                table: "Visits",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecord_Visits_VisitId",
                table: "PatientRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Visits_VisitId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Patients_PatientId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visits",
                table: "Visits");

            migrationBuilder.RenameTable(
                name: "Visits",
                newName: "Visit");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_PatientId",
                table: "Visit",
                newName: "IX_Visit_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visit",
                table: "Visit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecord_Visit_VisitId",
                table: "PatientRecord",
                column: "VisitId",
                principalTable: "Visit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Visit_VisitId",
                table: "Prescription",
                column: "VisitId",
                principalTable: "Visit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Patients_PatientId",
                table: "Visit",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
