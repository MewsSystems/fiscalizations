using System;
using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class ITryExtensions
    {
        public static T Get<T, E>(this ITry<T, E> value)
            where E : Exception
        {
            return value.Get(e => e);
        }

        public static ITry<T> Where<T>(this ITry<T> value, Func<T, bool> evaluator, Func<Unit, Exception> error)
        {
            return value.FlatMap(v => evaluator(v).ToTry(
                t => v,
                f => error(Unit.Value)
            ));
        }

        public static ITry<T, E> Where<T, E>(this ITry<T, E> value, Func<T, bool> evaluator, Func<Unit, E> error)
        {
            return value.FlatMap(v => evaluator(v).ToTry(
                t => v,
                f => error(Unit.Value)
            ));
        }
    }
}
