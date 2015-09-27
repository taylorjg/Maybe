using System;

namespace MaybeApp
{
    public static partial class Maybe
    {
        // Lift the simple mapping function, 'f', which only knows how to map a
        // T1 value into a T2 value so that we can use it to go from a Maybe<T1>
        // to a Maybe<T2>.
        public static Maybe<T2> Select<T1, T2>(this Maybe<T1> m, Func<T1, T2> f)
        {
            return m.IsJust ? Return(f(m.FromJust)) : Nothing<T2>();
        }

        // Here 'f' returns a Maybe<T2> rather than a raw T2. This is also known
        // as a FlatMap operation. This is the same as a monadic Bind operation.
        public static Maybe<T2> SelectMany<T1, T2>(this Maybe<T1> m, Func<T1, Maybe<T2>> f)
        {
            return m.Bind(f);
        }

        // Here we have an additional transform function, 'f2', which takes a T1 and a T2
        // and returns a T3. In order to gain access to the raw T1 and T2 values, we need
        // to perform two Bind operations.
        public static Maybe<T3> SelectMany<T1, T2, T3>(this Maybe<T1> m, Func<T1, Maybe<T2>> f1, Func<T1, T2, T3> f2)
        {
            return m.Bind(a =>
                f1(a).Bind(b =>
                    Return(f2(a, b))));
        }
    }
}
