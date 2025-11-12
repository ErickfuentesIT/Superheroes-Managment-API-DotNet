using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Superheroes_Managment.Migrations
{
    /// <inheritdoc />
    public partial class SuperheroesDbv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeroId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Powers_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Heroes",
                columns: new[] { "Id", "Alias", "Name" },
                values: new object[,]
                {
                    { 1, "The Dark Knight", "Batman" },
                    { 2, "Friendly Neighbor", "Spider-Man" },
                    { 3, "The Fastest Man", "Flash" },
                    { 4, null, "Green Lantern" },
                    { 5, "God of Thunder", "Thor" },
                    { 6, "The First Avenger", "Captain America" }
                });

            migrationBuilder.InsertData(
                table: "Powers",
                columns: new[] { "Id", "Description", "HeroId", "Name" },
                values: new object[,]
                {
                    { 1, "Exceptional deduction", 1, "Detective Skills" },
                    { 2, "Hand-to-hand combat", 1, "Martial Arts" },
                    { 3, "Alerts to danger", 2, "Spider Sense" },
                    { 4, "Spins spider webs", 2, "Web-Shooting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Powers_HeroId",
                table: "Powers",
                column: "HeroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropTable(
                name: "Heroes");
        }
    }
}
