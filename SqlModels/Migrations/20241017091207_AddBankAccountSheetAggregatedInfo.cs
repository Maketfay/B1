using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlModels.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAccountSheetAggregatedInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AggregatedInfo_IncomingBalanceActive",
                table: "BankBalanceSheets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AggregatedInfo_IncomingBalancePassive",
                table: "BankBalanceSheets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AggregatedInfo_OutgoingBalanceActive",
                table: "BankBalanceSheets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AggregatedInfo_OutgoingBalancePassive",
                table: "BankBalanceSheets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AggregatedInfo_TurnoverCredit",
                table: "BankBalanceSheets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AggregatedInfo_TurnoverDebit",
                table: "BankBalanceSheets",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregatedInfo_IncomingBalanceActive",
                table: "BankBalanceSheets");

            migrationBuilder.DropColumn(
                name: "AggregatedInfo_IncomingBalancePassive",
                table: "BankBalanceSheets");

            migrationBuilder.DropColumn(
                name: "AggregatedInfo_OutgoingBalanceActive",
                table: "BankBalanceSheets");

            migrationBuilder.DropColumn(
                name: "AggregatedInfo_OutgoingBalancePassive",
                table: "BankBalanceSheets");

            migrationBuilder.DropColumn(
                name: "AggregatedInfo_TurnoverCredit",
                table: "BankBalanceSheets");

            migrationBuilder.DropColumn(
                name: "AggregatedInfo_TurnoverDebit",
                table: "BankBalanceSheets");
        }
    }
}
