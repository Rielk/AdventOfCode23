namespace AdventOfCode23Day18;
public enum Direction
{
	N, S, E, W,
	DeadEnd,
	Start
}

public static class Directions
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
