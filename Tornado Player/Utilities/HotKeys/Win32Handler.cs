namespace Tornado.Player.Utilities.HotKeys
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Interop;

    internal class Win32Handler : IDisposable
    {
        private readonly HwndSource _windowHandleSource;

        private readonly Dictionary<int, Win32HotKey> _registeredHotKeys = new Dictionary<int, Win32HotKey>();

        internal Win32Handler(Window mainWindow)
        {
            IntPtr mainWindowHandle = new WindowInteropHelper(mainWindow).Handle;

            _windowHandleSource = HwndSource.FromHwnd(mainWindowHandle);
            _windowHandleSource.AddHook(EventCallback);
        }

        ~Win32Handler()
        {
            Dispose(false);
        }

        private enum WindowsEvents
        {
            WM_HOTKEY = 0x0312
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal IHotKey RegisterHotKey(VirtualKey key, HotKeyModifiers modifiers = 0)
        {
            KeyValuePair<int, Win32HotKey> hotKeyKeyValuePair = Win32HotKey.RegisterHotKey(_windowHandleSource.Handle, key, modifiers);

            _registeredHotKeys.Add(hotKeyKeyValuePair.Key, hotKeyKeyValuePair.Value);

            return hotKeyKeyValuePair.Value;
        }

        private void Dispose(bool disposing)
        {
            _windowHandleSource.RemoveHook(EventCallback);

            if (disposing)
            {
                foreach (KeyValuePair<int, Win32HotKey> registeredHotKey in _registeredHotKeys)
                {
                    registeredHotKey.Value.Dispose();
                }

                _windowHandleSource.Dispose();
            }
        }

        private IntPtr EventCallback(IntPtr windowHandle, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (message == (int)WindowsEvents.WM_HOTKEY)
            {
                int hotKeyId = wParam.ToInt32();
                _registeredHotKeys[hotKeyId].Actuate();

                handled = true;
            }

            return IntPtr.Zero;
        }
    }
}