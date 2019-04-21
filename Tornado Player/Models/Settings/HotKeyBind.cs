namespace Tornado.Player.Models.Settings
{
    using Newtonsoft.Json;

    using Tornado.Player.Utilities.HotKeys;

    internal class HotKeyBind
    {
        [JsonConstructor]
        internal HotKeyBind(Shortcut shortcut, VirtualKey key, HotKeyModifiers modifiers)
        {
            Shortcut = shortcut;
            Key = key;
            Modifiers = modifiers;
        }

        [JsonProperty(nameof(Shortcut))]
        public Shortcut Shortcut { get; }

        [JsonProperty(nameof(Key))]
        public VirtualKey Key { get; set; }

        [JsonProperty(nameof(Modifiers))]
        public HotKeyModifiers Modifiers { get; set; }
    }
}