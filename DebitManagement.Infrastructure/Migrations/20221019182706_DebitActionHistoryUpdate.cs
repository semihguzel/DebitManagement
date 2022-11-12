using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebitManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DebitActionHistoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DebitActionHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DebitActionHistory",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
