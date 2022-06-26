namespace Library.Logging;

internal static class LogManager {

  private static Queue<TraceRecord> traceQueue = new Queue<TraceRecord>();
  private static Queue<LogRecord> logQueue = new Queue<LogRecord>();
  private static System.Timers.Timer traceTimer;
  private static System.Timers.Timer logTimer;

  static LogManager() {
    traceTimer = new System.Timers.Timer(5000);
    traceTimer.Elapsed += OnTraceEvent;
    traceTimer.AutoReset = true;
    traceTimer.Enabled = true;

    logTimer = new System.Timers.Timer(2500);
    logTimer.Elapsed += OnLogEvent;
    logTimer.AutoReset = true;
    logTimer.Enabled = true;
  }

  private static void OnTraceEvent(Object source, System.Timers.ElapsedEventArgs e)
  {
    int count = 0;
    TraceRecord? record;
    if (count < 10 && traceQueue.TryDequeue(out record)){
      //Insert record in DB
      //Deserialize
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Green;
      if(record.Request != null){
        Console.Write(record.Created.ToString() + "|" + record.Request + "|");
      }
      if(record.Response != null){
        Console.Write(record.Created.ToString() + "|" + record.Response + "|");
      }
      if(record.Error != null){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(record.Created.ToString() + "|" + record.Error + "|");
      }
      count++;
    }
  }

  private static void OnLogEvent(Object source, System.Timers.ElapsedEventArgs e)
  {
    int count = 0;
    LogRecord? record;
    if (count < 10 && logQueue.TryDequeue(out record)){
      //Insert record in DB
      //Deserialize
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Green;
      if(record.LogType != null){
        Console.Write(record.Created.ToString() + "|" + record.LogType + "|");
      }
      if(record.Message != null){
        Console.Write(record.Created.ToString() + "|" + record.Message + "|");
      }
      if(record.Context != null){
        Console.Write(record.Created.ToString() + "|" + record.Context + "|");
      }
      count++;
    }
  }  

  public static void EnqueueTrace(object? request, object? response, string? error) {
    //Table
    //Id PK
    //Request varchar
    //Response varchar
    //Error varchar
    //Created datetime
    //TODO: 
    traceQueue.Enqueue(new TraceRecord(){
      Request = request,
      Response = response,
      Error = error,
      Created = DateTime.Now
    });
  }

  public static void EnqueueException(Exception e) {
    EnqueueLog("Exception", e.Message, e.StackTrace?.ToString());
  }

  public static void EnqueueLog(string? logType, object? message, string? context) {
    //Table
    //Id PK
    //Request varchar
    //Response varchar
    //Error varchar
    //Created datetime
    //TODO: 
    logQueue.Enqueue(new LogRecord(){
      LogType = logType,
      Message = message,
      Context = context,
      Created = DateTime.Now
    });
  }  
}

internal class TraceRecord {

  public object? Request { get; set; } 
  public object? Response { get; set; }
  public object? Error { get; set; }
  public DateTime Created { get; set; }

}

internal class LogRecord {

  public string? LogType { get;set;}
  public object? Message { get; set; } 
  public object? Context { get; set; }
  public DateTime Created { get; set; }

}