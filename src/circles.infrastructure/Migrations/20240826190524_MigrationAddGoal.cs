using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circles.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CircleGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CircleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Denomination = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CircleGoals_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CircleGoals_CircleId",
                table: "CircleGoals",
                column: "CircleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CircleGoals");
        }
    }
}
