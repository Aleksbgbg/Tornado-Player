namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.Helpers;
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class ShellViewModel : Conductor<IMainViewModel>, IShellViewModel
    {
        public ShellViewModel(IEventAggregator eventAggregator, IMainViewModel mainViewModel)
        {
            DisplayName = "Tornado Player";

            SearchCommand = new RelayCommand(_ => eventAggregator.BeginPublishOnUIThread(Shortcut.Search));

            ActivateItem(mainViewModel);
        }

        public RelayCommand SearchCommand { get; }
    }
}