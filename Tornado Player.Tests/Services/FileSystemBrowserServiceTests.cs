namespace Tornado.Player.Tests.Services
{
    using Moq;

    using Tornado.Player.Services;
    using Tornado.Player.Utilities.FileSystemBrowsing;

    using Xunit;

    public class FileSystemBrowserServiceTests
    {
        private readonly Mock<IBrowseDialogFactory> _browseDialogFactoryMock;

        private readonly FileSystemBrowserService _fileSystemBrowserService;

        public FileSystemBrowserServiceTests()
        {
            _browseDialogFactoryMock = new Mock<IBrowseDialogFactory>();

            _fileSystemBrowserService = new FileSystemBrowserService(_browseDialogFactoryMock.Object);
        }

        [Fact]
        public void TestBrowseFolderOpensFolderDialog()
        {
            const string expectedDirectory = "SomeDirectory";

            SetupSelectDirectory(expectedDirectory);
            bool userSelectedDirectory = _fileSystemBrowserService.BrowseDirectory(out string directory);

            Assert.True(userSelectedDirectory);
            Assert.Equal(expectedDirectory, directory);
        }

        [Fact]
        public void TestBrowseFolderFalseWhenNotSelected()
        {
            SetupDoNotSelectDirectory();
            bool userSelectedDirectory = _fileSystemBrowserService.BrowseDirectory(out string directory);

            Assert.False(userSelectedDirectory);
            Assert.Null(directory);
        }

        private void SetupSelectDirectory(string directory)
        {
            SetupBrowseDialog(directory, true);
        }

        private void SetupDoNotSelectDirectory()
        {
            SetupBrowseDialog(null, false);
        }

        private void SetupBrowseDialog(string directory, bool selected)
        {
            Mock<IBrowseDirectoryDialog> browseDirectoryDialogMock = new Mock<IBrowseDirectoryDialog>();
            browseDirectoryDialogMock.SetupGet(browseDirectoryDialog => browseDirectoryDialog.SelectedDirectory)
                                     .Returns(directory);
            browseDirectoryDialogMock.Setup(browseDirectoryDialog => browseDirectoryDialog.ShowDialog())
                                     .Returns(selected);

            _browseDialogFactoryMock.Setup(browseDialogFactory => browseDialogFactory.CreateBrowseDirectoryDialog())
                                    .Returns(browseDirectoryDialogMock.Object);
        }
    }
}