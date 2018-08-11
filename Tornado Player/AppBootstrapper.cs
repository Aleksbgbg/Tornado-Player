namespace Tornado.Player
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

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
            // Register Services
            _container.Singleton<IWindowManager, WindowManager>();

            _container.Singleton<IFileSystemService, FileSystemService>();
            _container.Singleton<IMusicPlayerService, MusicPlayerService>();

            // Register ViewModels
            _container.Singleton<IShellViewModel, ShellViewModel>();
            _container.Singleton<IMainViewModel, MainViewModel>();
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
    }
}