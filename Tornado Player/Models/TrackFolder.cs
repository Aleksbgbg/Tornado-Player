namespace Tornado.Player.Models
{
    using Newtonsoft.Json;

    public class TrackFolder
    {
        [JsonConstructor]
        public TrackFolder(string path)
        {
            Path = path;
        }

        [JsonProperty(nameof(Path))]
        public string Path { get; }
    }
}