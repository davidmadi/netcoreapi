using System.Text.Json;
using System.Text.Json.Serialization;
using Library.Logging;

public abstract class Proxy {

  protected T? HttpJsonCall<T>(object? request, string url) {

    try
    {
      HttpClient client = new HttpClient();
      var stringTask = client.GetStringAsync(url);
      stringTask.Wait();

      var options = new JsonSerializerOptions()
      {
          NumberHandling = JsonNumberHandling.AllowReadingFromString |
          JsonNumberHandling.WriteAsString
      };

      LogManager.EnqueueTrace(request, stringTask.Result, null);
      if (stringTask.Result != null) {
        return JsonSerializer.Deserialize<T>(stringTask.Result, options);
      }
      else {
        return default(T);
      }
    }
    catch(Exception e) {
      LogManager.EnqueueTrace(request, null, e.ToString());
      return default(T);
    }
  }

}