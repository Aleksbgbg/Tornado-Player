namespace Tornado.Player.Models
{
    using System.IO;

    internal class Track
    {
        internal Track(string filepath)
        {
            Filepath = filepath;
        }

        public string Filepath { get; }

        public string Name => Path.GetFileNameWithoutExtension(Filepath);
    }
}