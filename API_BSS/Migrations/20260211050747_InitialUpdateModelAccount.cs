using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_BSS.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdateModelAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountNumberId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountNumberId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountNumberId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                newName: "IX_Accounts_Account");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Accounts",
                newName: "AccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Account",
                table: "Accounts",
                newName: "IX_Accounts_AccountNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountNumberId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumberId",
                table: "Transactions",
                column: "AccountNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountNumberId",
                table: "Transactions",
                column: "AccountNumberId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
