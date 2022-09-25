using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmSitesi.Migrations
{
    public partial class EkleSatisDbGuncel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TC",
                table: "Satislar");

            migrationBuilder.RenameColumn(
                name: "AdSoyad",
                table: "Satislar",
                newName: "FilmAdi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilmAdi",
                table: "Satislar",
                newName: "AdSoyad");

            migrationBuilder.AddColumn<int>(
                name: "TC",
                table: "Satislar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
