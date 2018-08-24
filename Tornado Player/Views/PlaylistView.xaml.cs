namespace Tornado.Player.Views
{
    using System.Windows;

    public partial class PlaylistView
    {
        public PlaylistView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Tracks.ScrollIntoView(Tracks.SelectedItem);
        }
    }
}