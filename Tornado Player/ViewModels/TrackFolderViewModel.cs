namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    public class TrackFolderViewModel : ViewModelBase, ITrackFolderViewModel
    {
        private readonly IStorageService _storageService;

        public TrackFolderViewModel(IStorageService storageService, TrackFolder trackFolder)
        {
            _storageService = storageService;
            TrackFolder = trackFolder;
        }

        public TrackFolder TrackFolder { get; }

        public void Remove()
        {
            _storageService.TrackFolders.Remove(TrackFolder);
            TryClose();
        }
    }
}