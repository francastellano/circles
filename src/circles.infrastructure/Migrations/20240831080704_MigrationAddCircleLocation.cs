using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circles.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddCircleLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "CircleActivities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CircleLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CircleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Denomination = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CircleLocations_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CircleActivities_LocationId",
                table: "CircleActivities",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CircleLocations_CircleId",
                table: "CircleLocations",
                column: "CircleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleActivities_CircleLocations_LocationId",
                table: "CircleActivities",
                column: "LocationId",
                principalTable: "CircleLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleActivities_CircleLocations_LocationId",
                table: "CircleActivities");

            migrationBuilder.DropTable(
                name: "CircleLocations");

            migrationBuilder.DropIndex(
                name: "IX_CircleActivities_LocationId",
                table: "CircleActivities");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CircleActivities");
        }
    }
}
