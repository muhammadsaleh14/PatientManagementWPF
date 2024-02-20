using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class PatientRecord_to_patientHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientRecord");

            migrationBuilder.DropTable(
                name: "PatientRecordHeading");

            migrationBuilder.CreateTable(
                name: "PatientHistoryHeading",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HeadingValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHistoryHeading", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PatientHistoryHeadingId = table.Column<string>(type: "TEXT", nullable: true),
                    PatientHistoryDetail = table.Column<string>(type: "TEXT", nullable: false),
                    VisitId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientHistory_PatientHistoryHeading_PatientHistoryHeadingId",
                        column: x => x.PatientHistoryHeadingId,
                        principalTable: "PatientHistoryHeading",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientHistory_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistory_PatientHistoryHeadingId",
                table: "PatientHistory",
                column: "PatientHistoryHeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistory_VisitId",
                table: "PatientHistory",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientHistory");

            migrationBuilder.DropTable(
                name: "PatientHistoryHeading");

            migrationBuilder.CreateTable(
                name: "PatientRecordHeading",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HeadingValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRecordHeading", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PatientRecordHeadingId = table.Column<string>(type: "TEXT", nullable: true),
                    PatientRecordDetail = table.Column<string>(type: "TEXT", nullable: false),
                    VisitId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientRecord_PatientRecordHeading_PatientRecordHeadingId",
                        column: x => x.PatientRecordHeadingId,
                        principalTable: "PatientRecordHeading",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientRecord_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecord_PatientRecordHeadingId",
                table: "PatientRecord",
                column: "PatientRecordHeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecord_VisitId",
                table: "PatientRecord",
                column: "VisitId");
        }
    }
}
