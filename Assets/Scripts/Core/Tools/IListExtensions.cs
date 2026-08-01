using System;
using System.Collections.Generic;

namespace Core.Tools
{
    public static class IListExtensions
    {
        private static readonly Random random = new();

        public static void Shuffle<T>(this IList<T> list, Random r = null)
        {
            r ??= random;

            for (var i = list.Count - 1; i > 0; i--)
            {
                var j = r.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
