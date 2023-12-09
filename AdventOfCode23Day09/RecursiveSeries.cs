using System.Collections;

namespace AdventOfCode23Day09;
internal class RecursiveSeries : IEnumerable<long>
{
	public long Start { get; }
	public RecursiveSeries? Differences { get; }
	public int Length { get; }

	public RecursiveSeries(IEnumerable<long> values)
	{
		Start = values.First();
		List<long> diffs = [];
		long first = values.First();
		foreach (long second in values.Skip(1))
		{
			diffs.Add(second - first);
			first = second;
		}

		if (diffs.All(d => d.Equals(0)))
			Differences = null;
		else
			Differences = new(diffs);
		Length = values.Count();
	}

	public IEnumerable<long> GetNValue(int n)
	{
		if (n == 0) yield break;

		yield return Start;
		long val = Start;
		foreach (long dif in Differences?.GetNValue(n - 1) ?? Enumerable.Repeat<long>(0, n - 1))
		{
			val += dif;
			yield return val;
		}
	}

	public IEnumerator<long> GetEnumerator() => GetNValue(Length).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
