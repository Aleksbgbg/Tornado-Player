namespace Tornado.Player.Tests.Util
{
    using System.Collections.Generic;

    using Tornado.Player.Models;

    public static class Data
    {
        public static readonly List<TrackFolder> TrackFolders = new List<TrackFolder>
        {
            new TrackFolder("folder1"),
            new TrackFolder("folder2")
        };
    }
}