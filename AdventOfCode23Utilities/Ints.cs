namespace AdventOfCode23Utilities;
public static class Ints
{
	public static bool IsEven(int i) => i % 2 == 0;
	public static bool IsOdd(int i) => i % 2 != 0;

	public static void Divide(int x, int by, out int quotient, out int remainder)
	{
		remainder = x % by;
		quotient = x / by;
	}
}
