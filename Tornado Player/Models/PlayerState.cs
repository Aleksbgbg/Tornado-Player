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

        [JsonProperty("TrackIndex")]
        internal int TrackIndex { get; }

        [JsonProperty("Progress")]
        internal TimeSpan Progress { get; }

        [JsonProperty("Volume")]
        internal double Volume { get; }

        [JsonProperty("Loop")]
        internal bool Loop { get; }

        [JsonProperty("Shuffle")]
        internal bool Shuffle { get; }
    }
}