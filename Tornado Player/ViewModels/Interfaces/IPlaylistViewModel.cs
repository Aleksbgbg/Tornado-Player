namespace Tornado.Player.ViewModels.Interfaces
{
    using Caliburn.Micro;

    internal interface IPlaylistViewModel : IViewModelBase, IConductor
    {
        void SelectPrevious();

        void SelectNext();
    }
}