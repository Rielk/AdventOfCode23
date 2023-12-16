namespace AdventOfCode23Day16;
internal enum Direction
{
	N, S, E, W
}

internal static class DirectionExtensions
{
	public static (int x, int y) NextPosition(this Direction direction, int x, int y)
	{
		return direction switch
		{
			Direction.N => (x, y - 1),
			Direction.S => (x, y + 1),
			Direction.E => (x + 1, y),
			Direction.W => (x - 1, y),
			_ => throw new NotImplementedException(),
		};
	}
}
