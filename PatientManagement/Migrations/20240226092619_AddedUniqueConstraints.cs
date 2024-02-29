using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class AddedUniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineName",
                table: "Medicines",
                column: "MedicineName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryHeadings_Heading",
                table: "HistoryHeadings",
                column: "Heading",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDetails_Detail",
                table: "HistoryDetails",
                column: "Detail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Durations_DurationTime",
                table: "Durations",
                column: "DurationTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dosages_Dose",
                table: "Dosages",
                column: "Dose",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicines_MedicineName",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_HistoryHeadings_Heading",
                table: "HistoryHeadings");

            migrationBuilder.DropIndex(
                name: "IX_HistoryDetails_Detail",
                table: "HistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_Durations_DurationTime",
                table: "Durations");

            migrationBuilder.DropIndex(
                name: "IX_Dosages_Dose",
                table: "Dosages");
        }
    }
}
