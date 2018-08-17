﻿namespace Tornado.Player.Services
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
            _tornadoPlayer.Load(fileSystemService.LoadTracks("E:\\MP3s"));

            _skipBackwardHotKey.Actuated += (sender, e) => _tornadoPlayer.Previous();
            _togglePlaybackHotKey.Actuated += (sender, e) => _tornadoPlayer.TogglePlay();
            _skipForwardHotKey.Actuated += (sender, e) => _tornadoPlayer.Next();
        }

        public event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated
        {
            add => _tornadoPlayer.ProgressUpdated += value;

            remove => _tornadoPlayer.ProgressUpdated -= value;
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

        public Track[] Tracks => _tornadoPlayer.Tracks;

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
            _tornadoPlayer.Previous();
        }

        public void Next()
        {
            _tornadoPlayer.Next();
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
            _tornadoPlayer.Switch(index);
        }
    }
}