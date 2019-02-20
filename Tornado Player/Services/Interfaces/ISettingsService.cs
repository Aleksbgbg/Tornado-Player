namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.Models.Settings;

    internal interface ISettingsService
    {
        Settings Settings { get; }

        void Save();
    }
}