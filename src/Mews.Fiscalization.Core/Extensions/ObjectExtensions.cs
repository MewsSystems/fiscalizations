﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull<T>(this T value)
        {
            return !value.IsNull();
        }

        public static bool IsNull<T>(this T value)
        {
            return value == null;
        }
    }
}
