namespace Tornado.Player.Models
{
    using System;
    using System.IO;

    internal class Track : IComparable<Track>
    {
        internal Track(string filepath)
        {
            Filepath = filepath;
        }

        public string Filepath { get; }

        public string Name => Path.GetFileNameWithoutExtension(Filepath);

        public int CompareTo(Track other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}