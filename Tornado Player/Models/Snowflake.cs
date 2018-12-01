namespace Tornado.Player.Models
{
    using Caliburn.Micro;

    using Newtonsoft.Json;

    internal class Snowflake : PropertyChangedBase
    {
        public Snowflake(ulong id)
        {
            Id = id;
        }

        [JsonProperty("Id")]
        public ulong Id { get; }
    }
}