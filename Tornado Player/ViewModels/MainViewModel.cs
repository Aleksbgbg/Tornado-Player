namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class MainViewModel : Conductor<IViewModelBase>.Collection.AllActive, IMainViewModel
    {
        private readonly IPlaylistCollectionViewModel _playlistCollectionViewModel;

        private readonly IPlaylistEditorViewModel _playlistEditorViewModel;

        public MainViewModel(IPlaybarViewModel playbarViewModel, IPlaylistCollectionViewModel playlistCollectionViewModel, IPlaylistEditorViewModel playlistEditorViewModel)
        {
            _playlistCollectionViewModel = playlistCollectionViewModel;
            _playlistEditorViewModel = playlistEditorViewModel;

            _mainContent = playlistCollectionViewModel;
            PlaybarViewModel = playbarViewModel;

            ActivateItem(playlistCollectionViewModel);
            ActivateItem(PlaybarViewModel);
        }

        private IViewModelBase _mainContent;
        public IViewModelBase MainContent
        {
            get => _mainContent;

            set
            {
                if (_mainContent == value) return;

                _mainContent = value;
                NotifyOfPropertyChange(() => MainContent);
            }
        }

        public IPlaybarViewModel PlaybarViewModel { get; }

        public void EditPlaylists()
        {
            if (MainContent == _playlistCollectionViewModel)
            {
                SwapMainContent(_playlistEditorViewModel);
            }
            else
            {
                SwapMainContent(_playlistCollectionViewModel, closeOld: true);
            }
        }

        private void SwapMainContent(IViewModelBase newContent, bool closeOld = default)
        {
            DeactivateItem(MainContent, closeOld);
            ActivateItem(newContent);

            MainContent = newContent;
        }
    }
}