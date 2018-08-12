namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistViewModel : ViewModelBase, IPlaylistViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        public PlaylistViewModel(IMusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;

            Tracks = new BindableCollection<Track>(_musicPlayerService.Tracks);

            _musicPlayerService.PlaylistLoaded += (sender, e) =>
            {
                Tracks.Clear();
                Tracks.AddRange(e.Tracks);
            };

            _musicPlayerService.TrackChanged += (sender, e) => SelectedIndex = e.TrackIndex;
        }

        public IObservableCollection<Track> Tracks { get; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;

            set
            {
                if (_selectedIndex == value) return;

                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);

                _musicPlayerService.SelectTrack(_selectedIndex);
            }
        }
    }
}