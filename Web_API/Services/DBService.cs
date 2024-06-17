using System;
using System.Data.Odbc;
using Web_API.models;

namespace Web_API.Services;

public class DBService
{
    private readonly IConfiguration _configuration;
    private readonly string ConnectionString;

    public DBService(IConfiguration configuration)
    {
        _configuration = configuration;
        ConnectionString = _configuration["ConnectionString"];
    }

    public List<WeatherForecast> GetCityForecast(string countryName , string cityName)
    {
        string query = @$"
            SELECT Forecast.Temp, Forecast.Time_Ahead, Forecast.Weather, Forecast.Weather_Pic, Astronomy.[Sunrise Time], Astronomy.[Sunset Time], Country.Name, City.Name
            FROM Country INNER JOIN((City INNER JOIN Forecast ON City.[ID] = Forecast.[CItyId]) INNER JOIN Astronomy ON City.[ID] = Astronomy.[CItyId]) ON Country.ID = City.CountryId
            WHERE(((City.Name) = ""{cityName}"") AND((Country.Name) = ""{countryName}""));
            ";
        
        List<WeatherForecast> response = new List<WeatherForecast>();

        OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
        builder.Driver = "Microsoft Access Driver (*.accdb)";

        // Call the Add method to explicitly add key/value
        // pairs to the internal collection.
        builder.Add("Dbq", "C:\\Workspace\\WebApiDB.accdb");
        builder.Add("Uid", "Admin");
        builder.Add("Pwd", "");
        
        using (OdbcConnection connection = new OdbcConnection(builder.ConnectionString))
        {
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var forecast = new WeatherForecast();

                        forecast.Name = reader.SafeGetString(8);
                        forecast.Temp = reader.SafeGetString(0);
                        forecast.Sunrise = reader.SafeGetString(5);
                        forecast.sunset = reader.SafeGetString(6);


                        response.Add(forecast);
                    }
                }
            }
        }
        return response;
    }
}

public static class SafeGetMethods
{
    public static string SafeGetString(this OdbcDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetString(colIndex);
        return string.Empty;
    }

    public static int SafeGetInt(this OdbcDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetInt32(colIndex);
        return 0;
    }

    public static double SafeGetDouble(this OdbcDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetDouble(colIndex);
        return 0;
    }

    public static DateTime SafeGetDate(this OdbcDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetDateTime(colIndex);
        return new DateTime();
    }

    public static bool SafeGetBool(this OdbcDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetBoolean(colIndex);
        return false;
    }
}