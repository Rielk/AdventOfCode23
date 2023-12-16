using AdventOfCode23Utilities;

namespace AdventOfCode23Day16;
internal class MirrorGrid
{
	private Mirror[,] Mirrors { get; }

	public int Width { get; }
	public int Height { get; }

	public MirrorGrid(string[] input)
	{
		Height = input.Length;
		Width = input[0].Length;

		Mirrors = new Mirror[Width, Height];
		foreach ((string s, int y) in input.Select((s, i) => (s, i)))
			foreach ((char c, int x) in s.Select((c, i) => (c, i)))
				Mirrors[x, y] = c.ToMirror();
	}

	public int FindMaxEnergized()
	{
		int max = int.MinValue;
		(Direction direction, int start, int count)[] ranges = [(Direction.N, 0, Width), (Direction.S, 0, Width), (Direction.E, 0, Height), (Direction.W, 0, Height)];
		foreach ((Direction direction, int start, int count) in ranges)
			foreach (int position in Enumerable.Range(0, count))
				max = Math.Max(max, FindTotalEnergized(direction, position));
		return max;
	}

	public int FindTotalEnergized(Direction entryDirection, int position)
	{
		var pathHistory = new List<Direction>[Width, Height];
		pathHistory.FirstFill();

		(int startX, int startY) = entryDirection switch
		{
			Direction.N => (position, Height - 1),
			Direction.S => (position, 0),
			Direction.E => (0, position),
			Direction.W => (Width - 1, position),
			_ => throw new NotImplementedException(),
		};
		TraceLight(startX, startY, entryDirection, pathHistory);

		int totalEnergized = 0;
		foreach (List<Direction> pointHistory in pathHistory)
			if (pointHistory.Count > 0)
				totalEnergized++;

		return totalEnergized;
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
