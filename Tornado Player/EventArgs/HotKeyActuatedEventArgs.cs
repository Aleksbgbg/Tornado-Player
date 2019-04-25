namespace Tornado.Player.EventArgs
{
    using System;

    using Tornado.Player.Models;

    public class HotKeyActuatedEventArgs : EventArgs
    {
        public HotKeyActuatedEventArgs(Shortcut shortcut)
        {
            Shortcut = shortcut;
        }

        public Shortcut Shortcut { get; }
    }
}