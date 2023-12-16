using AdventOfCode23Utilities;

namespace AdventOfCode23Day16;
internal class MirrorGrid
{
	private Mirror[,] Mirrors { get; }

	public int Width { get; }
	public int Height { get; }

	private int? totalEnergized = null;
	public int TotalEnergized => GetTotalEnergized();

	public MirrorGrid(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;

		Mirrors = new Mirror[Width, Height];
		foreach ((string s, int y) in input.Select((s, i) => (s, i)))
			foreach ((char c, int x) in s.Select((c, i) => (c, i)))
				Mirrors[x, y] = c.ToMirror();
	}

	private int GetTotalEnergized()
	{
		if (totalEnergized.HasValue) return totalEnergized.Value;
		var pathHistory = new List<Direction>[Width, Height];
		pathHistory.FirstFill();
		TraceLight(0, 0, Direction.E, pathHistory);

		totalEnergized = 0;
		foreach (List<Direction> pointHistory in pathHistory)
			if (pointHistory.Count > 0)
				totalEnergized++;

		return totalEnergized.Value;
	}

	private void TraceLight(int xStart, int yStart, Direction initialDirection, List<Direction>[,] pathHistory)
	{
		List<(int x, int y, Direction d)> todo = [(xStart, yStart, initialDirection)];

		while (todo.Count > 0)
		{
			(int x, int y, Direction direction) = todo[0];
			todo.RemoveAt(0);

			Mirror mirror = Mirrors[x, y];
			List<Direction> nextDirections = mirror.NewDirections(direction);
			foreach (Direction nextDirection in nextDirections)
			{
				if (TryAddDirectionToHistory(x, y, nextDirection, pathHistory))
				{
					(int newX, int newY) = nextDirection.NextPosition(x, y);
					if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
						todo.Add((newX, newY, nextDirection));
				}
			}
		}

		static bool TryAddDirectionToHistory(int x, int y, Direction direction, List<Direction>[,] pathHistory)
		{
			List<Direction> pointHistory = pathHistory[x, y];
			if (pointHistory.Contains(direction))
				return false;
			pointHistory.Add(direction);
			return true;
		}
	}
}
