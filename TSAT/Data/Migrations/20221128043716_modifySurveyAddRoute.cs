using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSAT.Data.Migrations
{
    public partial class modifySurveyAddRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Surveys");

            migrationBuilder.AlterColumn<int>(
                name: "Route",
                table: "Surveys",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Route",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Surveys",
                type: "int",
                nullable: true);
        }
    }
}
