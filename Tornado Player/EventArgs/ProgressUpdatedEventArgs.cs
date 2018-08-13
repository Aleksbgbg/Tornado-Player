namespace Tornado.Player.EventArgs
{
    using System;

    internal class ProgressUpdatedEventArgs : EventArgs
    {
        public ProgressUpdatedEventArgs(TimeSpan newProgress)
        {
            NewProgress = newProgress;
        }

        public TimeSpan NewProgress { get; }
    }
}