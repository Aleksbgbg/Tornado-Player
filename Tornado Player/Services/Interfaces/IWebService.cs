namespace Tornado.Player.Services.Interfaces
{
    internal interface IWebService
    {
        void YouTubeTrackQueryInBrowser(string trackName);

        void OpenUrl(string url);
    }
}