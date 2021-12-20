using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSiteAPI.Data.Migrations
{
    public partial class updatemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryName",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobNatureName",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IndustryName",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobNatureName",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Jobs");
        }
    }
}
