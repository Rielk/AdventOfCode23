

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

	public int PossibleLocationsAfterSteps(int steps)
	{
		while (!MaxesFound && PossibilityHistory.Count < steps + 1)
			TakeStep();
		if (MaxesFound && steps >= MaxAt)
			return IsEven(steps) ? EvenMax : OddMax;
		else
			return PossibilityHistory[steps];
	}

	private void TakeStep()
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
			SetMaxesFromHistory();
		else
			PossibilityHistory.Add(possibleCount);
	}

	private void SetMaxesFromHistory()
	{
		MaxesFound = true;
		int last = PossibilityHistory[^1];
		int secondLast = PossibilityHistory[^2];

		if (IsEven(last))
			(OddMax, EvenMax) = (secondLast, last);
		else
			(OddMax, EvenMax) = (last, secondLast);

		MaxAt = PossibilityHistory.Count - 1;
	}

	private static bool IsEven(int i) => i % 2 == 0;

	private int CountPossible()
	{
		int count = 0;
		foreach (bool value in StandOptions)
			if (value)
				count++;
		return count;
	}
}
