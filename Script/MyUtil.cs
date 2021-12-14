using System.Collections.Generic;

public static class MyUtil
{
	private static System.Random random = new System.Random();

#region Public Functions

	// randomize list. from http://stackoverflow.com/questions/273313/randomize-a-listt/1262619#1262619
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	// Random element from list
	public static T Ramdom<T>(this IList<T> list)
	{
		return list[random.Next(list.Count)];
	}

	// last element from list
	public static T Last<T>(this IList<T> list)
	{
		return list[list.Count - 1];
	}
#endregion
}
