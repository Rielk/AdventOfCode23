using System.Collections;

namespace AdventOfCode23Day08;
internal class InfiniteLoop<T>(IEnumerable<T> baseEnumerable) : IEnumerable<T>
{
	public IEnumerable<T> BaseEnumerable { get; } = baseEnumerable;

	public IEnumerable<T> Enumerable()
	{
		while (true)
			foreach (T c in BaseEnumerable)
				yield return c;
	}

	public IEnumerator<T> GetEnumerator()
	{
		foreach (T t in Enumerable()) yield return t;
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
