using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class EditJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkerId",
                table: "Jobs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_WorkerId",
                table: "Jobs",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AbpUsers_WorkerId",
                table: "Jobs",
                column: "WorkerId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AbpUsers_WorkerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_WorkerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Jobs");
        }
    }
}
