using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class update_createrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_createRole",
                table: "tbl_createRole");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "tbl_createRole",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tbl_createRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_createRole",
                table: "tbl_createRole",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_createRole",
                table: "tbl_createRole");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tbl_createRole");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "tbl_createRole",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_createRole",
                table: "tbl_createRole",
                column: "Username");
        }
    }
}
