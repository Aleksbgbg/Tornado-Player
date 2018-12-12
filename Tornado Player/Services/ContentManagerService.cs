namespace Tornado.Player.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;

    internal class ContentManagerService : IContentManagerService
    {
        private const string TracksFile = "TrackTest";

        private const string PlaylistsFile = "Playlists";

        private readonly IDataService _dataService;

        private readonly ISnowflakeService _snowflakeService;

        private readonly Dictionary<ulong, Track> _trackRepository;

        private readonly List<Playlist> _playlists;

        public ContentManagerService(IDataService dataService, ISnowflakeService snowflakeService)
        {
            _dataService = dataService;
            _snowflakeService = snowflakeService;

            Track[] tracks = dataService.Load(TracksFile, () => new Track[0]);
            _trackRepository = tracks.ToDictionary(track => track.Id, track => track);

            _playlists = _dataService.Load(PlaylistsFile, () => new List<Playlist>
            {
                new Playlist(0u, "Main", 0, _trackRepository.Values)
            });

            foreach (Playlist playlist in _playlists)
            {
                playlist.Load(_trackRepository);
            }
        }

        public Track AddTrack(string file)
        {
            Track track = new Track(_snowflakeService.GenerateSnowflake(), file);

            _trackRepository[track.Id] = track;

            AddTrackToPlaylist(_playlists[0], track); // Main playlist contains all tracks

            return track;
        }

        public Track[] AddTracks(string directory)
        {
            string[] files = Directory.GetFiles(directory);

            Track[] newTracks = new Track[files.Length];

            for (int index = 0; index < files.Length; ++index)
            {
                newTracks[index] = AddTrack(files[index]);
            }

            return newTracks;
        }

        public Playlist AddPlaylist(string name)
        {
            return AddPlaylist(name, Enumerable.Empty<Track>());
        }

        public Playlist AddPlaylist(string name, IEnumerable<Track> tracks)
        {
            Playlist playlist = new Playlist(_snowflakeService.GenerateSnowflake(), name, 0, tracks);

            _playlists.Add(playlist);

            return playlist;
        }

        public void AddTrackToPlaylist(Playlist playlist, Track track)
        {
            playlist.AddTrack(track.Id, _trackRepository);
        }

        public void RemoveTrackFromPlaylist(Playlist playlist, Track track)
        {
            playlist.RemoveTrack(track.Id);
        }

        public IEnumerable<Playlist> RetrievePlaylists()
        {
            return _playlists;
        }

        public IEnumerable<Track> RetrieveTracks()
        {
            return _trackRepository.Values;
        }

        public void SaveContent()
        {
            _dataService.Save(TracksFile, _trackRepository.Values);
            _dataService.Save(PlaylistsFile, _playlists);
        }
    }
}