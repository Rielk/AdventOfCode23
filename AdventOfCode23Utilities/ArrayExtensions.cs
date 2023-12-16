namespace AdventOfCode23Utilities;
public static class ArrayExtensions
{
	public static IEnumerable<T> GetColumn<T>(this T[,] array, int x)
	{
		for (int y = 0; y < array.GetLength(1); y++)
			yield return array[x, y];
	}
	public static IEnumerable<T> GetColumn<T>(this T[,] array, int x, int initialY)
	{
		for (int y = initialY; y < array.GetLength(1); y++)
			yield return array[x, y];
	}
	public static IEnumerable<T> GetReverseColumn<T>(this T[,] array, int x)
	{
		for (int y = array.GetLength(1) - 1; y >= 0; y--)
			yield return array[x, y];
	}
	public static IEnumerable<T> GetReverseColumn<T>(this T[,] array, int x, int initialY)
	{
		for (int y = initialY; y >= 0; y--)
			yield return array[x, y];
	}

	public static IEnumerable<T> GetRow<T>(this T[,] array, int y)
	{
		for (int x = 0; x < array.GetLength(0); x++)
			yield return array[x, y];
	}
	public static IEnumerable<T> GetRow<T>(this T[,] array, int y, int initialX)
	{
		for (int x = initialX; x < array.GetLength(0); x++)
			yield return array[x, y];
	}
	public static IEnumerable<T> GetReverseRow<T>(this T[,] array, int y)
	{
		for (int x = array.GetLength(0) - 1; x >= 0; x--)
			yield return array[x, y];
	}
	public static IEnumerable<T> GetReverseRow<T>(this T[,] array, int y, int initialX)
	{
		for (int x = initialX; x >= 0; x--)
			yield return array[x, y];
	}

	public static void FirstFill<T>(this List<T>[,] listArray)
	{
		for (int x = 0; x < listArray.GetLength(0); x++)
			for (int y = 0; y < listArray.GetLength(1); y++)
				listArray[x, y] = [];
	}
}
