namespace Tornado.Player.Services
{
    using System;

    using Tornado.Player.Services.Interfaces;

    using UniqueIdGenerator.Net;

    internal class SnowflakeService : ISnowflakeService
    {
        private readonly Generator _generator = new Generator(0, new DateTime(1970, 1, 1)); // Starts at Unix epoch

        public ulong GenerateSnowflake()
        {
            return _generator.NextLong();
        }
    }
}