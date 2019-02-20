namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models.Settings;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public SettingsViewModel(ISettingsService settingsService)
        {
            Settings = settingsService.Settings;
        }

        public Settings Settings { get; }
    }
}