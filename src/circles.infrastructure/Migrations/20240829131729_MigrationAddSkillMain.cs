using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circles.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddSkillMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "CircleSkills",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CircleSkills_SkillId",
                table: "CircleSkills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleSkills_CircleSkills_SkillId",
                table: "CircleSkills",
                column: "SkillId",
                principalTable: "CircleSkills",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleSkills_CircleSkills_SkillId",
                table: "CircleSkills");

            migrationBuilder.DropIndex(
                name: "IX_CircleSkills_SkillId",
                table: "CircleSkills");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "CircleSkills");
        }
    }
}
