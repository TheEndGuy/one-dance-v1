using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneDance.Core.Utilities
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Retorna uma nova enumeração contendo os elementos randomizados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var rand = new AsyncRandom();
            var elements = enumerable.ToArray();

            for (var i = elements.Length - 1; i > 0; i--)
            {
                var swapIndex = rand.Next(i + 1);
                var tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }

            return elements;
        }
    }
}