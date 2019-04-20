namespace Tornado.Player
{
    public static class Constants
    {
        public static readonly string[] SupportedMediaFormats =
        {
                "*.dat", ".3g2", ".3gp", ".3gp2", ".3gpp", ".aac", ".adt", ".adts", ".aif", ".aifc", ".aiff", ".amv", ".asf", ".avi", ".bin", ".cda", ".cue", ".divx", ".dv", ".flv", ".gxf", ".iso", ".m1v", ".m2t", ".m2ts", ".m2v", ".m4a", ".m4v", ".mkv", ".mov", ".mp2", ".mp2v", ".mp3", ".mp4", ".mp4v", ".mpa", ".mpe", ".mpeg", ".mpeg1", ".mpeg2", ".mpeg4", ".mpg", ".mpv2", ".mts", ".nsv", ".nuv", ".ogg", ".ogm", ".ogv", ".ogx", ".ps", ".rec", ".rm", ".rmvb", ".tod", ".ts", ".tts", ".vob", ".vro", ".wav", ".webm", ".wma", ".wmv"
        };

        public static class DataStoreNames
        {
            public const string Tracks = "Tracks";

            public const string Playlists = "Playlists";

            public const string TrackFolders = "Track Folders";

            public const string ManagedPlaylists = "Managed Playlists";

            public const string ActivePlaylist = "Active Playlist";

            public const string PlaybarState = "Playbar State";

            public const string AppLayout = "App Layout";

            public const string Settings = "Settings";
        }
    }
}