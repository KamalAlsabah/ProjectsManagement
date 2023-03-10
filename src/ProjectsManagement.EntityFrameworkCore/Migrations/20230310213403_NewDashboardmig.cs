using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class NewDashboardmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersJobs_ProjectWorkers_WorkerId",
                table: "WorkersJobs");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersJobs_AbpUsers_WorkerId",
                table: "WorkersJobs",
                column: "WorkerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersJobs_AbpUsers_WorkerId",
                table: "WorkersJobs");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersJobs_ProjectWorkers_WorkerId",
                table: "WorkersJobs",
                column: "WorkerId",
                principalTable: "ProjectWorkers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
