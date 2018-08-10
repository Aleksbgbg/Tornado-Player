namespace Tornado.Player.Services
{
    using System;
    using System.Windows.Media;

    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class MusicPlayerService : IMusicPlayerService
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        // CTRL + F6 hotkey
        private readonly Win32HotKey _togglePlaybackHotKey = new Win32HotKey(0x75, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        private bool _playing = true;

        public MusicPlayerService()
        {
            _mediaPlayer.Open(new Uri("E:\\MP3s\\Pete & Bas - Shut Ya Mouth [Music Video] Sindhuworld # Grime Report Tv.mp4"));
            _mediaPlayer.Play();

            _togglePlaybackHotKey.Actuated += (sender, e) =>
            {
                if (_playing)
                {
                    _mediaPlayer.Pause();
                }
                else
                {
                    _mediaPlayer.Play();
                }

                _playing = !_playing;
            };
        }
    }
}