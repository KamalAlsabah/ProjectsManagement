using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class addjobsTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JobsId",
                table: "JobTasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTasks_JobsId",
                table: "JobTasks",
                column: "JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTasks_Jobs_JobsId",
                table: "JobTasks",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTasks_Jobs_JobsId",
                table: "JobTasks");

            migrationBuilder.DropIndex(
                name: "IX_JobTasks_JobsId",
                table: "JobTasks");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "JobTasks");
        }
    }
}
