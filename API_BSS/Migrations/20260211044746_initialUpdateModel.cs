using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_BSS.Migrations
{
    /// <inheritdoc />
    public partial class initialUpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "DestinationAccountr",
                table: "Transactions",
                newName: "DestinationAccount");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Accounts",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionStatus",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountNumberId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumberId",
                table: "Transactions",
                column: "AccountNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UsersId",
                table: "Accounts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UsersId",
                table: "Accounts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountNumberId",
                table: "Transactions",
                column: "AccountNumberId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UsersId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountNumberId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountNumberId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UsersId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountNumberId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "DestinationAccount",
                table: "Transactions",
                newName: "DestinationAccountr");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Accounts",
                newName: "Balance");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionStatus",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
