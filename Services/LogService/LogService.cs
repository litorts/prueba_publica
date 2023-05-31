using System;
using System.IO;

namespace crecer_backend.Services.LogService
{
    public class LogService
    {
        private static string createHeader(string proyect, string fileName, string function, string type, string add = "")
        {
            string dateFormat = "HH:mm:ss.fff";
            string ret = $"[{DateTime.Now.ToString(dateFormat)}][{type}][{proyect}][{fileName}][{function}]";
            if (add != "") ret += $"[{add}]";
            return ret;
        }
        private static string getFolder()
        {
            CreateFolder();
            string dateString;
            dateString = Directory.GetCurrentDirectory() + @"\Logs\log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            return dateString;
        }
        public static void logInfo(string proyect, string fileName, string function, string message, string add = "")
        {
            using FileStream fileStream = File.Open(getFolder(), FileMode.Append); using StreamWriter sw = new StreamWriter(fileStream);
            {
                sw.WriteLine(createHeader(proyect, fileName, function, "Info", add) + $" {message}");
            }
        }
        public static void logWarning(string proyect, string fileName, string function, string message, string add = "")
        {
            using FileStream fileStream = File.Open(getFolder(), FileMode.Append); using StreamWriter sw = new StreamWriter(fileStream);
            {
                sw.WriteLine(createHeader(proyect, fileName, function, "Warning", add) + $" {message}");
            }
        }
        public static void logError(string proyect, string fileName, string function, string message, string add = "")
        {
            using FileStream fileStream = File.Open(getFolder(), FileMode.Append); using StreamWriter sw = new StreamWriter(fileStream);
            {
                sw.WriteLine(createHeader(proyect, fileName, function, "Error", add) + $" {message}");
            }
        }
        public static void logError(string proyect, string fileName, string function, Exception ex, string add = "")
        {
            using FileStream fileStream = File.Open(getFolder(), FileMode.Append); using StreamWriter sw = new StreamWriter(fileStream);
            {
                sw.WriteLine(createHeader(proyect, fileName, function, "Error", add) + $" {ex.Message}");
            }
        }
        private static void CreateFolder()
        {
            string path = Directory.GetCurrentDirectory() + @"\Logs";
            System.IO.Directory.CreateDirectory(path);

        }
    }
}
