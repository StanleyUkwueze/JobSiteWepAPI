using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSiteAPI.Data.Migrations
{
    public partial class ValidDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_AppUserId1",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_AppUserId1",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "JobValidDays",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "JobApplications",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_AppUserId",
                table: "JobApplications",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_AppUserId",
                table: "JobApplications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_AppUserId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_AppUserId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobValidDays",
                table: "Jobs");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "JobApplications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "JobApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_AppUserId1",
                table: "JobApplications",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_AppUserId1",
                table: "JobApplications",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
