using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class NewDashboardUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersJobs_AbpUsers_WorkerId",
                table: "WorkersJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersJobs_Jobs_JobId",
                table: "WorkersJobs");

            migrationBuilder.AlterColumn<long>(
                name: "WorkerId",
                table: "WorkersJobs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "JobId",
                table: "WorkersJobs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersJobs_AbpUsers_WorkerId",
                table: "WorkersJobs",
                column: "WorkerId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersJobs_Jobs_JobId",
                table: "WorkersJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersJobs_AbpUsers_WorkerId",
                table: "WorkersJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersJobs_Jobs_JobId",
                table: "WorkersJobs");

            migrationBuilder.AlterColumn<long>(
                name: "WorkerId",
                table: "WorkersJobs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "JobId",
                table: "WorkersJobs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersJobs_AbpUsers_WorkerId",
                table: "WorkersJobs",
                column: "WorkerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersJobs_Jobs_JobId",
                table: "WorkersJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
