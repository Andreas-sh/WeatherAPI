using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_API.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sunriseTime",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "sunsetTime",
                table: "WeatherItems");

            migrationBuilder.CreateTable(
                name: "AstronomyItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    sunriseTime = table.Column<string>(type: "TEXT", nullable: true),
                    sunsetTime = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronomyItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AstronomyItems");

            migrationBuilder.AddColumn<string>(
                name: "sunriseTime",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sunsetTime",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);
        }
    }
}
