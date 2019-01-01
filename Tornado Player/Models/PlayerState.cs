namespace Tornado.Player.Models
{
    using System;

    using Newtonsoft.Json;

    internal readonly struct PlayerState
    {
        [JsonConstructor]
        internal PlayerState(int trackIndex, TimeSpan progress, double volume, bool loop, bool shuffle)
        {
            TrackIndex = trackIndex;
            Progress = progress;
            Volume = volume;
            Loop = loop;
            Shuffle = shuffle;
        }

        [JsonProperty(nameof(TrackIndex))]
        internal int TrackIndex { get; }

        [JsonProperty(nameof(Progress))]
        internal TimeSpan Progress { get; }

        [JsonProperty(nameof(Volume))]
        internal double Volume { get; }

        [JsonProperty(nameof(Loop))]
        internal bool Loop { get; }

        [JsonProperty(nameof(Shuffle))]
        internal bool Shuffle { get; }
    }
}