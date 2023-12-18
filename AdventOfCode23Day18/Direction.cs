namespace AdventOfCode23Day18;
public enum Direction
{
	U, D, R, L
}

internal static class DirectionExtensions
{
	public static Direction ToDirection(this char c) => c switch
	{
		'U' => Direction.U,
		'D' => Direction.D,
		'L' => Direction.L,
		'R' => Direction.R,
		_ => throw new NotImplementedException()
	};
}
