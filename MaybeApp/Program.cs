using System;
using System.Collections.Generic;

namespace MaybeApp
{
    internal static class Program
    {
        public static void Main()
        {
            var dictionary = new Dictionary<string, int>
            {
                {"one", 1},
                {"two", 4},
                {"three", 9},
                {"four", 16}
            };

            var result1 =
                from x in dictionary.Lookup("two")
                from y in ToStringIfLessThenTen(x)
                select y;

            var result2 =
                from x in dictionary.Lookup("four")
                from y in ToStringIfLessThenTen(x)
                select y;

            var result3 =
                from x in dictionary.Lookup("five")
                from y in ToStringIfLessThenTen(x)
                select y;

            PrintMaybe(result1);
            PrintMaybe(result2);
            PrintMaybe(result3);

            PrintResult(result1.OrElse("some default value"));
            PrintResult(result2.OrElse("some default value"));
            PrintResult(result3.OrElse("some default value"));
        }

        private static Maybe<string> ToStringIfLessThenTen(int n)
        {
            return n < 10 ? Maybe.Just(Convert.ToString(n)) : Maybe.Nothing<string>();
        }

        private static void PrintMaybe(Maybe<string> m)
        {
            Console.WriteLine(m.IsNothing ? "Nothing" : m.FromJust);
        }

        private static void PrintResult(string s)
        {
            Console.WriteLine(s);
        }
    }
}
