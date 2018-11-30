namespace Tornado.Player.Services.Interfaces
{
    using System;

    using Tornado.Player.EventArgs;

    internal interface IHotKeyService
    {
        event EventHandler<HotKeyActuatedEventArgs> HotKeyActuated;
    }
}