namespace AdventOfCode23Day10;
internal readonly record struct Location(int X, int Y)
{
	public Location ApplyDirection(Direction direction) => direction switch
	{
		Direction.N => new(X, Y - 1),
		Direction.S => new(X, Y + 1),
		Direction.E => new(X + 1, Y),
		Direction.W => new(X - 1, Y),
		_ => throw new NotImplementedException(),
	};
}
