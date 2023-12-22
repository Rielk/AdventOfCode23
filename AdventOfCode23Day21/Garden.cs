

using AdventOfCode23Utilities;

namespace AdventOfCode23Day21;
internal class Garden
{
	private Tile[,] Tiles { get; }
	private bool[,] StandOptions { get; set; }

	public int Width { get; }
	public int Height { get; }

	public Location Start { get; }

	public int Steps { get; private set; } = 0;

	public int TotalPossible => PossibilityHistory[^1];
	public List<int> PossibilityHistory { get; } = [1];

	public bool MaxesFound { get; private set; } = false;
	public int EvenMax { get; private set; } = -1;
	public int OddMax { get; private set; } = -1;
	public int MaxAt { get; private set; } = -1;

	public const char StartChar = 'S';
	public Garden(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;

		Tiles = new Tile[Width, Height];
		StandOptions = new bool[Width, Height];
		foreach ((string s, int y) in input.Select((x, i) => (x, i)))
			foreach ((char c, int x) in s.Select((x, i) => (x, i)))
			{
				if (c == StartChar)
					Start = new(x, y);
				Tiles[x, y] = c.ToTile();
			}
		StandOptions[Start.X, Start.Y] = true;
	}

	private Garden(Tile[,] tiles, Location start)
	{
		Tiles = tiles;
		Width = tiles.GetLength(0);
		Height = tiles.GetLength(1);
		Start = start;
		StandOptions = new bool[Width, Height];
		StandOptions[Start.X, Start.Y] = true;
	}

	internal Garden WithStart(Location c)
	{
		return new(Tiles, c);
	}

	public void FindMaxes()
	{
		while (!MaxesFound) TakeStep();
	}

	public int CountLocationsAfterSteps(int steps)
	{
		if (steps < 0) return 0;

		while (!MaxesFound && PossibilityHistory.Count < steps + 1)
			TakeStep();
		if (MaxesFound && steps >= MaxAt)
			return Ints.IsEven(steps) ? EvenMax : OddMax;
		else
			return PossibilityHistory[steps];
	}

	private bool TakeStep()
	{
		bool[,] newStandOptions = new bool[Width, Height];
		foreach (int y in Enumerable.Range(0, Height))
			foreach (int x in Enumerable.Range(0, Width))
			{
				var checkLocation = new Location(x, y);
				if (!Tiles.TryGetValue(checkLocation.X, checkLocation.Y, out Tile tile))
					continue;
				if (tile != Tile.Plot)
					continue;
				foreach (Location adjacentLocation in checkLocation.AdjacentLocations())
					if (StandOptions.TryGetValue(adjacentLocation.X, adjacentLocation.Y, out bool hasAdjacent))
						if (hasAdjacent)
						{
							newStandOptions[x, y] = true;
							break;
						}
			}
		StandOptions = newStandOptions;
		Steps++;


		int possibleCount = CountPossible();
		if (PossibilityHistory.Count >= 2 && possibleCount == PossibilityHistory[^2])
		{
			SetMaxesFromHistory();
			return false;
		}
		else
		{
			PossibilityHistory.Add(possibleCount);
			return true;
		}
	}

	private void SetMaxesFromHistory()
	{
		MaxesFound = true;
		int last = PossibilityHistory[^1];
		int secondLast = PossibilityHistory[^2];

		if (Ints.IsEven(last))
			(OddMax, EvenMax) = (secondLast, last);
		else
			(OddMax, EvenMax) = (last, secondLast);

		MaxAt = PossibilityHistory.Count - 1;
	}

	private int CountPossible()
	{
		int count = 0;
		foreach (bool value in StandOptions)
			if (value)
				count++;
		return count;
	}

	internal void AssertAssumption()
	{
		int midWidth = Width / 2;
		int midHeight = Height / 2;
		if (Tiles.GetColumn(midWidth).Any(t => t == Tile.Rock)) throw new Exception("Found a rock in the center column");
		if (Tiles.GetRow(midHeight).Any(t => t == Tile.Rock)) throw new Exception("Found a rock in the center row");

		if (Tiles.GetColumn(0).Any(t => t == Tile.Rock)) throw new Exception("Found a rock in the left column");
		if (Tiles.GetColumn(Width - 1).Any(t => t == Tile.Rock)) throw new Exception("Found a rock in the right column");
		if (Tiles.GetRow(0).Any(t => t == Tile.Rock)) throw new Exception("Found a rock in the top row");
		if (Tiles.GetRow(Height - 1).Any(t => t == Tile.Rock)) throw new Exception("Found a rock in the bottom row");
	}
}
