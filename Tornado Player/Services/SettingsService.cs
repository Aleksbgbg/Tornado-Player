namespace Tornado.Player.Services
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Settings;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class SettingsService : ISettingsService
    {
        private const string SettingsDataName = "Settings";

        private readonly IDataService _dataService;

        public SettingsService(IDataService dataService)
        {
            _dataService = dataService;

            Settings = _dataService.Load(SettingsDataName, () => new Settings(new HotKeyBind[]
            {
                new HotKeyBind(Shortcut.SkipBackward, VirtualKey.F5, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat),
                new HotKeyBind(Shortcut.TogglePlayback, VirtualKey.F6, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat),
                new HotKeyBind(Shortcut.SkipForward, VirtualKey.F7, HotKeyModifiers.ControlKey | HotKeyModifiers.NoRepeat)
            }));
        }

        public Settings Settings { get; }

        public void Save()
        {
            _dataService.Save(SettingsDataName, Settings);
        }
    }
}