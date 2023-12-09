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
		Length = 1;
		foreach (long second in values.Skip(1))
		{
			diffs.Add(second - first);
			first = second;
			Length++;
		}

		if (diffs.All(d => d.Equals(0)))
			Differences = null;
		else
			Differences = new(diffs);
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
		int index = 0;

		IEnumerable<long> preDifs;
		if (firstIndex < 0)
		{
			preDifs = Differences.GetNValues(-firstIndex, firstIndex).ToList();
			foreach (long dif in preDifs.Reverse())
			{
				val -= dif; index--;
			}
		}
		else
			preDifs = Enumerable.Empty<long>();

		int count = 0;
		if (index >= firstIndex)
		{
			count++; yield return val;
		}
		foreach (long dif in preDifs.Concat(Differences.GetNValues(n + firstIndex - 1)))
		{
			if (count >= n)
				break;
			val += dif; index++;
			if (index >= firstIndex)
			{
				count++; yield return val;
			}
		}
	}

	public IEnumerator<long> GetEnumerator() => GetNValues(Length).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
