using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlModels.Migrations
{
    /// <inheritdoc />
    public partial class AddMedianSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS CalculateSumAndMedian;
                    create procedure CalculateSumAndMedian(OUT intSum bigint, OUT median decimal(65, 30))
                    BEGIN
                        SELECT SUM(RandomInteger)
                        INTO intSum
                        FROM RandomFiles;

                        SELECT AVG(RandomDouble)
                        INTO median
                        FROM (SELECT RandomDouble,
                                     ROW_NUMBER() OVER (ORDER BY RandomDouble, Id)           AS RowAsc,
                                     ROW_NUMBER() OVER (ORDER BY RandomDouble DESC, Id DESC) AS RowDesc
                              FROM RandomFiles th) AS data
                        WHERE RowAsc IN (RowDesc, RowDesc - 1, RowDesc + 1);
                    END;
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS CalculateSumAndMedian;");
        }
    }
}
