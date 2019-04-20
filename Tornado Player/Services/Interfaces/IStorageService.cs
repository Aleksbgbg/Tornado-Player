namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.Models;

    public interface IStorageService
    {
        TrackFolder[] TrackFolders { get; }
    }
}