namespace Tornado.Player.Models.Settings
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

    internal class Settings
    {
        [JsonConstructor]
        internal Settings(HotKeyBind[] hotKeyBinds)
        {
            HotKeyBinds = new HotKeyBindArray(hotKeyBinds);
        }

        [DisplayName("Hotkey Binds")]
        [Category("Hotkeys")]
        [Description("All of the global (system-wide) hotkeys for various player actions.")]
        [ExpandableObject]
        [JsonProperty(nameof(HotKeyBinds))]
        public HotKeyBindArray HotKeyBinds { get; }
    }
}