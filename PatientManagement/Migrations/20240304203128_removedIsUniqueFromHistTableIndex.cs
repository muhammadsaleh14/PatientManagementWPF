using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class removedIsUniqueFromHistTableIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryHeadingId",
                table: "Histories");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetailId",
                table: "Histories",
                columns: new[] { "HistoryHeadingId", "HistoryDetailId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Histories_HistoryHeadingId_HistoryDetailId",
                table: "Histories");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_HistoryHeadingId",
                table: "Histories",
                column: "HistoryHeadingId");
        }
    }
}
