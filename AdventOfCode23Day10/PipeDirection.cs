namespace AdventOfCode23Day10;
public enum PipeDirection
{
	NS, WE, NE, NW, SW, SE, None, Start
}

public static class PipeDirectionExtensions
{
	public static PipeDirection ToPipeDirection(this char input) => input switch
	{
		'|' => PipeDirection.NS,
		'-' => PipeDirection.WE,
		'L' => PipeDirection.NE,
		'J' => PipeDirection.NW,
		'7' => PipeDirection.SW,
		'F' => PipeDirection.SE,
		'.' => PipeDirection.None,
		'S' => PipeDirection.Start,
		_ => throw new NotImplementedException()
	};

	public static PipeDirection WithOutDirection(this Direction inDir, Direction outDir) => (inDir, outDir) switch
	{
		(Direction.S, Direction.S) => PipeDirection.NS,
		(Direction.N, Direction.N) => PipeDirection.NS,

		(Direction.E, Direction.E) => PipeDirection.WE,
		(Direction.W, Direction.W) => PipeDirection.WE,

		(Direction.S, Direction.E) => PipeDirection.NE,
		(Direction.W, Direction.N) => PipeDirection.NE,

		(Direction.S, Direction.W) => PipeDirection.NW,
		(Direction.E, Direction.N) => PipeDirection.NW,

		(Direction.N, Direction.W) => PipeDirection.SW,
		(Direction.E, Direction.S) => PipeDirection.SW,

		(Direction.N, Direction.E) => PipeDirection.SE,
		(Direction.W, Direction.S) => PipeDirection.SE,

		_ => throw new NotImplementedException(),
	};

	public static Direction NextDirection(this PipeDirection pipeDirection, Direction lastDirection)
	{
		return (pipeDirection, lastDirection) switch
		{
			(PipeDirection.NS, Direction.S) => Direction.S,
			(PipeDirection.NS, Direction.N) => Direction.N,
			(PipeDirection.NS, _) => Direction.DeadEnd,

			(PipeDirection.WE, Direction.E) => Direction.E,
			(PipeDirection.WE, Direction.W) => Direction.W,
			(PipeDirection.WE, _) => Direction.DeadEnd,

			(PipeDirection.NE, Direction.S) => Direction.E,
			(PipeDirection.NE, Direction.W) => Direction.N,
			(PipeDirection.NE, _) => Direction.DeadEnd,

			(PipeDirection.NW, Direction.S) => Direction.W,
			(PipeDirection.NW, Direction.E) => Direction.N,
			(PipeDirection.NW, _) => Direction.DeadEnd,

			(PipeDirection.SW, Direction.N) => Direction.W,
			(PipeDirection.SW, Direction.E) => Direction.S,
			(PipeDirection.SW, _) => Direction.DeadEnd,

			(PipeDirection.SE, Direction.N) => Direction.E,
			(PipeDirection.SE, Direction.W) => Direction.S,
			(PipeDirection.SE, _) => Direction.DeadEnd,

			(PipeDirection.None, _) => Direction.DeadEnd,
			(PipeDirection.Start, _) => Direction.Start,

			_ => throw new NotImplementedException(),
		};
	}

	public static bool IsHorizontal(this PipeDirection pipeDirection) => pipeDirection switch
	{
		PipeDirection.NS => false,
		PipeDirection.WE => true,
		PipeDirection.NE => true,
		PipeDirection.NW => true,
		PipeDirection.SW => true,
		PipeDirection.SE => true,
		_ => throw new NotImplementedException(),
	};
}
