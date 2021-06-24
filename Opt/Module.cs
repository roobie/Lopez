using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable

namespace Option
{
    public abstract class Option<T> : IEnumerable<T>
                                    , IEquatable<T>
    {
        /// <summary>
        /// This is the slot which stores the enumerable object, that
        /// may or may not contain a single element.
        /// </summary>
        protected T[] Value { get; set; } = Array.Empty<T>();

        /// <summary>
        /// We store the type of T here, so that we can use it in
        /// <see cref="ToString()" /> for logging and debugging purposes.
        /// </summary>
        protected static Type type = typeof(T);

        /// <summary>
        /// Create a new Some object, containing the supplied value.
        /// </summary>
        public static Option<T> Some(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new _Some(value);
        }

        public static readonly Option<T> None = new _None();

        public bool IsSome()
        {
            return this != None;
        }

        public bool IsNone()
        {
            return this == None;
        }

        private class _Some : Option<T>
        {
            internal _Some(T value)
            {
                this.Value = new T[] { value };
            }

            public override string ToString()
            {
                return $"Some<{type}>({this.Value[0]})";
            }
        }

        private class _None : Option<T>
        {
            internal _None()
            {
            }

            public override string ToString()
            {
                return $"None<{type}>";
            }
        }

        // Begin: IEquatable<T> implementation
        public bool Equals(T? other)
        {
            if (this is _Some)
            {
                var value = this.Value[0];
                if (value != null)
                {
                    return value.Equals(other);
                }

                throw new Exception(
                    "Invariant failed! The Some type cannot hold a null.");
            }

            return (this is _None) && (other == null);
        }
        // End: IEquatable<T> implementation

        // Begin: IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Value)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        // End: IEnumerable<T> implementation
    }
}
