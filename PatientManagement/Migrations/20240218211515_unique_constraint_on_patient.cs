using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class unique_constraint_on_patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name_Gender_Age",
                table: "Patients",
                columns: new[] { "Name", "Gender", "Age" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Name_Gender_Age",
                table: "Patients");
        }
    }
}
