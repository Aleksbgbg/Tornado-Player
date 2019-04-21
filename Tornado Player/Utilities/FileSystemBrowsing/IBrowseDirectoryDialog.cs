namespace Tornado.Player.Utilities.FileSystemBrowsing
{
    public interface IBrowseDirectoryDialog
    {
        string SelectedDirectory { get; }

        bool ShowDialog();
    }
}