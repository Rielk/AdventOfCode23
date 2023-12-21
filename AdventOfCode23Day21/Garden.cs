

namespace AdventOfCode23Day21;
internal class Garden
{
	private Tile[,] Tiles { get; }

	public int Width { get; }
	public int Height { get; }

	public Location Start { get; }

	private List<Location> CurrentLocations { get; set; }
	public int TotalPossible => CurrentLocations.Count;

	public const char StartChar = 'S';
	public Garden(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;

		Tiles = new Tile[Width, Height];
		foreach ((string s, int y) in input.Select((x, i) => (x, i)))
			foreach ((char c, int x) in s.Select((x, i) => (x, i)))
			{
				if (c == StartChar)
					Start = new(x, y);
				Tiles[x, y] = c.ToTile();
			}

		CurrentLocations = [Start];
	}

	internal void TakeSteps(int steps)
	{
		foreach (int _ in Enumerable.Range(0, steps))
		{
			List<Location> newLocations = [];
			foreach (Location location in CurrentLocations)
				foreach (Location adjacentLocation in location.AdjacentLocation())
					if (GetTile(adjacentLocation) == Tile.Plot && !newLocations.Contains(adjacentLocation))
						newLocations.Add(adjacentLocation);
			CurrentLocations = newLocations;
		}
	}

	public Tile GetTile(Location location)
	{
		if (location.X < 0 || location.X >= Width || location.Y < 0 || location.Y >= Height)
			return Tile.Rock;
		return Tiles[location.X, location.Y];
	}
}
