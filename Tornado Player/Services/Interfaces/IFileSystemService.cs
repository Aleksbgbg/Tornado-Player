namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;

    internal interface IFileSystemService
    {
        Track[] LoadTracks(string directory);
    }
}