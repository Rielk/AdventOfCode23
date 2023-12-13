
namespace AdventOfCode23Day13;
internal class Pattern
{
	private short[,] Values { get; }
	public int Width { get; }
	public int Height { get; }

	private int? splitValue = null;
	public int SplitValue => GetSplitValue();

	private List<string> Input { get; }

	public Pattern(List<string> input)
	{
		Height = input.Count;
		Width = input[0].Length;
		Values = new short[Width, Height];
		foreach ((string line, int y) in input.Select((line, y) => (line, y)))
			foreach ((short s, int x) in line.Select((c, x) => ((short)c, x)))
				Values[x, y] = s;
		Input = input;
	}

	public bool FixSmudge()
	{
		List<(int x, int y)> differences;
		if (ColumnSymmetry(1, out int leftColumn, out differences))
		{
			splitValue = leftColumn + 1;
			return true;
		}
		if (RowSymmetry(1, out int topRow, out differences))
		{
			splitValue = 100 * (topRow + 1);
			return true;
		}

		throw new NotImplementedException("Couldn't find any symmetry");
	}

	public int GetSplitValue()
	{
		if (splitValue.HasValue) return splitValue.Value;

		if (ColumnSymmetry(out int leftColumn))
			return (splitValue = leftColumn + 1).Value;
		if (RowSymmetry(out int topRow))
			return (splitValue = 100 * (topRow + 1)).Value;

		throw new NotImplementedException("Couldn't find any symmetry");
	}

	private bool ColumnSymmetry(out int leftColumn) => ColumnSymmetry(0, out leftColumn, out _);
	private bool ColumnSymmetry(int smudges, out int leftColumn, out List<(int x, int y)> differences)
	{
		bool ret = GeneralSymmetry(GetColumn, Width, out leftColumn, smudges, out differences);
		return ret;
	}

	private bool RowSymmetry(out int topRows) => RowSymmetry(0, out topRows, out _);
	private bool RowSymmetry(int smudges, out int topRows, out List<(int x, int y)> differences)
	{
		bool ret = GeneralSymmetry(GetRow, Height, out topRows, smudges, out List<(int perpendicular, int parallel)> tmpDifferences);
		differences = tmpDifferences.Select(t => (t.parallel, t.perpendicular)).ToList();
		return ret;
	}

	private static bool GeneralSymmetry(Func<int, IEnumerable<short>> lineGenerator, int limit, out int result, int targetSmudges, out List<(int perpendicular, int parallel)> differences)
	{
		for (int beforeLine = 0; beforeLine < limit - 1; beforeLine++)
		{
			int MaxOffset = Math.Min(beforeLine, limit - beforeLine - 2);
			bool isSymetry = true;
			int smudgesRequired = 0;
			differences = [];
			for (int offset = 0; offset <= MaxOffset; offset++)
			{
				smudgesRequired += CompareSequences(targetSmudges, lineGenerator(beforeLine - offset), lineGenerator(beforeLine + 1 + offset), out List<int> lineDifferences);
				foreach (int ld in lineDifferences)
					differences.Add((beforeLine - offset, ld));
				if (smudgesRequired <= targetSmudges)
					continue;
				else
				{
					isSymetry = false;
					break;
				}
			}
			if (smudgesRequired == targetSmudges && isSymetry)
			{
				result = beforeLine;
				return true;
			}
		}
		result = -1;
		differences = [];
		return false;
	}

	private static int CompareSequences(int maxDifferences, IEnumerable<short> x, IEnumerable<short> y, out List<int> differentIndices)
	{
		int differences = 0;
		differentIndices = [];
		foreach ((short First, short Second, int index) in x.Zip(y).Select((t, i) => (t.First, t.Second, i)))
		{
			if (First != Second)
			{
				differences++;
				differentIndices.Add(index);
				if (differences > maxDifferences)
					return maxDifferences + 1;
			}
		}
		return differences;
	}

	public short GetValue(int x, int y) => Values[x, y];

	public IEnumerable<short> GetColumn(int x)
	{
		for (int y = 0; y < Height; y++)
			yield return GetValue(x, y);
	}

	public IEnumerable<short> GetRow(int y)
	{
		for (int x = 0; x < Width; x++)
			yield return GetValue(x, y);
	}
}
