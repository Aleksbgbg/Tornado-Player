namespace Tornado.Player.Utilities
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;

    internal class Win32HotKey : IDisposable
    {
        [Flags]
        internal enum HotKeyModifiers : uint
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

        // Guarantee this is called after MainWindow creation
        private static IntPtr mainWindowHandle;

        private static int currentHotKeyId;

        private readonly HwndSource _windowHandleSource;

        private readonly int _id;

        static Win32HotKey()
        {
            SetMainWindowHandle();
        }

        internal Win32HotKey(uint keycode, HotKeyModifiers modifiers = 0)
        {
            int result = RegisterHotKey(mainWindowHandle, _id, (uint)modifiers, keycode);

            if (result == 0)
            {
                throw new InvalidOperationException("Did not manage to register specified Win32HotKey.");
            }

            _id = ++currentHotKeyId;

            _windowHandleSource = HwndSource.FromHwnd(mainWindowHandle);
            _windowHandleSource.AddHook(EventCallback);
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

        internal static void InitialiseWindowHandle()
        {
            if (mainWindowHandle == IntPtr.Zero)
            {
                SetMainWindowHandle();
            }
        }

        [DllImport("User32.dll")]
        private static extern int RegisterHotKey(IntPtr windowHandle, int id, uint modifierKeys, uint virtualKeyCode);

        [DllImport("User32.dll")]
        private static extern int UnregisterHotKey(IntPtr windowHandle, int id);

        private static void SetMainWindowHandle()
        {
            if (Application.Current.MainWindow != null)
            {
                mainWindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            }
        }

        private IntPtr EventCallback(IntPtr windowHandle, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (message == (int)WindowsEvents.WM_HOTKEY && wParam.ToInt32() == _id)
            {
                Actuated?.Invoke(this, EventArgs.Empty);
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void Dispose(bool disposing)
        {
            _windowHandleSource.RemoveHook(EventCallback);
            UnregisterHotKey(mainWindowHandle, _id);

            if (disposing)
            {
                _windowHandleSource.Dispose();
            }
        }
    }
}