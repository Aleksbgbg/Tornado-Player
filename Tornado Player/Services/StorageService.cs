namespace Tornado.Player.Services
{
    using System.Collections.Generic;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;

    public class StorageService : IStorageService
    {
        private readonly IDataService _dataService;

        public StorageService(IDataService dataService)
        {
            _dataService = dataService;

            TrackFolders = dataService.Load(Constants.DataStoreNames.TrackFolders, () => new List<TrackFolder>());
        }

        public List<TrackFolder> TrackFolders { get; }

        public void SaveData()
        {
            _dataService.Save(Constants.DataStoreNames.TrackFolders, TrackFolders);
        }
    }
}