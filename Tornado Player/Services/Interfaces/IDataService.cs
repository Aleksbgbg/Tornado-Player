namespace Tornado.Player.Services.Interfaces
{
    using System;

    internal interface IDataService
    {
        T Load<T>(string dataName, T emptyData = default);

        T Load<T>(string dataName, Func<T> emptyData);

        void Save<T>(string dataName, T data);
    }
}