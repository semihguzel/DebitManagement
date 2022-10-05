using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebitManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DebitTableNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debits_Product_ProductId",
                table: "Debits");

            migrationBuilder.DropForeignKey(
                name: "FK_Debits_Users_UserId",
                table: "Debits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Debits",
                table: "Debits");

            migrationBuilder.RenameTable(
                name: "Debits",
                newName: "Debit");

            migrationBuilder.RenameIndex(
                name: "IX_Debits_UserId",
                table: "Debit",
                newName: "IX_Debit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Debits_ProductId",
                table: "Debit",
                newName: "IX_Debit_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Debit",
                table: "Debit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Debit_Product_ProductId",
                table: "Debit",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Debit_Users_UserId",
                table: "Debit",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debit_Product_ProductId",
                table: "Debit");

            migrationBuilder.DropForeignKey(
                name: "FK_Debit_Users_UserId",
                table: "Debit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Debit",
                table: "Debit");

            migrationBuilder.RenameTable(
                name: "Debit",
                newName: "Debits");

            migrationBuilder.RenameIndex(
                name: "IX_Debit_UserId",
                table: "Debits",
                newName: "IX_Debits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Debit_ProductId",
                table: "Debits",
                newName: "IX_Debits_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Debits",
                table: "Debits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Debits_Product_ProductId",
                table: "Debits",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Debits_Users_UserId",
                table: "Debits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
