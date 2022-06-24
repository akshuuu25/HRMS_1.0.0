using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class update_offboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckPointName",
                table: "OffBoardingCheckPoint",
                newName: "offCheckPointName");

            migrationBuilder.RenameColumn(
                name: "CheckPointId",
                table: "OffBoardingCheckPoint",
                newName: "offCheckPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "offCheckPointName",
                table: "OffBoardingCheckPoint",
                newName: "CheckPointName");

            migrationBuilder.RenameColumn(
                name: "offCheckPointId",
                table: "OffBoardingCheckPoint",
                newName: "CheckPointId");
        }
    }
}
