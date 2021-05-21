using System;

namespace Mews.Eet.Events
{
    public class HttpRequestFinishedEventArgs : EventArgs
    {
        public HttpRequestFinishedEventArgs(long duration)
        {
            Duration = duration;
        }

        public long Duration { get; }
    }
}
