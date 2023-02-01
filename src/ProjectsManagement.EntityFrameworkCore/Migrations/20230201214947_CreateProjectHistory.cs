using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class CreateProjectHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    SprintId = table.Column<long>(type: "bigint", nullable: true),
                    JobId = table.Column<long>(type: "bigint", nullable: true),
                    JobTasksId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectWorkersId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectSupervisorsId = table.Column<long>(type: "bigint", nullable: true),
                    SupervisorNotesId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectHistoryActions = table.Column<int>(type: "int", nullable: false),
                    ProjectHistoryColumns = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHistory_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectHistory_JobTasks_JobTasksId",
                        column: x => x.JobTasksId,
                        principalTable: "JobTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectHistory_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectHistory_ProjectSupervisors_ProjectSupervisorsId",
                        column: x => x.ProjectSupervisorsId,
                        principalTable: "ProjectSupervisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectHistory_ProjectWorkers_ProjectWorkersId",
                        column: x => x.ProjectWorkersId,
                        principalTable: "ProjectWorkers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectHistory_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectHistory_SupervisorNotes_SupervisorNotesId",
                        column: x => x.SupervisorNotesId,
                        principalTable: "SupervisorNotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_JobId",
                table: "ProjectHistory",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_JobTasksId",
                table: "ProjectHistory",
                column: "JobTasksId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_ProjectId",
                table: "ProjectHistory",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_ProjectSupervisorsId",
                table: "ProjectHistory",
                column: "ProjectSupervisorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_ProjectWorkersId",
                table: "ProjectHistory",
                column: "ProjectWorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_SprintId",
                table: "ProjectHistory",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_SupervisorNotesId",
                table: "ProjectHistory",
                column: "SupervisorNotesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectHistory");
        }
    }
}
