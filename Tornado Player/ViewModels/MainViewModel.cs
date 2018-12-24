namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Editing;

    internal sealed class MainViewModel : Conductor<IViewModelBase>.Collection.AllActive, IMainViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;

        private readonly IPlaylistCollectionViewModel _playlistCollectionViewModel;

        public MainViewModel(IViewModelFactory viewModelFactory, IPlaybarViewModel playbarViewModel, IPlaylistCollectionViewModel playlistCollectionViewModel)
        {
            _viewModelFactory = viewModelFactory;
            _playlistCollectionViewModel = playlistCollectionViewModel;

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
                NotifyOfPropertyChange(nameof(MainContent));
            }
        }

        public IPlaybarViewModel PlaybarViewModel { get; }

        public void EditPlaylists()
        {
            if (MainContent == _playlistCollectionViewModel)
            {
                SwapMainContent(_viewModelFactory.MakeViewModel<IPlaylistEditorViewModel>());
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