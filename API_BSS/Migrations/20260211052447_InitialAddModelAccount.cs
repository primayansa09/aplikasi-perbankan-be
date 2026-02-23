using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_BSS.Migrations
{
    /// <inheritdoc />
    public partial class InitialAddModelAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceAccount",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceAccount",
                table: "Transactions");
        }
    }
}
