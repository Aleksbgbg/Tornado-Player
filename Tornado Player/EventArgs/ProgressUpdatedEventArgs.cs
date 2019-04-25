namespace Tornado.Player.EventArgs
{
    using System;

    public class ProgressUpdatedEventArgs : EventArgs
    {
        public ProgressUpdatedEventArgs(TimeSpan newProgress)
        {
            NewProgress = newProgress;
        }

        public TimeSpan NewProgress { get; }
    }
}