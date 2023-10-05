using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class GelUtil
{
	// because % is a remainder on C#, not a modulus... erck.
	// remainder -> can return negative numbers
	// modulus -> always positive
	// blog: https://blogs.msdn.microsoft.com/ericlippert/2011/12/05/whats-the-difference-remainder-vs-modulus/
	public static int Mod(int n, int m)
	{
		return ((n % m) + m) % m;
	}

	public static int WordCount(this String str)
	{
		return str.Split(new char[] { ' ', '.', '?' },
						 StringSplitOptions.RemoveEmptyEntries).Length;
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static void Shuffle<T>(this IList<T> list, System.Random rng)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static T Draw<T>(this IList<T> list)
	{
		T currentFirst = list.First();
		list.RemoveAt(0);
		return currentFirst;
	}

	public static T DrawLast<T>(this IList<T> list)
	{
		T currentFirst = list.Last();
		list.RemoveAt(list.Count - 1);
		return currentFirst;
	}
}