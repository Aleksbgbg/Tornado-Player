namespace Tornado.Player.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        public void TestLoadsTrackFoldersOnStartup()
        {
            CreateInstance();
            VerifyLoadDataCalled<List<TrackFolder>>(Constants.DataStoreNames.TrackFolders);
        }

        [Fact]
        public void TestGetTrackFolders()
        {
            List<TrackFolder> folders = Data.TrackFolders;

            SetupLoadData(Constants.DataStoreNames.TrackFolders, folders);
            CreateInstance();

            Assert.Equal(folders, _storageService.TrackFolders);
        }

        [Fact]
        public void TestSaveCallsSaveTrackFolders()
        {
            TestSaveDataListWithNewItem(Constants.DataStoreNames.TrackFolders, Data.TrackFolders.ToList(), new TrackFolder("newFolder"));
        }

        private void TestSaveDataListWithNewItem<T>(string dataName, List<T> data, T newDataItem)
        {
            SetupLoadData(dataName, data);
            CreateInstance();
            data.Add(newDataItem);

            _storageService.SaveData();

            _dataServiceMock.Verify(dataService => dataService.Save(dataName, data));
        }

        private void CreateInstance()
        {
            _storageService = new StorageService(_dataServiceMock.Object);
        }

        private void SetupLoadData<T>(string dataName, T data)
        {
            _dataServiceMock.Setup(dataService => dataService.Load(dataName, It.IsAny<Func<T>>()))
                            .Returns(data);
        }

        private void VerifyLoadDataCalled<T>(string dataName)
        {
            _dataServiceMock.Verify(dataService => dataService.Load(dataName, It.IsAny<Func<T>>()));
        }
    }
}