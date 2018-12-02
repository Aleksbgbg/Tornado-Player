namespace Tornado.Player.ViewModels.Interfaces
{
    using Caliburn.Micro;

    internal interface IMainViewModel : IViewModelBase, IConductor
    {
        void EditPlaylists();
    }
}