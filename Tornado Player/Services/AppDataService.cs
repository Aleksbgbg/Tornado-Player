namespace Tornado.Player.Services
{
    using System;
    using System.IO;

    using Tornado.Player.Services.Interfaces;

    internal class AppDataService : IAppDataService
    {
        private static readonly string ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tornado Player");

        public AppDataService()
        {
            if (!Directory.Exists(ApplicationPath))
            {
                Directory.CreateDirectory(ApplicationPath);
            }
        }

        public string GetFolder(string name)
        {
            string directoryPath = Path.Combine(ApplicationPath, name);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }

        public string GetFile(string name, string defaultContents = "")
        {
            return GetFile(name, () => defaultContents);
        }

        public string GetFile(string name, Func<string> defaultContents)
        {
            string filePath = Path.Combine(ApplicationPath, name);

            if (!File.Exists(filePath))
            {
                string directory = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(filePath, defaultContents());
            }

            return filePath;
        }
    }
}