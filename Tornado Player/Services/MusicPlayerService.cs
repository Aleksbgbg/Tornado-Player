namespace Tornado.Player.Services
{
    using System;

    using Newtonsoft.Json;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class MusicPlayerService : IMusicPlayerService
    {
        private readonly IDataService _dataService;

        private readonly TornadoPlayer _tornadoPlayer = new TornadoPlayer();

        private readonly Win32HotKey _skipBackwardHotKey = new Win32HotKey(VirtualKey.F5, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);
        private readonly Win32HotKey _togglePlaybackHotKey = new Win32HotKey(VirtualKey.F6, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);
        private readonly Win32HotKey _skipForwardHotKey = new Win32HotKey(VirtualKey.F7, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        public MusicPlayerService(IDataService dataService, IFileSystemService fileSystemService)
        {
            _dataService = dataService;

            _skipBackwardHotKey.Actuated += (sender, e) => Previous();
            _togglePlaybackHotKey.Actuated += (sender, e) => TogglePlayback();
            _skipForwardHotKey.Actuated += (sender, e) => Next();

            _tornadoPlayer.MediaOpened += (sender, e) => OnTrackChanged();
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

            {
                Track[] tracks = dataService.Load<Track[]>("Tracks", "[]");

                Tracks = tracks.Length == 0 ? fileSystemService.LoadTracks("E:\\MP3s") : tracks;
            }

            PlayerState playerState = dataService.Load<PlayerState>("Player State", () => JsonConvert.SerializeObject(new PlayerState(0, new TimeSpan(), 0.5, false, false)));

            {
                void LoadProgress(object sender, EventArgs e)
                {
                    _tornadoPlayer.MediaOpened -= LoadProgress;
                    Progress = playerState.Progress;
                }

                _tornadoPlayer.MediaOpened += LoadProgress;
            }

            SelectTrack(playerState.TrackIndex);

            Volume = playerState.Volume;
            Loop = playerState.Loop;
            _isShuffled = playerState.Shuffle;
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

        private bool _isShuffled;
        public bool IsShuffled
        {
            get => _isShuffled;

            set
            {
                if (_isShuffled == value) return;

                _isShuffled = value;

                if (_isShuffled)
                {
                    Shuffle();
                }
                else
                {
                    Sort();
                }
            }
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
            RearrangePlaylist(tracks =>
            {
                Random random = new Random();

                for (int index = 0; index < tracks.Length; index++)
                {
                    int swapIndex = index + random.Next(tracks.Length - index);

                    Track track = tracks[swapIndex];
                    tracks[swapIndex] = tracks[index];
                    tracks[index] = track;
                }
            });
            IsShuffled = true;
        }

        public void Sort()
        {
            RearrangePlaylist(Array.Sort);
            IsShuffled = false;
        }

        public void SaveState()
        {
            _dataService.Save("Tracks", Tracks);
            _dataService.Save("Player State", new PlayerState(TrackIndex, Progress, Volume, Loop, IsShuffled));
        }

        private void RearrangePlaylist(Action<Track[]> rearrangeMethod)
        {
            Track selectedTrack = Tracks[TrackIndex];

            rearrangeMethod(Tracks);

            TrackIndex = Array.IndexOf(Tracks, selectedTrack);

            OnPlaylistLoaded();
            OnTrackChanged();
        }

        private void OnTrackChanged()
        {
            TrackChanged?.Invoke(this, new TrackChangedEventArgs(TrackIndex, Duration));
        }

        private void OnPlaylistLoaded()
        {
            PlaylistLoaded?.Invoke(this, new PlaylistLoadedEventArgs(Tracks));
        }
    }
}