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

        public MusicPlayerService()
        {
            _tornadoPlayer.MediaOpened += (sender, e) => OnTrackChanged();
            _tornadoPlayer.MediaEnded += (sender, e) =>
            {
                if (Loop)
                {
                    _tornadoPlayer.Stop();
                    _tornadoPlayer.Play();
                }
            };
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

        public bool IsPlaying => _tornadoPlayer.IsPlaying;

        public Track Track { get; private set; }

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

        public bool Loop { get; set; }

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

        public void SelectTrack(Track track)
        {
            Track = track;
        }

        private void OnTrackChanged()
        {
            TrackChanged?.Invoke(this, new TrackChangedEventArgs(Track, Duration));
        }
    }
}