namespace Tornado.Player.ViewModels
{
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    public class TrackFoldersViewModel : Conductor<ITrackFolderViewModel>.Collection.AllActive, ITrackFoldersViewModel
    {
        private readonly IStorageService _storageService;

        private readonly IFileSystemBrowserService _fileSystemBrowserService;

        private readonly IViewModelFactory _viewModelFactory;

        public TrackFoldersViewModel(IStorageService storageService, IFileSystemBrowserService fileSystemBrowserService, IViewModelFactory viewModelFactory)
        {
            _storageService = storageService;
            _fileSystemBrowserService = fileSystemBrowserService;
            _viewModelFactory = viewModelFactory;

            Items.AddRange(storageService.TrackFolders.Select(trackFolder => viewModelFactory.MakeViewModel<ITrackFolderViewModel>(trackFolder)));
        }

        public void AddNewFolder()
        {
            bool directorySelected = _fileSystemBrowserService.BrowseDirectory(out string directory);

            if (directorySelected)
            {
                TrackFolder newTrackFolder = new TrackFolder(directory);

                _storageService.TrackFolders.Add(newTrackFolder);
                Items.Add(_viewModelFactory.MakeViewModel<ITrackFolderViewModel>(newTrackFolder));
            }
        }
    }
}