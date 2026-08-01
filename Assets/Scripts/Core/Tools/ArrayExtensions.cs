using System;

namespace Core.Tools
{
    public static class ArrayExtensions
    {
        private static readonly Random random = new();

        public static void Shuffle<T>(this T[] array, Random r = null)
        {
            r ??= random;

            for (var i = array.Length - 1; i > 0; i--)
            {
                var j = r.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}
