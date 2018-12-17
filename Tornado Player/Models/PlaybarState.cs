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

        [JsonProperty("Volume")]
        internal double Volume { get; }

        [JsonProperty("Loop")]
        internal bool Loop { get; }
    }
}