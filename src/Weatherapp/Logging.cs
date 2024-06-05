using System;
using System.IO;

public static class Logging
{
    private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application.log");
    private static readonly object lockObj = new object();

    static Logging()
    {
        if (!File.Exists(logFile))
        {
            File.WriteAllText(logFile, $"Log started on {DateTime.Now}\n");
        }
    }

    public static void Log(string message)
    {
        lock (lockObj)
        {
            File.AppendAllText(logFile, $"{DateTime.Now}: {message}\n");
        }
    }

    public static void LogException(string message, Exception ex)
    {
        lock (lockObj)
        {
            File.AppendAllText(logFile, $"{DateTime.Now}: ERROR: {message} - Exception: {ex.Message}\nStack Trace: {ex.StackTrace}\n");
        }
    }
}
