namespace Tornado.Player.Services.Interfaces
{
    using System.Collections.Generic;

    using Tornado.Player.Models;

    public interface IStorageService
    {
        List<TrackFolder> TrackFolders { get; }

        void SaveData();
    }
}