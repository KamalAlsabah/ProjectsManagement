using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class AddUserToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ProjectHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistory_UserId",
                table: "ProjectHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHistory_AbpUsers_UserId",
                table: "ProjectHistory",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHistory_AbpUsers_UserId",
                table: "ProjectHistory");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHistory_UserId",
                table: "ProjectHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectHistory");
        }
    }
}
