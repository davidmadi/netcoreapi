using Weather;
public class GoogleWeatherForecaster : Forecaster {
  
  private static readonly string[] Summaries = new[]
  {
      "GoogleFreezing", "GoogleBracing", "GoogleChilly", "GoogleCool", "GoogleMild", "GoogleWarm", "GoogleBalmy", "GoogleHot", "GoogleSweltering", "GoogleScorching"
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