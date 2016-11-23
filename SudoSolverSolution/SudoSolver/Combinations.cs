using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudoSolver
{
    public class Combinations<T>
    {
        /// <summary>
        /// Generate combinations of the items in a list.
        /// </summary>
        /// <param name="source">The list whose combinations will be generated.</param>
        /// <param name="comboLength">Length of combinations. Limited to less than equal to size of the source list.</param>
        /// <param name="startIndex">Optional index to start from. Will only get combinations from that index on. Meant to be used for recursive solution, not outside the class.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Get(List<T> source, int comboLength, int startIndex = 0)
        {
            if (comboLength == 1)
            {
                for (var i = startIndex; i < source.Count; i++)
                {
                    yield return new List<T> { source[i] };
                }
            }
            else
            {

                for (var i = startIndex; i <= source.Count - comboLength; i++)
                {
                    foreach (var combo in Get(source, comboLength - 1, i + 1))
                    {
                        var prefix = new List<T> { source[i] };
                        yield return prefix.Concat(combo);
                    }
                }
            }

            yield break;
        }
    }
}
