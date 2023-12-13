
namespace AdventOfCode23Day13;
internal class Pattern
{
	private short[,] Values { get; }
	public int Width { get; }
	public int Height { get; }

	public Pattern(List<string> input)
	{
		Height = input.Count;
		Width = input[0].Length;
		Values = new short[Width, Height];
		foreach ((string line, int y) in input.Select((line, y) => (line, y)))
			foreach ((short s, int x) in line.Select((c, x) => ((short)c, x)))
				Values[x, y] = s;
	}

	public int GetSplitValue(int smudges = 0)
	{
		if (ColumnSymmetry(out int leftColumn, smudges))
			return leftColumn + 1;
		if (RowSymmetry(out int topRow, smudges))
			return 100 * (topRow + 1);

		throw new NotImplementedException("Couldn't find any symmetry");
	}

	private bool ColumnSymmetry(out int leftColumn, int smudges) => GeneralSymmetry(GetColumn, Width, out leftColumn, smudges);

	private bool RowSymmetry(out int topRows, int smudges) => GeneralSymmetry(GetRow, Height, out topRows, smudges);

	private static bool GeneralSymmetry(Func<int, IEnumerable<short>> lineGenerator, int limit, out int result, int targetSmudges)
	{
		for (int beforeLine = 0; beforeLine < limit - 1; beforeLine++)
		{
			int MaxOffset = Math.Min(beforeLine, limit - beforeLine - 2);
			bool isSymetry = true;
			int smudgesRequired = 0;
			for (int offset = 0; offset <= MaxOffset; offset++)
			{
				smudgesRequired += CompareSequences(targetSmudges, lineGenerator(beforeLine - offset), lineGenerator(beforeLine + 1 + offset));
				if (smudgesRequired > targetSmudges)
				{
					isSymetry = false;
					break;
				}
			}
			if (isSymetry && smudgesRequired == targetSmudges)
			{
				result = beforeLine;
				return true;
			}
		}
		result = -1;
		return false;
	}

	private static int CompareSequences(int maxDifferences, IEnumerable<short> x, IEnumerable<short> y)
	{
		int differences = 0;
		foreach ((short First, short Second) in x.Zip(y))
		{
			if (First != Second)
			{
				differences++;
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
