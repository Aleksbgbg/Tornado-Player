namespace Tornado.Player.Utilities
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    internal class Win32Handler
    {
        private Win32Handler(Window mainWindow)
        {
            MainWindowHandle = new WindowInteropHelper(mainWindow).Handle;
        }

        internal static event EventHandler Initialised;

        internal static Win32Handler Instance { get; private set; }

        internal static bool IsInitialised => Instance != null;

        // Guarantee this is set after MainWindow creation
        internal IntPtr MainWindowHandle { get; }

        internal static void Initialise(Window mainWindow)
        {
            Instance = new Win32Handler(mainWindow);

            Initialised?.Invoke(null, EventArgs.Empty);
        }
    }
}