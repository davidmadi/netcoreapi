using Weather;
public static class ForecastorFactory {

  public static Forecaster Build(){
    return new AccuWeatherForecaster();
  }

}