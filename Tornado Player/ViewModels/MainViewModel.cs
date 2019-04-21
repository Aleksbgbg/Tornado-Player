namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Editing;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal sealed class MainViewModel : Conductor<IViewModelBase>.Collection.AllActive, IMainViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;

        public MainViewModel(
                IViewModelFactory viewModelFactory,
                ILayoutService layoutService,
                IPlaybarViewModel playbarViewModel,
                IPlaylistCollectionViewModel playlistCollectionViewModel,
                ISettingsViewModel settingsViewModel,
                ITrackFoldersViewModel trackFoldersViewModel
        )
        {
            _viewModelFactory = viewModelFactory;

            AppLayout = layoutService.AppLayout;
            PlaybarViewModel = playbarViewModel;

            Items.Add(playlistCollectionViewModel);
            Items.Add(null); // At index = 1, select IPlaylistEditorViewModel which is lazily instantiated
            Items.Add(settingsViewModel);
            Items.Add(trackFoldersViewModel);

            SelectView(0);

            ActivateItem(PlaybarViewModel);
        }

        public AppLayout AppLayout { get; }

        private int _mainContentIndex;
        public int MainContentIndex
        {
            get => _mainContentIndex;

            set
            {
                if (_mainContentIndex == value) return;

                _mainContentIndex = value;
                NotifyOfPropertyChange(nameof(MainContentIndex));
            }
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

        public void SelectView(int index)
        {
            IViewModelBase view;

            // Delayed construction to prevent
            // unnecessary cascade of instantiations
            if (index == 1)
            {
                // index = 1 is null
                view = _viewModelFactory.MakeViewModel<IPlaylistEditorViewModel>();
            }
            else
            {
                view = Items[index];
            }

            SwapMainContent(view);
            MainContentIndex = index;
        }

        private void SwapMainContent(IViewModelBase newContent, bool closeOld = default)
        {
            DeactivateItem(MainContent, closeOld);
            ActivateItem(newContent);

            MainContent = newContent;
        }
    }
}