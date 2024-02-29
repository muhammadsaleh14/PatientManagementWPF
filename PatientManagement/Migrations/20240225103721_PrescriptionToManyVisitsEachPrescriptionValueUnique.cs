using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class PrescriptionToManyVisitsEachPrescriptionValueUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Visits_VisitId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_VisitId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Prescriptions");

            migrationBuilder.CreateTable(
                name: "PrescriptionVisit",
                columns: table => new
                {
                    PrescriptionsId = table.Column<string>(type: "TEXT", nullable: false),
                    VisitsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionVisit", x => new { x.PrescriptionsId, x.VisitsId });
                    table.ForeignKey(
                        name: "FK_PrescriptionVisit_Prescriptions_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionVisit_Visits_VisitsId",
                        column: x => x.VisitsId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescriptionValue",
                table: "Prescriptions",
                column: "PrescriptionValue",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionVisit_VisitsId",
                table: "PrescriptionVisit",
                column: "VisitsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionVisit");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PrescriptionValue",
                table: "Prescriptions");

            migrationBuilder.AddColumn<string>(
                name: "VisitId",
                table: "Prescriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_VisitId",
                table: "Prescriptions",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Visits_VisitId",
                table: "Prescriptions",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
