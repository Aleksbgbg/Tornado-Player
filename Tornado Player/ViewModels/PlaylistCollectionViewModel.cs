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
        private readonly IEventAggregator _eventAggregator;

        public PlaylistCollectionViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IContentManagerService contentManagerService, ILayoutService layoutService)
        {
            _eventAggregator = eventAggregator;
            AppLayout = layoutService.AppLayout;

            Items.AddRange(contentManagerService.RetrievePlaylists()
                                                .Select(playlist => viewModelFactory.MakeViewModel<IPlaylistViewModel>(playlist)));

            ActivateItem(Items[0]);
        }

        public AppLayout AppLayout { get; }

        public IObservableCollection<IPlaylistViewModel> Playlists => Items;

        protected override void OnActivationProcessed(IPlaylistViewModel item, bool success)
        {
            if (success && item != null)
            {
                _eventAggregator.BeginPublishOnUIThread(item);
                item.Play();
            }
        }
    }
}