namespace Weather;
public interface Forecaster {
  public IEnumerable<WeatherForecast> Get();

}