using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circles.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddEmailCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Circles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Circles");
        }
    }
}
