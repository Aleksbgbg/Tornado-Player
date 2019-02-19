﻿namespace Tornado.Player.ViewModels.Playlist
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal abstract class PlaylistViewModel : Conductor<ITrackViewModel>.Collection.OneActive, IPlaylistViewModel, IHandle<Shortcut>
    {
        private readonly IViewModelFactory _viewModelFactory;

        private readonly IContentManagerService _contentManagerService;

        private readonly IMusicPlayerService _musicPlayerService;

        private readonly ICollectionView _tracksView;

        private protected PlaylistViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IContentManagerService contentManagerService, IMusicPlayerService musicPlayerService, Playlist playlist)
        {
            _viewModelFactory = viewModelFactory;
            _contentManagerService = contentManagerService;
            _musicPlayerService = musicPlayerService;
            _tracksView = CollectionViewSource.GetDefaultView(Items);

            DisplayName = playlist.Name;
            Playlist = playlist;
            SelectedIndex = Playlist.SelectedTrackIndex;

            _tracksView.SortDescriptions.Add(new SortDescription(nameof(ITrackViewModel.PlaylistTrack), ListSortDirection.Ascending));

            eventAggregator.Subscribe(this);

            Playlist.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Playlist.IsShuffled))
                {
                    _tracksView.Refresh();
                }
            };
        }

        public sealed override string DisplayName
        {
            get => base.DisplayName;

            set => base.DisplayName = value;
        }

        public Playlist Playlist { get; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;

            set
            {
                if (_selectedIndex == value) return;

                _selectedIndex = value;
                NotifyOfPropertyChange(nameof(SelectedIndex));
            }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get => _isSearching;

            set
            {
                if (_isSearching == value) return;

                _isSearching = value;
                NotifyOfPropertyChange(nameof(IsSearching));

                if (!_isSearching)
                {
                    _tracksView.Filter = null;
                }
            }
        }

        public IEnumerable<ITrackViewModel> Tracks => Items;

        public void Add(IEnumerable<Track> tracks)
        {
            foreach (Track track in tracks)
            {
                PlaylistTrack newTrack = _contentManagerService.AddTrackToPlaylist(Playlist, track);
                Items.Add(_viewModelFactory.MakeViewModel<ITrackViewModel>(newTrack));
            }
        }

        public void Remove(IEnumerable<Track> tracks)
        {
            foreach (Track track in tracks)
            {
                PlaylistTrack oldTrack = _contentManagerService.RemoveTrackFromPlaylist(Playlist, track);
                Items.Remove(Items.Single(item => item.PlaylistTrack.Track == oldTrack.Track));
            }
        }

        public void Play()
        {
            OnPlayed();

            if (Items.Count == 0 || Playlist.SelectedTrackIndex < 0)
            {
                return;
            }

            if (ActiveItem == null)
            {
                SelectTrack(Playlist.SelectedTrackIndex);
                return;
            }

            PlayTrack(ActiveItem);
        }

        public void SelectPrevious()
        {
            SelectTrack(Playlist.SelectedTrackIndex - 1);
        }

        public void SelectNext()
        {
            SelectTrack(Playlist.SelectedTrackIndex + 1);
        }

        public void Handle(Shortcut message)
        {
            if (message == Shortcut.Search)
            {
                IsSearching = !IsSearching;
            }
        }

        public void Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                _tracksView.Filter = null;
                return;
            }

            _tracksView.Filter = item => ((ITrackViewModel)item).PlaylistTrack.Track.MatchesSearch(searchText);
        }

        protected override void OnActivationProcessed(ITrackViewModel item, bool success)
        {
            if (success && item != null)
            {
                PlayTrack(item);
                Playlist.SelectedTrackIndex = SelectedIndex;
            }
        }

        private protected virtual void OnPlayed()
        {
        }

        private void SelectTrack(int index)
        {
            if (index < 0)
            {
                index = Items.Count - ((-index) % Items.Count);
            }
            else if (index >= Items.Count)
            {
                index %= Items.Count;
            }

            SelectedIndex = index;
        }

        private void PlayTrack(ITrackViewModel trackViewModel)
        {
            trackViewModel.Play();

            if (Playlist.SelectedTrackIndex == SelectedIndex) // Resumed
            {
                // Cannot use callback because play command will override TrackProgress to 0
                _musicPlayerService.Progress = Playlist.TrackProgress;
            }
        }
    }
}