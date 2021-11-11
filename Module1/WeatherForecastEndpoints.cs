using Microsoft.AspNetCore.Builder;

namespace Module1
{
    public static class WeatherForecastEndpoints
    {
        public static void MapWeatherForecastEndpoints(this WebApplication app)
        {
            app.MapGet("/weatherforecast", Getweatherforecast).WithName("GetWeatherForecast");
        }

        internal static IEnumerable<WeatherForecast> Getweatherforecast()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
               new WeatherForecast
               (
                   DateTime.Now.AddDays(index),
                   Random.Shared.Next(-20, 55),
                   summaries[Random.Shared.Next(summaries.Length)]
               )).ToArray();
            return forecast;
        }

        internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
        {
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}