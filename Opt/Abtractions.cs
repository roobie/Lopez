using System;
using System.Linq;
using System.Collections.Generic;

namespace Option.Abstractions
{
    public static class OptAbstractions
    {
        /// <summary>
        /// Used to evaluate iterators applied to option objects.
        /// E.g. (pseudocode)<code>
        /// // This is an object of type Option[int]
        /// Option[int].None
        /// // This becomes an object of type
        /// // System.Linq.Enumerable+WhereEnumerableIterator`1[System.Int32]
        ///   .Where(x => x < 2)
        /// // And now back to Option[int]
        ///   .IntoOption()
        /// </code>
        /// N.B. If the IEnumerable[T] has more than one element,
        /// an exception (InvalidOperationException) will be thrown.
        /// </summary>
        public static Option<T> IntoOption<T>(this IEnumerable<T> value)
        {
            if (value.Count() > 0)
            {
                return Option<T>.Some(value.Single());
            }

            return Option<T>.None;
        }

        /// <summary>
        /// Make an Option of a reference to an object of a reference type.
        /// </summary>
        public static Option<T> ToOption<T>(this T value)
            where T: class
        {
            if (value == null)
            {
                return Option<T>.None;
            }

            return Option<T>.Some(value);
        }

        /// <summary>
        /// Make an Option of a reference to an object of a nullable struct
        /// type.
        /// </summary>
        public static Option<T> AsOption<T>(this Nullable<T> value)
            where T : struct
        {
            if (!value.HasValue)
            {
                return Option<T>.None;
            }

            return Option<T>.Some(value.Value);
        }
    }
}
