using System;

namespace MaybeApp
{
    public static partial class Maybe
    {
        public static Maybe<T> Return<T>(T a)
        {
            return Just(a);
        }

        public static Maybe<T2> Bind<T1, T2>(this Maybe<T1> m, Func<T1, Maybe<T2>> f)
        {
            return m.IsJust ? f(m.FromJust) : Nothing<T2>();
        }
    }
}
