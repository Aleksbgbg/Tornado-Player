namespace Tornado.Player.ViewModels
{
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistCollectionViewModel : Conductor<IPlaylistViewModel>.Collection.OneActive, IPlaylistCollectionViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public PlaylistCollectionViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IContentManagerService contentManagerService)
        {
            _eventAggregator = eventAggregator;

            Items.AddRange(contentManagerService.RetrievePlaylists()
                                                .Select(playlist => viewModelFactory.MakeViewModel<IPlaylistViewModel>(playlist)));

            ScreenExtensions.TryActivate(this);
        }

        protected override void OnActivationProcessed(IPlaylistViewModel item, bool success)
        {
            if (success)
            {
                _eventAggregator.BeginPublishOnUIThread(item);
            }
        }

        protected override void OnViewAttached(object view, object context)
        {
            ActivateItem(Items[0]);
        }
    }
}