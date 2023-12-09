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

	public long GetValue(int index) => GetNValues(1, index).First();

	public IEnumerable<long> GetNValues(int n, int firstIndex = 0)
	{
		if (n == 0) yield break;

		if (Differences == null)
		{
			foreach (int _ in Enumerable.Repeat(0, n))
				yield return Start;
			yield break;
		}

		long val = Start;
		if (firstIndex < 0)
			foreach (long dif in Differences.GetNValues(-firstIndex, firstIndex).Reverse())
				val -= dif;
		else
			foreach (long dif in Differences.GetNValues(firstIndex, 0).Reverse())
				val += dif;


		yield return val;

		foreach (long dif in Differences.GetNValues(n - 1, firstIndex))
		{
			val += dif;
			yield return val;
		}
	}

	public IEnumerator<long> GetEnumerator() => GetNValues(Length).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
