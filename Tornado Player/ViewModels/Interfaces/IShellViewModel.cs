namespace Tornado.Player.ViewModels.Interfaces
{
    internal interface IShellViewModel : IViewModelBase
    {
        IMainViewModel MainViewModel { get; }
    }
}