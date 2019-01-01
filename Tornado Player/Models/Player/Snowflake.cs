namespace Tornado.Player.Models.Player
{
    using Caliburn.Micro;

    using Newtonsoft.Json;

    internal class Snowflake : PropertyChangedBase
    {
        public Snowflake(ulong id)
        {
            Id = id;
        }

        [JsonProperty(nameof(Id))]
        public ulong Id { get; }
    }
}