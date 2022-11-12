using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebitManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DebitUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDescription",
                table: "Debit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionDescription",
                table: "Debit",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
