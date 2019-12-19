using System.Collections.Generic;
using System.IO;

namespace AdventForCode
{
    public static class Util
    {
        /// <summary>
        /// Read input from given file and return the lines.
        /// </summary>
        public static string[] ReadInput(string filePath, string separator = "\n")
        {
            using var stream = new StreamReader(filePath);
            return stream.ReadToEnd().Trim().Split(separator);
        }

        public static void FindPermutations(int[] list, List<int[]> permutations, int pointer = 0)
        {
            if (pointer == list.Length)
            {
                permutations.Add(list);
                return;
            }
            for (var i = pointer; i < list.Length; i++)
            {
                var permutation = (int[])list.Clone();
                permutation[pointer] = list[i];
                permutation[i] = list[pointer];
                FindPermutations(permutation, permutations, pointer + 1);
            }
        }
    }
}