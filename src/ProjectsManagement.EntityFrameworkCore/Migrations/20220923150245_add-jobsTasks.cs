using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class addjobsTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTasks_Jobs_JobsId",
                table: "JobTasks");

            migrationBuilder.RenameColumn(
                name: "JobsId",
                table: "JobTasks",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobTasks_JobsId",
                table: "JobTasks",
                newName: "IX_JobTasks_JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTasks_Jobs_JobId",
                table: "JobTasks",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTasks_Jobs_JobId",
                table: "JobTasks");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "JobTasks",
                newName: "JobsId");

            migrationBuilder.RenameIndex(
                name: "IX_JobTasks_JobId",
                table: "JobTasks",
                newName: "IX_JobTasks_JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTasks_Jobs_JobsId",
                table: "JobTasks",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
