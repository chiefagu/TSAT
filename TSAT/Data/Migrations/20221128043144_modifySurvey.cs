using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSAT.Data.Migrations
{
    public partial class modifySurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Surveys");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Surveys",
                newName: "UserName");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Surveys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Surveys");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Surveys",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Surveys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
