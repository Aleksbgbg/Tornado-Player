﻿namespace Tornado.Player
{
    using System.Windows;

    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Factories;
    using Tornado.Player.Factories.Interfaces;
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
            // Register Factories
            Container.Singleton<ITrackFactory, TrackFactory>();

            // Register Services
            Container.Singleton<CM.IEventAggregator, CM.EventAggregator>();

            Container.Singleton<IAppDataService, AppDataService>();
            Container.Singleton<IDataService, DataService>();
            Container.Singleton<IFileSystemService, FileSystemService>();
            Container.Singleton<IHotKeyService, HotKeyService>();
            Container.Singleton<IMusicPlayerService, MusicPlayerService>();
            Container.Singleton<ISnowflakeService, SnowflakeService>();
            Container.Singleton<IWebService, WebService>();
        }

        protected override void RegisterViewModels(IViewModelFactory viewModelFactory)
        {
            Container.Singleton<IShellViewModel, ShellViewModel>();
            Container.Singleton<IMainViewModel, MainViewModel>();

            Container.Singleton<IPlaybarViewModel, PlaybarViewModel>();

            Container.PerRequest<ITrackViewModel, TrackViewModel>();

            viewModelFactory.Register<IPlaylistViewModel, PlaylistViewModel>();
        }

        protected override void OnStartupAfterDisplayRootView(object sender, StartupEventArgs e)
        {
            Win32Handler.Initialise(Application.Current.MainWindow);
        }

        protected override void OnExit(object sender, System.EventArgs e)
        {
            // _container.GetInstance<IMusicPlayerService>().SaveState();
        }
    }
}