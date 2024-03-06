using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "HistoryHeadings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Heading = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryHeadings", x => x.Id);
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
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT COLLATE NOCASE", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryHeadingId = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryDetailId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_HistoryDetails_HistoryDetailId",
                        column: x => x.HistoryDetailId,
                        principalTable: "HistoryDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Histories_HistoryHeadings_HistoryHeadingId",
                        column: x => x.HistoryHeadingId,
                        principalTable: "HistoryHeadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    MedicineId = table.Column<string>(type: "TEXT", nullable: false),
                    DosageId = table.Column<string>(type: "TEXT", nullable: false),
                    DurationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => new { x.MedicineId, x.DosageId, x.DurationId });
                    table.ForeignKey(
                        name: "FK_Prescriptions_Dosages_DosageId",
                        column: x => x.DosageId,
                        principalTable: "Dosages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PatientId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OptionalDetail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitHistory",
                columns: table => new
                {
                    HistoryId = table.Column<string>(type: "TEXT", nullable: false),
                    VisitId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitHistory", x => new { x.HistoryId, x.VisitId });
                    table.ForeignKey(
                        name: "FK_VisitHistory_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
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
                name: "IX_Dosages_Dose",
                table: "Dosages",
                column: "Dose",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Durations_DurationTime",
                table: "Durations",
                column: "DurationTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryHeadingId",
                table: "Histories",
                column: "HistoryHeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDetails_Detail",
                table: "HistoryDetails",
                column: "Detail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryHeadings_Heading",
                table: "HistoryHeadings",
                column: "Heading",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryHeadings_Priority",
                table: "HistoryHeadings",
                column: "Priority",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineName",
                table: "Medicines",
                column: "MedicineName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name_Gender_Age",
                table: "Patients",
                columns: new[] { "Name", "Gender", "Age" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DosageId",
                table: "Prescriptions",
                column: "DosageId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DurationId",
                table: "Prescriptions",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitHistory_VisitId",
                table: "VisitHistory",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitPrescription_MedicineId_DosageId_DurationId",
                table: "VisitPrescription",
                columns: new[] { "MedicineId", "DosageId", "DurationId" });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientId",
                table: "Visits",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitHistory");

            migrationBuilder.DropTable(
                name: "VisitPrescription");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "HistoryDetails");

            migrationBuilder.DropTable(
                name: "HistoryHeadings");

            migrationBuilder.DropTable(
                name: "Dosages");

            migrationBuilder.DropTable(
                name: "Durations");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
