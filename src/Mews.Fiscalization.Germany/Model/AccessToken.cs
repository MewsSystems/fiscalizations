﻿using System;

namespace Mews.Fiscalization.Germany.Model
{
    public sealed class AccessToken
    {
        public AccessToken(string value, DateTime expirationUtc)
        {
            Value = value;
            ExpirationUtc = expirationUtc;
        }

        public string Value { get; set; }

        public DateTime ExpirationUtc { get; set; }
    }
}
