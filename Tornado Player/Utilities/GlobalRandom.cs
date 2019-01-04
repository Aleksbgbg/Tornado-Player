namespace Tornado.Player.Utilities
{
    using System;

    internal static class GlobalRandom
    {
        private static readonly Random Random = new Random();

        internal static int Next()
        {
            return Random.Next();
        }

        internal static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        internal static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        internal static double NextDouble()
        {
            return Random.NextDouble();
        }

        internal static void NextBytes(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }
    }
}