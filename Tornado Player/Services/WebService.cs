namespace Tornado.Player.Services
{
    using System.Diagnostics;
    using System.Net;

    using Tornado.Player.Services.Interfaces;

    internal class WebService : IWebService
    {
        public void YouTubeTrackQueryInBrowser(string trackName)
        {
            OpenUrl($"https://youtube.com/results?search_query={WebUtility.UrlEncode(trackName)}");
        }

        public void OpenUrl(string url)
        {
            Process.Start(url);
        }
    }
}