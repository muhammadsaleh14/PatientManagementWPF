using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class MadedetailFkNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories");

            migrationBuilder.AlterColumn<string>(
                name: "HistoryDetailId",
                table: "Histories",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId",
                principalTable: "HistoryDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories");

            migrationBuilder.AlterColumn<string>(
                name: "HistoryDetailId",
                table: "Histories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_HistoryDetails_HistoryDetailId",
                table: "Histories",
                column: "HistoryDetailId",
                principalTable: "HistoryDetails",
                principalColumn: "Id");
        }
    }
}
