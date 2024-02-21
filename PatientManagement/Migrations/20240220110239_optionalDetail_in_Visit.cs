using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    public partial class optionalDetail_in_Visit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OptionalDetail",
                table: "Visits",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionalDetail",
                table: "Visits");
        }
    }
}
