using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebitManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DebitQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Debit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Debit");
        }
    }
}
