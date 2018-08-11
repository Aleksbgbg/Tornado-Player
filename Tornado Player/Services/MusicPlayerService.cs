namespace Tornado.Player.Services
{
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class MusicPlayerService : IMusicPlayerService
    {
        private readonly TornadoPlayer _tornadoPlayer = new TornadoPlayer();

        // CTRL + F6 hotkey
        private readonly Win32HotKey _togglePlaybackHotKey = new Win32HotKey(0x75, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        public MusicPlayerService(IFileSystemService fileSystemService)
        {
            _tornadoPlayer.Load(fileSystemService.LoadTracks("E:\\MP3s"));
            _togglePlaybackHotKey.Actuated += (sender, e) => _tornadoPlayer.TogglePlay();
        }
    }
}