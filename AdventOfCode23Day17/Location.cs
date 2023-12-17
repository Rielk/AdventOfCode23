namespace AdventOfCode23Day17;

internal readonly record struct Location(int X, int Y)
{
	public Location FollowDirection(Direction direction, int amount) => direction switch
	{

		Direction.N => new(X, Y - amount),
		Direction.S => new(X, Y + amount),
		Direction.E => new(X + amount, Y),
		Direction.W => new(X - amount, Y),
		_ => throw new NotImplementedException(),
	};
}