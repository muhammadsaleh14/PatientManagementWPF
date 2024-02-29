using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class fixedErrorNowIg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryVisit");

            migrationBuilder.DropTable(
                name: "PrescriptionVisit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PrescriptionValue",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Histories",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryHeadingId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "HistoryDetail",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "PrescriptionValue",
                table: "Prescriptions",
                newName: "DurationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prescriptions",
                newName: "DosageId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Histories",
                newName: "HistoryDetailId");

            migrationBuilder.AddColumn<string>(
                name: "MedicineId",
                table: "Prescriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                columns: new[] { "MedicineId", "DosageId", "DurationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Histories",
                table: "Histories",
                columns: new[] { "HistoryHeadingId", "HistoryDetailId" });

            migrationBuilder.CreateTable(
                name: "Dosages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Dose = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Durations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DurationTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Detail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    MedicineName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitHistory",
                columns: table => new
                {
                    VisitId = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryHeadingId = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryDetailId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitHistory", x => new { x.VisitId, x.HistoryHeadingId, x.HistoryDetailId });
                    table.ForeignKey(
                        name: "FK_VisitHistory_Histories_HistoryHeadingId_HistoryDetailId",
                        columns: x => new { x.HistoryHeadingId, x.HistoryDetailId },
                        principalTable: "Histories",
                        principalColumns: new[] { "HistoryHeadingId", "HistoryDetailId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitHistory_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitPrescription",
                columns: table => new
                {
                    VisitId = table.Column<string>(type: "TEXT", nullable: false),
                    MedicineId = table.Column<string>(type: "TEXT", nullable: false),
                    DosageId = table.Column<string>(type: "TEXT", nullable: false),
                    DurationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitPrescription", x => new { x.VisitId, x.MedicineId, x.DosageId, x.DurationId });
                    table.ForeignKey(
                        name: "FK_VisitPrescription_Prescriptions_MedicineId_DosageId_DurationId",
                        columns: x => new { x.MedicineId, x.DosageId, x.DurationId },
                        principalTable: "Prescriptions",
                        principalColumns: new[] { "MedicineId", "DosageId", "DurationId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitPrescription_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DosageId",
                table: "Prescriptions",
                column: "DosageId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DurationId",
                table: "Prescriptions",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitHistory_HistoryHeadingId_HistoryDetailId",
                table: "VisitHistory",
                columns: new[] { "HistoryHeadingId", "HistoryDetailId" });

            migrationBuilder.CreateIndex(
                name: "IX_VisitPrescription_MedicineId_DosageId_DurationId",
                table: "VisitPrescription",
                columns: new[] { "MedicineId", "DosageId", "DurationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId",
                principalTable: "HistoryDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Dosages_DosageId",
                table: "Prescriptions",
                column: "DosageId",
                principalTable: "Dosages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Durations_DurationId",
                table: "Prescriptions",
                column: "DurationId",
                principalTable: "Durations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Dosages_DosageId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Durations_DurationId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Dosages");

            migrationBuilder.DropTable(
                name: "Durations");

            migrationBuilder.DropTable(
                name: "HistoryDetails");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "VisitHistory");

            migrationBuilder.DropTable(
                name: "VisitPrescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DosageId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DurationId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Histories",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryDetailId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "DurationId",
                table: "Prescriptions",
                newName: "PrescriptionValue");

            migrationBuilder.RenameColumn(
                name: "DosageId",
                table: "Prescriptions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HistoryDetailId",
                table: "Histories",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "HistoryDetail",
                table: "Histories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Histories",
                table: "Histories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HistoryVisit",
                columns: table => new
                {
                    HistoriesId = table.Column<string>(type: "TEXT", nullable: false),
                    VisitsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryVisit", x => new { x.HistoriesId, x.VisitsId });
                    table.ForeignKey(
                        name: "FK_HistoryVisit_Histories_HistoriesId",
                        column: x => x.HistoriesId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryVisit_Visits_VisitsId",
                        column: x => x.VisitsId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Histories_HistoryHeadingId",
                table: "Histories",
                column: "HistoryHeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryVisit_VisitsId",
                table: "HistoryVisit",
                column: "VisitsId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionVisit_VisitsId",
                table: "PrescriptionVisit",
                column: "VisitsId");
        }
    }
}
