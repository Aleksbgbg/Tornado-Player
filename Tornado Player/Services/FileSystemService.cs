namespace Tornado.Player.Services
{
    using System.IO;
    using System.Linq;

    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;

    internal class FileSystemService : IFileSystemService
    {
        private readonly ISnowflakeService _snowflakeService;

        public FileSystemService(ISnowflakeService snowflakeService)
        {
            _snowflakeService = snowflakeService;
        }

        public Track[] LoadTracks(string directory)
        {
            return Directory.GetFiles(directory)
                            .Where(file => Constants.SupportedMediaFormats.Contains(Path.GetExtension(file)))
                            .Select(file => new Track(_snowflakeService.GenerateSnowflake(), file))
                            .ToArray();
        }
    }
}