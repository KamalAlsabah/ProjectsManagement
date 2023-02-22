using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    public partial class DateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalHours",
                table: "WorkersHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "WorkersHistory");
        }
    }
}
