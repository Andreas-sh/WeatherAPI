using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_API.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AstronomyId",
                table: "WeatherItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherItems_AstronomyId",
                table: "WeatherItems",
                column: "AstronomyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherItems_AstronomyItems_AstronomyId",
                table: "WeatherItems",
                column: "AstronomyId",
                principalTable: "AstronomyItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherItems_AstronomyItems_AstronomyId",
                table: "WeatherItems");

            migrationBuilder.DropIndex(
                name: "IX_WeatherItems_AstronomyId",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "AstronomyId",
                table: "WeatherItems");
        }
    }
}
