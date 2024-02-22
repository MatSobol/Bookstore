using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P05Shop.API.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(8,0)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title" },
                values: new object[,]
                {
                    { 1m, "Sydney Roberts", "maiores" },
                    { 2m, "Myrtice Beahan", "vel" },
                    { 3m, "Camden Dietrich", "sit" },
                    { 4m, "Tabitha Kunze", "et" },
                    { 5m, "Jailyn Block", "consectetur" },
                    { 6m, "Kennith Gaylord", "nulla" },
                    { 7m, "Tristin Cronin", "consequatur" },
                    { 8m, "Orval Abernathy", "accusantium" },
                    { 9m, "Louvenia Pfannerstill", "facere" },
                    { 10m, "Jovan Connelly", "eveniet" },
                    { 11m, "Royal Rosenbaum", "mollitia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
