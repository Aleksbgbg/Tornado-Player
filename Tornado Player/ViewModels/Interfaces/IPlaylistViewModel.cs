namespace Tornado.Player.ViewModels.Interfaces
{
    using Caliburn.Micro;

    using Tornado.Player.Models;

    internal interface IPlaylistViewModel : IViewModelBase
    {
        IObservableCollection<Track> Tracks { get; }

        int SelectedIndex { get; set; }
    }
}