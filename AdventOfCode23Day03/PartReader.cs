using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode23Day03;
internal class PartReader
{
	private const char BlankChar = '.';
	private const char CogChar = '*';

	private List<int>? Parts { get; set; }

	private List<int>? Cogs { get; set; }

	public int Width { get; }
	public int Height { get; }
	private char[,] CharGrid { get; }

	public PartReader(string partGrid)
	{
		Width = partGrid.Split(Environment.NewLine).First().Length;
		Height = partGrid.Split(Environment.NewLine).Length;

		CharGrid = new char[Width, Height];
		foreach ((int j, string line) in partGrid.Split(Environment.NewLine).Select((line, j) => (j, line)))
			foreach ((int i, char ch) in line.Select((ch, i) => (i, ch)))
				CharGrid[i, j] = ch;
	}

	public List<int> GetParts()
	{
		if (Parts != null) return [.. Parts];

		Parts = [];
		for (int i = 0; i < Width; i++)
			for (int j = 0; j < Height; j++)
			{
				char ch = CharGrid[i, j];
				if (ch != BlankChar && !char.IsDigit(ch))
					foreach (int number in GetAdjacentNumberPositions(new(i, j)).Select(MakeNumberAt))
						Parts.Add(number);
			}
		return [.. Parts];
	}

	public List<int> GetCogs()
	{
		if (Cogs != null) return [.. Cogs];

		Cogs = [];
		for (int i = 0; i < Width; i++)
			for (int j = 0; j < Height; j++)
			{
				if (CharGrid[i, j] == CogChar)
				{
					var adjacentLocations = GetAdjacentNumberPositions(new(i, j)).Take(3).ToList();
					if (adjacentLocations.Count == 2)
						Cogs.Add(adjacentLocations.Select(MakeNumberAt).Aggregate((x, y) => x * y));
				}
			}
		return [.. Cogs];
	}

	private int MakeNumberAt(Location loc)
	{
		int y = loc.Y;
		int x = loc.X;
		if (!GetNumberAt(x, y, out int? initNumber)) throw new ArgumentException($"Couldn't find a number to start at {loc}");
		int number = initNumber.Value;

		int xOffset = 1;
		while (GetNumberAt(x - xOffset, y, out int? additional))
		{
			number += additional.Value * (int)Math.Pow(10, xOffset);
			xOffset++;
		}

		xOffset = 1;
		while (GetNumberAt(x + xOffset, y, out int? additional))
		{
			number *= 10;
			number += additional.Value;
			xOffset++;
		}
		return number;
	}

	private bool GetNumberAt(int x, int y, [NotNullWhen(true)] out int? value)
	{
		if (x < 0 || x >= Width || y < 0 || y >= Height)
		{
			value = null;
			return false;
		}

		char c = CharGrid[x, y];
		if (char.IsDigit(c))
		{
			value = c - '0';
			return true;
		}
		value = null;
		return false;
	}

	private IEnumerable<Location> GetAdjacentNumberPositions(Location loc)
	{
		if (CheckForDigit(loc, Direction.W, out Location? WLoc))
			yield return WLoc;
		if (CheckForDigit(loc, Direction.E, out Location? ELoc))
			yield return ELoc;

		if (CheckForDigit(loc, Direction.N, out Location? NLoc))
			yield return NLoc;
		else
		{
			if (CheckForDigit(loc, Direction.NW, out Location? NWLoc))
				yield return NWLoc;
			if (CheckForDigit(loc, Direction.NE, out Location? NELoc))
				yield return NELoc;
		}

		if (CheckForDigit(loc, Direction.S, out Location? SLoc))
			yield return SLoc;
		else
		{
			if (CheckForDigit(loc, Direction.SW, out Location? SWLoc))
				yield return SWLoc;
			if (CheckForDigit(loc, Direction.SE, out Location? SELoc))
				yield return SELoc;
		}
	}

	private bool CheckForDigit(Location startLoc, Direction dir, [NotNullWhen(true)] out Location? adjLoc)
	{
		if (ApplyDirection(startLoc, dir, out adjLoc))
			return char.IsDigit(CharGrid[adjLoc.X, adjLoc.Y]);
		return false;
	}

	private bool ApplyDirection(Location startLoc, Direction dir, [NotNullWhen(true)] out Location? adjLoc)
	{
		int i = startLoc.X; int j = startLoc.Y;
		adjLoc = dir switch
		{
			Direction.N => new(i, j - 1),
			Direction.NE => new(i + 1, j - 1),
			Direction.E => new(i + 1, j),
			Direction.SE => new(i + 1, j + 1),
			Direction.S => new(i, j + 1),
			Direction.SW => new(i - 1, j + 1),
			Direction.W => new(i - 1, j),
			Direction.NW => new(i - 1, j - 1),
			_ => throw new NotImplementedException(),
		};
		if (adjLoc.X < 0 || adjLoc.X >= Width || adjLoc.Y < 0 || adjLoc.Y >= Height) return false;

		return true;
	}
}
