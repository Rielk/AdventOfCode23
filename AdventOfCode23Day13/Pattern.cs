
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

	public int GetSplitValue()
	{
		if (splitValue.HasValue) return splitValue.Value;

		if (ColumnSymmetry(out int leftColumn))
			return (splitValue = leftColumn + 1).Value;
		if (RowSymmetry(out int topRow))
			return (splitValue = 100 * (topRow + 1)).Value;

		throw new NotImplementedException("Couldn't find any symmetry");
	}

	private bool ColumnSymmetry(out int leftColumn) => GeneralSymmetry(GetColumn, Width, out leftColumn);

	private bool RowSymmetry(out int topRow) => GeneralSymmetry(GetRow, Height, out topRow);

	private static bool GeneralSymmetry(Func<int, IEnumerable<short>> lineGenerator, int limit, out int result)
	{
		for (int beforeLine = 0; beforeLine < limit - 1; beforeLine++)
		{
			int MaxOffset = Math.Min(beforeLine, limit - beforeLine - 2);
			bool isSymetry = true;
			for (int offset = 0; offset <= MaxOffset; offset++)
			{
				if (lineGenerator(beforeLine - offset).SequenceEqual(lineGenerator(beforeLine + 1 + offset)))
					continue;
				else
				{
					isSymetry = false;
					break;
				}
			}
			if (isSymetry)
			{
				result = beforeLine;
				return true;
			}
		}
		result = -1;
		return false;
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
