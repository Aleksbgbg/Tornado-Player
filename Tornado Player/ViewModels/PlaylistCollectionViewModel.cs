namespace Tornado.Player.ViewModels
{
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistCollectionViewModel : Conductor<IPlaylistViewModel>.Collection.OneActive, IPlaylistCollectionViewModel
    {
        private const string ActivePlaylistDataName = "ActivePlaylist";

        private readonly IEventAggregator _eventAggregator;

        private readonly IDataService _dataService;

        public PlaylistCollectionViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IContentManagerService contentManagerService, IDataService dataService, ILayoutService layoutService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            AppLayout = layoutService.AppLayout;

            Items.AddRange(contentManagerService.RetrievePlaylists()
                                                .Select(playlist => viewModelFactory.MakeViewModel<IPlaylistViewModel>(playlist)));

            int activePlaylist = dataService.Load<int>(ActivePlaylistDataName);
            ActivateItem(Items[activePlaylist]);
        }

        public AppLayout AppLayout { get; }

        public IObservableCollection<IPlaylistViewModel> Playlists => Items;

        protected override void OnActivationProcessed(IPlaylistViewModel item, bool success)
        {
            if (success && item != null)
            {
                _eventAggregator.BeginPublishOnUIThread(item);
                item.Play();
                _dataService.Save(ActivePlaylistDataName, Items.IndexOf(item));
            }
        }
    }
}