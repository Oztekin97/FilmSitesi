using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmSitesi.Migrations
{
    public partial class PersonelDbTablosu1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personeller");
        }
    }
}
