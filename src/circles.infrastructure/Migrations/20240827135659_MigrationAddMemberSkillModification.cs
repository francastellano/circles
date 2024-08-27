using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace circles.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAddMemberSkillModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkills_Circles_CircleId",
                table: "MemberSkills");

            migrationBuilder.RenameColumn(
                name: "CircleId",
                table: "MemberSkills",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSkills_CircleId",
                table: "MemberSkills",
                newName: "IX_MemberSkills_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkills_CircleMembers_MemberId",
                table: "MemberSkills",
                column: "MemberId",
                principalTable: "CircleMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkills_CircleMembers_MemberId",
                table: "MemberSkills");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "MemberSkills",
                newName: "CircleId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSkills_MemberId",
                table: "MemberSkills",
                newName: "IX_MemberSkills_CircleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkills_Circles_CircleId",
                table: "MemberSkills",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
