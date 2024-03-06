using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class RemovedDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories");

            migrationBuilder.DropTable(
                name: "HistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_HistoryHeadings_Priority",
                table: "HistoryHeadings");

            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryDetailId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetailId",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "HistoryDetailId",
                table: "Histories",
                newName: "HistoryDetail");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryHeadings_Heading_Priority",
                table: "HistoryHeadings",
                columns: new[] { "Heading", "Priority" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetail",
                table: "Histories",
                columns: new[] { "HistoryHeadingId", "HistoryDetail" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoryHeadings_Heading_Priority",
                table: "HistoryHeadings");

            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetail",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "HistoryDetail",
                table: "Histories",
                newName: "HistoryDetailId");

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

            migrationBuilder.CreateIndex(
                name: "IX_HistoryHeadings_Priority",
                table: "HistoryHeadings",
                column: "Priority",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetailId",
                table: "Histories",
                columns: new[] { "HistoryHeadingId", "HistoryDetailId" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDetails_Detail",
                table: "HistoryDetails",
                column: "Detail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId",
                principalTable: "HistoryDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
