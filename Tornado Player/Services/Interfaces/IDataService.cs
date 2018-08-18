namespace Tornado.Player.Services.Interfaces
{
    using System;

    internal interface IDataService
    {
        T Load<T>(string dataName, string emptyData = "");

        T Load<T>(string dataName, Func<string> emptyData);

        void Save<T>(string dataName, T data);
    }
}