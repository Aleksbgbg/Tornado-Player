namespace Tornado.Player.Utilities.HotKeys
{
    using System;

    // Used to ensure access to C++ friend-type members
    // of Win32HotKey (e.g. Actuate()) isn't
    // available to HotKey consumers
    internal interface IHotKey
    {
        event EventHandler Actuated;
    }
}