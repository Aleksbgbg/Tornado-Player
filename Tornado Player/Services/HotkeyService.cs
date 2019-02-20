namespace Tornado.Player.Services
{
    using System;
    using System.Windows;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class HotKeyService : IHotKeyService
    {
        private Win32Handler _win32Handler;

        internal void Initialize(Window mainWindow)
        {
            _win32Handler = new Win32Handler(mainWindow);

            _win32Handler.RegisterHotKey(VirtualKey.F5, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat).Actuated += (sender, e) => OnHotKeyActuated(Shortcut.SkipBackward);
            _win32Handler.RegisterHotKey(VirtualKey.F6, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat).Actuated += (sender, e) => OnHotKeyActuated(Shortcut.TogglePlayback);
            _win32Handler.RegisterHotKey(VirtualKey.F7, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat).Actuated += (sender, e) => OnHotKeyActuated(Shortcut.SkipForward);
        }

        public event EventHandler<HotKeyActuatedEventArgs> HotKeyActuated;

        private void OnHotKeyActuated(Shortcut shortcut)
        {
            HotKeyActuated?.Invoke(this, new HotKeyActuatedEventArgs(shortcut));
        }
    }
}