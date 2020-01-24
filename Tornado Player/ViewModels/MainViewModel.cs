namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Editing;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal sealed class MainViewModel : Conductor<ITabViewModel>.Collection.OneActive, IMainViewModel
    {
        public MainViewModel(
                ILayoutService layoutService,
                IPlaybarViewModel playbarViewModel,
                IPlaylistCollectionViewModel playlistCollectionViewModel,
                ITrackFoldersViewModel trackFoldersViewModel,
                IPlaylistEditorViewModel playlistEditorViewModel,
                ISettingsViewModel settingsViewModel
        )
        {
            AppLayout = layoutService.AppLayout;
            PlaybarViewModel = playbarViewModel;

            Items.Add(playlistCollectionViewModel);
            Items.Add(trackFoldersViewModel);
            Items.Add(playlistEditorViewModel);
            Items.Add(settingsViewModel);

            SelectMainView();
        }

        public AppLayout AppLayout { get; }

        private ITabViewModel _selectedItem;
        public ITabViewModel SelectedItem
        {
            get => _selectedItem;

            set
            {
                if (_selectedItem == value) return;

                _selectedItem = value;
                NotifyOfPropertyChange(nameof(SelectedItem));
            }
        }

        public IPlaybarViewModel PlaybarViewModel { get; }

        private void SelectMainView()
        {
            SelectView(0);
        }

        private void SelectView(int index)
        {
            ITabViewModel view = Items[index];
            SwapMainContent(view);
        }

        private void SwapMainContent(ITabViewModel newContent, bool closeOld = default)
        {
            DeactivateItem(SelectedItem, closeOld);
            ActivateItem(newContent);

            SelectedItem = newContent;
        }
    }
}