namespace Tornado.Player
{
    using System.Windows;

    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Services;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;
    using Tornado.Player.ViewModels;
    using Tornado.Player.ViewModels.Dialogs;
    using Tornado.Player.ViewModels.Editing;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;
    using Tornado.Player.ViewModels.Interfaces.Editing;

    using CM = Caliburn.Micro; // Required due to extension method conflict

    internal class AppBootstrapper : BootstrapperBase<IShellViewModel>
    {
        protected override void RegisterServices()
        {
            Container.Singleton<CM.IEventAggregator, CM.EventAggregator>();

            Container.Singleton<IAppDataService, AppDataService>();
            Container.Singleton<IContentManagerService, ContentManagerService>();
            Container.Singleton<IDataService, DataService>();
            Container.Singleton<IDialogService, DialogService>();
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

            Container.PerRequest<IPlaylistEditorViewModel, PlaylistEditorViewModel>();

            viewModelFactory.Register<IManagedPlaylistViewModel, ManagedPlaylistViewModel>();
            viewModelFactory.Register<ICustomPlaylistViewModel, CustomPlaylistViewModel>();
            viewModelFactory.Register<ITrackViewModel, TrackViewModel>();

            viewModelFactory.Register<IPlaylistEditorViewModel, PlaylistEditorViewModel>();
            viewModelFactory.Register<IEditPlaylistViewModel, EditPlaylistViewModel>();
            viewModelFactory.Register<ITrackSinkViewModel, TrackSinkViewModel>();

            viewModelFactory.Register<ICreatePlaylistDialogViewModel, CreatePlaylistDialogViewModel>();
            viewModelFactory.Register<IConfirmationDialogViewModel, ConfirmationDialogViewModel>();
        }

        protected override void OnStartupAfterDisplayRootView(object sender, StartupEventArgs e)
        {
            Win32Handler.Initialise(Application.Current.MainWindow);
        }

        protected override void OnExit(object sender, System.EventArgs e)
        {
            Container.GetInstance<IContentManagerService>().SaveContent();
            Container.GetInstance<ILayoutService>().SaveLayout();
            Container.GetInstance<IPlaybarViewModel>().SavePlaybarState();
        }
    }
}