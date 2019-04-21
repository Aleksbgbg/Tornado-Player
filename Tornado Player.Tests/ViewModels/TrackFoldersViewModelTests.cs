namespace Tornado.Player.Tests.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Moq;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Tests.Util;
    using Tornado.Player.ViewModels;

    using Xunit;

    public class TrackFoldersViewModelTests
    {
        private readonly Mock<IStorageService> _storageServiceMock;

        private FileSystemBrowserServiceMock _fileSystemBrowserServiceMock;

        private TrackFoldersViewModel _trackFoldersViewModel;

        public TrackFoldersViewModelTests()
        {
            _storageServiceMock = new Mock<IStorageService>();
        }

        [Fact]
        public void TestFoldersTypeIsObservableCollection()
        {
            SetupStorageServiceFolders(new List<TrackFolder>());
            CreateInstance();
            Assert.IsType<ObservableCollection<TrackFolder>>(_trackFoldersViewModel.Folders);
        }

        [Fact]
        public void TestPopulatesFoldersFromStorageService()
        {
            List<TrackFolder> folders = Data.TrackFolders;

            SetupStorageServiceFolders(folders);
            CreateInstance();

            Assert.Equal(folders, _trackFoldersViewModel.Folders);
        }

        [Fact]
        public void TestAddNewFolderCreatesDialog()
        {
            SetupDirectorySelected(null);

            _trackFoldersViewModel.AddNewFolder();

            VerifyBrowseFolderCalled();
        }

        [Fact]
        public void TestAddNewFolderAddsFolderToCollection()
        {
            const string directory = "SomeSongDirectory";
            SetupDirectorySelected(directory);

            _trackFoldersViewModel.AddNewFolder();

            Assert.Collection(_trackFoldersViewModel.Folders, trackFolder => Assert.Equal(directory, trackFolder.Path));
        }

        [Fact]
        public void TestAddNewFolderStoresNewFolder()
        {
            const string directory = "SomeSongDirectory";
            SetupDirectorySelected(directory);

            _trackFoldersViewModel.AddNewFolder();

            Assert.Collection(_storageServiceMock.Object.TrackFolders, trackFolder => Assert.Equal(directory, trackFolder.Path));
        }

        [Fact]
        public void TestAddNewFolderDoesNotAddFolderToCollectionWhenNotSelected()
        {
            SetupDirectoryNotSelected();

            _trackFoldersViewModel.AddNewFolder();

            Assert.Empty(_trackFoldersViewModel.Folders);
        }

        [Fact]
        public void TestAddNewFolderDoesNotStoreNewFolderWhenNotSelected()
        {
            SetupDirectoryNotSelected();

            _trackFoldersViewModel.AddNewFolder();

            Assert.Empty(_storageServiceMock.Object.TrackFolders);
        }

        private void SetupDirectoryNotSelected()
        {
            SetupStorageServiceFolders(new List<TrackFolder>());
            _fileSystemBrowserServiceMock = new FileSystemBrowserServiceMock(false, null);
            CreateInstance();
        }

        private void SetupDirectorySelected(string directory)
        {
            SetupStorageServiceFolders(new List<TrackFolder>());
            _fileSystemBrowserServiceMock = new FileSystemBrowserServiceMock(true, directory);
            CreateInstance();
        }

        private void CreateInstance()
        {
            _trackFoldersViewModel = new TrackFoldersViewModel(_storageServiceMock.Object, _fileSystemBrowserServiceMock);
        }

        private void SetupStorageServiceFolders(List<TrackFolder> folders)
        {
            _storageServiceMock.SetupGet(storageService => storageService.TrackFolders)
                               .Returns(folders);
        }

        private void VerifyBrowseFolderCalled()
        {
            Assert.Equal(1, _fileSystemBrowserServiceMock.Calls);
        }

        private class FileSystemBrowserServiceMock : IFileSystemBrowserService
        {
            private readonly bool _exists;

            private readonly string _directory;

            public FileSystemBrowserServiceMock(bool exists, string directory)
            {
                _exists = exists;
                _directory = directory;
            }

            public int Calls { get; private set; }

            public bool BrowseDirectory(out string directory)
            {
                ++Calls;
                directory = _directory;
                return _exists;
            }
        }
    }
}