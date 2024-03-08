using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class AddedHistoryTableAndHistoryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitHistory");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.AddColumn<string>(
                name: "HistoryTableId",
                table: "Visits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoryTables",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    InitialVisitId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryHeadingId = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryTableId = table.Column<string>(type: "TEXT", nullable: false),
                    Detail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryItems_HistoryHeadings_HistoryHeadingId",
                        column: x => x.HistoryHeadingId,
                        principalTable: "HistoryHeadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryItems_HistoryTables_HistoryTableId",
                        column: x => x.HistoryTableId,
                        principalTable: "HistoryTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_HistoryTableId",
                table: "Visits",
                column: "HistoryTableId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryItems_HistoryHeadingId",
                table: "HistoryItems",
                column: "HistoryHeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryItems_HistoryTableId",
                table: "HistoryItems",
                column: "HistoryTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_HistoryTables_HistoryTableId",
                table: "Visits",
                column: "HistoryTableId",
                principalTable: "HistoryTables",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_HistoryTables_HistoryTableId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "HistoryItems");

            migrationBuilder.DropTable(
                name: "HistoryTables");

            migrationBuilder.DropIndex(
                name: "IX_Visits_HistoryTableId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "HistoryTableId",
                table: "Visits");

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryHeadingId = table.Column<string>(type: "TEXT", nullable: false),
                    HistoryDetail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_HistoryHeadings_HistoryHeadingId",
                        column: x => x.HistoryHeadingId,
                        principalTable: "HistoryHeadings",
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

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetail",
                table: "Histories",
                columns: new[] { "HistoryHeadingId", "HistoryDetail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitHistory_VisitId",
                table: "VisitHistory",
                column: "VisitId");
        }
    }
}
