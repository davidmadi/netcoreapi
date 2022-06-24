using Weather;
public class AccuWeatherForecaster : Forecaster {
  
  private static readonly string[] Summaries = new[]
  {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };
  
  public IEnumerable<WeatherForecast> Get()
    {
        var all = new List<WeatherForecast>();
        foreach(int index in Enumerable.Range(1, 5)){
            var weather = new WeatherForecast();
            weather.Date = DateTime.Now.AddDays(index);
            weather.TemperatureC = Random.Shared.Next(-20, 55);
            weather.Summary = Summaries[Random.Shared.Next(Summaries.Length)];
            all.Add(weather);
        }
        return all.ToArray();
    }

}