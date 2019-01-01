namespace Tornado.Player.Models
{
    using Newtonsoft.Json;

    internal class PlaybarState
    {
        [JsonConstructor]
        internal PlaybarState(double volume, bool loop)
        {
            Volume = volume;
            Loop = loop;
        }

        [JsonProperty(nameof(Volume))]
        internal double Volume { get; }

        [JsonProperty(nameof(Loop))]
        internal bool Loop { get; }
    }
}