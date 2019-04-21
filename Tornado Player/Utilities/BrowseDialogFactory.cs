namespace Tornado.Player.Utilities
{
    public class BrowseDialogFactory : IBrowseDialogFactory
    {
        public IBrowseDirectoryDialog CreateBrowseDirectoryDialog()
        {
            return new BrowseDirectoryDialog();
        }
    }
}