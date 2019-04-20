﻿namespace Tornado.Player.Services.Interfaces
{
    using System;

    public interface IAppDataService
    {
        string GetFolder(string name);

        string GetFile(string name, string defaultContents = "");

        string GetFile(string name, Func<string> defaultContents);
    }
}