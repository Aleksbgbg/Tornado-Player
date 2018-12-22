namespace Tornado.Player.Models
{
    using System.Windows;

    public class WindowSettings
    {
        public ResizeMode ResizeMode { get; set; } = ResizeMode.CanResize;

        public bool ShowInTaskbar { get; set; } = true;

        public string Title { get; set; }

        public bool Topmost { get; set; } = false;

        public double Height { get; set; }

        public double Width { get; set; }

        public WindowStartupLocation WindowStartupLocation { get; set; } = WindowStartupLocation.CenterOwner;

        public WindowState WindowState { get; set; } = WindowState.Normal;

        public WindowStyle WindowStyle { get; set; } = WindowStyle.SingleBorderWindow;
    }
}