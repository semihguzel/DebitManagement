using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebitManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserIdUserUserRoleRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRoles_Users_UserRoleId",
                table: "UserUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRoles_Users_UserId",
                table: "UserUserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRoles_Users_UserId",
                table: "UserUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRoles_Users_UserRoleId",
                table: "UserUserRoles",
                column: "UserRoleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
