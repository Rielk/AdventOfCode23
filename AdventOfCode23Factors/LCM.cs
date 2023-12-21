namespace AdventOfCode23Factors;
public static class LCM
{
	public static int Find(int a, int b)
	{
		return (a * b) / GCF.Find(a, b);
	}

	public static long Find(long a, long b)
	{
		return (a * b) / GCF.Find(a, b);
	}

	public static int Find(IEnumerable<int> numbers) => numbers.Aggregate(Find);

	public static long Find(IEnumerable<long> numbers) => numbers.Aggregate(Find);
}
