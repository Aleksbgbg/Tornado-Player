namespace Tornado.Player.Models
{
    using Newtonsoft.Json;

    internal class Snowflake
    {
        public Snowflake(ulong id)
        {
            Id = id;
        }

        [JsonProperty("Id")]
        public ulong Id { get; }
    }
}