using AdventOfCode23EnclosedSpace;

namespace AdventOfCode23Day10;
internal class Map
{
	private PathDirection[,] Pipes { get; }
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
		Pipes = new PathDirection[Width, Height];
		foreach ((IEnumerable<char> lineChars, int y) in characters.Select((lineChars, y) => (lineChars, y)))
			foreach ((char c, int x) in lineChars.Select((c, x) => (c, x)))
			{
				PathDirection pipeDirection = c.ToPipeDirection();
				Pipes[x, y] = pipeDirection;
				if (pipeDirection == PathDirection.Start)
					Start = new(x, y);
			}
	}

	public PathDirection GetPipeDirection(Location location)
	{
		if (location.X < 0 || location.X >= Width || location.Y < 0 || location.Y >= Height)
			return PathDirection.None;
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
				PathDirection pipeDirection = GetPipeDirection(location);
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

		return path.FindEnclosedArea(GetPipeDirection, false);
	}
}
