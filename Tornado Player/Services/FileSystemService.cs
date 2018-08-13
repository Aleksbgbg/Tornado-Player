namespace Tornado.Player.Services
{
    using System.IO;
    using System.Linq;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;

    internal class FileSystemService : IFileSystemService
    {
        public Track[] LoadTracks(string directory)
        {
            return Directory.GetFiles(directory).Where(file => Constants.SupportedMediaFormats.Contains(Path.GetExtension(file))).Select(file => new Track(file)).ToArray();
        }
    }
}