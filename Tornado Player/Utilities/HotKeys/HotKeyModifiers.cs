namespace Tornado.Player.Utilities.HotKeys
{
    using System;

    [Flags]
    internal enum HotKeyModifiers : uint
    {
        AltKey = 0x0001,
        ControlKey = 0x0002,
        ShiftKey = 0x0004,
        WinKey = 0x0008,
        NoRepeat = 0x4000
    }
}