using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmSitesi.Migrations
{
    public partial class AccountYetkiColoumnEkle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "yetki",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "yetki",
                table: "Accounts");
        }
    }
}
