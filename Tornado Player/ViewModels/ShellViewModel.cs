namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.Helpers;
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IEventAggregator eventAggregator, IMainViewModel mainViewModel)
        {
            DisplayName = "Tornado Player";

            MainViewModel = mainViewModel;
            SearchCommand = new RelayCommand(_ => eventAggregator.BeginPublishOnUIThread(Shortcut.Search));
        }

        public IMainViewModel MainViewModel { get; }

        public RelayCommand SearchCommand { get; }
    }
}