namespace Tornado.Player.Services
{
    using System;
    using System.Windows;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Models.Settings;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class HotKeyService : IHotKeyService
    {
        private readonly HotKeyBind[] _hotKeyBinds;

        private Win32Handler _win32Handler;

        public HotKeyService(ISettingsService settingsService)
        {
            _hotKeyBinds = settingsService.Settings.HotKeyBinds;
        }

        internal void Initialize(Window mainWindow)
        {
            _win32Handler = new Win32Handler(mainWindow);

            foreach (HotKeyBind hotKeyBind in _hotKeyBinds)
            {
                _win32Handler.RegisterHotKey(hotKeyBind.Key, hotKeyBind.Modifiers).Actuated += (sender, e) => OnHotKeyActuated(hotKeyBind.Shortcut);
            }
        }

        public event EventHandler<HotKeyActuatedEventArgs> HotKeyActuated;

        private void OnHotKeyActuated(Shortcut shortcut)
        {
            HotKeyActuated?.Invoke(this, new HotKeyActuatedEventArgs(shortcut));
        }
    }
}