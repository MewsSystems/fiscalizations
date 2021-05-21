using System;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model
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

        public static ITry<T, INonEmptyEnumerable<E>> Where<T, E>(this ITry<T, INonEmptyEnumerable<E>> value, Func<T, bool> evaluator, Func<Unit, E> error)
        {
            return value.FlatMap(v => evaluator(v).ToTry(
                t => v,
                f => error(Unit.Value).ToEnumerable()
            ));
        }

        public static T GetUnsafe<T>(this ITry<T, INonEmptyEnumerable<Error>> value)
        {
            return value.Get(errors => new ArgumentException(errors.Select(e => e.Message).MkString(",")));
        }
    }
}
