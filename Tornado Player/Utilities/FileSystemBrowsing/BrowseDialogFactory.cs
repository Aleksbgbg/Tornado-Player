namespace Tornado.Player.Utilities.FileSystemBrowsing
{
    public class BrowseDialogFactory : IBrowseDialogFactory
    {
        public IBrowseDirectoryDialog CreateBrowseDirectoryDialog()
        {
            return new BrowseDirectoryDialog();
        }
    }
}