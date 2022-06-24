using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class Update_onboardcheckpoint_RESET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeName",
                table: "tbl_checkPoint");

            migrationBuilder.DropColumn(
                name: "BUnitName",
                table: "tbl_checkPoint");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "tbl_checkPoint");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "tbl_checkPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BUnitId",
                table: "tbl_checkPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeptID",
                table: "tbl_checkPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "tbl_checkPoint");

            migrationBuilder.DropColumn(
                name: "BUnitId",
                table: "tbl_checkPoint");

            migrationBuilder.DropColumn(
                name: "DeptID",
                table: "tbl_checkPoint");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeName",
                table: "tbl_checkPoint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BUnitName",
                table: "tbl_checkPoint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "tbl_checkPoint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
