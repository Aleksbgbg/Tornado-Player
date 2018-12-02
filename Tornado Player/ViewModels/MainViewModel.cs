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
            _playlistEditorViewModel.AttemptingDeactivation += PlaylistEditorViewModelAttemptingDeactivation;

            SwapMainContent(_playlistEditorViewModel);
        }

        private void PlaylistEditorViewModelAttemptingDeactivation(object sender, DeactivationEventArgs e)
        {
            MainContent.AttemptingDeactivation -= PlaylistEditorViewModelAttemptingDeactivation;

            SwapMainContent(_playlistCollectionViewModel);
        }

        private void SwapMainContent(IViewModelBase newContent)
        {
            DeactivateItem(MainContent, close: false);
            ActivateItem(newContent);

            MainContent = newContent;
        }
    }
}