namespace BankSimulator.Utils
{
    static class ApplicationLog
    {
        private static string _path { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BankSimulator");

        public static void RegisterLog(string message)
        {
            try
            {
                string logPath = Path.Combine(_path, "Logs.txt");

                using var writeLog = new StreamWriter(logPath, true);

                string messageWithDate = $"({DateTime.Now})\n" + message + "\n";

                writeLog.WriteLine(messageWithDate);

            }
            catch (IOException)
            {
                Console.WriteLine("It was not possible register the log");
            }
        }
    }
}