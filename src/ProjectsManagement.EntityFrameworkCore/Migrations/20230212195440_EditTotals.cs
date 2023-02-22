using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class EditTotals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "WorkersHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalHours",
                table: "WorkersHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
