namespace Tornado.Player.Services.Interfaces
{
    using Tornado.Player.Models;

    internal interface ILayoutService
    {
        AppLayout AppLayout { get; }

        void SaveLayout();
    }
}