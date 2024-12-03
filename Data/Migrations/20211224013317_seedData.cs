using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Formations",
                columns: new[] { "Id", "Description", "Nom", "NomSeo" },
                values: new object[,]
                {
                    { 1, "Grace à cette formation vous saurez créer votre site web en peu de temps", "Créer un site web avec ASP.Net Core", "asp-net-core" },
                    { 2, "Grace à cette formation vous saurez créer votre site web en peu de temps", "Créer un site web avec PHP", "php" },
                    { 3, "Apprenez à faire pousser des tomates, navets, courgettes et autre fruits et légumes savoureux toute l'année", "Devenez un pro du jardinage", "apro-jardinage" },
                    { 4, "un pro de la photo! vous saurez meme faire des selfies!", "Photo Pro", "photo-pro" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Formations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Formations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Formations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Formations",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
