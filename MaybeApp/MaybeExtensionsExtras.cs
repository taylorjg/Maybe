using System.Collections.Generic;

namespace MaybeApp
{
    public static partial class Maybe
    {
        public static Maybe<TValue> Lookup<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? Just(value) : Nothing<TValue>();
        }
    }
}
