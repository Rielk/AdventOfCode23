namespace AdventOfCode23Factors;
public static class GCF
{
	public static int Find(int a, int b)
	{
		while (b != 0)
		{
			(a, b) = (b, a % b);
		}
		return a;
	}

	public static long Find(long a, long b)
	{
		while (b != 0)
		{
			(a, b) = (b, a % b);
		}
		return a;
	}

	public static int Find(IEnumerable<int> numbers) => numbers.Aggregate(Find);

	public static long Find(IEnumerable<long> numbers) => numbers.Aggregate(Find);
}
