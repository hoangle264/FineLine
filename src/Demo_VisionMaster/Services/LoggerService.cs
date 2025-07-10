using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Demo_VisionMaster.Enums.LogEnum;

namespace Demo_VisionMaster.Services
{
    public class LoggerService
    {
        private static readonly string logDir = "Logs";

        public static void Info(string note, string errorCode = "N/A") =>
            WriteLog(LogType.Info, note, errorCode);

        public static void Warning(string note, string errorCode = "N/A") =>
            WriteLog(LogType.Warning, note, errorCode);

        public static void Error(string note, string errorCode = "ERR000") =>
            WriteLog(LogType.Error, note, errorCode, LogLevel.High, LogStatus.Failure, LogPriority.Critical);

        private static void WriteLog(
            LogType type,
            string note,
            string errorCode,
            LogLevel level = LogLevel.Medium,
            LogStatus status = LogStatus.Success,
            LogPriority priority = LogPriority.Normal)
        {
            var entry = new LogEntry
            {
                Type = type,
                Note = note,
                ErrorCode = errorCode,
                Level = level,
                Status = status,
                Priority = priority
            };

            SaveToCsv(entry);
        }

        private static void SaveToCsv(LogEntry entry)
        {
            Directory.CreateDirectory(logDir);
            string filePath = Path.Combine(logDir, $"log_{entry.Timestamp:yyyy-MM-dd}.csv");
            bool fileExists = File.Exists(filePath);
            int stt = fileExists ? File.ReadLines(filePath).Count() : 1;

            using (var writer = new StreamWriter(filePath, true, new UTF8Encoding(true)))
            {
                if (!fileExists)
                    writer.WriteLine("STT,Timestamp,Type,Level,Source,Action,Status,Category,Priority,ErrorCode,Note");

                writer.WriteLine(string.Join(",",
                    stt,
                    entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    entry.Type,
                    entry.Level,
                    entry.Source,
                    entry.Action,
                    entry.Status,
                    entry.Category,
                    entry.Priority,
                    entry.ErrorCode,
                    $"\"{entry.Note.Replace("\"", "\"\"")}\""
                ));
            }
        }
    }
    public class LogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Note { get; set; }
        public string ErrorCode { get; set; } = "N/A";
        public LogType Type { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Medium;
        public LogSource Source { get; set; } = LogSource.Application;
        public LogDestination Destination { get; set; } = LogDestination.Database;
        public LogAction Action { get; set; } = LogAction.Read;
        public LogStatus Status { get; set; } = LogStatus.Success;
        public LogCategory Category { get; set; } = LogCategory.Application;
        public LogPriority Priority { get; set; } = LogPriority.Normal;
    }
    public class LogCleanupService
    {
        private readonly string logDirectory = "Logger";
        private readonly int retentionDays = 3;

        public void CleanOldLogs()
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                    return;

                var files = Directory.GetFiles(logDirectory, "log_*.csv");

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < DateTime.Now.AddDays(-retentionDays))
                    {
                        fileInfo.Delete();
                        Console.WriteLine($"Đã xóa log cũ: {fileInfo.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa log cũ: " + ex.Message);
            }
        }
    }

}
