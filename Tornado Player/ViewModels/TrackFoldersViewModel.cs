namespace Tornado.Player.ViewModels
{
    using System.Collections.ObjectModel;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    public class TrackFoldersViewModel : ViewModelBase, ITrackFoldersViewModel
    {
        public TrackFoldersViewModel(IStorageService storageService)
        {
            Folders = new ObservableCollection<TrackFolder>(storageService.TrackFolders);
        }

        public ObservableCollection<TrackFolder> Folders { get; }
    }
}