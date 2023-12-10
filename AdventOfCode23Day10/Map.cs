namespace AdventOfCode23Day10;
internal class Map
{
	private PipeDirection[,] Pipes { get; }
	private int Width { get; }
	private int Height { get; }

	public Location Start { get; }

	private List<Location>? loop = null;
	public List<Location> Loop { get => GetLoop(); }

	public Map(IEnumerable<IEnumerable<char>> characters)
	{
		Start = new(-1, -1);
		Width = characters.First().Count();
		Height = characters.Count();
		Pipes = new PipeDirection[Width, Height];
		foreach ((IEnumerable<char> lineChars, int y) in characters.Select((lineChars, y) => (lineChars, y)))
			foreach ((char c, int x) in lineChars.Select((c, x) => (c, x)))
			{
				var pipeDirection = c.ToPipeDirection();
				Pipes[x, y] = pipeDirection;
				if (pipeDirection == PipeDirection.Start)
					Start = new(x, y);
			}
	}

	public PipeDirection GetPipeDirection(Location location)
	{
		if (location.X < 0 || location.X >= Width || location.Y < 0 || location.Y >= Height)
			return PipeDirection.Ground;
		return Pipes[location.X, location.Y];
	}

	public List<Location> GetLoop()
	{
		if (loop != null) return loop;

		foreach (Direction initialDirection in Directions.Cardinal)
		{
			List<Location> path = [];
			Location location = Start;
			Direction nextDirection, direction = initialDirection;

			while (true)
			{
				path.Add(location);
				location = location.ApplyDirection(direction);
				PipeDirection pipeDirection = GetPipeDirection(location);
				nextDirection = pipeDirection.NextDirection(direction);
				if (nextDirection == Direction.DeadEnd)
					break;
				if (nextDirection == Direction.Start)
				{
					loop = path;
					Pipes[Start.X, Start.Y] = direction.WithOutDirection(initialDirection);
					return path;
				}
				direction = nextDirection;
			}
		}
		throw new NotImplementedException("There is no valid loop.");
	}



	public int FindEnclosedArea()
	{
		List<Location> path = GetLoop();
		int count = 0;

		IEnumerable<IGrouping<int, Location>> yIntersects = path.Where(l => GetPipeDirection(l).IsHorizontal()).GroupBy(l => l.X);
		foreach (IGrouping<int, Location> yIntersect in yIntersects)
		{
			int x = yIntersect.Key;
			var yValues = yIntersect.OrderBy(l => l.Y).ToList();
			bool inside = true;
			foreach ((Location First, Location Second) in yValues.Zip(yValues.Skip(1)))
			{
				if (GetPipeDirection(First) is PipeDirection.SE)
				{
					if (GetPipeDirection(Second) is PipeDirection.NE)
						inside = !inside;
					continue;
				}

				if (GetPipeDirection(First) is PipeDirection.SW)
				{
					if (GetPipeDirection(Second) is PipeDirection.NW)
						inside = !inside;
					continue;
				}

				if (inside)
					count += Second.Y - First.Y - 1;
				inside = !inside;
			}
		}
		return count;
	}
}
