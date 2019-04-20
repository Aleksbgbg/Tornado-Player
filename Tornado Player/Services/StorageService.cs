namespace Tornado.Player.Services
{
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;

    public class StorageService : IStorageService
    {
        public StorageService(IDataService dataService)
        {
            TrackFolders = dataService.Load(Constants.DataStoreNames.TrackFolders, () => new TrackFolder[0]);
        }

        public TrackFolder[] TrackFolders { get; }
    }
}