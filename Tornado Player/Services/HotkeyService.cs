namespace Tornado.Player.Services
{
    using System;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class HotKeyService : IHotKeyService
    {
        private readonly Win32HotKey _skipBackwardHotKey = new Win32HotKey(VirtualKey.F5, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        private readonly Win32HotKey _togglePlaybackHotKey = new Win32HotKey(VirtualKey.F6, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        private readonly Win32HotKey _skipForwardHotKey = new Win32HotKey(VirtualKey.F7, Win32HotKey.Modifiers.ControlKey | Win32HotKey.Modifiers.NoRepeat);

        public HotKeyService()
        {
            _skipBackwardHotKey.Actuated += (sender, e) => OnHotKeyActuated(Shortcut.SkipBackward);
            _togglePlaybackHotKey.Actuated += (sender, e) => OnHotKeyActuated(Shortcut.TogglePlayback);
            _skipForwardHotKey.Actuated += (sender, e) => OnHotKeyActuated(Shortcut.SkipForward);
        }

        public event EventHandler<HotKeyActuatedEventArgs> HotKeyActuated;

        private void OnHotKeyActuated(Shortcut shortcut)
        {
            HotKeyActuated?.Invoke(this, new HotKeyActuatedEventArgs(shortcut));
        }
    }
}