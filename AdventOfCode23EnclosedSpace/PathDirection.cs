namespace AdventOfCode23EnclosedSpace;
public enum PathDirection
{
	NS, WE, NE, NW, SW, SE, None, Start
}

public static class PipeDirectionExtensions
{
	public static PathDirection WithOutDirection(this Direction inDir, Direction outDir) => (inDir, outDir) switch
	{
		(Direction.S, Direction.S) => PathDirection.NS,
		(Direction.N, Direction.N) => PathDirection.NS,

		(Direction.E, Direction.E) => PathDirection.WE,
		(Direction.W, Direction.W) => PathDirection.WE,

		(Direction.S, Direction.E) => PathDirection.NE,
		(Direction.W, Direction.N) => PathDirection.NE,

		(Direction.S, Direction.W) => PathDirection.NW,
		(Direction.E, Direction.N) => PathDirection.NW,

		(Direction.N, Direction.W) => PathDirection.SW,
		(Direction.E, Direction.S) => PathDirection.SW,

		(Direction.N, Direction.E) => PathDirection.SE,
		(Direction.W, Direction.S) => PathDirection.SE,

		_ => throw new NotImplementedException(),
	};

	public static Direction NextDirection(this PathDirection pipeDirection, Direction lastDirection)
	{
		return (pipeDirection, lastDirection) switch
		{
			(PathDirection.NS, Direction.S) => Direction.S,
			(PathDirection.NS, Direction.N) => Direction.N,
			(PathDirection.NS, _) => Direction.DeadEnd,

			(PathDirection.WE, Direction.E) => Direction.E,
			(PathDirection.WE, Direction.W) => Direction.W,
			(PathDirection.WE, _) => Direction.DeadEnd,

			(PathDirection.NE, Direction.S) => Direction.E,
			(PathDirection.NE, Direction.W) => Direction.N,
			(PathDirection.NE, _) => Direction.DeadEnd,

			(PathDirection.NW, Direction.S) => Direction.W,
			(PathDirection.NW, Direction.E) => Direction.N,
			(PathDirection.NW, _) => Direction.DeadEnd,

			(PathDirection.SW, Direction.N) => Direction.W,
			(PathDirection.SW, Direction.E) => Direction.S,
			(PathDirection.SW, _) => Direction.DeadEnd,

			(PathDirection.SE, Direction.N) => Direction.E,
			(PathDirection.SE, Direction.W) => Direction.S,
			(PathDirection.SE, _) => Direction.DeadEnd,

			(PathDirection.None, _) => Direction.DeadEnd,
			(PathDirection.Start, _) => Direction.Start,

			_ => throw new NotImplementedException(),
		};
	}

	public static bool IsHorizontal(this PathDirection pipeDirection) => pipeDirection switch
	{
		PathDirection.NS => false,
		PathDirection.WE => true,
		PathDirection.NE => true,
		PathDirection.NW => true,
		PathDirection.SW => true,
		PathDirection.SE => true,
		_ => throw new NotImplementedException(),
	};

	public static int FindEnclosedArea(this IEnumerable<Location> path, Func<Location, PathDirection> getPathDirection, bool includePath)
	{
		int count = 0;
		if (includePath)
			if (!path.TryGetNonEnumeratedCount(out count))
				count = path.Count();

		IEnumerable<IGrouping<int, Location>> yIntersects = path.Where(l => getPathDirection(l).IsHorizontal()).GroupBy(l => l.X);
		foreach (IGrouping<int, Location> yIntersect in yIntersects)
		{
			int x = yIntersect.Key;
			var yValues = yIntersect.OrderBy(l => l.Y).ToList();
			bool inside = true;
			foreach ((Location First, Location Second) in yValues.Zip(yValues.Skip(1)))
			{
				if (getPathDirection(First) is PathDirection.SE)
				{
					if (getPathDirection(Second) is PathDirection.NE)
						inside = !inside;
					continue;
				}

				if (getPathDirection(First) is PathDirection.SW)
				{
					if (getPathDirection(Second) is PathDirection.NW)
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
