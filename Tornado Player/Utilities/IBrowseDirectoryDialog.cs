namespace Tornado.Player.Utilities
{
    public interface IBrowseDirectoryDialog
    {
        string SelectedDirectory { get; }

        bool ShowDialog();
    }
}