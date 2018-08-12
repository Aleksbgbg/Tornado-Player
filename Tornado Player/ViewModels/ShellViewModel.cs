namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        public ShellViewModel(IMainViewModel mainViewModel, IMusicPlayerService musicPlayerService)
        {
            DisplayName = "Tornado Player";

            MainViewModel = mainViewModel;

            _musicPlayerService = musicPlayerService;
        }

        public IMainViewModel MainViewModel { get; }
    }
}