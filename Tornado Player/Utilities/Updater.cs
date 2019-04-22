namespace Tornado.Player.Utilities
{
    using System.Threading.Tasks;

    using Squirrel;

    internal static class Updater
    {
        internal static async Task Update()
        {
            using (UpdateManager gitHubUpdateManager = await UpdateManager.GitHubUpdateManager("https://github.com/Aleksbgbg/Tornado-Player"))
            {
                await gitHubUpdateManager.UpdateApp();
            }
        }
    }
}