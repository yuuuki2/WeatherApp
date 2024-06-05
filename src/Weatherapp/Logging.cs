using System;
using System.IO;
using System.Threading;

public static class Logging
{
    private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application.log");
    private static readonly object lockObj = new object();

    static Logging()
    {
        // Initialisiere
        try
        {
            if (!File.Exists(logFile))
            {
                File.WriteAllText(logFile, $"Log started on {DateTime.Now}\n");
            }
        }
        catch (Exception ex)
        {
            HandleLoggingFailure($"Failed to initialize logging: {ex.Message}");
        }
    }

    public static void Log(string message)
    {
        try
        {
            lock (lockObj)
            {
                File.AppendAllText(logFile, $"{DateTime.Now}: {message}\n");
            }
        }
        catch (Exception ex)
        {
            HandleLoggingFailure($"Failed to log message: {message} - Exception: {ex.Message}");
        }
    }

    public static void LogException(string message, Exception ex)
    {
        Log($"ERROR: {message} - Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
    }

    private static void HandleLoggingFailure(string error)
    {
        // Backup log file path
        var backupLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application_backup.log");
        try
        {
            lock (lockObj)
            {
                File.AppendAllText(backupLogFile, $"{DateTime.Now}: {error}\n");
            }
        }
        catch
        {
            //sending email if
        }
    }
}
