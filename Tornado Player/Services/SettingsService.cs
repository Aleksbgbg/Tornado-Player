namespace Tornado.Player.Services
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Settings;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities.HotKeys;

    internal class SettingsService : ISettingsService
    {
        private readonly IDataService _dataService;

        public SettingsService(IDataService dataService)
        {
            _dataService = dataService;

            Settings = _dataService.Load(Constants.DataStoreNames.Settings, () => new Settings(new HotKeyBind[]
            {
                new HotKeyBind(Shortcut.SkipBackward, VirtualKey.F5, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat),
                new HotKeyBind(Shortcut.TogglePlayback, VirtualKey.F6, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat),
                new HotKeyBind(Shortcut.SkipForward, VirtualKey.F7, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat),
                new HotKeyBind(Shortcut.VolumeUp, VirtualKey.F3, HotKeyModifiers.ControlKey),
                new HotKeyBind(Shortcut.VolumeDown, VirtualKey.F2, HotKeyModifiers.ControlKey),
                new HotKeyBind(Shortcut.ToggleMute, VirtualKey.F1, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat)
            }));
        }

        public Settings Settings { get; }

        public void Save()
        {
            _dataService.Save(Constants.DataStoreNames.Settings, Settings);
        }
    }
}