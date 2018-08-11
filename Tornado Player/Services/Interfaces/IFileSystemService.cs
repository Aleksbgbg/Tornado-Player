namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.Models;

    internal interface IFileSystemService
    {
        Track[] LoadTracks(string directory);
    }
}