namespace Tornado.Player
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

    using Tornado.Player.Factories;
    using Tornado.Player.Factories.Interfaces;
    using Tornado.Player.Services;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;
    using Tornado.Player.ViewModels;
    using Tornado.Player.ViewModels.Interfaces;

    internal class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        internal AppBootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            // Register Factories
            _container.Singleton<ITrackFactory, TrackFactory>();

            // Register Services
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IWindowManager, WindowManager>();

            _container.Singleton<IAppDataService, AppDataService>();
            _container.Singleton<IDataService, DataService>();
            _container.Singleton<IFileSystemService, FileSystemService>();
            _container.Singleton<IMusicPlayerService, MusicPlayerService>();
            _container.Singleton<IWebService, WebService>();

            // Register ViewModels
            _container.Singleton<IShellViewModel, ShellViewModel>();
            _container.Singleton<IMainViewModel, MainViewModel>();

            _container.Singleton<IPlaylistViewModel, PlaylistViewModel>();
            _container.Singleton<IPlaybarViewModel, PlaybarViewModel>();

            _container.PerRequest<ITrackViewModel, TrackViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
            Win32Handler.Initialise(Application.Current.MainWindow);
        }

        protected override void OnExit(object sender, System.EventArgs e)
        {
            _container.GetInstance<IMusicPlayerService>().SaveState();
        }
    }
}