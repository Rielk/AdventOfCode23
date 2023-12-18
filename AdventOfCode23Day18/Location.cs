namespace AdventOfCode23Day18;
public readonly record struct Location(int X, int Y)
{
	public Location ApplyDirection(Direction direction, int amount = 1) => direction switch
	{
		Direction.U => new(X, Y - amount),
		Direction.D => new(X, Y + amount),
		Direction.R => new(X + amount, Y),
		Direction.L => new(X - amount, Y),
		_ => throw new NotImplementedException(),
	};
	internal (int x, int y) ToTuple() => (X, Y);
}
