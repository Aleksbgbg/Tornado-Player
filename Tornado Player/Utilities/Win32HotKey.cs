namespace Tornado.Player.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class Win32HotKey : IHotKey, IDisposable
    {
        private static int currentHotKeyId;

        private readonly int _id;

        private readonly IntPtr _mainWindowHandle;

        private Win32HotKey(int id, IntPtr mainWindowHandle, VirtualKey key, HotKeyModifiers modifiers)
        {
            _id = id;
            _mainWindowHandle = mainWindowHandle;

            int registerResult = RegisterHotKey(mainWindowHandle, id, (uint)modifiers, (uint)key);

            if (registerResult == 0)
            {
                throw new InvalidOperationException("Did not manage to register specified Win32HotKey.");
            }
        }

        internal static KeyValuePair<int, Win32HotKey> RegisterHotKey(IntPtr mainWindowHandle, VirtualKey key, HotKeyModifiers modifiers)
        {
            int id = ++currentHotKeyId;
            Win32HotKey hotKey = new Win32HotKey(id, mainWindowHandle, key, modifiers);

            return new KeyValuePair<int, Win32HotKey>(id, hotKey);
        }

        ~Win32HotKey()
        {
            Dispose(false);
        }

        public event EventHandler Actuated;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Actuate()
        {
            Actuated?.Invoke(this, EventArgs.Empty);
        }

        [DllImport("User32.dll")]
        private static extern int RegisterHotKey(IntPtr windowHandle, int id, uint modifierKeys, uint virtualKeyCode);

        [DllImport("User32.dll")]
        private static extern int UnregisterHotKey(IntPtr windowHandle, int id);

        private void Dispose(bool disposing)
        {
            UnregisterHotKey(_mainWindowHandle, _id);
        }
    }
}