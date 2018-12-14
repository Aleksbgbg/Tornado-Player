namespace Tornado.Player
{
    using System.Windows;

    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Services;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;
    using Tornado.Player.ViewModels;
    using Tornado.Player.ViewModels.Interfaces;

    using CM = Caliburn.Micro; // Required due to extension method conflict

    internal class AppBootstrapper : BootstrapperBase<IShellViewModel>
    {
        protected override void RegisterServices()
        {
            Container.Singleton<CM.IEventAggregator, CM.EventAggregator>();

            Container.Singleton<IAppDataService, AppDataService>();
            Container.Singleton<IContentManagerService, ContentManagerService>();
            Container.Singleton<IDataService, DataService>();
            Container.Singleton<IFileSystemService, FileSystemService>();
            Container.Singleton<IHotKeyService, HotKeyService>();
            Container.Singleton<ILayoutService, LayoutService>();
            Container.Singleton<IMusicPlayerService, MusicPlayerService>();
            Container.Singleton<ISnowflakeService, SnowflakeService>();
            Container.Singleton<IWebService, WebService>();
        }

        protected override void RegisterViewModels(IViewModelFactory viewModelFactory)
        {
            Container.Singleton<IShellViewModel, ShellViewModel>();
            Container.Singleton<IMainViewModel, MainViewModel>();

            Container.Singleton<IPlaylistCollectionViewModel, PlaylistCollectionViewModel>();
            Container.Singleton<IPlaybarViewModel, PlaybarViewModel>();

            Container.Singleton<IPlaylistEditorViewModel, PlaylistEditorViewModel>();

            viewModelFactory.Register<IPlaylistViewModel, PlaylistViewModel>();
            viewModelFactory.Register<ITrackViewModel, TrackViewModel>();
            viewModelFactory.Register<IEditTrackViewModel, EditTrackViewModel>();
            viewModelFactory.Register<IEditPlaylistViewModel, EditPlaylistViewModel>();
            viewModelFactory.Register<ITrackSinkViewModel, TrackSinkViewModel>();
        }

        protected override void OnStartupAfterDisplayRootView(object sender, StartupEventArgs e)
        {
            Win32Handler.Initialise(Application.Current.MainWindow);
        }

        protected override void OnExit(object sender, System.EventArgs e)
        {
            Container.GetInstance<IContentManagerService>().SaveContent();
            Container.GetInstance<ILayoutService>().SaveLayout();
        }
    }
}