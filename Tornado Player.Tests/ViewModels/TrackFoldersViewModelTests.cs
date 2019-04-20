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

        private void CreateInstance()
        {
            _trackFoldersViewModel = new TrackFoldersViewModel(_storageServiceMock.Object);
        }

        private void SetupStorageServiceFolders(List<TrackFolder> folders)
        {
            _storageServiceMock.SetupGet(storageService => storageService.TrackFolders)
                               .Returns(folders);
        }
    }
}