namespace Tornado.Player.Services
{
    using System;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class MusicPlayerService : IMusicPlayerService
    {
        private readonly TornadoPlayer _tornadoPlayer = new TornadoPlayer();

        private readonly Win32HotKey _togglePlaybackHotKey = new Win32HotKey(VirtualKey.F6, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        public MusicPlayerService(IFileSystemService fileSystemService)
        {
            _tornadoPlayer.Load(fileSystemService.LoadTracks("E:\\MP3s"));
            _togglePlaybackHotKey.Actuated += (sender, e) => _tornadoPlayer.TogglePlay();
        }

        public event EventHandler<TrackChangedEventArgs> TrackChanged
        {
            add => _tornadoPlayer.TrackChanged += value;

            remove => _tornadoPlayer.TrackChanged -= value;
        }

        public event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded
        {
            add => _tornadoPlayer.PlaylistLoaded += value;

            remove => _tornadoPlayer.PlaylistLoaded -= value;
        }

        public Track[] Tracks => _tornadoPlayer.Tracks;

        public void SelectTrack(int index)
        {
            _tornadoPlayer.Switch(index);
        }
    }
}