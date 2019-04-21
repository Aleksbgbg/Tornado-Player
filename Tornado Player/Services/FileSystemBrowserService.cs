namespace Tornado.Player.Services
{
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities.FileSystemBrowsing;

    public class FileSystemBrowserService : IFileSystemBrowserService
    {
        private readonly IBrowseDialogFactory _browseDialogFactory;

        public FileSystemBrowserService(IBrowseDialogFactory browseDialogFactory)
        {
            _browseDialogFactory = browseDialogFactory;
        }

        public bool BrowseDirectory(out string directory)
        {
            IBrowseDirectoryDialog browseDirectoryDialog = _browseDialogFactory.CreateBrowseDirectoryDialog();

            bool userSelectedDirectory = browseDirectoryDialog.ShowDialog();

            if (userSelectedDirectory)
            {
                directory = browseDirectoryDialog.SelectedDirectory;
                return true;
            }

            directory = null;
            return false;
        }
    }
}