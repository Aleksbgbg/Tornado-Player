namespace Tornado.Player.Utilities
{
    using System;
    using System.Windows.Media;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;

    internal class TornadoPlayer
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        private bool _isPlaying;

        private Track[] _tracks;

        internal TornadoPlayer()
        {
            _mediaPlayer.MediaEnded += (sender, e) => Next();
        }

        internal event EventHandler<TrackChangedEventArgs> TrackChanged;

        private int _trackIndex;
        private int TrackIndex
        {
            get => _trackIndex;

            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Cannot play a track at index smaller than 0.");
                }

                if (value > _tracks.Length)
                {
                    _trackIndex = value % _tracks.Length;
                }
                else
                {
                    _trackIndex = value;
                }
            }
        }

        internal void Load(Track[] tracks)
        {
            _tracks = tracks;
            Switch(0);
        }

        internal void Next()
        {
            Switch(TrackIndex + 1);
        }

        internal void Play()
        {
            _mediaPlayer.Play();
            _isPlaying = true;
        }

        internal void Pause()
        {
            _mediaPlayer.Pause();
            _isPlaying = false;
        }

        internal void TogglePlay()
        {
            if (_isPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void Switch(int index)
        {
            TrackIndex = index;
            _mediaPlayer.Open(new Uri(_tracks[TrackIndex].Filepath));
            Play();

            TrackChanged?.Invoke(this, new TrackChangedEventArgs(TrackIndex));
        }
    }
}