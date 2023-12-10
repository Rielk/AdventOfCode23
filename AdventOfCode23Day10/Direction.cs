namespace AdventOfCode23Day10;
internal enum Direction : byte
{
	N, S, E, W,
	DeadEnd,
	Start
}

internal static class Directions
{
	public static IEnumerable<Direction> Cardinal
	{
		get
		{
			yield return Direction.N;
			yield return Direction.S;
			yield return Direction.E;
			yield return Direction.W;
		}
	}
}
