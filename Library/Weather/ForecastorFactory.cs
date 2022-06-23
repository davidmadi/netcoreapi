using Weather;
public static class ForecastorFactory {

  public static Forecaster Build(WeatherVersion version){
    switch(version){
      case WeatherVersion.v1:
        return new AccuWeatherForecaster();
      case WeatherVersion.v2:
        return new GoogleWeatherForecaster();
    }
    return new AccuWeatherForecaster();
  }

}