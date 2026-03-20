using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyReportSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubmitReportFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "DailyReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkDescription",
                table: "DailyReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkQuantity",
                table: "DailyReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "DailyReports");

            migrationBuilder.DropColumn(
                name: "WorkDescription",
                table: "DailyReports");

            migrationBuilder.DropColumn(
                name: "WorkQuantity",
                table: "DailyReports");
        }
    }
}
