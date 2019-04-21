namespace Tornado.Player.ViewModels
{
    using System.Collections.ObjectModel;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    public class TrackFoldersViewModel : ViewModelBase, ITrackFoldersViewModel
    {
        private readonly IStorageService _storageService;

        private readonly IFileSystemBrowserService _fileSystemBrowserService;

        public TrackFoldersViewModel(IStorageService storageService, IFileSystemBrowserService fileSystemBrowserService)
        {
            _storageService = storageService;
            _fileSystemBrowserService = fileSystemBrowserService;

            Folders = new ObservableCollection<TrackFolder>(storageService.TrackFolders);
        }

        public ObservableCollection<TrackFolder> Folders { get; }

        public void AddNewFolder()
        {
            bool directorySelected = _fileSystemBrowserService.BrowseDirectory(out string directory);

            if (directorySelected)
            {
                TrackFolder newTrackFolder = new TrackFolder(directory);

                _storageService.TrackFolders.Add(newTrackFolder);
                Folders.Add(newTrackFolder);
            }
        }
    }
}