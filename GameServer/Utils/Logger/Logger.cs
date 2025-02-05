using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils.Logger
{
    public class Logger
    {
        private static StreamWriter _sw;
        private static LogLevel _minLevel;
        private static string _path;

        private static DateTime _lastDate;

        public static void Initialize(LogLevel minLevel, string path)
        {
            _minLevel = minLevel;
            _path = path;

            DirectoryUtils.EnsureDirectoryExists(path);
            CreateFile();
        }

        public static void CreateFile()
        {
            _lastDate = DateTime.UtcNow.Date;

            string filePath = Path.Combine(_path, $"log_{DateTime.UtcNow.Date:yyyyMMdd}.txt");

            if (_sw != null)
            {
                _sw.Close();
            }
            _sw = new StreamWriter(filePath, false);
            _sw.AutoFlush = true;
        }

        public static void Log(LogLevel level, string message)
        {
            if (level < _minLevel)
            {
                return;
            }

            string log = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
        
            if(_lastDate != DateTime.UtcNow.Date)
            {
                CreateFile();
            }

            _sw.WriteLine(log);
            Console.WriteLine(log);
        }

        public static void Debug(string message) => Log(LogLevel.Debug, message);
        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warn(string message) => Log(LogLevel.Warn, message);
        public static void Error(string message) => Log(LogLevel.Error, message);
    }
}
