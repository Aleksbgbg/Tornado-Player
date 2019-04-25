namespace Tornado.Player.Services.Interfaces
{
    using System;

    using Tornado.Player.EventArgs;

    public interface IHotKeyService
    {
        event EventHandler<HotKeyActuatedEventArgs> HotKeyActuated;
    }
}