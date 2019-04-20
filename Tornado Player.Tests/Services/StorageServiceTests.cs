namespace Tornado.Player.Tests.Services
{
    using System;

    using Moq;

    using Tornado.Player.Models;
    using Tornado.Player.Services;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Tests.Util;

    using Xunit;

    public class StorageServiceTests
    {
        private readonly Mock<IDataService> _dataServiceMock;

        private StorageService _storageService;

        public StorageServiceTests()
        {
            _dataServiceMock = new Mock<IDataService>();
        }

        [Fact]
        public void TestCallsDataServiceLoadOnStartup()
        {
            CreateInstance();
            VerifyLoadDataCalled<TrackFolder[]>();
        }

        [Fact]
        public void TestGetTrackFolders()
        {
            TrackFolder[] folders = Data.TrackFolders;

            SetupDataServiceLoad(folders);
            CreateInstance();

            Assert.Equal(folders, _storageService.TrackFolders);
        }

        private void CreateInstance()
        {
            _storageService = new StorageService(_dataServiceMock.Object);
        }

        private void SetupDataServiceLoad<T>(T data)
        {
            _dataServiceMock.Setup(dataService => dataService.Load(Constants.DataStoreNames.TrackFolders, It.IsAny<Func<T>>()))
                            .Returns(data);
        }

        private void VerifyLoadDataCalled<T>()
        {
            _dataServiceMock.Verify(dataService => dataService.Load(Constants.DataStoreNames.TrackFolders, It.IsAny<Func<T>>()));
        }
    }
}