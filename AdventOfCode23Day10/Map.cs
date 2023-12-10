namespace AdventOfCode23Day10;
internal class Map
{
	private PipeDirection[,] Pipes { get; }
	private int Width { get; }
	private int Height { get; }

	public Location Start { get; }

	private List<Direction>? loop = null;
	public List<Direction> Loop { get => GetLoop(); }

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

	public List<Direction> GetLoop()
	{
		if (loop != null) return loop;

		foreach (Direction initialDirection in Directions.Cardinal)
		{
			List<Direction> path = [];
			Location location = Start;
			Direction direction = initialDirection;

			while (true)
			{
				path.Add(direction);
				location = location.ApplyDirection(direction);
				PipeDirection pipeDirection = GetPipeDirection(location);
				direction = pipeDirection.NextDirection(direction);
				if (direction == Direction.DeadEnd)
					break;
				if (direction == Direction.Start)
				{
					loop = path;
					return path;
				}
			}
		}
		throw new NotImplementedException("There is no valid loop.");
	}
}
