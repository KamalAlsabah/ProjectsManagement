using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class EditSupervisorNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JobTasksId",
                table: "SupervisorNotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorNotes_JobTasksId",
                table: "SupervisorNotes",
                column: "JobTasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorNotes_JobTasks_JobTasksId",
                table: "SupervisorNotes",
                column: "JobTasksId",
                principalTable: "JobTasks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorNotes_JobTasks_JobTasksId",
                table: "SupervisorNotes");

            migrationBuilder.DropIndex(
                name: "IX_SupervisorNotes_JobTasksId",
                table: "SupervisorNotes");

            migrationBuilder.DropColumn(
                name: "JobTasksId",
                table: "SupervisorNotes");
        }
    }
}
