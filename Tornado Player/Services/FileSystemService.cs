namespace Tornado.Player.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;

    internal class FileSystemService : IFileSystemService
    {
        private readonly ISnowflakeService _snowflakeService;

        public FileSystemService(ISnowflakeService snowflakeService)
        {
            _snowflakeService = snowflakeService;
        }

        public bool IsValidTrack(string file)
        {
            return Constants.SupportedMediaFormats.Contains(Path.GetExtension(file));
        }

        public IEnumerable<string> LoadFiles(string directory)
        {
            return Directory.GetFiles(directory)
                            .Where(IsValidTrack);
        }

        public Track[] LoadTracks(string directory)
        {
            return LoadFiles(directory).Select(file => new Track(_snowflakeService.GenerateSnowflake(), file))
                                       .ToArray();
        }
    }
}