using System;
using System.Collections.Generic;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class ITryExtensions
    {
        public static ITry<T> Where<T>(this ITry<T> value, Func<T, bool> evaluator, Func<Unit, Exception> error)
        {
            return value.FlatMap(v => evaluator(v).ToTry(
                t => v,
                f => error(Unit.Value)
            ));
        }

        public static ITry<T, IEnumerable<E>> Where<T, E>(this ITry<T, E> value, Func<T, bool> evaluator, Func<Unit, E> error)
        {
            var result = value.FlatMap(v => evaluator(v).ToTry(
                t => v,
                f => error(Unit.Value)
            ));

            return result.MapError(e => e.ToEnumerable());
        }
    }
}
