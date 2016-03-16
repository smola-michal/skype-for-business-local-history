using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SkyForBusLH.Properties;

namespace SkyForBusLH
{
    public class Storage
    {
        static readonly string MyDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private const string ProgramFolder = "LyncIMHistory";
        private const string AllInOneFileName = @"AllLyncIMHistory.txt";

        public static string ConversationIdStorageFolder => Path.Combine(AppDataPath, ProgramFolder);

        public static string LogToAllInOneFile(string sender, string message, DateTime now)
        {
            return LogToFile(Path.Combine(MyDocsPath, ProgramFolder, AllInOneFileName), sender, message, now);
        }

        public static string LogToParticipiantsFiles(string sender, string participiant, string message, DateTime now)
        {
            string directory = Path.Combine(MyDocsPath, ProgramFolder);
            string filename = $"{directory}\\{participiant}.txt";
            return LogToFile(Path.Combine(directory, filename), sender, message, now);
        }

        private static string LogToFile(string path, string sender, string message, DateTime now)
        {
            string log = $"{sender.ToUpper()}  {now.ToString("HH:mm:ss  dd.MM.yyyy")}{Environment.NewLine}{message}";

            using (var outfile = new StreamWriter(path, true))
            {
                outfile.WriteLine(log);
                outfile.Close();
            }

            return log;
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void EnsureDirectoriesExist()
        {
            EnsureDirectoryExists(Path.Combine(MyDocsPath, ProgramFolder));
            EnsureDirectoryExists(Path.Combine(AppDataPath, ProgramFolder));
        }

        public static void OpenAllInOne()
        {
            string filePath = Path.Combine(MyDocsPath, ProgramFolder, AllInOneFileName);
            if (!File.Exists(filePath))
            {
                MessageBox.Show(Resources.HistoryFileDoesNotExist);
                return;
            }
            Process.Start(filePath);
        }

        public static void OpenStorageFolder()
        {
            Process.Start("explorer.exe", Path.Combine(MyDocsPath, ProgramFolder));
        }
    }
}
