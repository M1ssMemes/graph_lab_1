using System.Collections.Generic;

namespace Graph_Lab_1
{
    internal static class EnumExtensions
    {
        public static IEnumerable<T> AlternateElements<T>(this IEnumerable<T> source, List<int> num)
        {
            var i = 0;
            var k = num[i];
            var paint = true;
            foreach (var element in source)
            {

                if (paint)
                    yield return element;

                if (k > 0)
                    k--;

                if (k <= 0)
                {
                    i++;

                    if (i == num.Count)
                        i = 0;

                    k = num[i];
                    paint = !paint;
                }

            }
        }
    }
}