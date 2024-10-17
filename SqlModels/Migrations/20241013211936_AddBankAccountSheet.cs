using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlModels.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAccountSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankBalanceSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BankName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Currency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBalanceSheets", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BankAccountGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankBalanceSheetId = table.Column<int>(type: "int", nullable: false),
                    AggregatedInfo_IncomingBalanceActive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_IncomingBalancePassive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_OutgoingBalanceActive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_OutgoingBalancePassive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_TurnoverCredit = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_TurnoverDebit = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountGroups_BankBalanceSheets_BankBalanceSheetId",
                        column: x => x.BankBalanceSheetId,
                        principalTable: "BankBalanceSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BankAccountBalanceGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupIndex = table.Column<int>(type: "int", nullable: false),
                    BankAccountGroupId = table.Column<int>(type: "int", nullable: true),
                    AggregatedInfo_IncomingBalanceActive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_IncomingBalancePassive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_OutgoingBalanceActive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_OutgoingBalancePassive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_TurnoverCredit = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AggregatedInfo_TurnoverDebit = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBalanceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountBalanceGroups_BankAccountGroups_BankAccountGroupId",
                        column: x => x.BankAccountGroupId,
                        principalTable: "BankAccountGroups",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BankAccountDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    BankAccountBalanceGroupId = table.Column<int>(type: "int", nullable: false),
                    Info_IncomingBalanceActive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Info_IncomingBalancePassive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Info_OutgoingBalanceActive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Info_OutgoingBalancePassive = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Info_TurnoverCredit = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Info_TurnoverDebit = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountDatas_BankAccountBalanceGroups_BankAccountBalance~",
                        column: x => x.BankAccountBalanceGroupId,
                        principalTable: "BankAccountBalanceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceGroups_BankAccountGroupId",
                table: "BankAccountBalanceGroups",
                column: "BankAccountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountDatas_BankAccountBalanceGroupId",
                table: "BankAccountDatas",
                column: "BankAccountBalanceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountGroups_BankBalanceSheetId",
                table: "BankAccountGroups",
                column: "BankBalanceSheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountDatas");

            migrationBuilder.DropTable(
                name: "BankAccountBalanceGroups");

            migrationBuilder.DropTable(
                name: "BankAccountGroups");

            migrationBuilder.DropTable(
                name: "BankBalanceSheets");
        }
    }
}
