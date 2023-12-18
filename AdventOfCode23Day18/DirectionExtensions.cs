namespace AdventOfCode23Day18;
internal static class DirectionExtensions
{
	public static Direction ToDirection(this char c) => c switch
	{
		'U' => Direction.N,
		'D' => Direction.S,
		'L' => Direction.W,
		'R' => Direction.E,
		_ => throw new NotImplementedException()
	};
}
