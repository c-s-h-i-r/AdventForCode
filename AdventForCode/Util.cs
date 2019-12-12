using System.Collections.Generic;

namespace AdventForCode
{
	public static class Util
	{
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
