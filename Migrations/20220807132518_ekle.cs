using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmSitesi.Migrations
{
    public partial class ekle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdSoyad",
                table: "Satislar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Stok",
                table: "Satislar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stok",
                table: "Satislar");

            migrationBuilder.AlterColumn<string>(
                name: "AdSoyad",
                table: "Satislar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
