namespace Tornado.Player.ViewModels.Editing
{
    using System.Linq;
    using System.Windows;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities.EventAggregator;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;
    using Tornado.Player.ViewModels.Interfaces.Editing;

    internal class PlaylistEditorViewModel : Conductor<IEditPlaylistViewModel>.Collection.OneActive, IPlaylistEditorViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly IViewModelFactory _viewModelFactory;

        private readonly IContentManagerService _contentManagerService;

        private readonly IDialogService _dialogService;

        public PlaylistEditorViewModel(IEventAggregator eventAggregator, IViewModelFactory viewModelFactory, IContentManagerService contentManagerService, IDialogService dialogService, IPlaylistCollectionViewModel playlistCollectionViewModel)
        {
            _eventAggregator = eventAggregator;
            _viewModelFactory = viewModelFactory;
            _contentManagerService = contentManagerService;
            _dialogService = dialogService;

            Items.AddRange(playlistCollectionViewModel.Playlists.Select(playlist => viewModelFactory.MakeViewModel<IEditPlaylistViewModel>(playlist)));
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;

            set
            {
                if (_selectedIndex == value) return;

                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
                NotifyOfPropertyChange(() => CanDeletePlaylist);
            }
        }

        public bool CanDeletePlaylist => SelectedIndex != -1;

        public void DeletePlaylist()
        {
            Confirmation confirmation = new Confirmation
            {
                Message = $"delete playlist '{ActiveItem.Playlist.Name}'"
            };

            _dialogService.ShowDialog<Confirmation, IConfirmationDialogViewModel>(confirmation);

            if (!confirmation.Confirmed)
            {
                return;
            }

            _contentManagerService.DeletePlaylist(ActiveItem.Playlist);

            _eventAggregator.BeginPublishOnUIThread(new PlaylistDeletionMessage(ActiveItem.PlaylistViewModel));

            Items.Remove(ActiveItem);
        }

        public void CreateNewPlaylist()
        {
            PlaylistCreation playlistCreation = _dialogService.ShowDialog<PlaylistCreation, ICreatePlaylistDialogViewModel>
            (
                new WindowSettings
                {
                    ResizeMode = ResizeMode.NoResize,
                    ShowInTaskbar = false
                }
            );

            if (!playlistCreation.Create)
            {
                return;
            }

            Playlist playlist = _contentManagerService.AddPlaylist(playlistCreation.Name);

            IPlaylistViewModel playlistViewModel = _viewModelFactory.MakeViewModel<IPlaylistViewModel>(playlist);

            IEditPlaylistViewModel editPlaylistViewModel = _viewModelFactory.MakeViewModel<IEditPlaylistViewModel>(playlistViewModel);

            Items.Add(editPlaylistViewModel);

            ActivateItem(editPlaylistViewModel);

            _eventAggregator.BeginPublishOnUIThread(new PlaylistCreationMessage(playlistViewModel));
        }
    }
}