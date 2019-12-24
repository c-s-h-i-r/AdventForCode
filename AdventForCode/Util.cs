using System;
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
        public static long[] ResizeArray(long[] array, long index)
        {
            // Create a new array and copy the values over
            long[] newArray = (long[])Array.CreateInstance(typeof(long), index + 1);
            for (var i = 0; i < Math.Min(array.Length, newArray.Length); i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

    }
}