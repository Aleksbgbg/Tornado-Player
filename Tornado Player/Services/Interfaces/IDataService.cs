namespace Tornado.Player.Services.Interfaces
{
    using System;

    public interface IDataService
    {
        T Load<T>(string dataName, T emptyData = default);

        T Load<T>(string dataName, Func<T> emptyData);

        void Save(string dataName, object data);
    }
}