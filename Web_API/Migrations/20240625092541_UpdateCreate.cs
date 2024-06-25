using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "TodoItems");

            migrationBuilder.RenameTable(
                name: "TodoItems",
                newName: "WeatherItems");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WeatherItems",
                newName: "time");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "forecast",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "forecastIcon",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "temp",
                table: "WeatherItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherItems",
                table: "WeatherItems",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherItems",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "forecast",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "forecastIcon",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "sunriseTime",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "sunsetTime",
                table: "WeatherItems");

            migrationBuilder.DropColumn(
                name: "temp",
                table: "WeatherItems");

            migrationBuilder.RenameTable(
                name: "WeatherItems",
                newName: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "TodoItems",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "TodoItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems",
                column: "Id");
        }
    }
}
