using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circles.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddStatusToActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CircleActivities",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CircleActivities");
        }
    }
}
