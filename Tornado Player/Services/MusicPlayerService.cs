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

        private readonly Win32HotKey _skipBackwardHotKey = new Win32HotKey(VirtualKey.F5, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);
        private readonly Win32HotKey _togglePlaybackHotKey = new Win32HotKey(VirtualKey.F6, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);
        private readonly Win32HotKey _skipForwardHotKey = new Win32HotKey(VirtualKey.F7, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        public MusicPlayerService(IFileSystemService fileSystemService)
        {
            _skipBackwardHotKey.Actuated += (sender, e) => Previous();
            _togglePlaybackHotKey.Actuated += (sender, e) => TogglePlayback();
            _skipForwardHotKey.Actuated += (sender, e) => Next();

            _tornadoPlayer.MediaOpened += (sender, e) => TrackChanged?.Invoke(this, new TrackChangedEventArgs(TrackIndex, Duration));
            _tornadoPlayer.MediaEnded += (sender, e) =>
            {
                if (Loop)
                {
                    _tornadoPlayer.Stop();
                    _tornadoPlayer.Play();
                }
                else
                {
                    Next();
                }
            };

            Tracks = fileSystemService.LoadTracks("E:\\MP3s");
            SelectTrack(0);
        }

        public event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated
        {
            add => _tornadoPlayer.ProgressUpdated += value;

            remove => _tornadoPlayer.ProgressUpdated -= value;
        }

        public event EventHandler Paused
        {
            add => _tornadoPlayer.Paused += value;

            remove => _tornadoPlayer.Paused -= value;
        }

        public event EventHandler Played
        {
            add => _tornadoPlayer.Played += value;

            remove => _tornadoPlayer.Played -= value;
        }

        public event EventHandler<TrackChangedEventArgs> TrackChanged;

        public event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded;

        private Track[] _tracks;
        public Track[] Tracks
        {
            get => _tracks;

            private set
            {
                _tracks = value;
                OnPlaylistLoaded();
            }
        }

        private int _trackIndex = -1;
        public int TrackIndex
        {
            get => _trackIndex;

            private set
            {
                if (value < 0)
                {
                    _trackIndex = Tracks.Length - ((-value) % Tracks.Length);
                }
                else if (value >= Tracks.Length)
                {
                    _trackIndex = value % Tracks.Length;
                }
                else
                {
                    _trackIndex = value;
                }
            }
        }

        public bool IsPlaying => _tornadoPlayer.IsPlaying;

        public TimeSpan Progress
        {
            get => _tornadoPlayer.Progress;

            set => _tornadoPlayer.Progress = value;
        }

        public TimeSpan Duration => _tornadoPlayer.Duration;

        public double Volume
        {
            get => _tornadoPlayer.Volume;

            set => _tornadoPlayer.Volume = value;
        }

        public bool Loop
        {
            get => _tornadoPlayer.Loop;

            set => _tornadoPlayer.Loop = value;
        }

        public void Previous()
        {
            SelectTrack(TrackIndex - 1);
        }

        public void Next()
        {
            SelectTrack(TrackIndex + 1);
        }

        public void Play()
        {
            _tornadoPlayer.Play();
        }

        public void Pause()
        {
            _tornadoPlayer.Pause();
        }

        public void TogglePlayback()
        {
            _tornadoPlayer.TogglePlay();
        }

        public void SelectTrack(int index)
        {
            if (TrackIndex == index) return;

            TrackIndex = index;

            _tornadoPlayer.Open(Tracks[TrackIndex]);
        }

        public void Shuffle()
        {
            Random random = new Random();

            for (int index = 0; index < Tracks.Length; index++)
            {
                int swapIndex = index + random.Next(Tracks.Length - index);

                Track track = Tracks[swapIndex];
                Tracks[swapIndex] = Tracks[index];
                Tracks[index] = track;
            }

            OnPlaylistLoaded();
        }

        public void Sort()
        {
            Array.Sort(Tracks);
            OnPlaylistLoaded();
        }

        private void OnPlaylistLoaded()
        {
            PlaylistLoaded?.Invoke(this, new PlaylistLoadedEventArgs(Tracks));
        }
    }
}