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
            return Directory.GetFiles(directory).Select(file => new Track(file)).ToArray();
        }
    }
}