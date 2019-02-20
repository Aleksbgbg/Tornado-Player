namespace Tornado.Player.Models.Settings
{
    using Newtonsoft.Json;

    internal class Settings
    {
        [JsonConstructor]
        internal Settings(HotKeyBind[] hotKeyBinds)
        {
            HotKeyBinds = hotKeyBinds;
        }

        [JsonProperty(nameof(HotKeyBinds))]
        public HotKeyBind[] HotKeyBinds { get; }
    }
}