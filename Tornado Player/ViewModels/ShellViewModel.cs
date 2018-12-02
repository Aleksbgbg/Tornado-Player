namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.Helpers;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class ShellViewModel : Conductor<IMainViewModel>, IShellViewModel
    {
        public ShellViewModel(IEventAggregator eventAggregator, ILayoutService layoutService, IMainViewModel mainViewModel)
        {
            DisplayName = "Tornado Player";

            AppLayout = layoutService.AppLayout;
            SearchCommand = new RelayCommand(_ => eventAggregator.BeginPublishOnUIThread(Shortcut.Search));

            ActivateItem(mainViewModel);
        }

        public AppLayout AppLayout { get; }

        public RelayCommand SearchCommand { get; }
    }
}