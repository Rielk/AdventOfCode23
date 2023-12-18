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
}
