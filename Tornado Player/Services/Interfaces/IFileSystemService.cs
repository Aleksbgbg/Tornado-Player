namespace Tornado.Player.Services.Interfaces
{
    using System.Collections.Generic;

    using Tornado.Player.Models.Player;

    internal interface IFileSystemService
    {
        bool IsValidTrack(string file);

        IEnumerable<string> LoadFiles(string directory);

        Track[] LoadTracks(string directory);
    }
}