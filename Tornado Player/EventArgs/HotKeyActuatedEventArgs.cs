namespace Tornado.Player.EventArgs
{
    using System;

    using Tornado.Player.Models;

    internal class HotKeyActuatedEventArgs : EventArgs
    {
        public HotKeyActuatedEventArgs(Shortcut shortcut)
        {
            Shortcut = shortcut;
        }

        public Shortcut Shortcut { get; }
    }
}