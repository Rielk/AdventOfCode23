namespace AdventOfCode23Day17;

[Flags]
internal enum Direction
{
	None = 0, N = 1, S = 2, E = 4, W = 8
}

internal static class DirectionExtensions
{
	public static Direction[] PerpendicularDirections(this Direction direction) => direction switch
	{
		Direction.None => [Direction.N, Direction.S, Direction.W, Direction.E],
		Direction.N or Direction.S => [Direction.W, Direction.E],
		Direction.E or Direction.W => [Direction.N, Direction.S],
		_ => throw new NotImplementedException(),
	};
}
