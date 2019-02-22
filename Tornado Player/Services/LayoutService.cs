namespace Tornado.Player.Services
{
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;

    internal class LayoutService : ILayoutService
    {
        private readonly IDataService _dataService;

        public LayoutService(IDataService dataService)
        {
            _dataService = dataService;

            AppLayout = _dataService.Load(Constants.DataStoreNames.AppLayout, () => new AppLayout(showPlaylists: true,
                                                                                                  playlistsBoxColumnWidth: 300.0));
        }

        public AppLayout AppLayout { get; }

        public void SaveLayout()
        {
            _dataService.Save(Constants.DataStoreNames.AppLayout, AppLayout);
        }
    }
}