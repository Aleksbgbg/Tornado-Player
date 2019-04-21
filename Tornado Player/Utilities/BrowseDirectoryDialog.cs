namespace Tornado.Player.Utilities
{
    using Microsoft.WindowsAPICodePack.Dialogs;

    public class BrowseDirectoryDialog : IBrowseDirectoryDialog
    {
        private readonly CommonOpenFileDialog _commonOpenFileDialog;

        public BrowseDirectoryDialog()
        {
            _commonOpenFileDialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
        }

        public string SelectedDirectory => _commonOpenFileDialog.FileName;

        public bool ShowDialog()
        {
            return _commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok;
        }
    }
}