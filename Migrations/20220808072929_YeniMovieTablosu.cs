using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmSitesi.Migrations
{
    public partial class YeniMovieTablosu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciro");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Rezervasyonlar");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Rezervasyonlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stok",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_MovieId",
                table: "Rezervasyonlar",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervasyonlar_Movie_MovieId",
                table: "Rezervasyonlar",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervasyonlar_Movie_MovieId",
                table: "Rezervasyonlar");

            migrationBuilder.DropIndex(
                name: "IX_Rezervasyonlar_MovieId",
                table: "Rezervasyonlar");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Rezervasyonlar");

            migrationBuilder.DropColumn(
                name: "Stok",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Rezervasyonlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Ciro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciro_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciro_MovieId",
                table: "Ciro",
                column: "MovieId",
                unique: true,
                filter: "[MovieId] IS NOT NULL");
        }
    }
}
