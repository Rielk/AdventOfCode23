
using AdventOfCode23Utilities;

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

	public int GetSplitValue(int smudges = 0) => GetSplitValueAsync(smudges).Result;
	public async Task<int> GetSplitValueAsync(int smudges = 0)
	{
		(bool, int)[] results = await Task.WhenAll(ColumnSymmetryAsync(smudges), RowSymmetryAsync(smudges));
		(bool colHasSymmetry, int LeftColumn) = results[0];
		(bool rowHasSymmetry, int TopRow) = results[1];
		if (colHasSymmetry && rowHasSymmetry)
			throw new NotImplementedException("Found both horizontal and vertical symmetry");
		else if (colHasSymmetry)
			return LeftColumn + 1;
		else if (rowHasSymmetry)
			return 100 * (TopRow + 1);

		throw new NotImplementedException("Couldn't find any symmetry");
	}

	private Task<(bool HasSymmetry, int LeftColumn)> ColumnSymmetryAsync(int smudges)
	{
		return Task.Run(() =>
		{
			bool hasSymetry = ColumnSymmetry(out int leftColumn, smudges);
			return (hasSymetry, leftColumn);
		});
	}

	private bool ColumnSymmetry(out int leftColumn, int smudges) => GeneralSymmetry(Values.GetColumn, Width, out leftColumn, smudges);

	private Task<(bool HasSymmetry, int TopRow)> RowSymmetryAsync(int smudges)
	{
		return Task.Run(() =>
		{
			bool hasSymetry = RowSymmetry(out int topRows, smudges);
			return (hasSymetry, topRows);
		});
	}

	private bool RowSymmetry(out int topRows, int smudges) => GeneralSymmetry(Values.GetRow, Height, out topRows, smudges);

	private static bool GeneralSymmetry(Func<int, IEnumerable<short>> lineGenerator, int limit, out int result, int targetSmudges)
	{
		for (int beforeLine = 0; beforeLine < limit - 1; beforeLine++)
		{
			int MaxOffset = Math.Min(beforeLine, limit - beforeLine - 2);
			bool isSymetry = true;
			int smudgesRequired = 0;
			for (int offset = 0; offset <= MaxOffset; offset++)
			{
				smudgesRequired += CompareSequences(targetSmudges - smudgesRequired, lineGenerator(beforeLine - offset), lineGenerator(beforeLine + 1 + offset));
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
}
