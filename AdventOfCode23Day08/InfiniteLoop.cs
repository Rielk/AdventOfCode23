namespace AdventOfCode23Day08;
internal class InfiniteLoop<T>(IEnumerable<T> baseEnumerable)
{
	public IEnumerable<T> BaseEnumerable { get; } = baseEnumerable;

	public IEnumerable<T> Enumerable()
	{
		while (true)
			foreach (T c in BaseEnumerable)
				yield return c;
	}
}
