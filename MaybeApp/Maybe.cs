using System;

namespace MaybeApp
{
    public class Maybe<T>
    {
        private readonly T _value;

        private Maybe()
        {
            IsNothing = true;
        }

        private Maybe(T value)
        {
            _value = value;
            IsNothing = false;
        }

        public bool IsNothing { get; }
        public bool IsJust => !IsNothing;

        public T FromJust {
            get
            {
                if (IsNothing) throw new InvalidOperationException("FromJust called on Nothing");
                return _value;
            }
        }

        public T OrElse(T defaultValue)
        {
            return IsJust ? FromJust : defaultValue;
        }

        public static Maybe<T2> Nothing<T2>()
        {
            return new Maybe<T2>();
        }

        public static Maybe<T2> Just<T2>(T2 value)
        {
            return new Maybe<T2>(value);
        }
    }

    public static partial class Maybe
    {
        public static Maybe<T2> Nothing<T2>()
        {
            return Maybe<T2>.Nothing<T2>();
        }

        public static Maybe<T2> Just<T2>(T2 value)
        {
            return Maybe<T2>.Just(value);
        }
    }
}
