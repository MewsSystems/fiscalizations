﻿using System;

namespace Mews.Fiscalization.Spain
{
    public class HttpRequestFinishedEventArgs : EventArgs
    {
        public HttpRequestFinishedEventArgs(string response, long duration)
        {
            Response = response;
            Duration = duration;
        }

        public string Response { get; }

        public long Duration { get; }
    }
}
