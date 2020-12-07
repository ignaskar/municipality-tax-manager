using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxManager.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearlyTaxRate = table.Column<double>(type: "float", nullable: false),
                    MonthlyTaxRate = table.Column<double>(type: "float", nullable: false),
                    WeeklyTaxRate = table.Column<double>(type: "float", nullable: true),
                    DailyTaxRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Municipalities");
        }
    }
}
