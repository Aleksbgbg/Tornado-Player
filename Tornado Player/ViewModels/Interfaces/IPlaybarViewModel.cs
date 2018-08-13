namespace Tornado.Player.ViewModels.Interfaces
{
    using System;

    internal interface IPlaybarViewModel : IViewModelBase
    {
        TimeSpan CurrentProgress { get; set; }

        TimeSpan Duration { get; }
    }
}