namespace Tornado.Player.Utilities
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Interop;

    internal class Win32HotKey : IDisposable
    {
        [Flags]
        internal enum Modifiers : uint
        {
            AltKey = 0x0001,
            ControlKey = 0x0002,
            ShiftKey = 0x0004,
            WinKey = 0x0008,
            NoRepeat = 0x4000
        }

        private enum WindowsEvents
        {
            WM_HOTKEY = 0x0312
        }

        private static int currentHotKeyId;

        private HwndSource _windowHandleSource;

        private readonly int _id;

        internal Win32HotKey(VirtualKey key, Modifiers modifiers = 0)
        {
            _id = ++currentHotKeyId;
            TryRegisterHotKey(key, modifiers);
        }

        ~Win32HotKey()
        {
            Dispose(false);
        }

        internal event EventHandler Actuated;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DllImport("User32.dll")]
        private static extern int RegisterHotKey(IntPtr windowHandle, int id, uint modifierKeys, uint virtualKeyCode);

        [DllImport("User32.dll")]
        private static extern int UnregisterHotKey(IntPtr windowHandle, int id);

        private IntPtr EventCallback(IntPtr windowHandle, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (message == (int)WindowsEvents.WM_HOTKEY && wParam.ToInt32() == _id)
            {
                Actuated?.Invoke(this, EventArgs.Empty);
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void TryRegisterHotKey(VirtualKey key, Modifiers modifiers)
        {
            if (Win32Handler.IsInitialised)
            {
                int result = RegisterHotKey(Win32Handler.MainWindowHandle, _id, (uint)modifiers, (uint)key);

                if (result == 0)
                {
                    throw new InvalidOperationException("Did not manage to register specified Win32HotKey.");
                }

                _windowHandleSource = HwndSource.FromHwnd(Win32Handler.MainWindowHandle);
                _windowHandleSource.AddHook(EventCallback);

                return;
            }

            void OnWin32HandlerInitialised(object sender, EventArgs e)
            {
                Win32Handler.Initialised -= OnWin32HandlerInitialised;
                TryRegisterHotKey(key, modifiers);
            }

            Win32Handler.Initialised += OnWin32HandlerInitialised;
        }

        private void Dispose(bool disposing)
        {
            _windowHandleSource.RemoveHook(EventCallback);
            UnregisterHotKey(Win32Handler.MainWindowHandle, _id);

            if (disposing)
            {
                _windowHandleSource.Dispose();
            }
        }
    }
}