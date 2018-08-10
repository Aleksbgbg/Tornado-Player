namespace Tornado.Player.Utilities
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    internal static class Win32Handler
    {
        internal static event EventHandler Initialised;

        internal static bool IsInitialised { get; private set; }

        // Guarantee this is set after MainWindow creation
        internal static IntPtr MainWindowHandle { get; private set; }

        internal static void Initialise(Window mainWindow)
        {
            MainWindowHandle = new WindowInteropHelper(mainWindow).Handle;

            IsInitialised = true;
            Initialised?.Invoke(null, EventArgs.Empty);
        }
    }
}